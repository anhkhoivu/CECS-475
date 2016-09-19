using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        private System.Object lockThis = new System.Object();
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static ReaderWriterLockSlim newlock = new ReaderWriterLockSlim();
        DateTime thisDay = DateTime.Now;

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
            newlock.EnterWriteLock();
            Stock newStock = (Stock)o;
            string displayValue = newStock.stockCurrentValue.ToString();
            string displayChanges = newStock.stockChanges.ToString();
            Console.WriteLine(brokerName.PadRight(16) + newStock.stockName.PadRight(16)
                + displayValue.PadRight(16) + displayChanges);

            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\StockOutput.txt", true))
            {
                    outputFile.WriteLine(thisDay.ToString().PadRight(30) + newStock.stockName.PadRight(15) +
                            displayValue.PadRight(15) + displayChanges);
            }
            newlock.ExitWriteLock();
        }
    }
}
