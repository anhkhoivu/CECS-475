using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml;
using System.Collections.ObjectModel;

namespace CECS_475___Lab_Assignment_05
{
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
        private ObservableCollection<Employee> list1;
        private List<Employee> list;
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
            sortByPay = new DelegateCommand((p) => sortByPayFxn(p));
            sortBySSN = new DelegateCommand((p) => sortBySSNFxn(p));
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
