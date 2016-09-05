using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_02
{
    public class PayrollSystemTest
    {
        public delegate bool ComparisonHandler(Employee first, Employee second);
        public static void Main(string[] args)
        {
            // create derived class objects
            IPayable[] payableObjects = new IPayable[8];
            payableObjects[0] = new SalariedEmployee("John", "Smith", "111-11-1111", 700M);
            payableObjects[1] = new SalariedEmployee("Antonio", "Smith", "555-55-5555", 800M);
            payableObjects[2] = new SalariedEmployee("Victor", "Smith", "444-44-4444", 600M);
            payableObjects[3] = new HourlyEmployee("Karen", "Price", "222-22-2222", 16.75M, 40M);
            payableObjects[4] = new HourlyEmployee("Ruben", "Zamora", "666-66-6666", 20.00M, 40M);
            payableObjects[5] = new CommissionEmployee("Sue", "Jones", "333-33-3333", 10000M, .06M);
            payableObjects[6] = new BasePlusCommissionEmployee("Bob", "Lewis", "777-77-7777", 5000M, .04M, 300M);
            payableObjects[7] = new BasePlusCommissionEmployee("Lee", "Duarte", "888-88-888", 5000M, .04M, 300M);

            Console.WriteLine("Employees processed polymorphically:\n");
            displayResult(payableObjects);
           
            Array.Sort(payableObjects);
            Console.WriteLine("\nEmployees sorted by last name: ");
            displayResult(payableObjects);

            Array.Sort(payableObjects, Employee.sortByPay.sortYearAscending());
            Console.WriteLine("\nEmployees sorted by pay: ");
            displayResult(payableObjects);

            selectionSort(payableObjects, AlphabeticalGreaterThan);
            Console.WriteLine("\nEmployees sorted by SSN: ");
            displayResult(payableObjects);

            Console.Read();
        } // end Main

        static void displayResult(IPayable[] payableObjects)
        {
            foreach (IPayable currentEmployee in payableObjects)
            {
                Console.WriteLine(currentEmployee); // invokes ToString

                // determine whether element is a BasePlusCommissionEmployee
                if (currentEmployee is BasePlusCommissionEmployee)
                {
                    // downcast Employee reference to 
                    // BasePlusCommissionEmployee reference
                    BasePlusCommissionEmployee employee =
                       (BasePlusCommissionEmployee)currentEmployee;

                    employee.BaseSalary *= 1.10M;
                    Console.WriteLine(
                       "new base salary with 10% increase is: {0:C}",
                       employee.BaseSalary);
                } // end if

                Console.WriteLine(
                   "earned {0:C}\n", currentEmployee.Earnings());
            } // end foreach
        }

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

        static void selectionSort(IPayable[] arr, ComparisonHandler comparison)
        {
            //pos_min is short for position of min
            int pos_min;
            Employee temp;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                pos_min = i;//set pos_min to the current index of array

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (comparison((Employee)arr[j], (Employee)arr[pos_min]))
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
        }

    } // end class PayrollSystemTest
}
