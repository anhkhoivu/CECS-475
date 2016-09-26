using System;
using System.Windows.Input;

namespace CECS_475___Lab_Assignment_05.ViewModel
{
    class EmployeeViewModel
    { 
        private IPayable[] payableObjects;

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
        }

        public IPayable[] PayableObjects
        {
            get
                { return payableObjects; }
        }

        private ICommand mUpdater;
        private ICommand updateLastName;
        private ICommand restore;

        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
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
                Array.Sort(payableObjects);
            }
        }

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
                new EmployeeViewModel();
            }
        }

        private class Updater : ICommand
        {
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
        }
    }
}
