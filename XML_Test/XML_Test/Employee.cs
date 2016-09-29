using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Test
{
    public abstract class Employee : IPayable, IComparable
    {
        string tag;
        public delegate bool ComparisonHandler(object first, object second);
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
                return (IComparer)new sortByPay();
            }
        }

        /// <summary>
        /// read-only property that gets employee's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// read-only property that gets employee's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// read-only property that gets employee's social security number
        /// </summary>
        public string SocialSecurityNumber { get; set; }

        public string Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
                if (tag == "SalariedEmployee")
                {
                    
                }
            }
        }
        public Employee()
        { }

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
        /// This method displays the Employee's basic information.
        /// </summary>
        /// <returns>return string representation of Employee object, using properties</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}\nsocial security number: {2}",
               FirstName, LastName, SocialSecurityNumber);
        } // end method ToString

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

        /// <summary>
        /// Abstract method that defines Earning method from IPayable.
        /// </summary>
        /// <returns>returns nothing</returns>
        public abstract decimal Earnings();
    } // end abstract class Employee
}
