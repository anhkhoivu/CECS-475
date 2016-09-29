using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CECS_475___Lab_Assignment_05.ViewModel
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

        void Selected(object sender, RoutedEventArgs e)
        {
            DataGrid cmd = (DataGrid)sender;
            string selectedItem = (string)(((System.Data.DataRowView)(cmd.SelectedItem)).Row[0]);
        }
        
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmployeeViewModel evm = new EmployeeViewModel();
            MessageBox.Show("The New command was invoked");
            Array.Sort(evm.PayableObjects);
        }
    }
}
