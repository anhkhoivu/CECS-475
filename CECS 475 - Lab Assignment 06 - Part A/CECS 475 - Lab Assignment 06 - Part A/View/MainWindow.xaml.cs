using System.Collections.ObjectModel;
using System.Windows;

namespace CECS_475___Lab_Assignment_06___Part_A
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function that triggers when combobox selection has changed
        /// </summary>
        /// <param name="sender">default parameter that takes in the changed object</param>
        /// <param name="e">parameter that was triggered by selection change.</param>
        private void selection_Changed(object sender, RoutedEventArgs e)
        {
            if (cmb.SelectedItem == i1)
            {
                EmployeeViewModel.SelectedSorting = SortingOrder.Ascending;
            }
            else if (cmb.SelectedItem == i2)
            {
                EmployeeViewModel.SelectedSorting = SortingOrder.Descending;
            }
        }
    }

    public class EmployeeRoster : ObservableCollection<Employee>
    {
        // Creating the Tasks collection in this way enables data binding from XAML.
    }
}

