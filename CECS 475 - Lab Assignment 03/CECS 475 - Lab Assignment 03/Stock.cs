using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CECS_475___Lab_Assignment_03
{
    class Stock
    {
        string name;
        int initialValue;
        double currentValue;
        int numberOfChanges = 0;
        int maximumChange;
        int notificationThreshold;
        Thread stockThread;

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

        public void Activate()
        {
            for (;;)
            {
                Thread.Sleep(500);
                ChangeStockValue();
            }
        }

        public void ChangeStockValue()
        {
            Random newRand = new Random();
            currentValue += newRand.NextDouble();
            numberOfChanges++;
            if ((currentValue - initialValue) > notificationThreshold)
            {

            }
        }
    }
}
