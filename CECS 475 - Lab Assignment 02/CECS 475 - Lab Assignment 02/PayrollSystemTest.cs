using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_02
{
    public class PayrollSystemTest
    {
        public delegate bool ComparisonHandler(object first, object second);
        public static void Main(string[] args)
        {
            bool programEnd = false;

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

            while (!programEnd)
            {
                Console.WriteLine("1. Sort by last name");
                Console.WriteLine("2. Sort by pay");
                Console.WriteLine("3. Sort by social security number");
                Console.WriteLine("4. Exit the program");

                Console.Write("Please select an option from the Employee menu: ");
                int input = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine();
                
                if (input == 1)
                {
                    Array.Sort(payableObjects);
                    Console.WriteLine("\nEmployees sorted by last name: ");
                    displayResult(payableObjects);
                }

                else if (input == 2)
                {
                    Array.Sort(payableObjects, Employee.sortByPay.sortPayAscending());
                    Console.WriteLine("\nEmployees sorted by pay: ");
                    displayResult(payableObjects);
                }

                else if (input == 3)
                {
                    selectionSort(payableObjects, SSN_Comparision);
                    Console.WriteLine("\nEmployees sorted by SSN: ");
                    displayResult(payableObjects);
                }

                else if (input == 4)
                {
                    Console.WriteLine("Thank you for using the program! Exiting.....");
                    programEnd = true;
                }

                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option from the menu.");
                }
            }

            Console.Read();
        } // end Main

        /// <summary>
        /// Displays the contents of the IPayable array
        /// </summary>
        /// <param name="payableObjects">Parameter that contains the IPayable array.</param>
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

        /// <summary>
        /// This delegate compares two different SSNs.
        /// </summary>
        /// <param name="first">Parameter for the first Employee object.</param>
        /// <param name="second">Parameter for the second Employee object.</param>
        /// <returns>returns the difference is true or false.</returns>
        public static bool SSN_Comparision(object first, object second)
        {
            int comparison;
            Employee e1 = (Employee)first;
            Employee e2 = (Employee)second;
            comparison = (e1.SocialSecurityNumber.ToString().CompareTo(e2.SocialSecurityNumber.ToString()));

            return comparison > 0;
        }

        /// <summary>
        /// This method performs the selection sort.
        /// </summary>
        /// <param name="arr">Parameter contains the array of IPayable objects.</param>
        /// <param name="comparison">Parameter contains the ComparisonHandler delegate.</param>
        static void selectionSort(object[] arr, ComparisonHandler comparison)
        {
            //pos_min is short for position of min
            int pos_min;
            object temp;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                pos_min = i;//set pos_min to the current index of array

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (comparison(arr[j], arr[pos_min]))
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

    } // end class PayrollSystemTest
}
