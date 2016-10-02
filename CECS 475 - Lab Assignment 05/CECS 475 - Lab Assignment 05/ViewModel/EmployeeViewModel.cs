using System;
using System.Collections;
using System.Windows.Input;
using CECS_475___Lab_Assignment_05.Command;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CECS_475___Lab_Assignment_05
{
    class EmployeeViewModel
    {
        private IPayable[] payableObjects;
        private ObservableCollection<Employee> list1;
        private List<Employee> list;
        
        //restore components
        private ICommand myRestore;
        private ICommand sortByLastName;
        public ICommand MyRestore
        {
            get
            {
                return myRestore;
            }
        }

        private void MyRestorefxn(object o)
        {
            list1.Clear();
            foreach (Employee e in payableObjects)
            {
                list1.Add(e);
            }
            
        }
        //end of restore components

        public ICommand SortByLastName
        {
            get
            {
                return sortByLastName;
            }
        }

        private void sortByLastNameFxn(object o)
        {
            Array.Sort(payableObjects);
            ReloadListCollection(payableObjects);
        }
        //end of ssc sort components

        public EmployeeViewModel()
        {
            payableObjects = new IPayable[8];
            list = new List<Employee>();
            list1 = new ObservableCollection<Employee>();
            loadXMLFile(list);
            payableObjects = list.ToArray();
            foreach (Employee e in payableObjects)
            {
                list1.Add(e);
            }

            sortByLastName = new DelegateCommand((p) => sortByLastNameFxn(p));
            myRestore = new DelegateCommand((p) => MyRestorefxn(p));
        }


        public IPayable[] PayableObjects
        {
            get
            { return payableObjects; }
        }

        public ObservableCollection<Employee> List1
        {
            get
            { return list1; }
        }

        private void ReloadListCollection(IPayable[] payableObjects)
        {
            list1.Clear();
            foreach (Employee e in payableObjects)
            {
                list1.Add(e);
            }
        }

        public void loadXMLFile(List<Employee> list)
        {

            bool readEndOfFile = false;

            string path = @"C:\Users\Anhkhoi\Source\Repos\CECS-475\XML_Test\XML_Test\employee_xml.xml";

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            // create the XmlReader object
            XmlReader xmlIn = XmlReader.Create(path, settings);

            while (!readEndOfFile)
            {

                // read past all nodes to the first Product node
                if (xmlIn.ReadToDescendant("Employee"))
                {
                    // create one Product object for each Product node
                    do
                    {
                        SalariedEmployee sEmployee = new SalariedEmployee();
                        sEmployee.Tag = xmlIn["Tag"];
                        if (sEmployee.Tag == "SalariedEmployee")
                        {
                            xmlIn.ReadStartElement("Employee");
                            sEmployee.FirstName = xmlIn.ReadElementContentAsString();
                            sEmployee.LastName = xmlIn.ReadElementContentAsString();
                            sEmployee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            sEmployee.GetPaymentAmount = xmlIn.ReadElementContentAsDecimal();
                            list.Add(sEmployee);
                        }
                        else if (sEmployee.Tag == "HourlyEmployee")
                        {
                            HourlyEmployee hEmployee = new HourlyEmployee();

                            xmlIn.ReadStartElement("Employee");
                            hEmployee.FirstName = xmlIn.ReadElementContentAsString();
                            hEmployee.LastName = xmlIn.ReadElementContentAsString();
                            hEmployee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            hEmployee.Wage = xmlIn.ReadElementContentAsDecimal();
                            hEmployee.Hours = xmlIn.ReadElementContentAsDecimal();
                            list.Add(hEmployee);
                        }
                        else if (sEmployee.Tag == "CommissionEmployee")
                        {
                            CommissionEmployee employee = new CommissionEmployee();
                            xmlIn.ReadStartElement("Employee");
                            employee.FirstName = xmlIn.ReadElementContentAsString();
                            employee.LastName = xmlIn.ReadElementContentAsString();
                            employee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            employee.GrossSales = xmlIn.ReadElementContentAsDecimal();
                            employee.CommissionRate = xmlIn.ReadElementContentAsDecimal();
                            list.Add(employee);
                        }
                        else if (sEmployee.Tag == "BasePlusCommissionEmployee")
                        {
                            BasePlusCommissionEmployee employee = new BasePlusCommissionEmployee();
                            xmlIn.ReadStartElement("Employee");
                            employee.FirstName = xmlIn.ReadElementContentAsString();
                            employee.LastName = xmlIn.ReadElementContentAsString();
                            employee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            employee.GrossSales = xmlIn.ReadElementContentAsDecimal();
                            employee.CommissionRate = xmlIn.ReadElementContentAsDecimal();
                            employee.BaseSalary = xmlIn.ReadElementContentAsDecimal();
                            list.Add(employee);
                        }
                    }
                    while (xmlIn.ReadToNextSibling("Employee"));
                }

                readEndOfFile = true;
            }

            // close the XmlReader object
            xmlIn.Close();
        }
    }

    public class DelegateCommand : ICommand
    {
        private Action<object> _executeMethod;
        private Predicate<object> _canExecute;

        public DelegateCommand(Action<object> _executeMethod)
        : this(_executeMethod, null)
        {

        }
        public DelegateCommand(Action<object> executeMethod, Predicate<object> canExecute)
        {
            _executeMethod = executeMethod;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        protected virtual void OnCanExecuteChanged()
        {
            // add this line
            CommandManager.InvalidateRequerySuggested();
        }
    }
    /*
    class EmployeeViewModel : INotifyPropertyChanged
    {
        private IPayable[] payableObjects = new IPayable[8];
        private IPayable[] restoreArray = new IPayable[8];
        List<Employee> list = new List<Employee>();
        private ICommand newGrid;
        private ICommand restore;

        public EmployeeViewModel()
        {
            payableObjects = initializeGrid(payableObjects);
            //sortSSC = new DelegateCommand((p) => SortSSCfxn(p));
            //myUpdater = new DelegateCommand((p) => MyUpdaterfxn(p));
            myRestore = new DelegateCommand((p) => MyRestorefxn(p));
        }

        public IPayable[] PayableObjects
        {
            get
                { return payableObjects; }
        }

        public ICommand NewGrid
        {
            get { return newGrid; }
        }

        public IPayable[] initializeGrid(IPayable[] payableObjects)
        {
            return loadXMLFile(payableObjects, list);
        }


        //restore components
        private ICommand myRestore;
        public ICommand MyRestore
        {
            get
            {
                return myRestore;
            }

        }

        private void MyRestorefxn(object o)
        {
            list.Clear();
            initializeGrid(payableObjects);
          
            payableObjects[0] = restoreArray[0];
            payableObjects[1] = restoreArray[1];
            payableObjects[2] = restoreArray[2];
            payableObjects[3] = restoreArray[3];
            payableObjects[4] = restoreArray[4];
            payableObjects[5] = restoreArray[5];
            payableObjects[6] = restoreArray[6];
            payableObjects[7] = restoreArray[7];
        }
        //end of restore components

        public IPayable[] loadXMLFile(IPayable[] payableObjects, List<Employee> list)
        {

            bool readEndOfFile = false;
      
            string path = @"C:\Users\Anhkhoi\Source\Repos\CECS-475\XML_Test\XML_Test\employee_xml.xml";

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            // create the XmlReader object
            XmlReader xmlIn = XmlReader.Create(path, settings);

            while (!readEndOfFile)
            {

                // read past all nodes to the first Product node
                if (xmlIn.ReadToDescendant("Employee"))
                {
                    // create one Product object for each Product node
                    do
                    {
                        SalariedEmployee sEmployee = new SalariedEmployee();
                        sEmployee.Tag = xmlIn["Tag"];
                        if (sEmployee.Tag == "SalariedEmployee")
                        {
                            xmlIn.ReadStartElement("Employee");
                            sEmployee.FirstName = xmlIn.ReadElementContentAsString();
                            sEmployee.LastName = xmlIn.ReadElementContentAsString();
                            sEmployee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            sEmployee.GetPaymentAmount = xmlIn.ReadElementContentAsDecimal();
                            list.Add(sEmployee);
                        }
                        else if (sEmployee.Tag == "HourlyEmployee")
                        {
                            HourlyEmployee hEmployee = new HourlyEmployee();

                            xmlIn.ReadStartElement("Employee");
                            hEmployee.FirstName = xmlIn.ReadElementContentAsString();
                            hEmployee.LastName = xmlIn.ReadElementContentAsString();
                            hEmployee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            hEmployee.Wage = xmlIn.ReadElementContentAsDecimal();
                            hEmployee.Hours = xmlIn.ReadElementContentAsDecimal();
                            list.Add(hEmployee);
                        }
                        else if (sEmployee.Tag == "CommissionEmployee")
                        {
                            CommissionEmployee employee = new CommissionEmployee();
                            xmlIn.ReadStartElement("Employee");
                            employee.FirstName = xmlIn.ReadElementContentAsString();
                            employee.LastName = xmlIn.ReadElementContentAsString();
                            employee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            employee.GrossSales = xmlIn.ReadElementContentAsDecimal();
                            employee.CommissionRate = xmlIn.ReadElementContentAsDecimal();
                            list.Add(employee);
                        }
                        else if (sEmployee.Tag == "BasePlusCommissionEmployee")
                        {
                            BasePlusCommissionEmployee employee = new BasePlusCommissionEmployee();
                            xmlIn.ReadStartElement("Employee");
                            employee.FirstName = xmlIn.ReadElementContentAsString();
                            employee.LastName = xmlIn.ReadElementContentAsString();
                            employee.SocialSecurityNumber = xmlIn.ReadElementContentAsString();
                            employee.GrossSales = xmlIn.ReadElementContentAsDecimal();
                            employee.CommissionRate = xmlIn.ReadElementContentAsDecimal();
                            employee.BaseSalary = xmlIn.ReadElementContentAsDecimal();
                            list.Add(employee);
                        }
                    }
                    while (xmlIn.ReadToNextSibling("Employee"));
                }

                readEndOfFile = true;
            }

            // close the XmlReader object
            xmlIn.Close();

            return payableObjects = list.ToArray();
        }
       
        /// <summary>
        /// Class that implements the IComparer interface.
        /// </summary>
        public class PaymentAmountComparer : IComparer
        {
            private static bool order;

            public PaymentAmountComparer(bool comparison)
            {
                order = comparison;
            }
            /// <summary>
            /// This method overrides the Compare method in IComparer to sort by Employee's earnings.
            /// </summary>
            /// <param name="a">Parameter that contains a generic object.</param>
            /// <param name="b">Parameter that contains a generic object.</param>
            /// <returns>returns the integer comparison between the two parameter objects</returns>
            int IComparer.Compare(object a, object b)
            {
                Employee e1 = (Employee)a;
                Employee e2 = (Employee)b;
                if (e1.Earnings() > e2.Earnings())
                    return 1;
                if (e1.Earnings() < e2.Earnings())
                    return -1;
                else
                    return 0;
            }

            /// <summary>
            /// This method allows the program to call upon the Compare method in IComparer
            /// </summary>
            /// <returns>returns the initialization of the class.</returns>
            public static IComparer sortPayAscending()
            {
                return (IComparer)new PaymentAmountComparer(order);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }*/
}
