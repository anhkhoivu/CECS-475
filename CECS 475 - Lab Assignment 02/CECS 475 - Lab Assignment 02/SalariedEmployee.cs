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

        // four-parameter constructor
        public SalariedEmployee(string first, string last, string ssn,
           decimal salary) : base(first, last, ssn)
        {
            GetPaymentAmount = salary; // validate salary via property
        } // end four-parameter SalariedEmployee constructor

        // property that gets and sets salaried employee's salary
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

        public override decimal Earnings()
        {
            return paymentAmount;
        }
    }
}
