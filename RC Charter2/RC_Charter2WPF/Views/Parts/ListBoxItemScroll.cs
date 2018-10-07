using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RC_Charter2WPF.Views.Parts
{
	public class ListBoxItemScroll : ListBox
	{
		public ListBoxItemScroll() : base()
		{
			SelectionChanged += new SelectionChangedEventHandler(ListBoxItemScroll_SelectionChanged);
		}

		void ListBoxItemScroll_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ScrollIntoView(SelectedItem);
		}
	}
}
