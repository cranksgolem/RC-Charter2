using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RC_Charter2.Models;
using RC_Charter2.UnitsOfWork;

namespace RC_Charter2WPF.ViewModels
{
    public class CustomerViewModel : ObservableObject
    {
	    private string _customerSearchText;
		private int _selectedPage;
	    private const int CustomerPerPageInList = 50;
	    private CharterTripUnitOfWork _ctUow;
	    private Customer _selectedCustomer;
	    private ListCollectionView _customerListView;
	    public ObservableCollection<Customer> CustomerList { get; } = new ObservableCollection<Customer>();
		public ObservableCollection<CharterTrip> CharterTripList { get; } = new ObservableCollection<CharterTrip>();
		public ObservableCollection<Flight> FlightList { get; } = new ObservableCollection<Flight>();
	    private ListCollectionView _charterTripListView;
	    private CharterTrip _selectedCharterTrip;
	    private Flight _selectedFlight;
	    private string _selectedCustomerNameHeader;

	    public CustomerViewModel(CharterTripUnitOfWork ctUow)
	    {
		    _ctUow = ctUow;

			LoadCustomers();
			GetCollectionViews();
	    }

	    public CustomerViewModel()
		    : this(new CharterTripUnitOfWork())
	    {

	    }

	    private void GetCollectionViews()
	    {
		    _customerListView = (ListCollectionView) CollectionViewSource.GetDefaultView(CustomerList);
			_customerListView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
			_customerListView.GroupDescriptions.Add(new PropertyGroupDescription("Name[0]"));
	    }

	    private void LoadCustomers()
	    {
		    int numberOfPages = _ctUow.GetCustomerCount() / CustomerPerPageInList;
		    for (int i = 0; i < numberOfPages; i++)
		    {
			    Pages.Add(i + 1);
		    }

		    // load the customer list initially on page 1
		    LoadCustomers(1);
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

	    public ICommand NextPageCommand => new RelayCommand(NextPageProc, NextPageCondition);
	    public ICommand PrevPageCommand => new RelayCommand(PrevPageProc, PrevPageCondition);
		public ICommand AddCustomerCommand => new RelayCommand(AddNewCustomer, true);
		public ICommand ViewFlightLegsCommand => new RelayCommand(LoadFlightLegs, true);

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
	    }

	    private void NextPageProc()
	    {
		    SelectedPage++;
	    }

	    private bool NextPageCondition()
	    {
		    return SelectedPage != Pages.Last();
	    }

	    private bool PrevPageCondition()
	    {
		    return SelectedPage != Pages.First();
	    }

	    private void PrevPageProc()
	    {
		    SelectedPage--;
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

		public string NewCustomerName { get; set; }
	    public string NewCustomerAddress { get; set; }
	    public decimal? NewCustomerAvailableCredits { get; set; }
	}
}
