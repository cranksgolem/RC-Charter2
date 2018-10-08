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
	/// Interaction logic for CharterTripDetails.xaml
	/// </summary>
	public partial class CharterTripDetails : UserControl
	{
		public CharterTripDetails()
		{
			InitializeComponent();
		}

		private void BtnViewFlightLegs_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				if ((parentWindow as MainWindow).CustomerView.CharterTripList.LbCharterTrips.SelectedItem != null)
				{
					(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Collapsed;
					(parentWindow as MainWindow).FlightLegsView.Visibility = Visibility.Visible;
				}
			}
		}
	}
}
