using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_475___Lab_Assignment_03
{
    class StockBroker
    {
        public event StockNotification notify;

        List<Stock> stockList = new List<Stock>();
        string brokerName;

        public StockBroker(string name)
        {
            brokerName = name;
        }
        
        public void AddStock(Stock stock)
        {
            stockList.Add(stock);
            stock.test += OnEventNotified;
        }

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
