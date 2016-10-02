using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_05
{
    /*interface IPayable
    {
        decimal Earnings();
    }*/
    public interface IPayable : IComparable
    {
        decimal GetPaymentAmount(); // calculate payment; no implementation
    } // end interface IPayable
}
