using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_05
{
    public abstract class Employee : IPayable, IComparable, INotifyPropertyChanged
    {
        public delegate bool ComparisonHandler(object first, object second);

        private string firstName;
        private string lastName;
        private string SSN;

        abstract public decimal GetPaymentAmount();
        /// <summary>
        /// Class that implements the IComparer interface.
        /// </summary>
        public class sortByPay : IComparer
        {
            /// <summary>
            /// This method overrides the Compare method in IComparer to sort by Employee's earnings.
            /// </summary>
            /// <param name="a">Parameter that contains a generic object.</param>
            /// <param name="b">Parameter that contains a generic object.</param>
            /// <returns>returns the integer comparison between the two parameter objects</returns>
            int IComparer.Compare(object a, object b)
            {
                Employee e1 = (Employee) a;
                Employee e2 = (Employee) b;
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
                return (IComparer)new sortByPay();
            }
        }

        /// <summary>
        /// read-only property that gets employee's first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// read-only property that gets employee's last name
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// read-only property that gets employee's social security number
        /// </summary>
        public string SocialSecurityNumber
        {
            get
            {
                return SSN;
            }
            set
            {
                SSN = value;
                OnPropertyChanged("SSN");
            }
        }

        /// <summary>
        /// three-parameter constructor
        /// </summary>
        /// <param name="first">Parameter for the Employee's first name</param>
        /// <param name="last">Parameter for the Employee's last name</param>
        /// <param name="ssn">Parameter for the Employee's social security number.</param>
        public Employee(string first, string last, string ssn)
        {
            FirstName = first;
            LastName = last;
            SocialSecurityNumber = ssn;
        } // end three-parameter Employee constructor

        /// <summary>
        /// This method overrides the CompareTo method of IComparable
        /// </summary>
        /// <param name="obj">Parameter that takes in any object.</param>
        /// <returns>returns the integer comparison of CompareTo.</returns>
        int IComparable.CompareTo(object obj)
        {
            Employee e1 = (Employee)obj;
            if (e1.LastName == this.LastName)
            {
                return String.Compare(e1.FirstName, this.FirstName);
            }
            return String.Compare(e1.LastName, this.LastName);
        }

        /// <summary>
        /// This method implements the ComparisonHandler delegate
        /// </summary>
        /// <param name="first">Parameter that has a Employee object.</param>
        /// <param name="second">Parameter that has a Employee object.</param>
        /// <returns>returns the boolean comparison</returns>
        public static bool SSN_Comparison(Employee first, Employee second)
        {
            int comparison;
            comparison = (first.SocialSecurityNumber.ToString().CompareTo(second.SocialSecurityNumber.ToString()));

            return comparison > 0;
        }
        public static IComparer sortAscending()
        {
            return (IComparer)new sortAscendingHelper();
        }

        /// <summary>
        /// sorting the pay amount
        /// </summary>
        private class sortAscendingHelper : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                Employee c1 = (Employee)x;
                Employee c2 = (Employee)y;
                if (c1.GetPaymentAmount() > c2.GetPaymentAmount())
                    return 1;
                if (c1.GetPaymentAmount() < c2.GetPaymentAmount())
                    return -1;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Abstract method that defines Earning method from IPayable.
        /// </summary>
        /// <returns>returns nothing</returns>
        public abstract decimal Earnings();

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    } // end abstract class Employee
}
