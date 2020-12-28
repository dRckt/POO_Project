using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Market
    {
        private double nuclearPrice;
        private double gasPrice;
        private double wattPrice;

        public Market()
        {
            changeCost();
        }

        public void changeCost()
        {
            var ran = new Random();

            nuclearPrice = ran.Next(1000, 3000);
            gasPrice = ran.Next(500, 1500);
            wattPrice = ran.Next(1000, 2000);
        }
        public double GetNuclearPrice { get { return nuclearPrice; } }
        public double GetGasPrice { get { return gasPrice; } }
        public double GetWattPrice { get { return wattPrice; } }
    }
}
