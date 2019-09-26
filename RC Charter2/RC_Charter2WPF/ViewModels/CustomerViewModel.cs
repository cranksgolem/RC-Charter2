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
using System.Windows.Media.Animation;
using BespokeFusion;
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
	    public EmployeeViewModel EmployeeViewModel { get; } = new EmployeeViewModel();

		//VISIBILITY VALUES
	    private bool _addCustomerViewVisibility;
	    private bool _addFlightViewVisibility;
	    private bool _flightLegsViewVisibility;
	    private bool _chargesAndPaymentsViewVisibility;
	    private bool _customerViewVisibility;
	    private bool _addCharterTripViewVisibility;
		private bool _addCharterTripView2Visibility;
	    private bool _completeButtonVisibility;
	    private bool _editButtonVisibility;
	    private bool _addChargeViewVisibility;
	    private bool _addPaymentViewVisibility;
	    private bool _checkNumberTextBoxVisibility;
	    private bool _crewHoursViewVisibility;
	    private bool _employeeViewVisibility;
	    private bool _addEmployeeViewVisibility;
	    private bool _addLicensureViewVisibility;
	    private bool _addResultViewVisibiility;

	    public bool AddResultViewVisibiility
		{
		    get => _addResultViewVisibiility;
		    set
		    {
			    _addResultViewVisibiility = value;
			    RaisePropertyChanged(nameof(AddResultViewVisibiility));
		    }
	    }

		public bool AddLicensureViewVisibility
	    {
		    get => _addLicensureViewVisibility;
		    set
		    {
			    _addLicensureViewVisibility = value;
				RaisePropertyChanged(nameof(AddLicensureViewVisibility));
		    }
	    }

		public bool EmployeeViewVisibility
	    {
		    get => _employeeViewVisibility;
		    set
		    {
				_employeeViewVisibility = value;
				RaisePropertyChanged(nameof(EmployeeViewVisibility));
		    }
	    }

	    public bool AddEmployeeViewVisibility
	    {
		    get => _addEmployeeViewVisibility;
		    set
		    {
			    _addEmployeeViewVisibility = value;
			    RaisePropertyChanged(nameof(AddEmployeeViewVisibility));
		    }
	    }

		public bool CrewHoursViewVisibility
	    {
		    get => _crewHoursViewVisibility;
		    set
		    {
				_crewHoursViewVisibility = value;
				RaisePropertyChanged(nameof(CrewHoursViewVisibility));
		    }
	    }

		public bool CheckNumberTextBoxVisibility
	    {
		    get => _checkNumberTextBoxVisibility;
		    set
		    {
			    _checkNumberTextBoxVisibility = value;
				RaisePropertyChanged(nameof(CheckNumberTextBoxVisibility));
		    }
	    }

	    public bool AddPaymentViewVisibility
	    {
		    get => _addPaymentViewVisibility;
		    set
		    {
			    _addPaymentViewVisibility = value;
				RaisePropertyChanged(nameof(AddPaymentViewVisibility));
		    }
	    }

	    public bool AddChargeViewVisibility
	    {
		    get => _addChargeViewVisibility;
		    set
		    {
				_addChargeViewVisibility = value;
				RaisePropertyChanged(nameof(AddChargeViewVisibility));
		    }
	    }

	    public bool AddCustomerViewVisibility
		{
		    get => _addCustomerViewVisibility;
		    set
		    {
			    _addCustomerViewVisibility = value;
			    RaisePropertyChanged(nameof(AddCustomerViewVisibility));
		    }
	    }

		public bool AddFlightViewVisibility
		{
		    get => _addFlightViewVisibility;
		    set
		    {
			    _addFlightViewVisibility = value;
			    RaisePropertyChanged(nameof(AddFlightViewVisibility));
		    }
	    }

		public bool FlightLegsViewVisibility
		{
		    get => _flightLegsViewVisibility;
		    set
		    {
			    _flightLegsViewVisibility = value;
			    RaisePropertyChanged(nameof(FlightLegsViewVisibility));
		    }
	    }

		public bool ChargesAndPaymentsViewVisibility
		{
		    get => _chargesAndPaymentsViewVisibility;
		    set
		    {
			    _chargesAndPaymentsViewVisibility = value;
			    RaisePropertyChanged(nameof(ChargesAndPaymentsViewVisibility));
		    }
	    }

		public bool EditButtonVisibility
		{
		    get => _editButtonVisibility;
		    set
		    {
			    _editButtonVisibility = value;
			    RaisePropertyChanged(nameof(EditButtonVisibility));
		    }
	    }

		public bool CompleteButtonVisibility
		{
		    get => _completeButtonVisibility;
		    set
		    {
			    _completeButtonVisibility = value;
			    RaisePropertyChanged(nameof(CompleteButtonVisibility));
		    }
	    }

		public bool CustomerViewVisibility
	    {
		    get => _customerViewVisibility;
		    set
		    {
			    _customerViewVisibility = value;
				RaisePropertyChanged(nameof(CustomerViewVisibility));
		    }
	    }

	    public bool AddCharterTripView2Visibility
	    {
		    get => _addCharterTripView2Visibility;
		    set
		    {
			    _addCharterTripView2Visibility = value;
				RaisePropertyChanged(nameof(AddCharterTripView2Visibility));
		    }
	    }

	    public bool AddCharterTripViewVisibility
	    {
		    get => _addCharterTripViewVisibility;
		    set
		    {
			    _addCharterTripViewVisibility = value;
			    RaisePropertyChanged(nameof(AddCharterTripViewVisibility));
		    }
	    }

		private void OpenAddCharterTripView()
	    {
		    if (SelectedCustomer != null)
		    {
			    SelectedCustomerUnusedCredits = SelectedCustomer.AvailableCredits - SelectedCustomer.UsedCredits;

			    CustomerViewVisibility = false;
			    AddCharterTripViewVisibility = true;
			    CompleteButtonVisibility = true;
			    EditButtonVisibility = false;
			}
	    }

	    private void OpenEmployeeView()
	    {
		    EmployeeViewVisibility = true;
		    CustomerViewVisibility = false;
		    AddEmployeeViewVisibility = false;
		    AddLicensureViewVisibility = false;
	    }

	    private void OpenCrewHoursView()
	    {
			LoadCrewAssignments();

		    CrewHoursViewVisibility = true;
		    CustomerViewVisibility = false;
	    }

	    private void OpenAddEmployeeView()
	    {
		    AddEmployeeViewVisibility = true;
		    EmployeeViewVisibility = false;
		    CompleteButtonVisibility = true;
		    EditButtonVisibility = false;

	    }

	    private void OpenAddResultView()
	    {
		    AddResultViewVisibiility = true;
		    EmployeeViewVisibility = false;
		    CompleteButtonVisibility = true;
		    EditButtonVisibility = false;
	    }

	    private void OpenEditResultView()
	    {
		    if (EmployeeViewModel.SelectedEmployeeResult != null)
		    {
			    EmployeeViewModel.NewResultExpiration = EmployeeViewModel.SelectedEmployeeResult.Expiration;
			    EmployeeViewModel.NewResultTestResult = EmployeeViewModel.SelectedEmployeeResult.TestResult;
			    EmployeeViewModel.NewResultTest = EmployeeViewModel.SelectedEmployeeResult.Test;
			    EmployeeViewModel.NewResultTestDate = EmployeeViewModel.SelectedEmployeeResult.TestDate;

				AddResultViewVisibiility = true;
			    EmployeeViewVisibility = false;
			    CompleteButtonVisibility = false;
			    EditButtonVisibility = true;
		    }
	    }

	    private void OpenAddLicensureView()
	    {
		    AddLicensureViewVisibility = true;
		    EmployeeViewVisibility = false;
		    CompleteButtonVisibility = true;
		    EditButtonVisibility = false;
	    }

	    private void OpenEditEmployeeView()
	    {
		    if (EmployeeViewModel.SelectedEmployee != null)
		    {
			    EmployeeViewModel.NewEmployeeName = EmployeeViewModel.SelectedEmployee.Name;
			    EmployeeViewModel.NewEmployeeAddress = EmployeeViewModel.SelectedEmployee.Address;

			    AddEmployeeViewVisibility = true;
			    EmployeeViewVisibility = false;
			    CompleteButtonVisibility = false;
			    EditButtonVisibility = true;
		    }
	    }

	    private void OpenAddPaymentView()
	    {
		    AddPaymentViewVisibility = true;
		    ChargesAndPaymentsViewVisibility = false;
		    CompleteButtonVisibility = true;
		    EditButtonVisibility = false;
	    }

	    private void OpenEditPaymentView()
	    {
		    if (SelectedPaymentMode != null)
		    {
			    NewPaymentModeOfPayment = SelectedPaymentMode.ModeOfPayment;
			    if (SelectedPaymentMode.ModeOfPayment == "Cash")
			    {
				    var cash = _paymentUow.GetSingleCash(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);
				    NewPaymentAmount = cash.Amount;
				    NewPaymentDate = cash.Date;
			    }
			    else
			    {
				    var check = _paymentUow.GetCheck(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);
				    NewPaymentAmount = check.Amount;
				    NewPaymentCheckNumber = check.CheckNumber;
				    NewPaymentDate = check.CheckDate;
			    }
				
			    AddPaymentViewVisibility = true;
			    ChargesAndPaymentsViewVisibility = false;
			    CompleteButtonVisibility = false;
			    EditButtonVisibility = true;
		    }
	    }

		private void OpenAddChargeView()
	    {
		    SelectedCustomerUnusedCredits = SelectedCustomer.AvailableCredits - SelectedCustomer.UsedCredits;

			AddChargeViewVisibility = true;
		    ChargesAndPaymentsViewVisibility = false;
		    CompleteButtonVisibility = true;
		    EditButtonVisibility = false;
		}

	    private void OpenEditChargeView()
	    {
		    if (SelectedCharterFlightCharge != null)
		    {
			    if (SelectedCharterFlightCharge.ChargeType != "Extra Crew" &&
			        SelectedCharterFlightCharge.ChargeType != "Distance Flown" &&
			        SelectedCharterFlightCharge.ChargeType != "Waiting Time" &&
			        SelectedCharterFlightCharge.ChargeType != "Crew Meals" &&
			        SelectedCharterFlightCharge.ChargeType != "Crew Lodging" &&
			        SelectedCharterFlightCharge.ChargeType != "Crew Ground Transportation")
			    {
				    NewChargeChargeType = SelectedCharterFlightCharge.ChargeType;
				    NewChargeQuantity = SelectedCharterFlightCharge.Quantity;
				    NewChargeAmount = SelectedCharterFlightCharge.Amount;

				    AddChargeViewVisibility = true;
				    ChargesAndPaymentsViewVisibility = false;
				    CompleteButtonVisibility = false;
				    EditButtonVisibility = true;
				}
			    else
			    {
					MaterialMessageBox.ShowError(
						"This type of charge can't be deleted or edited. Modify the source of this charge instead.");
				}
		    }
	    }

	    private void OpenAddCharterTripView2()
	    {
		    AddCharterTripViewVisibility = false;
		    AddCharterTripView2Visibility = true;
	    }

	    private void OpenEditCharterTripView()
	    {
		    SelectedPurpose = SelectedCharterTrip.Purpose;

		    var aircraft = _aircraftUow.GetSingleAircraft(c => c.AircraftNumber == SelectedCharterTrip.AircraftNumber);
		    SelectedAircraft = aircraft;

		    var crewAssignments = _ctUow.GetCrewAssignments(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);

		    var currentCrewMealsExpense = _ctUow.GetCharterFlightCharge(c =>
			    c.CharterTripId == SelectedCharterTrip.CharterTripId && c.ChargeType == "Crew Meals").Amount;

		    var currentCrewLodgingExpense = _ctUow.GetCharterFlightCharge(c =>
			    c.CharterTripId == SelectedCharterTrip.CharterTripId && c.ChargeType == "Crew Lodging").Amount;

		    var currentCrewGroundTransportationExpense = _ctUow.GetCharterFlightCharge(c =>
			    c.CharterTripId == SelectedCharterTrip.CharterTripId && c.ChargeType == "Crew Ground Transportation").Amount;

		    NewCharterTripCrewLodgingExpense = currentCrewLodgingExpense;
		    NewCharterTripCrewMealsExpense = currentCrewMealsExpense;
		    NewCharterTripGroundTransportationExpense = currentCrewGroundTransportationExpense;
		    SelectedCustomerUnusedCredits = SelectedCustomer.AvailableCredits - SelectedCustomer.UsedCredits +
		                                    currentCrewLodgingExpense + currentCrewMealsExpense +
		                                    currentCrewGroundTransportationExpense;

		    NewCharterTripEmployeesToBeAssigned.Clear();
		    foreach (var crewAssignment in crewAssignments)
		    {
			    var employee = _employeeUow.GetEmployee(c => c.EmployeeId == crewAssignment.EmployeeId);

			    var newEmployeeToBeAssgined = new EmployeeToBeAssigned();
			    newEmployeeToBeAssgined.EmployeeId = employee.EmployeeId;
			    newEmployeeToBeAssgined.Name = employee.Name;

			    var licensures = _employeeUow.GetLicensures(c => c.EmployeeId == employee.EmployeeId);

			    var licenses = new ObservableCollection<License>();
			    foreach (var employeeLicensure in licensures)
			    {
				    var license = _employeeUow.GetLicense(c => c.LicenseType == employeeLicensure.LicenseType);
				    licenses.Add(license);
			    }

			    foreach (var license in licenses)
			    {
				    newEmployeeToBeAssgined.Licenses.Add(license);
			    }

			    newEmployeeToBeAssgined.SelectedLicense =
				    newEmployeeToBeAssgined.Licenses.FirstOrDefault(c => c.Description == crewAssignment.Role);

			    NewCharterTripEmployeesToBeAssigned.Add(newEmployeeToBeAssgined);
			    FillCrewRequirementList();
		    }

			CustomerViewVisibility = false;
		    AddCharterTripViewVisibility = true;
		    CompleteButtonVisibility = false;
		    EditButtonVisibility = true;
	    }

	    private void OpenAddCustomerView()
	    {
		    NewCustomerName = "";
		    NewCustomerAddress = "";
		    NewCustomerAvailableCredits = null;

			CustomerViewVisibility = false;
		    AddCustomerViewVisibility = true;
		    CompleteButtonVisibility = true;
		    EditButtonVisibility = false;
		}

	    private void OpenEditCustomerView()
	    {
		    if (SelectedCustomer != null)
		    {
			    NewCustomerName = SelectedCustomer.Name;
			    NewCustomerAddress = SelectedCustomer.Address;
			    NewCustomerAvailableCredits = SelectedCustomer.AvailableCredits;

			    CustomerViewVisibility = false;
			    AddCustomerViewVisibility = true;
			    CompleteButtonVisibility = false;
			    EditButtonVisibility = true;
			}
		}

	    private void OpenEditLicensureView()
	    {
		    if (EmployeeViewModel.SelectedEmployeeLicensure != null)
		    {
			    EmployeeViewModel.NewLicensureSelectedLicense = EmployeeViewModel.SelectedEmployeeLicensure.License;
			    EmployeeViewModel.NewLicensureEarnedDate = EmployeeViewModel.SelectedEmployeeLicensure.DateEarned;

			    AddLicensureViewVisibility = true;
			    EmployeeViewVisibility = false;
			    CompleteButtonVisibility = false;
			    EditButtonVisibility = true;
		    }
	    }

	    public ICommand OpenEditLicensureViewCommand => new RelayCommand(OpenEditLicensureView);


		private void OpenFlightLegsView()
	    {
		    if (SelectedCharterTrip != null)
		    {
			    FlightList.Clear();
			    var flightLegs = _ctUow.GetFlights(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);
			    foreach (var flightLeg in flightLegs)
			    {
				    FlightList.Add(flightLeg);
			    }

			    CustomerViewVisibility = false;
			    AddFlightViewVisibility = false;
			    FlightLegsViewVisibility = true;
			}
		    else
		    {
			    FlightList.Clear();
		    }
	    }

	    private void OpenChargesAndPaymentsView()
	    {
		    if (SelectedCharterTrip != null)
		    {
			    LoadChargeList();
			    LoadPaymentModes();

			    //LoadPaymentModes();
			    //UpdateCredits();
			    //LoadCustomers();
			    //LoadChargeList();
			    //var holderSelectedCharterTripId = SelectedCharterTrip.CharterTripId;
			    //LoadCharterTrips();
			    //SelectedCharterTrip = CharterTripList.FirstOrDefault(c => c.CharterTripId == holderSelectedCharterTripId);

				CustomerViewVisibility = false;
			    ChargesAndPaymentsViewVisibility = true;
			    AddChargeViewVisibility = false;
			    AddPaymentViewVisibility = false;
		    }
	    }

	    private void OpenAddFlightView()
	    {
		    NewFlightDate = DateTime.Now.Date;
		    NewFlightOrder = FlightList.Count + 1;
		    SelectedCustomerUnusedCredits = SelectedCustomer.AvailableCredits - SelectedCustomer.UsedCredits;

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

		    FlightLegsViewVisibility = false;
		    AddFlightViewVisibility = true;
		    CompleteButtonVisibility = true;
		    EditButtonVisibility = false;
		}

	    private void OpenEditFlightView()
	    {
		    if (SelectedFlight != null)
		    {
				NewFlightDate = SelectedFlight.Date;
			    NewFlightTimeDeparture = SelectedFlight.TimeDeparture;
			    NewFlightTimeArrival = SelectedFlight.TimeArrival;
			    NewFlightOrigin = SelectedFlight.Origin;
			    NewFlightDestination = SelectedFlight.Destination;
			    NewFlightDistanceFlown = SelectedFlight.DistanceFlown;
			    NewFlightWaitingTime = SelectedFlight.WaitingTime;
			    NewFlightFuelUsage = SelectedFlight.FuelUsage;
			    NewFlightOrder = SelectedFlight.Order;

				var oldDistanceFlownCharge = _ctUow.GetCharterFlightCharge(c =>
				    c.ChargeType == "Distance Flown" && c.Quantity == SelectedFlight.DistanceFlown && c.CharterTripId == SelectedCharterTrip.CharterTripId);

			    var oldWaitingTimeCharge = _ctUow.GetCharterFlightCharge(c =>
				    c.ChargeType == "Waiting Time" && c.Quantity == SelectedFlight.WaitingTime && c.CharterTripId == SelectedCharterTrip.CharterTripId);

			    SelectedCustomerUnusedCredits = SelectedCustomer.AvailableCredits - SelectedCustomer.UsedCredits +
			                                    oldDistanceFlownCharge.Amount + oldWaitingTimeCharge.Amount;

				FlightLegsViewVisibility = false;
				AddFlightViewVisibility = true;
			    CompleteButtonVisibility = false;
			    EditButtonVisibility = true;
		    }
	    }

	    private void OpenCustomerView()
	    {
		    CustomerViewVisibility = true;
		    AddCustomerViewVisibility = false;
		    AddCharterTripViewVisibility = false;
			AddCharterTripView2Visibility = false;
		    FlightLegsViewVisibility = false;
		    ChargesAndPaymentsViewVisibility = false;
		    CrewHoursViewVisibility = false;
		    EmployeeViewVisibility = false;
	    }

	    private void OpenAddCharterTripView1From2()
	    {
		    AddCharterTripView2Visibility = false;
		    AddCharterTripViewVisibility = true;
	    }

	    public ICommand OpenAddCharterTripViewCommand => new RelayCommand(OpenAddCharterTripView);
	    public ICommand OpenEditCharterTripViewCommand => new RelayCommand(OpenEditCharterTripView);
	    public ICommand OpenAddCustomerViewCommand => new RelayCommand(OpenAddCustomerView);
	    public ICommand OpenEditCustomerViewCommand => new RelayCommand(OpenEditCustomerView);
	    public ICommand OpenFlightLegsViewCommand => new RelayCommand(OpenFlightLegsView);
	    public ICommand OpenChargesAndPaymentsViewCommand => new RelayCommand(OpenChargesAndPaymentsView);
	    public ICommand OpenAddFlightViewCommand => new RelayCommand(OpenAddFlightView);
	    public ICommand OpenEditFlightViewCommand => new RelayCommand(OpenEditFlightView);
	    public ICommand OpenCustomerViewCommand => new RelayCommand(OpenCustomerView);
	    public ICommand OpenAddCharterTripView2Command => new RelayCommand(OpenAddCharterTripView2);
	    public ICommand OpenAddCharterTripView1From2Command => new RelayCommand(OpenAddCharterTripView1From2);
	    public ICommand OpenAddChargeViewCommand => new RelayCommand(OpenAddChargeView);
	    public ICommand OpenEditChargeViewCommand => new RelayCommand(OpenEditChargeView);
	    public ICommand OpenAddPaymentViewCommand => new RelayCommand(OpenAddPaymentView);
	    public ICommand OpenEditPaymentViewCommand => new RelayCommand(OpenEditPaymentView);
	    public ICommand OpenCrewHoursViewCommand => new RelayCommand(OpenCrewHoursView);
	    public ICommand OpenEmployeeViewCommand => new RelayCommand(OpenEmployeeView);
	    public ICommand OpenAddEmployeeViewCommand => new RelayCommand(OpenAddEmployeeView);
	    public ICommand OpenEditEmployeeViewCommand => new RelayCommand(OpenEditEmployeeView);
	    public ICommand OpenAddLicensureViewCommand => new RelayCommand(OpenAddLicensureView);
	    public ICommand OpenAddResultViewCommand => new RelayCommand(OpenAddResultView);
	    public ICommand OpenEditResultViewCommand => new RelayCommand(OpenEditResultView);


		//END OF VISIBILITY VALUES
		private string _selectedPurpose;
	    private decimal? _newCharterTripCrewLodgingExpense;
	    private decimal? _newCharterTripCrewMealsExpense;
	    private decimal? _newCharterTripCrewGroundTransportationExpense;
	    private string _aircraftSearchText;
	    private string _employeeSearchText;
		private int _selectedPage;
	    private int _selectedAircraftPage;
	    private int _selectedEmployeePage;
	    private const int AircraftPerPageInList = 50;
	    private const int EmployeePerPageInList = 50;
	    private AircraftUnitOfWork _aircraftUow;
	    private EmployeeUnitOfWork _employeeUow;
	    private PaymentUnitOfWork _paymentUow;
	    private Aircraft _selectedAircraft;
	    private Employee _selectedEmployee;
	    private EmployeeToBeAssigned _selectedEmployeeToBeAssigned;
	    private AircraftProperties _selectedAircraftProperties;
	    private ListCollectionView _aircraftListView;
	    private ListCollectionView _employeeListView;
	    public ObservableCollection<Aircraft> AircraftList { get; } = new ObservableCollection<Aircraft>();
		public ObservableCollection<string> PurposeList { get; } = new ObservableCollection<string>();
		public ObservableCollection<string> ModeOfPaymentList { get; } = new ObservableCollection<string>();
		public ObservableCollection<Employee> EmployeeList { get; } = new ObservableCollection<Employee>();
		public ObservableCollection<EmployeeToBeAssigned> NewCharterTripEmployeesToBeAssigned { get; } = new ObservableCollection<EmployeeToBeAssigned>();
	    private ObservableCollection<ListViewItem> _crewRequirementListViewItems;
		public ObservableCollection<License> SelectedEmployeeLicenses { get; } = new ObservableCollection<License>();

	    public decimal? NewCharterTripCrewLodgingExpense
	    {
		    get => _newCharterTripCrewLodgingExpense;
		    set
		    {
			    _newCharterTripCrewLodgingExpense = value;
				RaisePropertyChanged(nameof(NewCharterTripCrewLodgingExpense));
		    }
	    }

	    public decimal? NewCharterTripCrewMealsExpense
	    {
		    get => _newCharterTripCrewMealsExpense;
		    set
		    {
			    _newCharterTripCrewMealsExpense = value;
			    RaisePropertyChanged(nameof(NewCharterTripCrewMealsExpense));
		    }
	    }

	    public decimal? NewCharterTripGroundTransportationExpense
	    {
		    get => _newCharterTripCrewGroundTransportationExpense;
		    set
		    {
			    _newCharterTripCrewGroundTransportationExpense = value;
			    RaisePropertyChanged(nameof(NewCharterTripGroundTransportationExpense));
		    }
	    }

		//FOR CHARGE FIELDS AND METHODS --------------------------------------------------------------------------------------------------------------------

	    private string _newChargeChargeType;
	    private float? _newChargeQuantity;
	    private decimal? _newChargeAmount;
		private CharterFlightCharge _selectedCharterFlightCharge;
		public ObservableCollection<CharterFlightCharge> ChargeList { get; } = new ObservableCollection<CharterFlightCharge>();

	    public decimal? NewChargeAmount
		{
		    get => _newChargeAmount;
		    set
		    {
			    _newChargeAmount = value;
			    RaisePropertyChanged(nameof(NewChargeAmount));
		    }
	    }

	    public float? NewChargeQuantity
	    {
		    get => _newChargeQuantity;
		    set
		    {
			    _newChargeQuantity = value;
				RaisePropertyChanged(nameof(NewChargeQuantity));
		    }
	    }

		public string NewChargeChargeType
	    {
		    get => _newChargeChargeType;
		    set
		    {
			    _newChargeChargeType = value;
				RaisePropertyChanged(nameof(NewChargeChargeType));
		    }
	    }

	    public CharterFlightCharge SelectedCharterFlightCharge
	    {
		    get => _selectedCharterFlightCharge;
		    set
		    {
			    _selectedCharterFlightCharge = value;
				RaisePropertyChanged(nameof(SelectedCharterFlightCharge));
		    }
	    }

	    private void LoadChargeList()
	    {
		    ChargeList.Clear();
		    var charges = _ctUow.GetCharterFlightCharges(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);

		    foreach (var charterFlightCharge in charges)
		    {
			    ChargeList.Add(charterFlightCharge);
		    }
		}

	    private void AddNewCharge()
	    {
		    if ((!string.IsNullOrEmpty(NewChargeChargeType)) && NewChargeQuantity != null && NewChargeAmount != null)
		    {
			    if (SelectedCustomer.UsedCredits + NewChargeAmount <= SelectedCustomer.AvailableCredits)
			    {
				    var newCharge = new CharterFlightCharge();
				    newCharge.ChargeType = NewChargeChargeType;
				    newCharge.Quantity = NewChargeQuantity;
				    newCharge.Amount = NewChargeAmount;

					_ctUow.AddCharterFlightCharge(newCharge,SelectedCharterTrip);

					UpdateCredits();
					LoadCustomers();
					LoadChargeList();
				    LoadCharterTrips();

				    AddChargeViewVisibility = false;
				    ChargesAndPaymentsViewVisibility = true;
			    }
			    else
			    {
				    MaterialMessageBox.ShowError("The customer does not have enough credits to create this extra charge!");
				}
		    }
		    else
		    {
			    MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
			}
	    }

	    private void DeleteExtraCharge()
	    {
		    if (SelectedCharterFlightCharge != null)
		    {
			    var msg = new CustomMaterialMessageBox
			    {
				    TxtMessage = { Text = "Are you sure you want to delete this charge?", Foreground = Brushes.Black },
				    TxtTitle = { Text = "Warning", Foreground = Brushes.Black },
				    BtnOk = { Content = "Yes" },
				    BtnCancel = { Content = "No" },
				    MainContentControl = { Background = Brushes.White },
				    TitleBackgroundPanel = { Background = Brushes.LightSlateGray },

				    BorderBrush = Brushes.LightSlateGray
			    };
			    msg.Show();

			    var _result = msg.Result;
			    if (_result == MessageBoxResult.OK)
			    {
				    if (SelectedCharterFlightCharge.ChargeType != "Extra Crew" &&
				        SelectedCharterFlightCharge.ChargeType != "Distance Flown" &&
				        SelectedCharterFlightCharge.ChargeType != "Waiting Time" &&
				        SelectedCharterFlightCharge.ChargeType != "Crew Meals" &&
				        SelectedCharterFlightCharge.ChargeType != "Crew Lodging" &&
				        SelectedCharterFlightCharge.ChargeType != "Crew Ground Transportation")
				    {
					    _ctUow.DeleteCharterFlightCharge(SelectedCharterFlightCharge);

						UpdateCredits();
					    LoadCustomers();
					    LoadChargeList();
					    LoadCharterTrips();

					    AddChargeViewVisibility = false;
					    ChargesAndPaymentsViewVisibility = true;
					}
				    else
				    {
					    MaterialMessageBox.ShowError(
						    "This type of charge can't be deleted or edited. Modify the source of this charge instead.");
				    }
			    }
		    }
	    }

	    private void EditExtraCharge()
	    {
		    if (SelectedCharterFlightCharge.ChargeType != "Extra Crew" &&
		        SelectedCharterFlightCharge.ChargeType != "Distance Flown" &&
		        SelectedCharterFlightCharge.ChargeType != "Waiting Time" &&
		        SelectedCharterFlightCharge.ChargeType != "Crew Meals" &&
		        SelectedCharterFlightCharge.ChargeType != "Crew Lodging" &&
		        SelectedCharterFlightCharge.ChargeType != "Crew Ground Transportation")
		    {
			    SelectedCharterTrip.TotalCharge -= SelectedCharterFlightCharge.Amount;
			    SelectedCharterTrip.RemainingBalance -= SelectedCharterFlightCharge.Amount;

			    SelectedCharterFlightCharge.ChargeType = NewChargeChargeType;
			    SelectedCharterFlightCharge.Amount = NewChargeAmount;
			    SelectedCharterFlightCharge.Quantity = NewChargeQuantity;
			    _ctUow.UpdateCharterFlightCharge(SelectedCharterFlightCharge);

			    SelectedCharterTrip.TotalCharge += SelectedCharterFlightCharge.Amount;
			    SelectedCharterTrip.RemainingBalance += SelectedCharterFlightCharge.Amount;
				_ctUow.UpdateCharterTrip(SelectedCharterTrip);

				UpdateCredits();
			    LoadCustomers();
			    LoadChargeList();
			    LoadCharterTrips();

			    AddChargeViewVisibility = false;
			    ChargesAndPaymentsViewVisibility = true;
		    }
		    else
		    {
			    MaterialMessageBox.ShowError(
				    "This type of charge can't be deleted or edited. Modify the source of this charge instead.");
		    }
		}

	    public ICommand AddNewChargeCommand => new RelayCommand(AddNewCharge);
	    public ICommand DeleteExtraChargeCommand => new RelayCommand(DeleteExtraCharge);
	    public ICommand EditExtraChargeCommand => new RelayCommand(EditExtraCharge);

		//END OF FOR CHARGE FIELDS AND METHODS-------------------------------------------------------------------------------------------------------------------

		//FOR PAYMENT FIELDS AND METHODS----------------------------------------------------------------------------------------------------------------------------------------------------

		private PaymentMode _selectedPaymentMode;
	    private string _newPaymentModeOfPayment;
	    private string _newPaymentCheckNumber;
	    private decimal? _newPaymentAmount;
	    private DateTime? _newPaymentDate;
	    public ObservableCollection<PaymentMode> PaymentModeList { get; } = new ObservableCollection<PaymentMode>();

	    public PaymentMode SelectedPaymentMode
		{
		    get => _selectedPaymentMode;
		    set
		    {
			    _selectedPaymentMode = value;
			    RaisePropertyChanged(nameof(SelectedPaymentMode));
		    }
	    }

		public string NewPaymentModeOfPayment
	    {
		    get => _newPaymentModeOfPayment;
		    set
		    {
			    _newPaymentModeOfPayment = value;
				RaisePropertyChanged(nameof(NewPaymentModeOfPayment));
			    if (value == "Check")
			    {
				    CheckNumberTextBoxVisibility = true;
			    }
			    else
			    {
				    CheckNumberTextBoxVisibility = false;
			    }
		    }
	    }

	    public string NewPaymentCheckNumber
		{
		    get => _newPaymentCheckNumber;
		    set
		    {
			    _newPaymentCheckNumber = value;
			    RaisePropertyChanged(nameof(NewPaymentCheckNumber));
		    }
	    }

	    public decimal? NewPaymentAmount
	    {
		    get => _newPaymentAmount;
		    set
		    {
			    _newPaymentAmount = value;
				RaisePropertyChanged(nameof(NewPaymentAmount));
		    }
	    }

	    public DateTime? NewPaymentDate
	    {
		    get => _newPaymentDate;
		    set
		    {
			    _newPaymentDate = value;
				RaisePropertyChanged(nameof(NewPaymentDate));
		    }
	    }

	    private void LoadPaymentModes()
	    {
			PaymentModeList.Clear();
		    var paymentModes = _paymentUow.GetPaymentModes(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);

		    foreach (var paymentMode in paymentModes)
		    {
			    PaymentModeList.Add(paymentMode);
		    }
	    }

	    private void AddNewPayment()
	    {
		    if (!(string.IsNullOrEmpty(NewPaymentModeOfPayment)))
		    {
			    if (NewPaymentModeOfPayment == "Cash")
			    {
				    if (NewPaymentAmount != null && NewPaymentDate != null)
				    {
					    var newCash = new Cash();
					    newCash.Amount = NewPaymentAmount;
					    newCash.Date = NewPaymentDate;

					    _paymentUow.AddCash(newCash);

						var newPaymentMode = new PaymentMode();
					    newPaymentMode.ModeOfPayment = NewPaymentModeOfPayment;
					    newPaymentMode.CharterTripId = SelectedCharterTrip.CharterTripId;
					    newPaymentMode.CashId = newCash.CashId;
					    newPaymentMode.CustomerId = SelectedCustomer.CustomerId;
					    _paymentUow.AddPaymentMode(newPaymentMode);
					    _paymentUow.AddPaymentModeIdToCash(newCash, newPaymentMode);
					    SelectedCharterTrip.RemainingBalance -= NewPaymentAmount;
					    _ctUow.UpdateCharterTrip(SelectedCharterTrip);


						LoadPaymentModes();
					    UpdateCredits();
					    LoadCustomers();
					    LoadChargeList();
						var holderSelectedCharterTripId = SelectedCharterTrip.CharterTripId;
					    LoadCharterTrips();
					    SelectedCharterTrip = CharterTripList.FirstOrDefault(c => c.CharterTripId == holderSelectedCharterTripId);

						AddPaymentViewVisibility = false;
					    ChargesAndPaymentsViewVisibility = true;
				    }
				    else
				    {
						MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
					}
			    }
			    else if (NewPaymentModeOfPayment == "Check")
			    {
				    if (NewPaymentAmount != null && NewPaymentDate != null && !(string.IsNullOrEmpty(NewPaymentCheckNumber)))
				    {
					    var newCheck = new Check();
					    newCheck.Amount = NewPaymentAmount;
					    newCheck.CheckDate = NewPaymentDate;
					    newCheck.CheckNumber = NewPaymentCheckNumber;

					    _paymentUow.AddCheck(newCheck);

						var newPaymentMode = new PaymentMode();
						newPaymentMode.ModeOfPayment = NewPaymentModeOfPayment;
					    newPaymentMode.CharterTripId = SelectedCharterTrip.CharterTripId;
					    newPaymentMode.CheckId = newCheck.CheckId;
					    newPaymentMode.CustomerId = SelectedCustomer.CustomerId;
						_paymentUow.AddPaymentMode(newPaymentMode);					
						_paymentUow.AddPaymentModeIdToCheck(newCheck,newPaymentMode);
					    SelectedCharterTrip.RemainingBalance -= NewPaymentAmount;
						_ctUow.UpdateCharterTrip(SelectedCharterTrip);

						LoadPaymentModes();
						UpdateCredits();
						LoadCustomers();
						LoadChargeList();
					    var holderSelectedCharterTripId = SelectedCharterTrip.CharterTripId;
					    LoadCharterTrips();
					    SelectedCharterTrip = CharterTripList.FirstOrDefault(c => c.CharterTripId == holderSelectedCharterTripId);

						AddPaymentViewVisibility = false;
						ChargesAndPaymentsViewVisibility = true;
					}
				    else
				    {
					    MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
					}
			    }
		    }
		    else
		    {
				MaterialMessageBox.ShowError("Please select a mode of payment!");
		    }
	    }

	    private void DeletePayment()
	    {
		    if (SelectedPaymentMode != null)
		    {
			    var msg = new CustomMaterialMessageBox
			    {
				    TxtMessage = { Text = "Are you sure you want to delete this payment?", Foreground = Brushes.Black },
				    TxtTitle = { Text = "Warning", Foreground = Brushes.Black },
				    BtnOk = { Content = "Yes" },
				    BtnCancel = { Content = "No" },
				    MainContentControl = { Background = Brushes.White },
				    TitleBackgroundPanel = { Background = Brushes.LightSlateGray },

				    BorderBrush = Brushes.LightSlateGray
			    };
			    msg.Show();

			    var result = msg.Result;

			    if (result == MessageBoxResult.OK)
			    {
				    if (SelectedPaymentMode.ModeOfPayment == "Check")
				    {
					    var check = _paymentUow.GetCheck(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);

					    SelectedCharterTrip.RemainingBalance += check.Amount;
					    _ctUow.UpdateCharterTrip(SelectedCharterTrip);

					    _paymentUow.DeleteCheck(check);
					    _paymentUow.DeletePaymentMode(SelectedPaymentMode);
				    }
				    else if (SelectedPaymentMode.ModeOfPayment == "Cash")
				    {
					    var cash = _paymentUow.GetSingleCash(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);

					    SelectedCharterTrip.RemainingBalance += cash.Amount;
					    _ctUow.UpdateCharterTrip(SelectedCharterTrip);

					    _paymentUow.DeleteCash(cash);
					    _paymentUow.DeletePaymentMode(SelectedPaymentMode);
				    }

				    UpdateCredits();
				    LoadCustomers();
				    LoadPaymentModes();
					var holderSelectedCharterTripId = SelectedCharterTrip.CharterTripId;
				    LoadCharterTrips();
				    SelectedCharterTrip = CharterTripList.FirstOrDefault(c => c.CharterTripId == holderSelectedCharterTripId);

				}
		    }
	    }

	    private void EditPayment()
	    {
		    if (NewPaymentModeOfPayment == "Cash" && SelectedPaymentMode.ModeOfPayment == NewPaymentModeOfPayment)
		    {
			    var cash = _paymentUow.GetSingleCash(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);

			    SelectedCharterTrip.RemainingBalance += cash.Amount;

			    cash.Amount = NewPaymentAmount;
			    cash.Date = NewPaymentDate;
				_paymentUow.UpdateCash(cash);

			    SelectedCharterTrip.RemainingBalance -= NewPaymentAmount;
				_ctUow.UpdateCharterTrip(SelectedCharterTrip);
		    }
			else if (NewPaymentModeOfPayment == "Cash" && SelectedPaymentMode.ModeOfPayment == "Check")
		    {
			    var check = _paymentUow.GetCheck(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);
			    SelectedCharterTrip.RemainingBalance += check.Amount;
			    _paymentUow.DeleteCheck(check);

			    var newCash = new Cash();
			    newCash.Amount = NewPaymentAmount;
			    newCash.Date = NewPaymentDate;

			    _paymentUow.AddCash(newCash);

			    SelectedPaymentMode.ModeOfPayment = NewPaymentModeOfPayment;
			    SelectedPaymentMode.CashId = newCash.CashId;
			    SelectedPaymentMode.CheckId = null;
			    _paymentUow.UpdatePaymentMode(SelectedPaymentMode);
			    _paymentUow.AddPaymentModeIdToCash(newCash, SelectedPaymentMode);
			    SelectedCharterTrip.RemainingBalance -= NewPaymentAmount;
			    _ctUow.UpdateCharterTrip(SelectedCharterTrip);
			}
			else if (NewPaymentModeOfPayment == "Check" && SelectedPaymentMode.ModeOfPayment == NewPaymentModeOfPayment)
		    {
			    var check = _paymentUow.GetCheck(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);

			    SelectedCharterTrip.RemainingBalance += check.Amount;

			    check.Amount = NewPaymentAmount;
			    check.CheckDate = NewPaymentDate;
			    check.CheckNumber = NewPaymentCheckNumber;
				_paymentUow.UpdateCheck(check);

			    SelectedCharterTrip.RemainingBalance -= NewPaymentAmount;
				_ctUow.UpdateCharterTrip(SelectedCharterTrip);
		    }
			else if (NewPaymentModeOfPayment == "Check" && SelectedPaymentMode.ModeOfPayment == "Cash")
		    {
				var cash = _paymentUow.GetSingleCash(c => c.PaymentModeId == SelectedPaymentMode.PaymentModeId);
			    SelectedCharterTrip.RemainingBalance += cash.Amount;
			    _paymentUow.DeleteCash(cash);

			    var newCheck = new Check();
			    newCheck.Amount = NewPaymentAmount;
			    newCheck.CheckDate = NewPaymentDate;
			    newCheck.CheckNumber = NewPaymentCheckNumber;

			    _paymentUow.AddCheck(newCheck);

			    SelectedPaymentMode.ModeOfPayment = NewPaymentModeOfPayment;
			    SelectedPaymentMode.CheckId = newCheck.CheckId;
			    SelectedPaymentMode.CashId = null;
			    _paymentUow.UpdatePaymentMode(SelectedPaymentMode);
			    _paymentUow.AddPaymentModeIdToCheck(newCheck, SelectedPaymentMode);
			    SelectedCharterTrip.RemainingBalance -= NewPaymentAmount;
			    _ctUow.UpdateCharterTrip(SelectedCharterTrip);
			}

		    LoadPaymentModes();
		    UpdateCredits();
		    LoadCustomers();
		    LoadChargeList();
		    var holderSelectedCharterTripId = SelectedCharterTrip.CharterTripId;
		    LoadCharterTrips();
		    SelectedCharterTrip = CharterTripList.FirstOrDefault(c => c.CharterTripId == holderSelectedCharterTripId);

		    AddPaymentViewVisibility = false;
		    ChargesAndPaymentsViewVisibility = true;
		}

	    public ICommand AddNewPaymentCommand => new RelayCommand(AddNewPayment);
	    public ICommand DeletePaymentCommand => new RelayCommand(DeletePayment);
	    public ICommand EditPaymentCommand => new RelayCommand(EditPayment);

		//END OF FOR PAYMENT FIELDS AND METHODS----------------------------------------------------------------------------------------------------------------------------------------

		//FOR EMPLOYEE FIELDS AND METHODS---------------------------------------------------------------------------------------------------------------------------------------------------------------
		public ObservableCollection<CrewAssignment> CrewAssignmentList { get; } = new ObservableCollection<CrewAssignment>();

	    private void LoadCrewAssignments()
	    {
		    if (SelectedCharterTrip != null)
		    {
			    var crewAssignments =
				    _ctUow.GetCrewAssignments(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);
				CrewAssignmentList.Clear();

			    foreach (var crewAssignment in crewAssignments)
			    {
				    CrewAssignmentList.Add(crewAssignment);
			    }
		    }
	    }

	    private void SaveEmployeeHours()
	    {
		    foreach (var crewAssignment in CrewAssignmentList)
		    {
			    if (crewAssignment.Role.Contains("EXTRA CREW"))
			    {
				    var charge = _ctUow.GetCharterFlightCharge(c => c.EmployeeId == crewAssignment.EmployeeId && c.CharterTripId == SelectedCharterTrip.CharterTripId);

				    if (charge == null)
				    {
						var newCharge = new CharterFlightCharge();

					    newCharge.Quantity = crewAssignment.WorkHours;

					    var licenseDescription = crewAssignment.Role.Substring(0, crewAssignment.Role.Length - 11);
					    var license = _employeeUow.GetLicense(c => c.Description.Contains(licenseDescription));

					    newCharge.Amount = license.HourlyCharge * Convert.ToDecimal(crewAssignment.WorkHours);
					    newCharge.ChargeType = "EXTRA CREW";
					    newCharge.EmployeeId = crewAssignment.EmployeeId;
						_ctUow.AddCharterFlightCharge(newCharge, SelectedCharterTrip);
				    }
				    else
				    {
					    SelectedCharterTrip.TotalCharge -= charge.Amount;
					    SelectedCharterTrip.RemainingBalance -= charge.Amount;

						var licenseDescription = crewAssignment.Role.Substring(0, crewAssignment.Role.Length - 11);
					    var license = _employeeUow.GetLicense(c => c.Description.Contains(licenseDescription));

						charge.Quantity = crewAssignment.WorkHours;
					    charge.Amount = Convert.ToDecimal(crewAssignment.WorkHours) * license.HourlyCharge;
					    SelectedCharterTrip.TotalCharge += charge.Amount;
					    SelectedCharterTrip.RemainingBalance += charge.Amount;
						_ctUow.UpdateCharterFlightCharge(charge);
						_ctUow.UpdateCharterTrip(SelectedCharterTrip);
				    }
			    }

				_ctUow.UpdateCrewAssignment(crewAssignment);
		    }

		    UpdateCredits();
		    LoadCustomers();
		    LoadChargeList();
		    LoadCharterTrips();

			CrewHoursViewVisibility = false;
		    CustomerViewVisibility = true;
	    }

	    private void AddNewEmployee()
	    {
			EmployeeViewModel.AddNewEmployee();
		    AddEmployeeViewVisibility = false;
		    EmployeeViewVisibility = true;
	    }

	    private void EditEmployee()
	    {
			EmployeeViewModel.EditEmployee();
		    AddEmployeeViewVisibility = false;
		    EmployeeViewVisibility = true;
	    }

	    private void DeleteEmployee()
	    {
			EmployeeViewModel.DeleteEmployee();

	    }

	    public ICommand SaveEmployeeHoursCommand => new RelayCommand(SaveEmployeeHours);

	    public ICommand AddNewEmployeeCommand => new RelayCommand(AddNewEmployee);

	    public ICommand EditEmployeeCommand => new RelayCommand(EditEmployee);

	    public ICommand DeleteEmployeeCommand => new RelayCommand(DeleteEmployee);
		//END OF FOR EMPLOYEE FIELDS AND METHODS---------------------------------------------------------------------------------------------------------------------------------------------------------

		//FOR CHARTER TRIP FIELDS AND METHODS -------------------------------------------------------------------------------------------------------
		private Aircraft _selectedCharterTripAircraft;
	    private CharterTripUnitOfWork _ctUow;
	    public ObservableCollection<CharterTrip> CharterTripList { get; } = new ObservableCollection<CharterTrip>();
	    private ObservableCollection<CrewRequirement> _selectedAircraftCrewRequirements;
	    private ListCollectionView _charterTripListView;
	    private CharterTrip _selectedCharterTrip;

	    public Aircraft SelectedCharterTripAircraft
	    {
		    get => _selectedCharterTripAircraft;
		    set
		    {
			    _selectedCharterTripAircraft = value;
				RaisePropertyChanged(nameof(SelectedCharterTripAircraft));
		    }
	    }

		private void FinalizeNewCharterTrip()
		{
			bool hasSufficientCrew = true;
			bool hasSufficientCredits = true;

			foreach (var selectedAircraftCrewRequirement in SelectedAircraftCrewRequirements)
			{
				var testList = NewCharterTripEmployeesToBeAssigned.Where(c =>
					c.SelectedLicense.LicenseType == selectedAircraftCrewRequirement.LicenseType);

				if (!(testList.Count() >= selectedAircraftCrewRequirement.RequiredNumber))
				{
					hasSufficientCrew = false;
					break;
				}
			}

			if (SelectedCustomer.UsedCredits + NewCharterTripCrewLodgingExpense + NewCharterTripCrewMealsExpense +
			    NewCharterTripGroundTransportationExpense > SelectedCustomer.AvailableCredits)
			{
				hasSufficientCredits = false;
			}

			if (SelectedPurpose != null && SelectedAircraft != null && hasSufficientCrew && hasSufficientCredits)
			{
				var newCharterTrip = new CharterTrip { Purpose = SelectedPurpose };

				_ctUow.AddCharterTrip(newCharterTrip, SelectedAircraft, SelectedCustomer);

				foreach (var employeeToBeAssigned in NewCharterTripEmployeesToBeAssigned)
				{
					var newCrewAssignment = new CrewAssignment();
					newCrewAssignment.Role = employeeToBeAssigned.SelectedLicense.Description;

					if (employeeToBeAssigned.IsExtraCrew == true)
					{
						newCrewAssignment.Role += " EXTRA CREW";
					}

					newCrewAssignment.DateAssigned = DateTime.Now.Date;

					var employeeAssigned =
						_employeeUow.GetEmployee(c => c.EmployeeId == employeeToBeAssigned.EmployeeId);

					_ctUow.AddCrewMember(newCrewAssignment, employeeAssigned, newCharterTrip);
				}

				var newCharge1 = new CharterFlightCharge
				{
					ChargeType = "Crew Meals", Amount = NewCharterTripCrewMealsExpense
				};

				var newCharge2 = new CharterFlightCharge
				{
					ChargeType = "Crew Lodging", Amount = NewCharterTripCrewLodgingExpense
				};

				var newCharge3 = new CharterFlightCharge
				{
					ChargeType = "Crew Ground Transportation", Amount = NewCharterTripGroundTransportationExpense
				};

				_ctUow.AddCharterFlightCharge(newCharge1, newCharterTrip);
				_ctUow.AddCharterFlightCharge(newCharge2, newCharterTrip);
				_ctUow.AddCharterFlightCharge(newCharge3, newCharterTrip);

				UpdateCredits();

				SelectedPurpose = null;
				SelectedAircraft = null;
				SelectedAircraftCrewRequirements.Clear();
				CrewRequirementListViewItems.Clear();
				SelectedEmployee = null;
				NewCharterTripEmployeesToBeAssigned.Clear();
				LoadCharterTrips();
				LoadCustomers();

				CustomerViewVisibility = true;
				AddCharterTripView2Visibility = false;
			}
			else
			{
				if (!hasSufficientCrew || SelectedPurpose == null || SelectedAircraft == null)
				{
					MaterialMessageBox.ShowError("Make sure to fill out every field and ensure that you have sufficient crew members!");
				}

				if (!hasSufficientCredits)
				{
					MaterialMessageBox.ShowError("The customer does not have enough credits to create this charter trip!");
				}
			}
		}

	    private void EditCharterTrip()
	    {
		    bool hasSufficientCrew = true;
		    bool hasSufficientCredits = true;

		    foreach (var selectedAircraftCrewRequirement in SelectedAircraftCrewRequirements)
		    {
			    var testList = NewCharterTripEmployeesToBeAssigned.Where(c =>
				    c.SelectedLicense.LicenseType == selectedAircraftCrewRequirement.LicenseType);

			    if (!(testList.Count() >= selectedAircraftCrewRequirement.RequiredNumber))
			    {
				    hasSufficientCrew = false;
				    break;
			    }
		    }

		    var previousCrewLodgeExpense = _ctUow.GetCharterFlightCharge(c =>
			    c.CharterTripId == SelectedCharterTrip.CharterTripId && c.ChargeType == "Crew Lodging");

		    var previousCrewMealsExpense = _ctUow.GetCharterFlightCharge(c =>
			    c.CharterTripId == SelectedCharterTrip.CharterTripId && c.ChargeType == "Crew Meals");

		    var previousCrewGroundTransportationExpense = _ctUow.GetCharterFlightCharge(c =>
			    c.CharterTripId == SelectedCharterTrip.CharterTripId && c.ChargeType == "Crew Ground Transportation");

		    if (SelectedCustomerUnusedCredits - NewCharterTripCrewLodgingExpense -
		        NewCharterTripCrewMealsExpense - NewCharterTripGroundTransportationExpense <
		        0)
		    {
			    hasSufficientCredits = false;
			}

			if (SelectedPurpose != null && SelectedAircraft != null && hasSufficientCrew && hasSufficientCredits)
		    {
			    SelectedCharterTrip.Purpose = SelectedPurpose;

			    var existingCrewAssignments =
				    _ctUow.GetCrewAssignments(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);

			    foreach (var existingCrewAssignment in existingCrewAssignments)
			    {
				    _ctUow.DeleteCrewAssignment(existingCrewAssignment);
			    }

				foreach (var employeeToBeAssigned in NewCharterTripEmployeesToBeAssigned)
				{
					var newCrewAssignment = new CrewAssignment();
					newCrewAssignment.Role = employeeToBeAssigned.SelectedLicense.Description;

					if (employeeToBeAssigned.IsExtraCrew == true)
					{
						newCrewAssignment.Role += " EXTRA CREW";
					}

					newCrewAssignment.DateAssigned = DateTime.Now.Date;

					var employeeAssigned =
					 _employeeUow.GetEmployee(c => c.EmployeeId == employeeToBeAssigned.EmployeeId);

					_ctUow.AddCrewMember(newCrewAssignment, employeeAssigned, SelectedCharterTrip);
				}

				_ctUow.DeleteCharterFlightCharge(previousCrewGroundTransportationExpense);
				_ctUow.DeleteCharterFlightCharge(previousCrewLodgeExpense);
				_ctUow.DeleteCharterFlightCharge(previousCrewMealsExpense);


			    var newCharge1 = new CharterFlightCharge
			    {
				    ChargeType = "Crew Meals",
				    Amount = NewCharterTripCrewMealsExpense
			    };

			    var newCharge2 = new CharterFlightCharge
			    {
				    ChargeType = "Crew Lodging",
				    Amount = NewCharterTripCrewLodgingExpense
			    };

			    var newCharge3 = new CharterFlightCharge
			    {
				    ChargeType = "Crew Ground Transportation",
				    Amount = NewCharterTripGroundTransportationExpense
			    };

			    _ctUow.AddCharterFlightCharge(newCharge1, SelectedCharterTrip);
			    _ctUow.AddCharterFlightCharge(newCharge2, SelectedCharterTrip);
			    _ctUow.AddCharterFlightCharge(newCharge3, SelectedCharterTrip);

			    UpdateCredits();

				SelectedPurpose = null;
				SelectedAircraft = null;
				SelectedAircraftCrewRequirements.Clear();
				CrewRequirementListViewItems.Clear();
				SelectedEmployee = null;
				NewCharterTripEmployeesToBeAssigned.Clear();
				LoadCharterTrips();
			    LoadCustomers();

				CustomerViewVisibility = true;
			    AddCharterTripView2Visibility = false;
			}
		    else
		    {
				if (!hasSufficientCrew || SelectedPurpose == null || SelectedAircraft == null)
			    {
				    MaterialMessageBox.ShowError(
					    "Make sure to fill out every field and ensure that you have sufficient crew members!");
			    }

			    if (!hasSufficientCredits)
			    {
				    MaterialMessageBox.ShowError("The customer does not have enough credits to create this charter trip!");
			    }
			}
		}

	    private void AddNewResult()
	    {
			if(!(string.IsNullOrEmpty(EmployeeViewModel.NewResultTestResult)) && EmployeeViewModel.NewResultTest != null && EmployeeViewModel.NewResultExpiration != null && EmployeeViewModel.NewResultTestDate != null)
		    {
				EmployeeViewModel.AddResult();

			    AddResultViewVisibiility = false;
			    EmployeeViewVisibility = true;
		    }
			else
			{
				MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
			}
	    }

	    private void EditResult()
	    {
		    if (!(string.IsNullOrEmpty(EmployeeViewModel.NewResultTestResult)) &&
		        EmployeeViewModel.NewResultExpiration != null && EmployeeViewModel.NewResultTest != null &&
		        EmployeeViewModel.NewResultTestDate != null)
		    {
				EmployeeViewModel.EditResult();

			    AddResultViewVisibiility = false;
			    EmployeeViewVisibility = true;
		    }
		    else
		    {
			    MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
			}
	    }

	    private void DeleteResult()
	    {
		    if (EmployeeViewModel.SelectedEmployeeResult != null)
		    {
			    var msg = new CustomMaterialMessageBox
			    {
				    TxtMessage = { Text = "Are you sure you want to delete this test result from this employee?", Foreground = Brushes.Black },
				    TxtTitle = { Text = "Warning", Foreground = Brushes.Black },
				    BtnOk = { Content = "Yes" },
				    BtnCancel = { Content = "No" },
				    MainContentControl = { Background = Brushes.White },
				    TitleBackgroundPanel = { Background = Brushes.LightSlateGray },

				    BorderBrush = Brushes.LightSlateGray
			    };
			    msg.Show();

			    var _result = msg.Result;

			    if (_result == MessageBoxResult.OK)
			    {
					EmployeeViewModel.DeleteResult();
			    }

			}
	    }

	    public ICommand DeleteResultCommand => new RelayCommand(DeleteResult);


		public ICommand EditResultCommand => new RelayCommand(EditResult);

		public ICommand AddNewResultCommand => new RelayCommand(AddNewResult);


		private void DeleteSelectedCharterTrip()
		{
			if (SelectedCharterTrip != null)
			{
				var msg = new CustomMaterialMessageBox
				{
					TxtMessage = { Text = "Are you sure you want to delete this charter trip? All flights, charges, payments, and other entities associated with this charter trip will be deleted as well.", Foreground = Brushes.Black },
					TxtTitle = { Text = "Warning", Foreground = Brushes.Black },
					BtnOk = { Content = "Yes" },
					BtnCancel = { Content = "No" },
					MainContentControl = { Background = Brushes.White },
					TitleBackgroundPanel = { Background = Brushes.LightSlateGray },

					BorderBrush = Brushes.LightSlateGray
				};
				msg.Show();

				var _result = msg.Result;

				if (_result == MessageBoxResult.OK)
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

					var listPaymentModes = _paymentUow.GetPaymentModes(c => c.CharterTripId == SelectedCharterTrip.CharterTripId);

					foreach (var paymentMode in listPaymentModes)
					{
						if (paymentMode.ModeOfPayment == "Check")
						{
							var check = _paymentUow.GetCheck(c => c.PaymentModeId == paymentMode.PaymentModeId);
							_paymentUow.DeleteCheck(check);
						}
						else if (paymentMode.ModeOfPayment == "Cash")
						{
							var cash = _paymentUow.GetSingleCash(c => c.PaymentModeId == paymentMode.PaymentModeId);
							_paymentUow.DeleteCash(cash);
						}

						_paymentUow.DeletePaymentMode(paymentMode);
					}

					_ctUow.DeleteCharterTrip(SelectedCharterTrip);
					UpdateCredits();
					LoadCharterTrips();
				}
			}
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

	    private void UpdateCredits()
	    {
		    var unpaidCharterTrips = _ctUow.GetCharterTrips(c => c.RemainingBalance != 0 && c.CustomerId == SelectedCustomer.CustomerId);

		    decimal? totalUsedCredits = 0;

		    foreach (var unpaidCharterTrip in unpaidCharterTrips)
		    {
			    totalUsedCredits += unpaidCharterTrip.RemainingBalance;
		    }

		    SelectedCustomer.UsedCredits = totalUsedCredits;
			_ctUow.UpdateCustomer(SelectedCustomer);
	    }

		public ICommand FinalizeNewCharterTripCommand => new RelayCommand(FinalizeNewCharterTrip);
		public ICommand DeleteSelectedCharterTripCommand => new RelayCommand(DeleteSelectedCharterTrip);
	    public ICommand EditCharterTripCommand => new RelayCommand(EditCharterTrip);

		public CharterTrip SelectedCharterTrip
		{
			get => _selectedCharterTrip;
			set
			{
				_selectedCharterTrip = value;
				RaisePropertyChanged(nameof(SelectedCharterTrip));
				if (value != null)
				{
					SelectedCharterTripAircraft =
						_aircraftUow.GetSingleAircraft(c => c.AircraftNumber == value.AircraftNumber);
				}
			}
		}

		//END OF FOR CHARTER TRIP FIELDS AND METHODS-------------------------------------------------------------------------------------------------------------

		//FOR CUSTOMER FIELDS AND METHODS --------------------------------------------------------------------------------------------------------------------
	    private decimal? _selectedCustomerUnusedCredits;
		private string _selectedCustomerNameHeader;
		private string _customerSearchText;
	    private const int CustomerPerPageInList = 50;
	    private Customer _selectedCustomer;
	    private ListCollectionView _customerListView;
	    public ObservableCollection<Customer> CustomerList { get; } = new ObservableCollection<Customer>();
	    private string _newCustomerName;
	    private string _newCustomerAddress;
	    private decimal? _newCustomerAvailableCredits;

	    public decimal? SelectedCustomerUnusedCredits
	    {
		    get => _selectedCustomerUnusedCredits;
		    set
		    {
			    _selectedCustomerUnusedCredits = value;
				RaisePropertyChanged(nameof(SelectedCustomerUnusedCredits));
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

	    private void LoadCustomers(int page)
	    {
		    CustomerList.Clear();
		    var customers = _ctUow.GetCustomersByPage(CustomerPerPageInList, page);
		    foreach (var customer in customers)
		    {
			    CustomerList.Add(customer);
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
		    
		    if (CustomerSearchText == "")
		    {
			    _selectedPage = 1;
			    LoadCustomers(1);
		    }
	    }

	    private void AddNewCustomer()
	    {
		    if (NewCustomerName != "" && NewCustomerAddress != "" && NewCustomerAvailableCredits != null)
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
			    CustomerSearchText = "";

			    AddCustomerViewVisibility = false;
			    CustomerViewVisibility = true;
		    }
		    else
		    {
			    MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
		    }
	    }

	    private void DeleteCustomer()
	    {
		    if (SelectedCustomer != null)
		    {
			    var msg = new CustomMaterialMessageBox
			    {
				    TxtMessage = { Text = "Are you sure you want to delete this customer?", Foreground = Brushes.Black },
				    TxtTitle = { Text = "Confirm Delete", Foreground = Brushes.Black },
				    BtnOk = { Content = "Yes" },
				    BtnCancel = { Content = "No" },
				    MainContentControl = { Background = Brushes.White },
				    TitleBackgroundPanel = { Background = Brushes.LightSlateGray },

				    BorderBrush = Brushes.LightSlateGray
			    };
				msg.Show();

			    var _result = msg.Result;

			    if (_result == MessageBoxResult.OK)
			    {
				    var charterTrips = _ctUow.GetCharterTrips(c => c.CustomerId == SelectedCustomer.CustomerId);

				    foreach (var charterTrip in charterTrips)
				    {
					    DeleteSpecifiedCharterTrip(charterTrip);
				    }

				    _ctUow.DeleteCustomer(SelectedCustomer);
				    CustomerSearchText = "";
				    LoadCustomers();
			    }
		    }
	    }

	    private void DeleteSpecifiedCharterTrip(CharterTrip charterTrip)
	    {
		    if (charterTrip != null)
		    {

				 var listCrewAssignments =
				  _ctUow.GetCrewAssignments(c =>
				   c.CharterTrip.CharterTripId == charterTrip.CharterTripId);

				 foreach (var x in listCrewAssignments)
				 {
					_ctUow.DeleteCrewAssignment(x);
				 }

				 var listFlightLegs = _ctUow.GetFlights(c => c.CharterTripId == charterTrip.CharterTripId);

				 foreach (var flightLeg in listFlightLegs)
				 {
					_ctUow.DeleteFlight(flightLeg);
				 }

				 var listCharterFlightCharges =
				   _ctUow.GetCharterFlightCharges(c => c.CharterTripId == charterTrip.CharterTripId)
				  ;

				 foreach (var charterFlightCharge in listCharterFlightCharges)
				 {
					 _ctUow.DeleteCharterFlightCharge(charterFlightCharge);
				 }

				 var listBalanceHistories =
				  _ctUow.GetBalanceHistories(c => c.CharterTripId == charterTrip.CharterTripId);

				 foreach (var balanceHistory in listBalanceHistories)
				 {
				  _ctUow.DeleteBalanceHistory(balanceHistory);
				 }

			    var listPaymentModes = _paymentUow.GetPaymentModes(c => c.CharterTripId == charterTrip.CharterTripId);

			    foreach (var paymentMode in listPaymentModes)
			    {
				    if (paymentMode.ModeOfPayment == "Check")
				    {
					    var check = _paymentUow.GetCheck(c => c.PaymentModeId == paymentMode.PaymentModeId);
						_paymentUow.DeleteCheck(check);
				    }
					else if (paymentMode.ModeOfPayment == "Cash")
				    {
					    var cash = _paymentUow.GetSingleCash(c => c.PaymentModeId == paymentMode.PaymentModeId);
						_paymentUow.DeleteCash(cash);
				    }

					_paymentUow.DeletePaymentMode(paymentMode);
			    }

				 _ctUow.DeleteCharterTrip(charterTrip);
			    
		    }
	    }

		private void EditCustomer()
	    {
		    if (NewCustomerName != "" && NewCustomerAddress != "" && NewCustomerAvailableCredits != null)
		    {
			    SelectedCustomer.Name = NewCustomerName;
			    SelectedCustomer.Address = NewCustomerAddress;
			    SelectedCustomer.AvailableCredits = NewCustomerAvailableCredits;
			    _ctUow.UpdateCustomer(SelectedCustomer);
			    NewCustomerName = "";
			    NewCustomerAddress = "";
			    NewCustomerAvailableCredits = null;
			    LoadCustomers();
			    CustomerSearchText = "";

				AddCustomerViewVisibility = false;
			    CustomerViewVisibility = true;
			}
		    else
		    {
			    MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
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
				RaisePropertyChanged(nameof(CustomerSearchText));
			    FilterCustomerList(value);
		    }
	    }

	    public ICommand NextPageCommand => new RelayCommand(NextPageProc, NextPageCondition);
	    public ICommand PrevPageCommand => new RelayCommand(PrevPageProc);
	    public ICommand AddCustomerCommand => new RelayCommand(AddNewCustomer);
	    public ICommand EditCustomerCommand => new RelayCommand(EditCustomer);
	    public ICommand DeleteCustomerCommand => new RelayCommand(DeleteCustomer);

		//END OF FOR CUSTOMER FIELDS AND METHODS--------------------------------------------------------------------------------------------------------------------------------

		//FOR FLIGHT LEG FIELDS AND METHODS-------------------------------------------------------------------------------------------------------------------------------------
		public ObservableCollection<string> StatesList { get; } = new ObservableCollection<string>{"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut",
			"Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland",
			"Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
			"New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota",
			"Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming", "Canada"};

	    private decimal? _newFlightDistanceFlownCharge;
	    private decimal? _newFlightWaitingTimeCharge;
	    private decimal? _newFlightTotalCharge;
	    private DateTime? _newFlightDate;
	    private DateTime? _newFlightTimeDeparture;
	    private DateTime? _newFlightTimeArrival;
	    private string _newFlightOrigin;
	    private string _newFlightDestination;
	    private float? _newFlightDistanceFlown;
	    private float? _newFlightWaitingTime;
	    private float? _newFlightFuelUsage;
	    private int? _newFlightOrder;
	    private Flight _selectedFlight;
	    public ObservableCollection<Flight> FlightList { get; } = new ObservableCollection<Flight>();

	    public decimal? NewFlightTotalCharge
		{
		    get => _newFlightTotalCharge;
		    set
		    {
			    _newFlightTotalCharge = value;
			    RaisePropertyChanged(nameof(NewFlightTotalCharge));
		    }
	    }

		public decimal? NewFlightDistanceFlownCharge
	    {
		    get => _newFlightDistanceFlownCharge;
		    set
		    {
			    _newFlightDistanceFlownCharge = value;
				RaisePropertyChanged(nameof(NewFlightDistanceFlownCharge));
			    NewFlightTotalCharge = value + NewFlightWaitingTimeCharge;
			}
	    }

	    public decimal? NewFlightWaitingTimeCharge
		{
		    get => _newFlightWaitingTimeCharge;
		    set
		    {
			    _newFlightWaitingTimeCharge = value;
			    RaisePropertyChanged(nameof(NewFlightWaitingTimeCharge));
			    NewFlightTotalCharge = value + NewFlightDistanceFlownCharge;
		    }
	    }

		public DateTime? NewFlightDate
	    {
		    get => _newFlightDate;
		    set
		    {
			    _newFlightDate = value;
				RaisePropertyChanged(nameof(NewFlightDate));
		    }
	    }

	    public DateTime? NewFlightTimeDeparture
	    {
		    get => _newFlightTimeDeparture;
		    set
		    {
			    _newFlightTimeDeparture = value;
				RaisePropertyChanged(nameof(NewFlightTimeDeparture));
		    }
	    }

	    public DateTime? NewFlightTimeArrival
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
			    NewFlightDistanceFlownCharge = Convert.ToDecimal(SelectedCharterTripAircraft.ChargePerMile * NewFlightDistanceFlown);
		    }
	    }

	    public float? NewFlightWaitingTime
	    {
		    get => _newFlightWaitingTime;
		    set
		    {
			    _newFlightWaitingTime = value;
				RaisePropertyChanged(nameof(NewFlightWaitingTime));
			    NewFlightWaitingTimeCharge =
				    Convert.ToDecimal(SelectedCharterTripAircraft.HourlyWaitingCharge * NewFlightWaitingTime);
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

	    public Flight SelectedFlight
	    {
		    get => _selectedFlight;
		    set
		    {
			    _selectedFlight = value;
			    RaisePropertyChanged(nameof(SelectedFlight));
		    }
	    }

	    private void AddNewLicensure()
	    {
		    if (EmployeeViewModel.NewLicensureEarnedDate != null &&
		        EmployeeViewModel.NewLicensureSelectedLicense != null)
		    {
				EmployeeViewModel.AddLicensure();

			    AddLicensureViewVisibility = false;
			    EmployeeViewVisibility = true;
		    }
	    }

	    private void DeleteLicensure()
	    {
		    if (EmployeeViewModel.SelectedEmployeeLicensure != null)
		    {

				EmployeeViewModel.DeleteLicensure();
			  
			}
	    }

	    private void EditLicensure()
	    {
		    if (EmployeeViewModel.SelectedEmployeeLicensure != null)
		    {
			    if (EmployeeViewModel.NewLicensureEarnedDate != null && EmployeeViewModel.NewLicensureSelectedLicense != null)
			    {
					EmployeeViewModel.EditLicensure();

				    EmployeeViewVisibility = true;
				    AddLicensureViewVisibility = false;
			    }
			    else
			    {
				    MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
				}
		    }
	    }

	    public ICommand AddNewLicensureCommand => new RelayCommand(AddNewLicensure);
	    public ICommand DeleteLicensureCommand => new RelayCommand(DeleteLicensure);
	    public ICommand EditLicensureCommand => new RelayCommand(EditLicensure);


		private void AddNewFlight()
	    {
		    if (!string.IsNullOrEmpty(NewFlightDestination) && NewFlightDate != null && NewFlightDistanceFlown != null && NewFlightFuelUsage != null && !string.IsNullOrEmpty(NewFlightOrigin) && NewFlightTimeArrival != null && NewFlightTimeDeparture != null && NewFlightWaitingTime != null)
		    {
			    var aircraft = _aircraftUow.GetSingleAircraft(c => c.AircraftNumber == SelectedCharterTrip.AircraftNumber);

				var distanceFlownCharge = new CharterFlightCharge();
			    distanceFlownCharge.ChargeType = "Distance Flown";
			    distanceFlownCharge.Quantity = NewFlightDistanceFlown;
			    distanceFlownCharge.Amount = Convert.ToDecimal(aircraft.ChargePerMile * NewFlightDistanceFlown);

				var waitingTimeCharge = new CharterFlightCharge();
			    waitingTimeCharge.ChargeType = "Waiting Time";
			    waitingTimeCharge.Quantity = NewFlightWaitingTime;
			    waitingTimeCharge.Amount = Convert.ToDecimal(aircraft.HourlyWaitingCharge * NewFlightWaitingTime);

			    if (distanceFlownCharge.Amount + waitingTimeCharge.Amount + SelectedCustomer.UsedCredits <=
			        SelectedCustomer.AvailableCredits)
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

					_ctUow.AddCharterFlightCharge(distanceFlownCharge, SelectedCharterTrip);
					_ctUow.AddCharterFlightCharge(waitingTimeCharge, SelectedCharterTrip);

				    _ctUow.UpdateCharterTrip(SelectedCharterTrip);

				    UpdateCredits();
					LoadCustomers();
				    LoadFlightLegs();

					NewFlightDestination = null;
				    NewFlightDistanceFlown = null;
				    NewFlightFuelUsage = null;
				    NewFlightWaitingTime = null;

				    AddFlightViewVisibility = false;
				    FlightLegsViewVisibility = true;
			    }
			    else
			    {
				    MaterialMessageBox.ShowError("The customer does not have enough unused credits to create this flight!");
			    }
		    }
		    else
		    {
			    MaterialMessageBox.ShowError("Please make sure that there are no empty fields!");
		    }
	    }

	    private void DeleteFlight()
	    {
		    if (SelectedFlight != null)
		    {
			    var msg = new CustomMaterialMessageBox
			    {
				    TxtMessage = { Text = "Are you sure you want to delete this flight leg?", Foreground = Brushes.Black },
				    TxtTitle = { Text = "Confirm Delete Flight Leg", Foreground = Brushes.Black },
				    BtnOk = { Content = "Yes" },
				    BtnCancel = { Content = "No" },
				    MainContentControl = { Background = Brushes.White },
				    TitleBackgroundPanel = { Background = Brushes.LightSlateGray },

				    BorderBrush = Brushes.LightSlateGray
			    };
			    msg.Show();

			    var _result = msg.Result;

			    if (_result == MessageBoxResult.OK)
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

				    var distanceFlownCharge = _ctUow.GetCharterFlightCharge(c =>
					    c.ChargeType == "Distance Flown" && c.Quantity == SelectedFlight.DistanceFlown && c.CharterTripId == SelectedCharterTrip.CharterTripId);

				    var waitingTimeCharge = _ctUow.GetCharterFlightCharge(c =>
					    c.ChargeType == "Waiting Time" && c.Quantity == SelectedFlight.WaitingTime && c.CharterTripId == SelectedCharterTrip.CharterTripId);

					_ctUow.DeleteCharterFlightCharge(distanceFlownCharge);
					_ctUow.DeleteCharterFlightCharge(waitingTimeCharge);
					_ctUow.UpdateCharterTrip(SelectedCharterTrip);
				    _ctUow.DeleteFlight(SelectedFlight);
					UpdateCredits();
					LoadCustomers();
				    LoadFlightLegs();
			    }
		    }
	    }

	    private void EditFlight()
	    {
		    if (!string.IsNullOrEmpty(NewFlightDestination) && NewFlightDate != null &&
		        NewFlightDistanceFlown != null && NewFlightFuelUsage != null &&
		        !string.IsNullOrEmpty(NewFlightOrigin) && NewFlightTimeArrival != null &&
		        NewFlightTimeDeparture != null && NewFlightWaitingTime != null)
		    {
			    var oldDistanceFlownCharge = _ctUow.GetCharterFlightCharge(c =>
				    c.ChargeType == "Distance Flown" && c.Quantity == SelectedFlight.DistanceFlown && c.CharterTripId == SelectedCharterTrip.CharterTripId);

			    var oldWaitingTimeCharge = _ctUow.GetCharterFlightCharge(c =>
				    c.ChargeType == "Waiting Time" && c.Quantity == SelectedFlight.WaitingTime && c.CharterTripId == SelectedCharterTrip.CharterTripId);

			    var aircraft = _aircraftUow.GetSingleAircraft(c => c.AircraftNumber == SelectedCharterTrip.AircraftNumber);		

				if (SelectedCustomerUnusedCredits - Convert.ToDecimal(aircraft.HourlyWaitingCharge * NewFlightWaitingTime) - Convert.ToDecimal(aircraft.ChargePerMile * NewFlightDistanceFlown) >= 0)
			    {

				    var oldDistanceFlown = SelectedFlight.DistanceFlown;
				    var oldWaitingTime = SelectedFlight.WaitingTime;
				    var oldFuelUsage = SelectedFlight.FuelUsage;

				    SelectedFlight.Date = NewFlightDate;
				    SelectedFlight.TimeDeparture = NewFlightTimeDeparture;
				    SelectedFlight.TimeArrival = NewFlightTimeArrival;
				    SelectedFlight.Origin = NewFlightOrigin;
				    SelectedFlight.Destination = NewFlightDestination;
				    SelectedFlight.DistanceFlown = NewFlightDistanceFlown;
				    SelectedFlight.WaitingTime = NewFlightWaitingTime;
				    SelectedFlight.FuelUsage = NewFlightFuelUsage;

				    _ctUow.UpdateFlight(SelectedFlight);

				    if (SelectedFlight.Order == FlightList.Count)
				    {
					    SelectedCharterTrip.FinalDestination = NewFlightDestination;
				    }

				    if (SelectedFlight.Order == FlightList.Count && FlightList.Count == 1)
				    {
					    SelectedCharterTrip.FinalDestination = NewFlightDestination;
					    SelectedCharterTrip.Origin = NewFlightOrigin;
				    }

				    if (SelectedFlight.Order == 1 && FlightList.Count != 1)
				    {
					    SelectedCharterTrip.Origin = NewFlightOrigin;
				    }

				    SelectedCharterTrip.TotalDistanceFlown -= oldDistanceFlown;
				    SelectedCharterTrip.TotalFuelUsage -= oldFuelUsage;
				    SelectedCharterTrip.TotalWaitingTime -= oldWaitingTime;

				    SelectedCharterTrip.TotalDistanceFlown += NewFlightDistanceFlown;
				    SelectedCharterTrip.TotalFuelUsage += NewFlightFuelUsage;
				    SelectedCharterTrip.TotalWaitingTime += NewFlightWaitingTime;

				    SelectedCharterTrip.TotalCharge -= oldDistanceFlownCharge.Amount;
				    SelectedCharterTrip.TotalCharge -= oldWaitingTimeCharge.Amount;
					SelectedCharterTrip.RemainingBalance -= oldDistanceFlownCharge.Amount;
					SelectedCharterTrip.RemainingBalance -= oldWaitingTimeCharge.Amount;

					oldDistanceFlownCharge.Amount = Convert.ToDecimal(aircraft.ChargePerMile * NewFlightDistanceFlown);
				    oldDistanceFlownCharge.Quantity = NewFlightDistanceFlown;
					_ctUow.UpdateCharterFlightCharge(oldDistanceFlownCharge);
					oldWaitingTimeCharge.Amount = Convert.ToDecimal(aircraft.HourlyWaitingCharge * NewFlightWaitingTime);
				    oldWaitingTimeCharge.Quantity = NewFlightWaitingTime;
					_ctUow.UpdateCharterFlightCharge(oldWaitingTimeCharge);

				    SelectedCharterTrip.TotalCharge += oldDistanceFlownCharge.Amount;
				    SelectedCharterTrip.TotalCharge += oldWaitingTimeCharge.Amount;
				    SelectedCharterTrip.RemainingBalance += oldDistanceFlownCharge.Amount;
				    SelectedCharterTrip.RemainingBalance += oldWaitingTimeCharge.Amount;
					_ctUow.UpdateCharterTrip(SelectedCharterTrip);

				    LoadFlightLegs();
					LoadChargeList();
					UpdateCredits();
				    FlightLegsViewVisibility = true;
				    AddFlightViewVisibility = false;
			    }
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

		public ICommand AddNewFlightCommand => new RelayCommand(AddNewFlight);
	    public ICommand DeleteFlightCommand => new RelayCommand(DeleteFlight);
	    public ICommand LoadCharterTripsCommand => new RelayCommand(LoadCharterTrips);
	    public ICommand EditFlightCommand => new RelayCommand(EditFlight);

		//END OF FLIGHT LEG FIELDS AND METHODS------------------------------------------------------------------------------------------------------------------------------------

	    private void FillSelectedEmployeeLicenses()
	    {
			SelectedEmployeeLicenses.Clear();

		    var licensures = _employeeUow.GetLicensures(c => c.EmployeeId == SelectedEmployee.EmployeeId);

			var licenses = new ObservableCollection<License>();
		    foreach (var licensure in licensures)
		    {
			    var license = _employeeUow.GetLicense(c => c.LicenseType == licensure.LicenseType);
				SelectedEmployeeLicenses.Add(license);
		    }
	    }

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
			    if (value != null)
			    {
				    FillSelectedEmployeeLicenses();
			    }

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

	    

		public CustomerViewModel(CharterTripUnitOfWork ctUow, AircraftUnitOfWork aircraftUow, EmployeeUnitOfWork employeeUow, PaymentUnitOfWork paymentUow)
	    {
		    _ctUow = ctUow;
		    _aircraftUow = aircraftUow;
		    _employeeUow = employeeUow;
		    _paymentUow = paymentUow;

			LoadCustomers();
		    LoadAircraft();
			LoadEmployees();
			GetCollectionViews();
			PurposeList.Add("Transport Cargo");
			PurposeList.Add("Transport Passengers");
			PurposeList.Add("Transport Passengers and Cargo");
			ModeOfPaymentList.Add("Cash");
			ModeOfPaymentList.Add("Check");
		    CustomerViewVisibility = true;
		    AddCharterTripView2Visibility = false;
	    }

	    public CustomerViewModel()
		    : this(new CharterTripUnitOfWork(), new AircraftUnitOfWork(), new EmployeeUnitOfWork(), new PaymentUnitOfWork())
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
					    employeeToBeAssigned.IsExtraCrew = false;
					    break;
				    }

				    if (employeeToBeAssigned.SelectedLicense.LicenseType != null && !(_neededLicenses.Contains(employeeToBeAssigned.SelectedLicense)))
				    {
					    employeeToBeAssigned.Status = "EXTRA CREW";
					    employeeToBeAssigned.IsExtraCrew = true;
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
						NewCharterTripEmployeesToBeAssigned[
							NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].IsExtraCrew = false;
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
							NewCharterTripEmployeesToBeAssigned[
								NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].IsExtraCrew = true;
						}

						else
						{
							NewCharterTripEmployeesToBeAssigned[
								NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].Status = "";
							NewCharterTripEmployeesToBeAssigned[
								NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].IsExtraCrew = false;
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
						NewCharterTripEmployeesToBeAssigned[
							NewCharterTripEmployeesToBeAssigned.IndexOf(employeeToBeAssigned)].IsExtraCrew = false;
					}
				}

				CrewRequirementListViewItems.Add(_newListRequirement);
		    }
		}

	    public ObservableCollection<int> Pages { get; set; } = new ObservableCollection<int>();
	    public ObservableCollection<int> AircraftPages { get; set; } = new ObservableCollection<int>();
	    public ObservableCollection<int> EmployeePages { get; set; } = new ObservableCollection<int>();

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

			if (AircraftSearchText == "")
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

		    if (EmployeeSearchText == "")
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

	    public ICommand NextPageAircraftCommand => new RelayCommand(NextPageAircraftProc, NextPageAircraftCondition);
	    public ICommand PrevPageAircraftCommand => new RelayCommand(PrevPageAircraftProc);
	    public ICommand NextPageEmployeeCommand => new RelayCommand(NextPageEmployeeProc, NextPageEmployeeCondition);
	    public ICommand PrevPageEmployeeCommand => new RelayCommand(PrevPageEmployeeProc);
		
	    public ICommand AddSelectedEmployeeToEmployeesToBeAssignedCommand =>
		    new RelayCommand(AddSelectedEmployeeToEmployeesToBeAssigned);

	    public ICommand FillCrewRequirementListCommand => new RelayCommand(FillCrewRequirementList);

	    public ICommand RemoveSelectedEmployeeFromEmployeesToBeAssignedCommand =>
		    new RelayCommand(RemoveSelectedEmployeeFromEmployeesToBeAssigned);

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
