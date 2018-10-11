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
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : UserControl
    {
        public EmployeeList()
        {
            InitializeComponent();
        }

		private void TbxEmployeeSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			TblSearchLabel.Visibility = Visibility.Collapsed;
		}

		private void TbxEmployeeSearch_LostFocus(object sender, RoutedEventArgs e)
		{
			if (TbxEmployeeSearch.Text == "")
			{
				TblSearchLabel.Visibility = Visibility.Visible;
			}
		}
	}
}
