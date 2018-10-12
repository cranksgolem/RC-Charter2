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

namespace RC_Charter2WPF.Views
{
    /// <summary>
    /// Interaction logic for AddCharterTripView2.xaml
    /// </summary>
    public partial class AddCharterTripView2 : UserControl
    {
        public AddCharterTripView2()
        {
            InitializeComponent();
        }

		private void BtnBack_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).AddCharterTripView.Visibility = Visibility.Visible;
				(parentWindow as MainWindow).AddCharterTripView2.Visibility = Visibility.Collapsed;
			}
		}

		private void BtnComplete_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).AddCharterTripView2.Visibility = Visibility.Collapsed;
				(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Visible;
			}
		}

		private void BtnConfirmEdit_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).AddCharterTripView2.Visibility = Visibility.Collapsed;
				(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Visible;
			}
		}
	}
}
