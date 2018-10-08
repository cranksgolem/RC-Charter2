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
    /// Interaction logic for FlightLegsView.xaml
    /// </summary>
    public partial class FlightLegsView : UserControl
    {
        public FlightLegsView()
        {
            InitializeComponent();
        }

		private void BtnBack_Click(object sender, RoutedEventArgs e)
		{
			Window parentWindow = Application.Current.MainWindow;
			if (parentWindow.GetType() == typeof(MainWindow))
			{
				(parentWindow as MainWindow).CustomerView.Visibility = Visibility.Visible;
				(parentWindow as MainWindow).FlightLegsView.Visibility = Visibility.Collapsed;
			}
		}
	}
}
