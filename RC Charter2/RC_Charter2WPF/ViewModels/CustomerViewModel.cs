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
using RC_Charter2WPF.Views.Parts;
using License = RC_Charter2.Models.License;

namespace RC_Charter2WPF.ViewModels
{
    public class CustomerViewModel : ObservableObject
    {
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
	    private Aircraft _confirmedSelectedAircraft;
	    private AircraftProperties _confirmedSelectedAircraftProperties;
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

	    public AircraftProperties ConfirmedSelectedAircraftProperties
	    {
		    get => _confirmedSelectedAircraftProperties;
		    set
		    {
			    _confirmedSelectedAircraftProperties = value;
				RaisePropertyChanged(nameof(ConfirmedSelectedAircraftProperties));
				GetSelectedAircraftCrewRequirements();
				FillCrewRequirementList();
		    }
	    }

	    public Aircraft ConfirmedSelectedAircraft
	    {
		    get => _confirmedSelectedAircraft;
		    set
		    {
			    _confirmedSelectedAircraft = value;
				RaisePropertyChanged(nameof(ConfirmedSelectedAircraft));
			    ConfirmedSelectedAircraftProperties = _aircraftUow.GetAircraftProperties(c => c.AircraftPropertiesId == value.AircraftPropertiesId);
		    }
	    }

	    public Aircraft SelectedAircraft
	    {
		    get => _selectedAircraft;
		    set
		    {
			    _selectedAircraft = value;
			    RaisePropertyChanged(nameof(SelectedAircraft));
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

	    private void AddSelectedEmployeeToEmployeesToBeAssigned()
	    {
		    var testEmployee =
			    NewCharterTripEmployeesToBeAssigned.FirstOrDefault(c => c.EmployeeId == SelectedEmployee.EmployeeId);

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
			    c.AircraftPropertiesId == ConfirmedSelectedAircraftProperties.AircraftPropertiesId);

			SelectedAircraftCrewRequirements.Clear();
			foreach (var x in _crewRequirements)
			{
				SelectedAircraftCrewRequirements.Add(x);
			}
	    }

	    private void FillCrewRequirementList()
	    {
		    CrewRequirementListViewItems.Clear();
		    foreach (var x in SelectedAircraftCrewRequirements)
		    {
			    ListViewItem _newListRequirement = new ListViewItem();
			    var _neededLicense = _aircraftUow.GetLicense(c => c.LicenseType == x.LicenseType);
			    _newListRequirement.Content = x.RequiredNumber.ToString() + " " + _neededLicense.Description;

				var testList = NewCharterTripEmployeesToBeAssigned.Where(c =>
					c.SelectedLicense.LicenseType == _neededLicense.LicenseType);

				if (testList.Count() >= x.RequiredNumber)
				{
					_newListRequirement.Background = Brushes.LightGreen;
				}
				else
				{
					_newListRequirement.Background = Brushes.Pink;
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
	    public ICommand ConfirmSelectedAircraftCommand => new RelayCommand(ConfirmSelectedAircraft);
	    public ICommand AddSelectedEmployeeToEmployeesToBeAssignedCommand =>
		    new RelayCommand(AddSelectedEmployeeToEmployeesToBeAssigned);

	    public ICommand FillCrewRequirementListCommand => new RelayCommand(FillCrewRequirementList);


		private void ConfirmSelectedAircraft()
	    {
		    ConfirmedSelectedAircraft = SelectedAircraft;
	    }

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
