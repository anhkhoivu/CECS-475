using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CECS_475___Lab_Assignment_05
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow window = new MainWindow();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            EmployeeViewModel VM = new EmployeeViewModel();
            window.DataContext = VM;
            window.Show();
        }
    }
}
