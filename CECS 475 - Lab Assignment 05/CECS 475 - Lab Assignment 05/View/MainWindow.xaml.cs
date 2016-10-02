using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;
using CECS_475___Lab_Assignment_05;

namespace CECS_475___Lab_Assignment_05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> view = new ObservableCollection<Employee>();
        EmployeeViewModel evm = new EmployeeViewModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        public ObservableCollection<Employee> View
        {
            get
            {
                return view;
            }
        }

        private void CompleteFilter_Changed(object sender, RoutedEventArgs e)
        {
            // Refresh the view to apply filters.
            dataGrid1.ClearValue(ItemsControl.ItemsSourceProperty);
            dataGrid1.ItemsSource = view;
            CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource).Refresh();
            dataGrid1.ItemsSource = evm.List1;
        }

        private void sortByLastName_Changed(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = evm.List1;
            CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource).Refresh();
        }
        
    }

    // Requires using System.Collections.ObjectModel;
    public class List1 : ObservableCollection<Employee>
    {
        // Creating the Tasks collection in this way enables data binding from XAML.
    }
}
