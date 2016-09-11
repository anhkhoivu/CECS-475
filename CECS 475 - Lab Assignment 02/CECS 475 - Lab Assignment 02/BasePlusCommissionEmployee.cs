using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_02
{
    class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary; // base salary per week
        
        /// <summary>
        /// six parameter constructor for BasePlusCommissionEmployee
        /// </summary>
        /// <param name="first">Parameter that contains the first name of the employee.</param>
        /// <param name="last">Parameter that contains the last name of the employee.</param>
        /// <param name="ssn">Parameter that contains the SSN of the employee.</param>
        /// <param name="sales">Parameter that contains the sales of an employee.</param>
        /// <param name="rate">Parameter that contains the rate of an employee.</param>
        /// <param name="salary"></param>
        public BasePlusCommissionEmployee(string first, string last,
           string ssn, decimal sales, decimal rate, decimal salary) : base(first, last,
           ssn, sales, rate)
        {
            BaseSalary = salary; // validate base salary via property
        } // end six-parameter BasePlusCommissionEmployee constructor

        /// <summary>
        /// property that gets and sets base-salaried commission employee's base salary
        /// </summary>
        public decimal BaseSalary
        {
            get
            {
                return baseSalary;
            } // end get
            set
            {
                if (value >= 0)
                    baseSalary = value;
                else
                    throw new ArgumentOutOfRangeException("BaseSalary",
                       value, "BaseSalary must be >= 0");
            } // end set
        } // end property BaseSalary

        /// <summary>
        /// calculate earnings; override method Earnings in CommissionEmployee
        /// </summary>
        /// <returns>return the earnings of an BasePlusCommissionEmployee</returns>
        public override decimal Earnings()
        {
            return BaseSalary + base.Earnings();
        } // end method Earnings               

        /// <summary>
        /// return string representation of BasePlusCommissionEmployee object
        /// </summary>
        /// <returns>display the String of the BasePlusCommissionEmployee</returns>
        public override string ToString()
        {
            return string.Format("base-salaried {0}; base salary: {1:C}",
               base.ToString(), BaseSalary);
        } // end method ToString                                            
    }
}
