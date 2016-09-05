using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_02
{
    public abstract class Employee : IPayable, IComparable
    {
        public delegate bool ComparisonHandler(Employee first, Employee second);
        public class sortByPay : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Employee e1 = (Employee) a;
                Employee e2 = (Employee)b;
                if (e1.Earnings() > e2.Earnings())
                    return 1;
                if (e1.Earnings() < e2.Earnings())
                    return -1;
                else
                 return 0;
            }

            public static IComparer sortYearAscending()
            {
                return (IComparer)new sortByPay();
            }
        }

        // read-only property that gets employee's first name
        public string FirstName { get; private set; }

        // read-only property that gets employee's last name
        public string LastName { get; private set; }

        // read-only property that gets employee's social security number
        public string SocialSecurityNumber { get; private set; }

        // three-parameter constructor
        public Employee(string first, string last, string ssn)
        {
            FirstName = first;
            LastName = last;
            SocialSecurityNumber = ssn;
        } // end three-parameter Employee constructor

        // return string representation of Employee object, using properties
        public override string ToString()
        {
            return string.Format("{0} {1}\nsocial security number: {2}",
               FirstName, LastName, SocialSecurityNumber);
        } // end method ToString

        int IComparable.CompareTo(object obj)
        {
            Employee e1 = (Employee)obj;
            return String.Compare(e1.LastName, this.LastName);
        }
        /*
        public static void selectionSort(IPayable[] arr, ComparisonHandler comparison)
        {
            //pos_min is short for position of min
            int pos_min;
            Employee temp;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                pos_min = i;//set pos_min to the current index of array

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (comparison((Employee)arr[j], (Employee) arr[pos_min]))
                    {
                        //pos_min will keep track of the index that min is in, this is needed when a swap happens
                        pos_min = j;
                    }
                }

                //if pos_min no longer equals i than a smaller value must have been found, so a swap must occur
                if (pos_min != i)
                {
                    temp = (Employee)arr[i];
                    arr[i] = arr[pos_min];
                    arr[pos_min] = temp;
                }
            }
        }*/

        public static bool descendingOrder(int first, int second)
        {
            return second > first;
        }

        public static bool AlphabeticalGreaterThan(Employee first, Employee second)
        {
            int comparison;
            comparison = (first.SocialSecurityNumber.ToString().CompareTo(second.SocialSecurityNumber.ToString()));

            return comparison > 0;
        }

        public abstract decimal Earnings();
    } // end abstract class Employee
}
