using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Test
{
    class HourlyEmployee : Employee
    {
        private decimal wage; // wage per hour
        private decimal hours; // hours worked for the week

        public HourlyEmployee() : base()
        { }

        /// <summary>
        /// 5 parameter constructor for HourlyEmployee
        /// </summary>
        /// <param name="first">Parameter that contains the first name of an Employee.</param>
        /// <param name="last">Parameter that contains the last name of an Employee.</param>
        /// <param name="ssn">Parameter that contains the SSN of an Employee.</param>
        /// <param name="hourlyWage">Parameter that contains the hourly wage of an Employee.</param>
        /// <param name="hoursWorked">Parameter that contains the number of hours worked by an Employee.</param>
        // five-parameter constructor
        public HourlyEmployee(string first, string last, string ssn,
           decimal hourlyWage, decimal hoursWorked)
           : base(first, last, ssn)
        {
            Wage = hourlyWage; // validate hourly wage via property
            Hours = hoursWorked; // validate hours worked via property
        } // end five-parameter HourlyEmployee constructor

        /// <summary>
        /// property that gets and sets hourly employee's wage
        /// </summary>
        public decimal Wage
        {
            get
            {
                return wage;
            } // end get
            set
            {
                if (value >= 0) // validation
                    wage = value;
                else
                    throw new ArgumentOutOfRangeException("Wage",
                       value, "Wage must be >= 0");
            } // end set
        } // end property Wage

        /// <summary>
        /// property that gets and sets hourly employee's hours
        /// </summary>
        public decimal Hours
        {
            get
            {
                return hours;
            } // end get
            set
            {
                if (value >= 0 && value <= 168) // validation
                    hours = value;
                else
                    throw new ArgumentOutOfRangeException("Hours",
                       value, "Hours must be >= 0 and <= 168");
            } // end set
        } // end property Hours

        /// <summary>
        /// calculate earnings; override Employee’s abstract method Earnings
        /// </summary>
        /// <returns>returns the Employee's earnings.</returns>
        public override decimal Earnings()
        {
            if (Hours <= 40) // no overtime                          
                return Wage * Hours;
            else
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5M);
        } // end method Earnings                                      

        /// <summary>
        /// return string representation of HourlyEmployee object
        /// </summary>
        /// <returns>displays the setup for an HourlyEmployee object.</returns>
        public override string ToString()
        {
            return string.Format(
               "hourly employee: {0}\n{1}: {2:C}; {3}: {4:F2}",
               base.ToString(), "hourly wage", Wage, "hours worked", Hours);
        } // end method ToString     
    }
}
