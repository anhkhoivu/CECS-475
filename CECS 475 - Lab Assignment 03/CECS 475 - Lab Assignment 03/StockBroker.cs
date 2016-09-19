using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_03
{
    /// <summary>
    /// StockBroker class.
    /// </summary>
    class StockBroker
    {
        List<Stock> stockList = new List<Stock>();
        string brokerName;

        /// <summary>
        /// Default constructor for StockBroker.
        /// </summary>
        /// <param name="name">Parameter for brokerName</param>
        public StockBroker(string name)
        {
            brokerName = name;
        }
        
        /// <summary>
        /// Adds a Stock to the List.
        /// </summary>
        /// <param name="stock">Parameter that takes in a stock.</param>
        public void AddStock(Stock stock)
        {
            stockList.Add(stock);
            stock.stockHandler += OnEventNotified;
        }

        /// <summary>
        /// Listener for EventHandler
        /// </summary>
        /// <param name="o">Default object parameter for an object type.</param>
        /// <param name="e">Default EventArgs parameter.</param>
        protected void OnEventNotified(object o, EventArgs e)
        {
            Stock newStock = (Stock)o;
            string displayValue = newStock.stockCurrentValue.ToString();
            string displayChanges = newStock.stockChanges.ToString();
            Console.WriteLine(brokerName.PadRight(16) + newStock.stockName.PadRight(16)
                + displayValue.PadRight(16) + displayChanges);
        }
    }
}
