using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Battery
    {
        private string name;
        private double maximum_charge;
        private double charge;

        Line inputLine;

        private string alert_message;

        public Battery(string name, Line inputLine, double maximum_charge)
        {
            this.name = name;
            this.inputLine = inputLine;
            this.maximum_charge = maximum_charge;

            charge = 0;     // batterie vide lors de sa construction

            alert_message = "";
        }
        public string GetAlert { get { return alert_message; } }
        public string GetName { get { return name; } }
        public string GetLine { get { return inputLine.getName; } }


        public void ChargeBattery()
        {
            double charge_supp = inputLine.getCurrentPower();
            if ((charge + charge_supp) <= maximum_charge)       // si la charge maximum permet de stocker la puissance de l'inputLine, la batterie stocke la puissance
            {
                charge += charge_supp;
            }
            else
            {
                alert_message = String.Format("Battery {0} can not support the charge ({1}) coming from {2}", name, charge_supp, inputLine.getName);
            }

        }

    }
}
