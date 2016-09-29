using System;
using System.Collections;
using System.Windows.Input;
using CECS_475___Lab_Assignment_05.Command;
using System.ComponentModel;

namespace CECS_475___Lab_Assignment_05.ViewModel
{
    class EmployeeViewModel : INotifyPropertyChanged
    {
        private IPayable[] payableObjects = new IPayable[8];
        private ICommand newGrid;

        public EmployeeViewModel()
        {
            initializeGrid(payableObjects);
            DelegateCommand newGrid = new DelegateCommand((p) => reloadGrid(p));
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

        public void initializeGrid(IPayable[] payableObjects)
        {
            payableObjects[0] = new SalariedEmployee("John", "Smith", "111-11-1111", 700M);
            payableObjects[1] = new SalariedEmployee("Antonio", "Smith", "555-55-5555", 800M);
            payableObjects[2] = new SalariedEmployee("Victor", "Smith", "444-44-4444", 600M);
            payableObjects[3] = new HourlyEmployee("Karen", "Price", "222-22-2222", 16.75M, 40M);
            payableObjects[4] = new HourlyEmployee("Ruben", "Zamora", "666-66-6666", 20.00M, 40M);
            payableObjects[5] = new CommissionEmployee("Sue", "Jones", "333-33-3333", 10000M, .06M);
            payableObjects[6] = new BasePlusCommissionEmployee("Bob", "Lewis", "777-77-7777", 5000M, .04M, 300M);
            payableObjects[7] = new BasePlusCommissionEmployee("Lee", "Duarte", "888-88-888", 5000M, .04M, 300M);
        }

        public void reloadGrid(object obj)
        {
            Array.Clear(payableObjects, 0, payableObjects.Length);
            //initializeGrid(payableObjects);
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
    }
}
