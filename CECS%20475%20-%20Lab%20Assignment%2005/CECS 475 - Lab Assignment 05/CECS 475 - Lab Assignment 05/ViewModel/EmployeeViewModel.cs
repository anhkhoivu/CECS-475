using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_05.ViewModel
{
    class EmployeeViewModel
    {
        private IPayable[] payableObjects;
        private IPayable[] restoreArray;



        private ICommand updateLastName;
        private ICommand restore;

        //update components
        private ICommand myUpdater;
        public ICommand MyUpdater
        {
            get
            {
                return myUpdater;
            }
            set
            {
                myUpdater = value;
            }

        }



        private void MyUpdaterfxn(object o)
        {

        }
        //end of update components

        //last name components, option 1
        private ICommand myLname;
        public ICommand MyLname
        {
            get
            {
                return myLname;
            }
            set
            {
                myUpdater = value;
            }

        }

        private void MyLnamefxn(object o)
        {
             Array.Sort(payableObjects);
            
        }
        //end of last name option 1 components

        //[ay amount sort components, option 2
        private ICommand myPay;
        public ICommand MyPay
        {
            get
            {
                return myPay;
            }

        }

        private void MyPayfxn(object o)
        {
            Array.Sort(payableObjects, Employee.sortAscending());
 
        }
        //end of ssc sort option 2 components


        //ssc sort components, option 3
        private ICommand mySSC;
        public ICommand MySSC
        {
            get
            {
                return mySSC;
            }

        }

        private void MySSCfxn(object o)
        {
            selectSort(payableObjects, AlphabeticalGreaterThan);
            
        }
        //end of ssc sort option 3 components




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




        public EmployeeViewModel()
        {
            payableObjects = new IPayable[8];
            payableObjects[0] = new SalariedEmployee("John", "Smith", "111-11-1111", 700M);
            payableObjects[1] = new SalariedEmployee("Antonio", "Smith", "555-55-5555", 800M);
            payableObjects[2] = new SalariedEmployee("Victor", "Smith", "444-44-4444", 600M);
            payableObjects[3] = new HourlyEmployee("Karen", "Price", "222-22-2222", 16.75M, 40M);
            payableObjects[4] = new HourlyEmployee("Ruben", "Zamora", "666-66-6666", 20.00M, 40M);
            payableObjects[5] = new CommissionEmployee("Sue", "Jones", "333-33-3333", 10000M, .06M);
            payableObjects[6] = new BasePlusCommissionEmployee("Bob", "Lewis", "777-77-7777", 5000M, .04M, 300M);
            payableObjects[7] = new BasePlusCommissionEmployee("Lee", "Duarte", "888-88-888", 5000M, .04M, 300M);

            restoreArray = new IPayable[8];
            restoreArray[0] = new SalariedEmployee("John", "Smith", "111-11-1111", 700M);
            restoreArray[1] = new SalariedEmployee("Antonio", "Smith", "555-55-5555", 800M);
            restoreArray[2] = new SalariedEmployee("Victor", "Smith", "444-44-4444", 600M);
            restoreArray[3] = new HourlyEmployee("Karen", "Price", "222-22-2222", 16.75M, 40M);
            restoreArray[4] = new HourlyEmployee("Ruben", "Zamora", "666-66-6666", 20.00M, 40M);
            restoreArray[5] = new CommissionEmployee("Sue", "Jones", "333-33-3333", 10000M, .06M);
            restoreArray[6] = new BasePlusCommissionEmployee("Bob", "Lewis", "777-77-7777", 5000M, .04M, 300M);
            restoreArray[7] = new BasePlusCommissionEmployee("Lee", "Duarte", "888-88-888", 5000M, .04M, 300M);

            myLname = new DelegateCommand((p) => MyLnamefxn(p));//option 1
            myPay = new DelegateCommand((p) => MyPayfxn(p));//option 2
            mySSC = new DelegateCommand((p) => MySSCfxn(p));//option 3
            myUpdater = new DelegateCommand((p) => MyUpdaterfxn(p));
            myRestore = new DelegateCommand((p) => MyRestorefxn(p));//restore
        }


        public IPayable[] PayableObjects
        {
            get
            { return payableObjects; }
        }


        public ICommand SortByLastName
        {
            get
            {
                if (updateLastName == null)
                    updateLastName = new sortByLastName(new EmployeeViewModel());
                return updateLastName;
            }
            set
            {
                Console.WriteLine("sortSet");
                Array.Sort(payableObjects);
            }
        }


        private class sortByLastName : ICommand
        {
            EmployeeViewModel evm;

            public sortByLastName(EmployeeViewModel newEVM)
            {
                evm = newEVM;
            }

            #region ICommand Members
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }
            #endregion
        }

        /// <summary>
        /// create delagate for ssc sort
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public delegate bool ComparisonHandler(object first, object second);

        /// <summary>
        /// comparing social security strings
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool AlphabeticalGreaterThan(object first, object second)
        {
            int comparison;
            Employee one = (Employee)first;
            Employee two = (Employee)second;
            comparison = (one.SocialSecurityNumber.CompareTo(two.SocialSecurityNumber));

            return comparison > 0;
        }

        /// <summary>
        /// selection sort used to sort ssc 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="comparisonMethod"></param>
        static void selectSort(object[] arr, ComparisonHandler comparisonMethod)
        {
            //pos_min is short for position of min
            int pos_min;
            object temp;
            object temp1;
            object temp2;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                pos_min = i;//set pos_min to the current index of array
                for (int j = i + 1; j < arr.Length; j++)
                {
                    temp1 = arr[j];
                    temp2 = arr[pos_min];
                    if (AlphabeticalGreaterThan(temp1, temp2))
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
    public class PayrollSystemTest
    {
    }
}
