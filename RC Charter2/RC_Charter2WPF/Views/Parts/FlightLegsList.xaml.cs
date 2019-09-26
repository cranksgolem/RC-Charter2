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
    /// Interaction logic for FlightLegsList.xaml
    /// </summary>
    public partial class FlightLegsList : UserControl
    {
        public FlightLegsList()
        {
            InitializeComponent();
        }

		private void LbFlightLegs_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				if (LbFlightLegs.SelectedItem != null)
				{
					(parentWindow as MainWindow).FlightLegsView.GridFlightLegDetails.GridFlightLegDetails.Visibility = Visibility.Visible;
				}
				else
				{
					(parentWindow as MainWindow).FlightLegsView.GridFlightLegDetails.GridFlightLegDetails.Visibility = Visibility.Collapsed;
				}
			}
		}

	}
}
