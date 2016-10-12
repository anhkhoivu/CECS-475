using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml;
using System.Collections.ObjectModel;

namespace CECS_475___Lab_Assignment_06___Part_A
{
    /// <summary>
    /// Enum for sorting order of employees.
    /// </summary>
    public enum SortingOrder
    {
        [Description("Ascending")]
        Ascending = 1,
        [Description("Descending")]
        Descending = 2
    }

    class EmployeeViewModel
    {
        private IPayable[] payableObjects;
        private ObservableCollection<Employee> employeeRoster;
        private List<Employee> originalList;
        public delegate bool ComparisonHandler(object first, object second, bool comparison);
    
        //restore components
        private ICommand myRestore;
        private ICommand sortByLastName;
        private ICommand sortByPay;
        private ICommand sortBySSN;

        public ICommand MyRestore
        {
            get
            {
                return myRestore;
            }
        }

        private void MyRestorefxn(object o)
        {
            employeeRoster.Clear();
            foreach (Employee e in originalList)
            {
                employeeRoster.Add(e);
            }
            
        }
        //end of restore components
        /// <summary>
        /// read-only modifier for SortByLastName
        /// </summary>
        public ICommand SortByLastName
        {
            get
            {
                return sortByLastName;
            }
        }

        /// <summary>
        /// Function that sorts the employees by last name.
        /// </summary>
        /// <param name="o">parameter that triggers when sorting by last name button is pressed.</param>
        private void sortByLastNameFxn(object o)
        {
            if (selectedSorting == SortingOrder.Ascending)
            {
                Array.Sort(payableObjects);
                ReloadListCollection(payableObjects);
            }
            else if (selectedSorting == SortingOrder.Descending)
            {
                Array.Sort(payableObjects);
                ReloadDescendingListCollection(payableObjects);
            }
        }
        //end of ssc sort components

        public ICommand SortByPay
        {
            get
            {
                return sortByPay;
            }
        }

        private void sortByPayFxn(object o)
        {
            if (selectedSorting == SortingOrder.Ascending)
            {
                Array.Sort(payableObjects, Employee.sortByPayAscending.sortPayAscending());
                ReloadListCollection(payableObjects);
            }
            else if (selectedSorting == SortingOrder.Descending)
            {
                Array.Sort(payableObjects, Employee.sortByPayDescending.sortPayDescending());
                ReloadListCollection(payableObjects);
            }
        }

        public ICommand SortBySSN
        {
            get
            {
                return sortBySSN;
            }
        }

        private void sortBySSNFxn(object o)
        {
            bool isAscending;
            if (selectedSorting == SortingOrder.Ascending)
            {
                isAscending = true;
                selectionSort(payableObjects, SSN_Comparision, isAscending);
                ReloadListCollection(payableObjects);
            }
            else if (selectedSorting == SortingOrder.Descending)
            {
                isAscending = false;
                selectionSort(payableObjects, SSN_Comparision, isAscending);
                ReloadListCollection(payableObjects);
            }
        }

        public static bool SSN_Comparision(object first, object second, bool isAscending)
        {
            int comparison;
            Employee e1 = (Employee)first;
            Employee e2 = (Employee)second;
            if (isAscending)
            {
                comparison = (e2.SocialSecurityNumber.ToString().CompareTo(e1.SocialSecurityNumber.ToString()));
                return comparison > 0;
            }
            else if (!isAscending)
            {
                comparison = (e1.SocialSecurityNumber.ToString().CompareTo(e2.SocialSecurityNumber.ToString()));
                return comparison > 0;
            }
            else
            {
                return true;
            }
        }

        static void selectionSort(object[] arr, ComparisonHandler comparison, bool isAscending)
        {
            //pos_min is short for position of min
            int pos_min;
            object temp;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                pos_min = i;//set pos_min to the current index of array

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (comparison(arr[j], arr[pos_min], isAscending))
                    {
                        //pos_min will keep track of the index that min is in, this is needed when a swap happens
                        pos_min = j;
                    }
                }

                //if pos_min no longer equals i than a smaller value must have been found, so a swap must occur
                if (pos_min != i)
                {
                    temp = arr[i];
                    arr[i] = arr[pos_min];
                    arr[pos_min] = temp;
                }
            }
        }

        private static SortingOrder selectedSorting = SortingOrder.Ascending; // Default is set to 'Ascending'
        public static SortingOrder SelectedSorting
        {
            get 
            {
                return selectedSorting;
            }
            set
            {
                selectedSorting = value;
            }
        }

        public delegate int SortSSNDelegate(object obj1, object obj2, bool isAscending);
        public SortSSNDelegate sortDelegate = null;

        /// <summary>
        /// Default constructor for EmployeeViewModel
        /// </summary>
        public EmployeeViewModel()
        {
            payableObjects = new IPayable[8];
            originalList = new List<Employee>();
            employeeRoster = new ObservableCollection<Employee>();
            loadXMLFile(originalList);
            payableObjects = originalList.ToArray();
            loadToCollection(payableObjects);

            sortByLastName = new DelegateCommand((p) => sortByLastNameFxn(p));
            sortByPay = new DelegateCommand((p) => sortByPayFxn(p));
            sortBySSN = new DelegateCommand((p) => sortBySSNFxn(p));
            myRestore = new DelegateCommand((p) => MyRestorefxn(p));
        }

        public void loadToCollection(IPayable [] payableObjects)
        {
            foreach (Employee e in payableObjects)
            {
                employeeRoster.Add(e);
            }

        }

        public IPayable[] PayableObjects
        {
            get
            { return payableObjects; }
        }

        public ObservableCollection<Employee> EmployeeRoster
        {
            get
            { return employeeRoster; }
        }

        private void ReloadListCollection(IPayable[] payableObjects)
        {
            employeeRoster.Clear();
            foreach (Employee e in payableObjects)
            {
                employeeRoster.Add(e);
            }
        }

        private void ReloadDescendingListCollection(IPayable[] payableObjects)
        {
            employeeRoster.Clear();
            for (int i = 7; i >= 0; i--)
            {
                employeeRoster.Add((Employee)payableObjects[i]);
            }
        }

        public void loadXMLFile(List<Employee> list)
        {

            bool readEndOfFile = false;

            string path = @"C:\Users\Anhkhoi\Source\Repos\CECS-475\CECS 475 - Lab Assignment 05\CECS 475 - Lab Assignment 05\employee_xml.xml";

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
}
