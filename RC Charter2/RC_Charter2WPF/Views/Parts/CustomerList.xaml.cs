using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RC_Charter2WPF.Views.Parts
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : UserControl
    {
        public CustomerList()
        {
            InitializeComponent();
        }

		private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).EmployeeView.Visibility = Visibility.Collapsed;
				(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Collapsed;
				(parentWindow as MainWindow).AddCustomerView.Visibility = Visibility.Visible;
				(parentWindow as MainWindow).AddCustomerView.TblAddCustomerHeader.Text = "Add Customer";
				(parentWindow as MainWindow).AddCustomerView.BtnConfirmAddCustomer.Visibility = Visibility.Visible;
				(parentWindow as MainWindow).AddCustomerView.BtnConfirmEditCustomer.Visibility = Visibility.Collapsed;
			}
		}

		private void LbxCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				if (LbxCustomers.SelectedItem != null)
				{
					(parentWindow as MainWindow).CustomerView.CharterTripList.Visibility = Visibility.Visible;
					(parentWindow as MainWindow).CustomerView.CharterTripDetails.Visibility = Visibility.Visible;
				}
				else
				{
					(parentWindow as MainWindow).CustomerView.CharterTripList.Visibility = Visibility.Collapsed;
					(parentWindow as MainWindow).CustomerView.CharterTripDetails.Visibility = Visibility.Collapsed;
				}
			}
		}

		private void BtnEditCustomer_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				if (LbxCustomers.SelectedItem != null)
				{
					(parentWindow as MainWindow).EmployeeView.Visibility = Visibility.Collapsed;
					(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Collapsed;
					(parentWindow as MainWindow).AddCustomerView.Visibility = Visibility.Visible;
					(parentWindow as MainWindow).AddCustomerView.TblAddCustomerHeader.Text = "Edit Customer";
					(parentWindow as MainWindow).AddCustomerView.BtnConfirmAddCustomer.Visibility = Visibility.Collapsed;
					(parentWindow as MainWindow).AddCustomerView.BtnConfirmEditCustomer.Visibility = Visibility.Visible;
				}
			}
		}

		private void TbxCustomerSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			TblSearchLabel.Visibility = Visibility.Collapsed;
		}

		private void TbxCustomerSearch_LostFocus(object sender, RoutedEventArgs e)
		{
			if (TbxCustomerSearch.Text == "")
			{
				TblSearchLabel.Visibility = Visibility.Visible;
			}
		}
	}
}
