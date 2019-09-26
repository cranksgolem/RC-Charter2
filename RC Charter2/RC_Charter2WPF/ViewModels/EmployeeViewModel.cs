using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using BespokeFusion;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RC_Charter2.Models;
using RC_Charter2.UnitsOfWork;
using RC_Charter2WPF.Views.Parts;
using License = RC_Charter2.Models.License;

namespace RC_Charter2WPF.ViewModels
{
	public class EmployeeViewModel : ObservableObject
	{
		//VISIBILITY


		public bool EmployeePersonalDetailsVisibility
		{
			get => _employeePersonalDetailsVisibility;
			set
			{
				_employeePersonalDetailsVisibility = value;
				RaisePropertyChanged(nameof(EmployeePersonalDetailsVisibility));
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

		public bool EditButtonVisibility
		{
			get => _editButtonVisibility;
			set
			{
				_editButtonVisibility = value;
				RaisePropertyChanged(nameof(EditButtonVisibility));
			}
		}

		//END OF VISIBILITY

		private Licensure _selectedEmployeeLicensure;
		private const int EmployeePerPageInList = 50;
		public ObservableCollection<Employee> EmployeeList { get; } = new ObservableCollection<Employee>();
		public ObservableCollection<Licensure> SelectedEmployeeLicensures { get; } = new ObservableCollection<Licensure>();
		public ObservableCollection<Result> SelectedEmployeeResults { get; } = new ObservableCollection<Result>();
		public ObservableCollection<int> EmployeePages { get; set; } = new ObservableCollection<int>();
		public ObservableCollection<License> LicensesList { get; } = new ObservableCollection<License>();
		public ObservableCollection<Test> TestsList { get; } = new ObservableCollection<Test>();
		public ObservableCollection<string> PassFail { get; } = new ObservableCollection<string>();
		private EmployeeUnitOfWork _employeeUow;
		private CharterTripUnitOfWork _ctUow;
		private PaymentUnitOfWork _paymentUow;
		private int _selectedEmployeePage;
		private ListCollectionView _employeeListView;
		private string _employeeSearchText;
		private Employee _selectedEmployee;
		private bool _employeePersonalDetailsVisibility;
		private bool _completeButtonVisibility;
		private bool _editButtonVisibility;
		private string _newEmployeeName;
		private string _newEmployeeAddress;
		private License _newLicensureSelectedLicense;
		private DateTime? _newLicensureEarnedDate;
		private Test _newResultTest;
		private DateTime? _newResultTestDate;
		private string _newResultTestResult;
		private DateTime? _newResultExpiration;
		private Result _selectedEmployeeResult;

		public Result SelectedEmployeeResult
		{
			get => _selectedEmployeeResult;
			set
			{
				_selectedEmployeeResult = value;
				RaisePropertyChanged(nameof(SelectedEmployeeResult));
			}
		}

		public DateTime? NewResultExpiration
		{
			get => _newResultExpiration;
			set
			{
				_newResultExpiration = value;
				RaisePropertyChanged(nameof(NewResultExpiration));
			}
		}

		public string NewResultTestResult
		{
			get => _newResultTestResult;
			set
			{
				_newResultTestResult = value;
				RaisePropertyChanged(nameof(NewResultTestResult));
			}
		}

		public DateTime? NewResultTestDate
		{
			get => _newResultTestDate;
			set
			{
				_newResultTestDate = value;
				RaisePropertyChanged(nameof(NewResultTestDate));
			}
		}

		public Test NewResultTest
		{
			get => _newResultTest;
			set
			{
				_newResultTest = value;
				RaisePropertyChanged(nameof(NewResultTest));
			}
		}

		public Licensure SelectedEmployeeLicensure
		{
			get => _selectedEmployeeLicensure;
			set
			{
				_selectedEmployeeLicensure = value;
				RaisePropertyChanged(nameof(SelectedEmployeeLicensure));
			}
		}

		public DateTime? NewLicensureEarnedDate
		{
			get => _newLicensureEarnedDate;
			set
			{
				_newLicensureEarnedDate = value;
				RaisePropertyChanged(nameof(NewLicensureEarnedDate));
			}
		}

		public License NewLicensureSelectedLicense
		{
			get => _newLicensureSelectedLicense;
			set
			{
				_newLicensureSelectedLicense = value;
				RaisePropertyChanged(nameof(NewLicensureSelectedLicense));
			}
		}

		public string NewEmployeeName
		{
			get => _newEmployeeName;
			set
			{
				_newEmployeeName = value;
				RaisePropertyChanged(nameof(NewEmployeeName));
			}
		}

		public string NewEmployeeAddress
		{
			get => _newEmployeeAddress;
			set
			{
				_newEmployeeAddress = value;
				RaisePropertyChanged(nameof(NewEmployeeAddress));
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
					EmployeePersonalDetailsVisibility = true;
					LoadSelectedEmployeeLicensures();
					LoadSelectedEmployeeResults();
				}
				else
				{
					EmployeePersonalDetailsVisibility = false;
				}
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

		public EmployeeViewModel(EmployeeUnitOfWork employeeUow, CharterTripUnitOfWork ctUow, PaymentUnitOfWork paymentUow)
		{
			_employeeUow = employeeUow;
			_ctUow = ctUow;
			_paymentUow = paymentUow;

			LoadEmployees();
			LoadLicensesList();
			LoadTestsList();
			PassFail.Add("Pass");
			PassFail.Add("Fail");
			GetCollectionViews();
		}

		public EmployeeViewModel()
			: this(new EmployeeUnitOfWork(), new CharterTripUnitOfWork(), new PaymentUnitOfWork())
		{

		}


		private void LoadLicensesList()
		{
			var licenses = _employeeUow.GetAllLicenses();

			LicensesList.Clear();

			foreach (var license in licenses)
			{
				LicensesList.Add(license);
			}
		}

		private void LoadTestsList()
		{
			var tests = _employeeUow.GetAllTests();

			TestsList.Clear();

			foreach (var test in tests)
			{
				TestsList.Add(test);
			}
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

		private void LoadEmployees(int page)
		{
			EmployeeList.Clear();
			var employees = _employeeUow.GetEmployeesByPage(EmployeePerPageInList, page);
			foreach (var x in employees)
			{
				EmployeeList.Add(x);
			}
		}

		public void AddNewEmployee()
		{
			if (!(string.IsNullOrEmpty(NewEmployeeName)) && !(string.IsNullOrEmpty(NewEmployeeAddress)))
			{
				var newEmployee = new Employee();
				newEmployee.Name = NewEmployeeName;
				newEmployee.Address = NewEmployeeAddress;

				_employeeUow.AddEmployee(newEmployee);
				LoadEmployees();
				EmployeeSearchText = "";

			}
			else
			{
				MaterialMessageBox.ShowError("Please make sure there are no empty fields!");
			}
		}

		public void EditEmployee()
		{
			if (!(string.IsNullOrEmpty(NewEmployeeName)) && !(string.IsNullOrEmpty(NewEmployeeAddress)))
			{
				SelectedEmployee.Name = NewEmployeeName;
				SelectedEmployee.Address = NewEmployeeAddress;

				_employeeUow.UpdateEmployee(SelectedEmployee);
				LoadEmployees();
				EmployeeSearchText = "";
			}
			else
			{
				MaterialMessageBox.ShowError("Please make sure there are no empty fields!");
			}
		}

		public void DeleteEmployee()
		{
			if (SelectedEmployee != null)
			{
				var msg = new CustomMaterialMessageBox
				{
					TxtMessage = { Text = "Are you sure you want to delete this employee?", Foreground = Brushes.Black },
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
					var licensures = _employeeUow.GetLicensures(c => c.EmployeeId == SelectedEmployee.EmployeeId);
					foreach (var licensure in licensures)
					{
						_employeeUow.DeleteLicensure(licensure);
					}

					var results = _employeeUow.GetResults(c => c.EmployeeId == SelectedEmployee.EmployeeId);
					foreach (var result1 in results)
					{
						_employeeUow.DeleteResult(result1);
					}

					var crewAssignments = _ctUow.GetCrewAssignments(c => c.EmployeeId == SelectedEmployee.EmployeeId);
					var charterTrips = new List<CharterTrip>();

					foreach (var crewAssignment in crewAssignments)
					{
						charterTrips.Add(_ctUow.GetCharterTrip(c => c.CharterTripId == crewAssignment.CharterTripId));
					}

					foreach (var charterTrip in charterTrips)
					{
						DeleteCharterTrip(charterTrip);
					}

					_employeeUow.DeleteEmployee(SelectedEmployee);
					LoadEmployees();
					EmployeeSearchText = "";
				}
			}
		}

		private void DeleteCharterTrip(CharterTrip charterTrip)
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

		private void LoadSelectedEmployeeLicensures()
		{
			if (SelectedEmployee != null)
			{
				var licensures = _employeeUow.GetLicensures(c => c.EmployeeId == SelectedEmployee.EmployeeId);

				SelectedEmployeeLicensures.Clear();

				foreach (var licensure in licensures)
				{
					SelectedEmployeeLicensures.Add(licensure);
				}
			}
		}

		private void LoadSelectedEmployeeResults()
		{
			if (SelectedEmployee != null)
			{
				var results = _employeeUow.GetResults(c => c.EmployeeId == SelectedEmployee.EmployeeId);

				SelectedEmployeeResults.Clear();

				foreach (var result in results)
				{
					SelectedEmployeeResults.Add(result);
				}
			}
		}

		public void AddResult()
		{
			var newResult = new Result();
			newResult.Expiration = NewResultExpiration;
			newResult.TestDate = NewResultTestDate;
			newResult.TestResult = NewResultTestResult;
			_employeeUow.AddResult(newResult,SelectedEmployee,NewResultTest);
			LoadSelectedEmployeeResults();
		}

		public void EditResult()
		{
			SelectedEmployeeResult.Expiration = NewResultExpiration;
			SelectedEmployeeResult.TestDate = NewResultTestDate;
			SelectedEmployeeResult.TestResult = NewResultTestResult;
			SelectedEmployeeResult.Test = NewResultTest;

			_employeeUow.UpdateResult(SelectedEmployeeResult);
			LoadSelectedEmployeeResults();
		}

		public void DeleteResult()
		{
			_employeeUow.DeleteResult(SelectedEmployeeResult);
			LoadSelectedEmployeeResults();
		}

		public void AddLicensure()
		{
			var newLicensure = new Licensure();
			newLicensure.DateEarned = NewLicensureEarnedDate;
			_employeeUow.AddLicensure(newLicensure,SelectedEmployee, NewLicensureSelectedLicense);
			LoadSelectedEmployeeLicensures();
		}

		public void DeleteLicensure()
		{
			if (SelectedEmployeeLicensure != null)
			{
				var msg = new CustomMaterialMessageBox
				{
					TxtMessage = { Text = "Are you sure you want to delete this licensure from this employee?", Foreground = Brushes.Black },
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
					_employeeUow.DeleteLicensure(SelectedEmployeeLicensure);
					LoadSelectedEmployeeLicensures();
				}
			}
		}

		public void EditLicensure()
		{
			SelectedEmployeeLicensure.LicenseType = NewLicensureSelectedLicense.LicenseType;
			SelectedEmployeeLicensure.DateEarned = NewLicensureEarnedDate;

			_employeeUow.UpdateLicensure(SelectedEmployeeLicensure);
			LoadSelectedEmployeeLicensures();
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

		public ICommand NextPageProcCommand => new RelayCommand(NextPageProc, NextPageCondition);
		public ICommand PrevPageProcCommand => new RelayCommand(PrevPageProc);

		private void NextPageProc()
		{
			SelectedEmployeePage++;
		}

		private bool NextPageCondition()
		{
			return SelectedEmployeePage != EmployeePages.Last();
		}

		private void PrevPageProc()
		{
			if (SelectedEmployeePage != 1)
			{
				SelectedEmployeePage--;
			}
		}

		private void GetCollectionViews()
		{
			_employeeListView = (ListCollectionView)CollectionViewSource.GetDefaultView(EmployeeList);
			_employeeListView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
			_employeeListView.GroupDescriptions.Add(new PropertyGroupDescription("Name[0]"));
		}
	}
}
