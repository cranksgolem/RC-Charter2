using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RC_Charter2.Models;
using RC_Charter2.UnitsOfWork;
using RC_Charter2WPF.Placeholder_Classes;
using RC_Charter2WPF.Views.Parts;
using License = RC_Charter2.Models.License;

namespace RC_Charter2WPF.ViewModels
{
    public class CustomerViewModel : ObservableObject
    {
	    private string _selectedPurpose;
	    private string _customerSearchText;
	    private string _aircraftSearchText;
	    private string _employeeSearchText;
		private int _selectedPage;
	    private int _selectedAircraftPage;
	    private int _selectedEmployeePage;
	    private const int CustomerPerPageInList = 50;
	    private const int AircraftPerPageInList = 50;
	    private const int EmployeePerPageInList = 50;
	    private CharterTripUnitOfWork _ctUow;
	    private AircraftUnitOfWork _aircraftUow;
	    private EmployeeUnitOfWork _employeeUow;
	    private Customer _selectedCustomer;
	    private Aircraft _selectedAircraft;
	    private Employee _selectedEmployee;
	    private EmployeeToBeAssigned _selectedEmployeeToBeAssigned;
	    private AircraftProperties _selectedAircraftProperties;
	    private ListCollectionView _customerListView;
	    private ListCollectionView _aircraftListView;
	    private ListCollectionView _employeeListView;
	    private ObservableCollection<CrewRequirement> _selectedAircraftCrewRequirements;
	    public ObservableCollection<Customer> CustomerList { get; } = new ObservableCollection<Customer>();
		public ObservableCollection<CharterTrip> CharterTripList { get; } = new ObservableCollection<CharterTrip>();
		public ObservableCollection<Flight> FlightList { get; } = new ObservableCollection<Flight>();
	    public ObservableCollection<Aircraft> AircraftList { get; } = new ObservableCollection<Aircraft>();
		public ObservableCollection<string> PurposeList { get; } = new ObservableCollection<string>();
		public ObservableCollection<Employee> EmployeeList { get; } = new ObservableCollection<Employee>();
		public ObservableCollection<EmployeeToBeAssigned> NewCharterTripEmployeesToBeAssigned { get; } = new ObservableCollection<EmployeeToBeAssigned>();
	    private ListCollectionView _charterTripListView;
	    private CharterTrip _selectedCharterTrip;
	    private Flight _selectedFlight;
	    private string _selectedCustomerNameHeader;
	    private string _newCustomerName;
	    private string _newCustomerAddress;
	    private decimal? _newCustomerAvailableCredits;
	    private ObservableCollection<ListViewItem> _crewRequirementListViewItems;

		//For new flight leg
		public ObservableCollection<string> StatesList { get; } = new ObservableCollection<string>{"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut",
			"Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland",
			"Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
			"New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota",
			"Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming", "Canada"};

	    private DateTime _newFlightDate;
	    private DateTime _newFlightTimeDeparture;
	    private DateTime _newFlightTimeArrival;
	    private string _newFlightOrigin;
	    private string _newFlightDestination;
	    private float? _newFlightDistanceFlown;
	    private float? _newFlightWaitingTime;
	    private float? _newFlightFuelUsage;
	    private int? _newFlightOrder;

	    public DateTime NewFlightDate
	    {
		    get => _newFlightDate;
		    set
		    {
			    _newFlightDate = value;
				RaisePropertyChanged(nameof(NewFlightDate));
		    }
	    }

	    public DateTime NewFlightTimeDeparture
	    {
		    get => _newFlightTimeDeparture;
		    set
		    {
			    _newFlightTimeDeparture = value;
				RaisePropertyChanged(nameof(NewFlightTimeDeparture));
		    }
	    }

	    public DateTime NewFlightTimeArrival
	    {
		    get => _newFlightTimeArrival;
		    set
		    {
			    _newFlightTimeArrival = value;
				RaisePropertyChanged(nameof(NewFlightTimeArrival));
		    }
	    }

	    public string NewFlightOrigin
	    {
		    get => _newFlightOrigin;
		    set
		    {
			    _newFlightOrigin = value;
				RaisePropertyChanged(nameof(NewFlightOrigin));
		    }
	    }

	    public string NewFlightDestination
	    {
		    get => _newFlightDestination;
		    set
		    {
			    _newFlightDestination = value;
				RaisePropertyChanged(nameof(NewFlightDestination));
		    }
	    }

	    public float? NewFlightDistanceFlown
	    {
		    get => _newFlightDistanceFlown;
		    set
		    {
			    _newFlightDistanceFlown = value;
				RaisePropertyChanged(nameof(NewFlightDistanceFlown));
		    }
	    }

	    public float? NewFlightWaitingTime
	    {
		    get => _newFlightWaitingTime;
		    set
		    {
			    _newFlightWaitingTime = value;
				RaisePropertyChanged(nameof(NewFlightWaitingTime));
		    }
	    }

	    public float? NewFlightFuelUsage
	    {
		    get => _newFlightFuelUsage;
		    set
		    {
			    _newFlightFuelUsage = value;
				RaisePropertyChanged(nameof(NewFlightFuelUsage));
		    }
	    }

	    public int? NewFlightOrder
	    {
		    get => _newFlightOrder;
		    set
		    {
			    _newFlightOrder = value;
				RaisePropertyChanged(nameof(NewFlightOrder));
		    }
	    }

	    private void InitializeNewFlightAttributes()
	    {
		    NewFlightDate = DateTime.Now.Date;
		    NewFlightOrder = FlightList.Count + 1;

		    if (FlightList.Count != 0)
		    {
			    int? orderOfLastFlight = 0;

			    foreach (var flight in FlightList)
			    {
				    if (flight.Order > orderOfLastFlight)
				    {
					    orderOfLastFlight = flight.Order;
				    }
			    }

			    NewFlightOrigin = FlightList.FirstOrDefault(c => c.Order == orderOfLastFlight).Destination;
		    }
	    }

	    private void AddNewFlight()
	    {
			var newFlight = new Flight();
		    newFlight.Destination = NewFlightDestination;
		    newFlight.Order = NewFlightOrder;
		    newFlight.Date = NewFlightDate;
		    newFlight.DistanceFlown = NewFlightDistanceFlown;
		    newFlight.FuelUsage = NewFlightFuelUsage;
		    newFlight.Origin = NewFlightOrigin;
		    newFlight.TimeArrival = NewFlightTimeArrival;
		    newFlight.TimeDeparture = NewFlightTimeDeparture;
		    newFlight.WaitingTime = NewFlightWaitingTime;

			_ctUow.AddFlightToCharterTrip(newFlight, SelectedCharterTrip);

		    if (SelectedCharterTrip.TotalDistanceFlown == null || SelectedCharterTrip.TotalDistanceFlown == 0)
		    {
			    SelectedCharterTrip.TotalDistanceFlown = NewFlightDistanceFlown;
			    SelectedCharterTrip.Origin = NewFlightOrigin;
			    SelectedCharterTrip.FinalDestination = NewFlightDestination;
		    }
		    else
		    {
			    SelectedCharterTrip.TotalDistanceFlown += NewFlightDistanceFlown;
			    SelectedCharterTrip.FinalDestination = NewFlightDestination;
		    }

		    if (SelectedCharterTrip.TotalFuelUsage == null || SelectedCharterTrip.TotalFuelUsage == 0)
		    {
			    SelectedCharterTrip.TotalFuelUsage = NewFlightFuelUsage;
		    }
		    else
		    {
			    SelectedCharterTrip.TotalFuelUsage += NewFlightFuelUsage;
		    }

		    if (SelectedCharterTrip.TotalWaitingTime == null || SelectedCharterTrip.TotalWaitingTime == 0)
		    {
			    SelectedCharterTrip.TotalWaitingTime = NewFlightWaitingTime;
		    }
		    else
		    {
			    SelectedCharterTrip.TotalWaitingTime += NewFlightWaitingTime;
		    }

			_ctUow.UpdateCharterTrip(SelectedCharterTrip);

			LoadFlightLegs();

		    NewFlightDestination = null;
		    NewFlightDistanceFlown = null;
		    NewFlightFuelUsage = null;
		    NewFlightWaitingTime = null;
	    }

	    private void DeleteFlight()
	    {
		    if (SelectedFlight != null)
		    {
			    var result = MessageBox.Show("Are you sure you want to delete this flight leg?",
				    "Confirm Delete Flight Leg", MessageBoxButton.YesNo, MessageBoxImage.Question);

			    if (result == MessageBoxResult.Yes)
			    {
				    SelectedCharterTrip.TotalDistanceFlown -= SelectedFlight.DistanceFlown;
				    SelectedCharterTrip.TotalFuelUsage -= SelectedFlight.FuelUsage;
				    SelectedCharterTrip.TotalWaitingTime -= SelectedFlight.WaitingTime;

				    if (SelectedCharterTrip.TotalDistanceFlown == 0)
				    {
					    SelectedCharterTrip.Origin = null;
					    SelectedCharterTrip.FinalDestination = null;
				    }
				    else
				    {
						int? flightOrderOfLastDestination = 0;

					    foreach (var flight in FlightList)
					    {
						    if (flight.Order > flightOrderOfLastDestination && flight.FlightId != SelectedFlight.FlightId)
						    {
							    flightOrderOfLastDestination = flight.Order;
						    }
					    }

					    SelectedCharterTrip.FinalDestination =
						    FlightList.FirstOrDefault(c => c.Order == flightOrderOfLastDestination).Destination;
				    }

					_ctUow.UpdateCharterTrip(SelectedCharterTrip);
				    _ctUow.DeleteFlight(SelectedFlight);
				    LoadFlightLegs();
			    }
		    }
	    }

	    public ICommand AddNewFlightCommand => new RelayCommand(AddNewFlight);
	    public ICommand InitializeNewFlightAttributesCommand => new RelayCommand(InitializeNewFlightAttributes);
	    public ICommand DeleteFlightCommand => new RelayCommand(DeleteFlight);
	    public ICommand LoadCharterTripsCommand => new RelayCommand(LoadCharterTrips);

		//End of for new flight leg

		public String SelectedPurpose
	    {
		    get => _selectedPurpose;
		    set
		    {
			    _selectedPurpose = value;
				RaisePropertyChanged(nameof(SelectedPurpose));
		    }
	    }

	    public EmployeeToBeAssigned SelectedEmployeeToBeAssigned
	    {
		    get => _selectedEmployeeToBeAssigned;
		    set
		    {
			    _selectedEmployeeToBeAssigned = value;
				RaisePropertyChanged(nameof(SelectedEmployeeToBeAssigned));

		    }
	    }

	    public string EmployeeSearchText
	    {
		    get => _employeeSearchText;
		    set
		    {
			    _employeeSearchText = value;
				RaisePropertyChanged(nameof(EmployeeSearchText));
				FilterEmployeeList(value);
		    }
	    }

	    public Employee SelectedEmployee
	    {
		    get => _selectedEmployee;
		    set
		    {
			    _selectedEmployee = value;
				RaisePropertyChanged(nameof(SelectedEmployee));
		    }
	    }

	    public ObservableCollection<ListViewItem> CrewRequirementListViewItems
	    {
		    get => _crewRequirementListViewItems ??
		           (_crewRequirementListViewItems = new ObservableCollection<ListViewItem>());
		    set
		    {
			    _crewRequirementListViewItems = value;
				RaisePropertyChanged(nameof(CrewRequirementListViewItems));
		    }
	    }

	    public ObservableCollection<CrewRequirement> SelectedAircraftCrewRequirements
	    {
		    get => _selectedAircraftCrewRequirements ??
		           (_selectedAircraftCrewRequirements = new ObservableCollection<CrewRequirement>());
		    set
		    {
			    _selectedAircraftCrewRequirements = value;
				RaisePropertyChanged(nameof(SelectedAircraftCrewRequirements));
		    }
	    }

	    public AircraftProperties SelectedAircraftProperties
	    {
		    get => _selectedAircraftProperties;
		    set
		    {
			    _selectedAircraftProperties = value;
				RaisePropertyChanged(nameof(SelectedAircraftProperties));
				GetSelectedAircraftCrewRequirements();
				FillCrewRequirementList();
		    }
	    }

	    public Aircraft SelectedAircraft
	    {
		    get => _selectedAircraft;
		    set
		    {
			    _selectedAircraft = value;
			    RaisePropertyChanged(nameof(SelectedAircraft));
			    if (value != null)
			    {
				    SelectedAircraftProperties =
					    _aircraftUow.GetAircraftProperties(c => c.AircraftPropertiesId == value.AircraftPropertiesId);
			    }

			    NewCharterTripEmployeesToBeAssigned.Clear();
			}
	    }

		public string NewCustomerName
	    {
		    get => _newCustomerName;
		    set
		    {
			    _newCustomerName = value;
				RaisePropertyChanged(nameof(NewCustomerName));
		    }
	    }

	    public string NewCustomerAddress
	    {
		    get => _newCustomerAddress;
		    set
		    {
			    _newCustomerAddress = value;
			    RaisePropertyChanged(nameof(NewCustomerAddress));
		    }
	    }

	    private void FinalizeNewCharterTrip()
	    {
		    bool hasSufficientCrew = true;

		    foreach (var selectedAircraftCrewRequirement in SelectedAircraftCrewRequirements)
		    {
			    var testList = NewCharterTripEmployeesToBeAssigned.Where(c =>
				    c.SelectedLicense.LicenseType == selectedAircraftCrewRequirement.LicenseType);

			    if (!(testList.Count() >= selectedAircraftCrewRequirement.RequiredNumber))
			    {
				    hasSufficientCrew = false;
			    }
		    }

		    if (SelectedPurpose != null && SelectedAircraft != null && hasSufficientCrew == true)
		    {
			    var newCharterTrip = new CharterTrip {Purpose = SelectedPurpose};

			    _ctUow.AddCharterTrip(newCharterTrip, SelectedAircraft, SelectedCustomer);

			    foreach (var employeeToBeAssigned in NewCharterTripEmployeesToBeAssigned)
			    {
				    var newCrewAssignment = new CrewAssignment();
				    newCrewAssignment.Role = employeeToBeAssigned.SelectedLicense.Description;
				    newCrewAssignment.DateAssigned = DateTime.Now.Date;

				    var employeeAssigned =
					    _employeeUow.GetEmployee(c => c.EmployeeId == employeeToBeAssigned.EmployeeId);

				    _ctUow.AddCrewMember(newCrewAssignment, employeeAssigned, newCharterTrip);
			    }

			    SelectedPurpose = null;
			    SelectedAircraft = null;
				SelectedAircraftCrewRequirements.Clear();
			    CrewRequirementListViewItems.Clear();
			    SelectedEmployee = null;
				NewCharterTripEmployeesToBeAssigned.Clear();
				LoadCharterTrips();
		    }
		    else
		    {
			    MessageBox.Show("Make sure to fill out every field and ensure that you have sufficient crew members!",
				    "Failed To Create Charter Trip", MessageBoxButton.OK, MessageBoxImage.Error);
		    }
	    }

	    public decimal? NewCustomerAvailableCredits
	    {
		    get => _newCustomerAvailableCredits;
		    set
		    {
			    _newCustomerAvailableCredits = value;
			    RaisePropertyChanged(nameof(NewCustomerAvailableCredits));
		    }
	    }

		public CustomerViewModel(CharterTripUnitOfWork ctUow, AircraftUnitOfWork aircraftUow, EmployeeUnitOfWork employeeUow)
	    {
		    _ctUow = ctUow;
		    _aircraftUow = aircraftUow;
		    _employeeUow = employeeUow;

			LoadCustomers();
		    LoadAircraft();
			LoadEmployees();
			GetCollectionViews();
			PurposeList.Add("Transport Cargo");
			PurposeList.Add("Transport Passengers");
			PurposeList.Add("Transport Passengers and Cargo");
	    }

	    public CustomerViewModel()
		    : this(new CharterTripUnitOfWork(), new AircraftUnitOfWork(), new EmployeeUnitOfWork())
	    {

	    }

	    private void GetCollectionViews()
	    {
		    _customerListView = (ListCollectionView) CollectionViewSource.GetDefaultView(CustomerList);
			_customerListView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
			_customerListView.GroupDescriptions.Add(new PropertyGroupDescription("Name[0]"));

		    _aircraftListView = (ListCollectionView) CollectionViewSource.GetDefaultView(AircraftList);
			_aircraftListView.SortDescriptions.Add(new SortDescription("Model", ListSortDirection.Ascending));

			_employeeListView = (ListCollectionView)CollectionViewSource.GetDefaultView(EmployeeList);
		    _employeeListView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
		    _employeeListView.GroupDescriptions.Add(new PropertyGroupDescription("Name[0]"));
		}

	    private void RemoveSelectedEmployeeFromEmployeesToBeAssigned()
	    {
		    if (SelectedEmployeeToBeAssigned != null)
		    {
			    NewCharterTripEmployeesToBeAssigned.Remove(SelectedEmployeeToBeAssigned);
			    FillCrewRequirementList();
		    }
	    }

	    private void AddSelectedEmployeeToEmployeesToBeAssigned()
	    {
		    if (SelectedEmployee != null)
		    {
			    var testEmployee =
				    NewCharterTripEmployeesToBeAssigned.FirstOrDefault(c =>
					    c.EmployeeId == SelectedEmployee.EmployeeId);

			    if (testEmployee == null)
			    {

				    EmployeeToBeAssigned newEmployeeToBeAssigned = new EmployeeToBeAssigned();

				    newEmployeeToBeAssigned.EmployeeId = SelectedEmployee.EmployeeId;
				    newEmployeeToBeAssigned.Name = SelectedEmployee.Name;
				    newEmployeeToBeAssigned.Address = SelectedEmployee.Address;

				    var licensures = _employeeUow.GetLicensures(c => c.EmployeeId == SelectedEmployee.EmployeeId);

				    foreach (var x in licensures)
				    {
					    var license = _employeeUow.GetLicense(c => c.LicenseType == x.LicenseType);
					    newEmployeeToBeAssigned.Licenses.Add(license);
				    }

				    NewCharterTripEmployeesToBeAssigned.Add(newEmployeeToBeAssigned);
			    }
		    }
	    }

	    private void DeleteSelectedCharterTrip()
	    {
		    if (SelectedCharterTrip != null)
		    {
			    var result = MessageBox.Show(
				    "Are you sure you want to delete this charter trip? All flights, charges, payments, and other entities associated with this charter trip will be deleted as well.",
				    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

			    if (result == MessageBoxResult.Yes)
			    {
				    var listCrewAssignments =
					    _ctUow.GetCrewAssignments(c =>
						    c.CharterTrip.CharterTripId == SelectedCharterTrip.CharterTripId);

				    foreach (var x in listCrewAssignments)
				    {
					    _ctUow.DeleteCrewAssignment(x);
				    }

				    var listFlightLegs = _ctUow.GetFlights(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);

				    foreach (var flightLeg in listFlightLegs)
				    {
					    _ctUow.DeleteFlight(flightLeg);
				    }

				    var listCharterFlightCharges =
						    _ctUow.GetCharterFlightCharges(c => c.CharterTripId == SelectedCharterTrip.CharterTripId)
					    ;

				    foreach (var charterFlightCharge in listCharterFlightCharges)
				    {
					    _ctUow.DeleteCharterFlightCharge(charterFlightCharge);
				    }

				    var listBalanceHistories =
					    _ctUow.GetBalanceHistories(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);

				    foreach (var balanceHistory in listBalanceHistories)
				    {
					    _ctUow.DeleteBalanceHistory(balanceHistory);
				    }

				    _ctUow.DeleteCharterTrip(SelectedCharterTrip);
				    LoadCharterTrips();
			    }
		    }
	    }

		private void LoadCustomers()
	    {
		    int numberOfPages = _ctUow.GetCustomerCount() / CustomerPerPageInList;
			Pages.Clear();
		    for (int i = 0; i < numberOfPages; i++)
		    {
			    Pages.Add(i + 1);
		    }
			// load the customer list initially on page 1
		    SelectedPage = 1;

	    }

	    private void LoadAircraft()
	    {
		    int numberOfPages = _aircraftUow.GetAircraftCount() / AircraftPerPageInList;
			AircraftPages.Clear();;
		    for (int i = 0; i < numberOfPages; i++)
		    {
			    AircraftPages.Add(i + 1);
		    }

		    SelectedAircraftPage = 1;
	    }

	    private void LoadEmployees()
	    {
		    int numberOfPages = _employeeUow.GetEmployeesCount() / EmployeePerPageInList;
			EmployeePages.Clear();
		    for (int i = 0; i < numberOfPages; i++)
		    {
			    EmployeePages.Add(i + 1);
		    }

		    SelectedEmployeePage = 1;
	    }

	    private void LoadCharterTrips()
	    {
		    if (SelectedCustomer != null)
		    {
			    CharterTripList.Clear();
			    var charterTrips = _ctUow.GetCharterTrips(c => c.CustomerId == SelectedCustomer.CustomerId);
			    foreach (var charterTrip in charterTrips)
			    {
				    CharterTripList.Add(charterTrip);
			    }
		    }
		    else
		    {
			    CharterTripList.Clear();
		    }
	    }

	    private void GetSelectedAircraftCrewRequirements()
	    {
		    var _crewRequirements = _aircraftUow.GetCrewRequirements(c =>
			    c.AircraftPropertiesId == SelectedAircraftProperties.AircraftPropertiesId);

			SelectedAircraftCrewRequirements.Clear();
			foreach (var x in _crewRequirements)
			{
				SelectedAircraftCrewRequirements.Add(x);
			}
	    }

	    private void FillCrewRequirementList()
	    {
		    CrewRequirementListViewItems.Clear();
			ObservableCollection<License> _neededLicenses = new ObservableCollection<License>();

		    foreach (var x in SelectedAircraftCrewRequirements)
		    {
				var _neededLicense = _aircraftUow.GetLicense(c => c.LicenseType == x.LicenseType);

			    _neededLicenses.Add(_neededLicense);
			}

		    foreach (var employeeToBeAssigned in NewCharterTripEmployeesToBeAssigned)
		    {
			    foreach (var neededLicense in _neededLicenses)
			    {
				    if (neededLicense.LicenseType == employeeToBeAssigned.SelectedLicense.LicenseType)
				    {
					    employeeToBeAssigned.Status = "";
					    break;
				    }

				    if (employeeToBeAssigned.SelectedLicense.LicenseType != null && !(_neededLicenses.Contains(employeeToBeAssigned.SelectedLicense)))
				    {
					    employeeToBeAssigned.Status = "EXTRA CREW";
				    }
			    }
		    }

			foreach (var x in SelectedAircraftCrewRequirements)
		    {
			    ListViewItem _newListRequirement = new ListViewItem();
			    var _neededLicense = _aircraftUow.GetLicense(c => c.LicenseType == x.LicenseType);
			    _newListRequirement.Content = x.RequiredNumber.ToString() + " " + _neededLicense.Description;


				var testList = NewCharterTripEmployeesToBeAssigned.Where(c =>
					c.SelectedLicense.LicenseType == _neededLicense.LicenseType);

				if (testList.Count() == x.RequiredNumber)
				{
					_newListRequirement.Background = Brushes.PaleGreen;
					foreach (var employeeToBeAssigned in testList)
					{
						NewCharterTripEmployeesToBeAssigned[
							NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].Status = "";
					}
				}

				else if (testList.Count() > x.RequiredNumber)
				{
					_newListRequirement.Background = Brushes.LightYellow;

					int? extraCount = testList.Count() - x.RequiredNumber;
					int currentCount = 0;

					foreach (var employeeToBeAssigned in testList)
					{
						currentCount++;

						if (currentCount >= testList.Count() - extraCount + 1)
						{
							NewCharterTripEmployeesToBeAssigned[
								NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].Status = "EXTRA CREW";
						}

						else
						{
							NewCharterTripEmployeesToBeAssigned[
								NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].Status = "";
						}
					}

				}
				else
				{
					_newListRequirement.Background = Brushes.Pink;
					foreach (var employeeToBeAssigned in testList)
					{
						NewCharterTripEmployeesToBeAssigned[
							NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].Status = "";
					}
				}

				CrewRequirementListViewItems.Add(_newListRequirement);
		    }
		}

	    private void LoadFlightLegs()
	    {
		    if (SelectedCharterTrip != null)
		    {
				FlightList.Clear();
			    var flightLegs = _ctUow.GetFlights(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);
			    foreach (var flightLeg in flightLegs)
			    {
				    FlightList.Add(flightLeg);
			    }
			}
		    else
		    {
				FlightList.Clear();
		    }
	    }

	    public ObservableCollection<int> Pages { get; set; } = new ObservableCollection<int>();
	    public ObservableCollection<int> AircraftPages { get; set; } = new ObservableCollection<int>();
	    public ObservableCollection<int> EmployeePages { get; set; } = new ObservableCollection<int>();

		private void LoadCustomers(int page)
	    {
			CustomerList.Clear();
		    var customers = _ctUow.GetCustomersByPage(CustomerPerPageInList, page);
		    foreach (var customer in customers)
		    {
			    CustomerList.Add(customer);
		    }
	    }

	    private void LoadAircraft(int page)
	    {
		    AircraftList.Clear();
		    var aircraft = _aircraftUow.GetAircraftByPage(AircraftPerPageInList, page);
		    foreach (var x in aircraft)
		    {
			    AircraftList.Add(x);
		    }
	    }

	    private void LoadEmployees(int page)
	    {
			EmployeeList.Clear();
		    var employees = _employeeUow.GetEmployeesByPage(EmployeePerPageInList, page);
		    foreach (var x in employees)
		    {
			    EmployeeList.Add(x);
		    }
	    }

		private void FilterCustomerList(string searchString)
	    {
			CustomerList.Clear();
			var customers = _ctUow.GetAllCustomers();
			foreach (var customer in customers)
			{
				CustomerList.Add(customer);
			}

			_customerListView.Filter = o =>
			{
			 var item = o as Customer;
			 if (item == null) return false;

			 string name = item.Name.ToLowerInvariant().Trim();
			 string customerId = item.CustomerId.ToString();

			 searchString = searchString.ToLowerInvariant().Trim();

			 return name.Contains(searchString) ||
			        customerId.Contains(searchString);

			};

		    if (searchString == "")
		    {
			    _selectedPage = 1;
				LoadCustomers(1);
		    }
	    }

		private void FilterAircraftList(string searchString)
		{
			AircraftList.Clear();
			var aircraft = _aircraftUow.GetAllAircraft();
			foreach (var x in aircraft)
			{
				AircraftList.Add(x);
			}
			_aircraftListView.Filter = o =>
			{
				var item = o as Aircraft;
				if (item == null) return false;

				string model = item.Model.ToLowerInvariant().Trim();
				string aircraftNumber = item.AircraftNumber.ToString();

				searchString = searchString.ToLowerInvariant().Trim();

				return model.Contains(searchString) ||
					   aircraftNumber.Contains(searchString);

			};

			if (searchString == "")
			{
				_selectedAircraftPage = 1;
				LoadAircraft(1);
			}
		}

	    private void FilterEmployeeList(string searchString)
	    {
			EmployeeList.Clear();
		    var employees = _employeeUow.GetAllEmployees();
		    foreach (var employee in employees)
		    {
			    EmployeeList.Add(employee);
		    }

		    _employeeListView.Filter = o =>
		    {
			    var item = o as Employee;
			    if (item == null) return false;

			    string name = item.Name.ToLowerInvariant().Trim();

			    var licensures = _employeeUow.GetLicensures(c => c.EmployeeId == item.EmployeeId);

			    List<License> licenses = new List<License>();

			    foreach (var x in licensures)
			    {
				    var license = _employeeUow.GetLicense(c => c.LicenseType == x.LicenseType);
				    licenses.Add(license);
			    }

			    bool hasLicense = false;
			    foreach (var x in licenses)
			    {
				    if (x.Description.ToLowerInvariant().Trim().Contains(searchString) || x.LicenseType.ToLowerInvariant().Trim().Contains(searchString))
				    {
					    hasLicense = true;
				    }
			    }

			    return name.Contains(searchString) || hasLicense;
		    };

		    if (searchString == "")
		    {
			    _selectedEmployeePage = 1;
				LoadEmployees(1);
		    }
	    }


		public int SelectedPage
	    {
		    get => _selectedPage;
		    set
		    {
			    if (value == _selectedPage) return;
			    _selectedPage = value;
				LoadCustomers(value);
				RaisePropertyChanged(nameof(SelectedPage));
		    }
	    }

	    public int SelectedAircraftPage
	    {
		    get => _selectedAircraftPage;
		    set
		    {
			    if (value == _selectedAircraftPage) return;
			    _selectedAircraftPage = value;
				LoadAircraft(value);
				RaisePropertyChanged(nameof(SelectedAircraftPage));
		    }
	    }

	    public int SelectedEmployeePage
	    {
		    get => _selectedEmployeePage;
		    set
		    {
			    if (value == _selectedEmployeePage) return;
			    _selectedEmployeePage = value;
				LoadEmployees(value);
				RaisePropertyChanged(nameof(SelectedEmployeePage));
		    }
	    }

	    public ICommand NextPageCommand => new RelayCommand(NextPageProc, NextPageCondition);
	    public ICommand PrevPageCommand => new RelayCommand(PrevPageProc);
	    public ICommand NextPageAircraftCommand => new RelayCommand(NextPageAircraftProc, NextPageAircraftCondition);
	    public ICommand PrevPageAircraftCommand => new RelayCommand(PrevPageAircraftProc);
	    public ICommand NextPageEmployeeCommand => new RelayCommand(NextPageEmployeeProc, NextPageEmployeeCondition);
	    public ICommand PrevPageEmployeeCommand => new RelayCommand(PrevPageEmployeeProc);
		public ICommand AddCustomerCommand => new RelayCommand(AddNewCustomer);
	    public ICommand EditCustomerCommand => new RelayCommand(EditCustomer);
		public ICommand ViewFlightLegsCommand => new RelayCommand(LoadFlightLegs);
	    public ICommand EditCustomerInitCommand => new RelayCommand(EditCustomerInit);
	    public ICommand AddCustomerInitCommand => new RelayCommand(AddCustomerInit);
	    public ICommand DeleteCustomerCommand => new RelayCommand(DeleteCustomer);
	    public ICommand AddSelectedEmployeeToEmployeesToBeAssignedCommand =>
		    new RelayCommand(AddSelectedEmployeeToEmployeesToBeAssigned);

	    public ICommand FillCrewRequirementListCommand => new RelayCommand(FillCrewRequirementList);

	    public ICommand RemoveSelectedEmployeeFromEmployeesToBeAssignedCommand =>
		    new RelayCommand(RemoveSelectedEmployeeFromEmployeesToBeAssigned);

	    public ICommand FinalizeNewCharterTripCommand => new RelayCommand(FinalizeNewCharterTrip);
	    public ICommand DeleteSelectedCharterTripCommand => new RelayCommand(DeleteSelectedCharterTrip);

		private void AddNewCustomer()
	    {
			var newCustomer = new Customer();
		    newCustomer.Name = NewCustomerName;
		    newCustomer.Address = NewCustomerAddress;
		    newCustomer.AvailableCredits = NewCustomerAvailableCredits;
			_ctUow.AddCustomer(newCustomer);
		    NewCustomerName = "";
		    NewCustomerAddress = "";
		    NewCustomerAvailableCredits = null;
		    LoadCustomers();
		}

	    private void DeleteCustomer()
	    {
		    if (SelectedCustomer != null)
		    {
				var _result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

			    if (_result == MessageBoxResult.Yes)
			    {
					_ctUow.DeleteCustomer(SelectedCustomer);
				    LoadCustomers();
				}
		    }
	    }

	    private void EditCustomerInit()
	    {
		    if (SelectedCustomer != null)
		    {
			    NewCustomerName = SelectedCustomer.Name;
			    NewCustomerAddress = SelectedCustomer.Address;
			    NewCustomerAvailableCredits = SelectedCustomer.AvailableCredits;
		    }
	    }

	    private void AddCustomerInit()
	    {
		    NewCustomerName = "";
		    NewCustomerAddress = "";
		    NewCustomerAvailableCredits = null;
	    }

		private void EditCustomer()
	    {
		    SelectedCustomer.Name = NewCustomerName;
		    SelectedCustomer.Address = NewCustomerAddress;
		    SelectedCustomer.AvailableCredits = NewCustomerAvailableCredits;
			_ctUow.UpdateCustomer(SelectedCustomer);
		    NewCustomerName = "";
		    NewCustomerAddress = "";
		    NewCustomerAvailableCredits = null;
		    LoadCustomers();
	    }

	    private void NextPageProc()
	    {
		    SelectedPage++;
	    }

	    private bool NextPageCondition()
	    {
		    return SelectedPage != Pages.Last();
	    }

	    private void PrevPageProc()
	    {
		    if (SelectedPage != 1)
		    {
			    SelectedPage--;
		    }
	    }

	    private void NextPageAircraftProc()
	    {
		    SelectedAircraftPage++;
	    }

	    private bool NextPageAircraftCondition()
	    {
		    return SelectedAircraftPage != AircraftPages.Last();
	    }

	    private void PrevPageAircraftProc()
	    {
		    if (SelectedAircraftPage != 1)
		    {
			    SelectedAircraftPage--;
		    }
	    }

	    private void NextPageEmployeeProc()
	    {
		    SelectedEmployeePage++;
	    }

	    private bool NextPageEmployeeCondition()
	    {
		    return SelectedEmployeePage != EmployeePages.Last();
	    }

	    private void PrevPageEmployeeProc()
	    {
		    if (SelectedEmployeePage != 1)
		    {
			    SelectedEmployeePage--;
		    }
	    }

		private void SetSelectedCustomerNameHeader(Customer customer)
	    {
		    if (SelectedCustomer != null)
		    {
			    var customerNameLastChar = customer.Name.Last();
			    if (customerNameLastChar != 's')
			    {
				    SelectedCustomerNameHeader = customer.Name + "'s";
			    }
			    else
			    {
				    SelectedCustomerNameHeader = customer.Name + "'";
			    }
		    }
	    }

		public Customer SelectedCustomer
	    {
		    get => _selectedCustomer;
		    set
		    {
			    _selectedCustomer = value;
				RaisePropertyChanged(nameof(SelectedCustomer));
				SetSelectedCustomerNameHeader(value);
			    LoadCharterTrips();
			}
	    }

	    public Flight SelectedFlight
	    {
		    get => _selectedFlight;
		    set
		    {
			    _selectedFlight = value;
				RaisePropertyChanged(nameof(SelectedFlight));
		    }
	    }

	    public CharterTrip SelectedCharterTrip
	    {
		    get => _selectedCharterTrip;
		    set
		    {
			    _selectedCharterTrip = value;
				RaisePropertyChanged(nameof(SelectedCharterTrip));
		    }
	    }

	    public string SelectedCustomerNameHeader
	    {
		    get => _selectedCustomerNameHeader;
		    set
		    {
			    _selectedCustomerNameHeader = value;
				RaisePropertyChanged(nameof(SelectedCustomerNameHeader));
		    }
	    }

	    public string CustomerSearchText
	    {
		    get => _customerSearchText;
		    set
		    {
			    _customerSearchText = value;
				FilterCustomerList(value);
		    }
	    }

	    public string AircraftSearchText
	    {
		    get => _aircraftSearchText;
		    set
		    {
			    _aircraftSearchText = value;
				FilterAircraftList(value);
		    }
	    }
	}
}
