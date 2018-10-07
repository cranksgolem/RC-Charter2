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
	    private ListCollectionView _charterTripListView;
	    private CharterTrip _selectedCharterTrip;

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

	    private async Task LoadCharterTrips()
	    {
			CharterTripList.Clear();
		    var charterTrips =(await _ctUow.GetCharterTrips(c => c.CustomerId == SelectedCustomer.CustomerId)).ToList();
		    foreach (var charterTrip in charterTrips)
		    {
			    CharterTripList.Add(charterTrip);
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

		public Customer SelectedCustomer
	    {
		    get => _selectedCustomer;
		    set
		    {
			    _selectedCustomer = value;
				RaisePropertyChanged(nameof(SelectedCustomer));
		    }
	    }

	    public CharterTrip SelectedCharterTrip
	    {
		    get => _selectedCharterTrip;
		    set
		    {
			    _selectedCharterTrip = value;
				RaisePropertyChanged(nameof(SelectedCharterTrip));
			    LoadCharterTrips();
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
	}
}
