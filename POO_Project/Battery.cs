using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Battery
    {
        private string name;
        private double maximum_charge;
        public double charge;

        PowerPlant input;

        private string alert_message;

        public Battery(string name, PowerPlant input)
        {
            this.name = name;
            this.input = input;
            maximum_charge = 1000;

            charge = 0;     // batterie vide lors de sa construction

            alert_message = "";
        }
        public string GetAlert { get { return alert_message; } }
        public string GetName { get { return name; } }
        public string GetPowerPlant { get { return input.GetName; } }
        public double GetMaximumCharge { get { return maximum_charge; } }


        public void ChargeBattery()
        {
            
        }

    }
}
