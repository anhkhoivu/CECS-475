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
        
        public EventHandler test;
        public event StockNotification stockEvent;

        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string name;
        int initialValue;
        int currentValue;
        int numberOfChanges = 0;
        int maximumChange;
        int notificationThreshold;
        Thread stockThread;
        DateTime thisDay = DateTime.Now;

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

        public string stockName
        {
            get
            {
                return this.name;
            }
        }

        public int stockInitialValue
        {
            get
            {
                return this.initialValue;
            }
        }

        public int stockCurrentValue
        {
            get
            {
                return this.currentValue;
            }
        }

        public int stockChanges
        {
            get
            {
                return this.numberOfChanges;
            }
        }

        public void Activate()
        {

            for (;;)
            {
                Thread.Sleep(500);
                ChangeStockValue();
                if (test != null)
                {
                    test(this, null);
                }
            }
        }

        public void ChangeStockValue()
        {
            Random newRand = new Random();
            currentValue += newRand.Next(0, maximumChange);
            numberOfChanges++;
            if ((currentValue - initialValue) > notificationThreshold)
            {
                OutputStock();
                OnStockNotified();
            }
        }

        protected virtual void OnStockNotified()
        {
            if (stockEvent != null)
            {
                stockEvent(name, currentValue, numberOfChanges);
            }
        }

        protected virtual void OutputStock()
        {
            string displayValue = stockCurrentValue.ToString();
            string displayInitValue = stockInitialValue.ToString();
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\StockOutput.txt", true))
            {
                outputFile.WriteLine(thisDay.ToString().PadRight(30) + name.PadRight(15) +
                       displayInitValue.PadRight(15) + displayValue);
            }
        }
    }
}