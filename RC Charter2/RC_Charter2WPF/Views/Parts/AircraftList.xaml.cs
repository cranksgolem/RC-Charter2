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
    /// Interaction logic for AircraftList.xaml
    /// </summary>
    public partial class AircraftList : UserControl
    {
        public AircraftList()
        {
            InitializeComponent();
        }

		private void TbxAircraftSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			TblSearchLabel.Visibility = Visibility.Collapsed;
		}

		private void TbxAircraftSearch_LostFocus(object sender, RoutedEventArgs e)
		{
			if (TbxAircraftSearch.Text == "")
			{
				TblSearchLabel.Visibility = Visibility.Visible;
			}
		}

		private void LbxAircraft_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				if (LbxAircraft.SelectedItem != null)
				{
					(parentWindow as MainWindow).AddCharterTripView.AircraftDetails.AircraftDetailsGrid.Visibility =
						Visibility.Visible;
				}
				else
				{
					(parentWindow as MainWindow).AddCharterTripView.AircraftDetails.AircraftDetailsGrid.Visibility =
						Visibility.Collapsed;
				}
			}
		}
	}
}
