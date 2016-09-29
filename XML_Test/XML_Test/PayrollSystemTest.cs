using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XML_Test
{
    public class PayrollSystemTest
    {
        public delegate bool ComparisonHandler(object first, object second);
        public static void Main(string[] args)
        {
            bool programEnd = false;
            bool readEndOfFile = false;

            // create derived class objects
            IPayable[] payableObjects = new IPayable[8];
            List<Employee> list = new List<Employee>();
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

            payableObjects = list.ToArray();

            // close the XmlReader object
            xmlIn.Close();

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

                else if (input == 5)
                {
                    displayResult(payableObjects);
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
