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
    /// Interaction logic for NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

		private void BtnCustomerView_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).NavBar.BtnCustomerView.Background = Brushes.LightGray;
				(parentWindow as MainWindow).NavBar.BtnEmployeeView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnAircraftView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnCharterTripView.Background = Brushes.Transparent;
			}
		}

		private void BtnEmployeeView_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).NavBar.BtnCustomerView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnEmployeeView.Background = Brushes.LightGray;
				(parentWindow as MainWindow).NavBar.BtnAircraftView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnCharterTripView.Background = Brushes.Transparent;
			}
		}

		private void BtnAircraftView_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).NavBar.BtnCustomerView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnEmployeeView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnAircraftView.Background = Brushes.LightGray;
				(parentWindow as MainWindow).NavBar.BtnCharterTripView.Background = Brushes.Transparent;
			}
		}

		private void BtnCharterTripView_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).NavBar.BtnCustomerView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnEmployeeView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnAircraftView.Background = Brushes.Transparent;
				(parentWindow as MainWindow).NavBar.BtnCharterTripView.Background = Brushes.LightGray;
			}
		}
	}
}
