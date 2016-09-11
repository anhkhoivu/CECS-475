using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_02
{
    class SalariedEmployee : Employee
    {
        private decimal paymentAmount;

        /// <summary>
        /// four-parameter constructor 
        /// </summary>
        /// <param name="first">Parameter that takes the first name of an employee.</param>
        /// <param name="last">Parameter that takes the last name of an employee</param>
        /// <param name="ssn">Parameter that takes the SNN of an Employee.</param>
        /// <param name="salary">Parameter that takes in the salary of an Employee.</param>
        public SalariedEmployee(string first, string last, string ssn,
           decimal salary) : base(first, last, ssn)
        {
            GetPaymentAmount = salary; // validate salary via property
        } // end four-parameter SalariedEmployee constructor

        /// <summary>
        /// property that gets and sets salaried employee's salary
        /// </summary>
        public decimal GetPaymentAmount
        {
            get
            {
                return paymentAmount;
            } // end get
            set
            {
                if (value >= 0) // validation
                    paymentAmount = value;
                else
                    throw new ArgumentOutOfRangeException("WeeklySalary",
                       value, "WeeklySalary must be >= 0");
            } // end set
        } // end property WeeklySalary

        /// <summary>
        /// This method returns the amount that an Emyploee earns.
        /// </summary>
        /// <returns>returns the amount that an Employee earned.</returns>
        public override decimal Earnings()
        {
            return paymentAmount;
        }
    }
}
