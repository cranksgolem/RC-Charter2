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
    /// Interaction logic for CharterTripList.xaml
    /// </summary>
    public partial class CharterTripList : UserControl
    {
        public CharterTripList()
        {
            InitializeComponent();
        }

		private void LbCharterTrips_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				if (LbCharterTrips.SelectedItem != null)
				{
					(parentWindow as MainWindow).CustomerView.CharterTripDetails.GridCharterTripDetails.Visibility = Visibility.Visible;
					(parentWindow as MainWindow).CustomerView.CharterTripDetails.GridButtons.Visibility =
						Visibility.Visible;
				}
			
				else
				{
					(parentWindow as MainWindow).CustomerView.CharterTripDetails.GridCharterTripDetails.Visibility = Visibility.Collapsed;
					(parentWindow as MainWindow).CustomerView.CharterTripDetails.GridButtons.Visibility =
						Visibility.Collapsed;
				}
			}
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			//Window parentWindow = Application.Current.MainWindow;
			//if (parentWindow.GetType() == typeof(MainWindow))
			//{
			//	(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Collapsed;
			//	(parentWindow as MainWindow).AddCharterTripView.Visibility = Visibility.Visible;
			//	(parentWindow as MainWindow).AddCharterTripView2.BtnConfirmEdit.Visibility = Visibility.Collapsed;
			//	(parentWindow as MainWindow).AddCharterTripView2.BtnComplete.Visibility = Visibility.Visible;
			//}
		}

		private void BtnEdit_Click(object sender, RoutedEventArgs e)
		{
			//Window parentWindow = Application.Current.MainWindow;
			//if (parentWindow.GetType() == typeof(MainWindow))
			//{
			//	(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Collapsed;
			//	(parentWindow as MainWindow).AddCharterTripView.Visibility = Visibility.Visible;
			//	(parentWindow as MainWindow).AddCharterTripView2.BtnConfirmEdit.Visibility = Visibility.Visible;
			//	(parentWindow as MainWindow).AddCharterTripView2.BtnComplete.Visibility = Visibility.Collapsed;
			//}
		}
	}
}
