using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CECS_475___Lab_Assignment_03
{
    public class Stock
    {
        public delegate void StockNotification(string name, int currentValue, int numberChanges);
        public EventHandler stockHandler;
        public event StockNotification stockEvent;

        string name;
        int initialValue;
        int currentValue;
        int numberOfChanges = 0;
        int maximumChange;
        int notificationThreshold;
        Thread stockThread;
        DateTime thisDay = DateTime.Now;

        /// <summary>
        /// Default constructor for Stock.
        /// </summary>
        /// <param name="name">Parmeter that takes in the stock name.</param>
        /// <param name="startingValue">Parameter that takes in the starting value of the stock.</param>
        /// <param name="maxChange">Parameter that takes in the maximum change of a stock.</param>
        /// <param name="threshold">Parameter that takes in the maximum threshold of changes for notification of a stock.</param>
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            this.name = name;
            initialValue = startingValue;
            currentValue = initialValue;
            notificationThreshold = threshold;
            maximumChange = maxChange;
            stockThread = new Thread(new ThreadStart(Activate));
            stockThread.Start();
        }

        /// <summary>
        /// Gets name of stock.
        /// </summary>
        public string stockName
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets initial value of a stock.
        /// </summary>
        public int stockInitialValue
        {
            get
            {
                return this.initialValue;
            }
        }

        /// <summary>
        /// Gets current value of a stock.
        /// </summary>
        public int stockCurrentValue
        {
            get
            {
                return this.currentValue;
            }
        }

        /// <summary>
        /// Gets number of changes of a stock.
        /// </summary>
        public int stockChanges
        {
            get
            {
                return this.numberOfChanges;
            }
        }

        /// <summary>
        /// Activates the thread and the process.
        /// </summary>
        public void Activate()
        {
            for (;;)
            {
                Thread.Sleep(500);
                ChangeStockValue();
                if (stockHandler != null)
                {
                    stockHandler(this, null);
                }
            }
        }

        /// <summary>
        /// Changes the stock value.
        /// </summary>
        public void ChangeStockValue()
        {
            Random newRand = new Random();
            currentValue += newRand.Next(0, maximumChange);
            numberOfChanges++;
            if ((currentValue - initialValue) > notificationThreshold)
            {
                OnStockNotified();
            }
        }

        /// <summary>
        /// Listens for the event OnStockNotified()
        /// </summary>
        protected virtual void OnStockNotified()
        {
            if (stockEvent != null)
            {
                stockEvent(name, currentValue, numberOfChanges);
            }
        }
    }
}