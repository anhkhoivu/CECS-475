using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Test
{
    class CommissionEmployee : Employee
    {
        private decimal grossSales; // gross weekly sales
        private decimal commissionRate; // commission percentage

        public CommissionEmployee() : base()
        { }

        /// <summary>
        /// five-parameter constructor for CommissionEmployee
        /// </summary>
        /// <param name="first">Parameter that contains the first name of the employee.</param>
        /// <param name="last">Parameter that contains the last name of the employee.</param>
        /// <param name="ssn">Parameter that contains the SSN of the employee.</param>
        /// <param name="sales">Parameter that contains the sales of an employee.</param>
        /// <param name="rate">Parameter that contains the rate of an employee.</param>
        public CommissionEmployee(string first, string last, string ssn,
           decimal sales, decimal rate) : base(first, last, ssn)
        {
            GrossSales = sales; // validate gross sales via property
            CommissionRate = rate; // validate commission rate via property
        } // end five-parameter CommissionEmployee constructor

        /// <summary>
        /// property that gets and sets commission employee's gross sales
        /// </summary>
        public decimal GrossSales
        {
            get
            {
                return grossSales;
            } // end get
            set
            {
                if (value >= 0)
                    grossSales = value;
                else
                    throw new ArgumentOutOfRangeException(
                       "GrossSales", value, "GrossSales must be >= 0");
            } // end set
        } // end property GrossSales

        /// <summary>
        /// property that gets and sets commission employee's commission rate
        /// </summary>
        public decimal CommissionRate
        {
            get
            {
                return commissionRate;
            } // end get
            set
            {
                if (value > 0 && value < 1)
                    commissionRate = value;
                else
                    throw new ArgumentOutOfRangeException("CommissionRate",
                       value, "CommissionRate must be > 0 and < 1");
            } // end set
        } // end property CommissionRate

        /// <summary>
        /// calculate earnings; override abstract method Earnings in Employee
        /// </summary>
        /// <returns>return the Earnings of the Employee.</returns>
        public override decimal Earnings()
        {
            return CommissionRate * GrossSales;
        } // end method Earnings              

        /// <summary>
        /// Displays the output of a CommissionEmployee.
        /// </summary>
        /// <returns>return string representation of CommissionEmployee object</returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}\n{2}: {3:C}\n{4}: {5:F2}",
               "commission employee", base.ToString(),
               "gross sales", GrossSales, "commission rate", CommissionRate);
        } // end method ToString 
    }
}
