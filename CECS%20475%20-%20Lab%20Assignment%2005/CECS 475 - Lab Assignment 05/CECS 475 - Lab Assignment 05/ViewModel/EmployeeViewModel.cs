using System;
using System.Windows.Input;

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


        //ssc sort components
        private ICommand sortSSC;
        public ICommand SortSSC
        {
            get
            {
                return sortSSC;
            }
            
        }

        private void SortSSCfxn(object o)
        {

        }
        //end of ssc sort components

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

            sortSSC = new DelegateCommand((p) => SortSSCfxn(p));
            myUpdater = new DelegateCommand((p) => MyUpdaterfxn(p));
            myRestore = new DelegateCommand((p) => MyRestorefxn(p));
        }


        public IPayable[] PayableObjects
        {
            get
                { return payableObjects; }
        }

        
/*

        public ICommand UpdateCommand
        {
            
            get
            {
                //payableObjects[0] = restoreArray[0];
                //payableObjects[1] = restoreArray[1];
                //payableObjects[2] = restoreArray[2];
                Console.WriteLine("1");
                if (mUpdater == null)
                    mUpdater = new Updater();       
                return mUpdater;
            }
            set
            {
                Console.WriteLine("updateSet");
                mUpdater = value;
            }
        }
*/
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
        /*
        public ICommand restoreCommand
        {
            
            get
            {
                if (restore == null)
                    restore = new restoreOriginal();
                return restore;
            }
            set
            {
                Console.WriteLine("restoreSet");
                new EmployeeViewModel();
            }
        }*/
        /*
        private class Updater : ICommand
        {
            #region ICommand Members
            public bool CanExecute(object parameter)
            {
                Console.WriteLine("3");
                return true;
            }
            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                Console.WriteLine("4");
            }
            #endregion
        }*/

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
        /*
        private class restoreOriginal : ICommand
        {
            EmployeeViewModel evm = new EmployeeViewModel();

            public restoreOriginal()
            {
                
               
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
        }*/
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
