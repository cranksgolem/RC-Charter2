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
    /// Interaction logic for AddCharterTripView.xaml
    /// </summary>
    public partial class AddCharterTripView : UserControl
    {
        public AddCharterTripView()
        {
            InitializeComponent();
        }

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Visible;
				(parentWindow as MainWindow).AddCharterTripView.Visibility = Visibility.Collapsed;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).AddCharterTripView.Visibility = Visibility.Collapsed;
				(parentWindow as MainWindow).AddCharterTripView2.Visibility = Visibility.Visible;
			}
		}
	}
}
