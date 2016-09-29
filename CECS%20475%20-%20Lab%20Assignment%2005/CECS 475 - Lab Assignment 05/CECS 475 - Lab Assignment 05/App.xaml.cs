using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CECS_475___Lab_Assignment_05.ViewModel;

namespace CECS_475___Lab_Assignment_05
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CECS_475___Lab_Assignment_05.ViewModel.MainWindow window = new CECS_475___Lab_Assignment_05.ViewModel.MainWindow();
            EmployeeViewModel VM = new EmployeeViewModel();
            window.DataContext = VM;
            window.Show();
        }
    }
}
