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

        private double PLBattery = 6;

        PowerPlant input;

        Line outputLine;

        private string alert_message;

        public Battery(string name, PowerPlant input)
        {
            this.name = name;
            this.input = input;
            maximum_charge = 1000;

            charge = 0;     // batterie vide lors de sa construction

            alert_message = "";
            outputLine = new Line(name + "_outputLine");
            outputLine.SetPriorityLevel(PLBattery);
            outputLine.SetIsBatteryLine(true);
        } 
        public double GetPLBattery { get { return PLBattery; } }
        public string GetAlert { get { return alert_message; } }
        public string GetName { get { return name; } }
        public string GetPowerPlant { get { return input.GetName; } }
        public double GetMaximumCharge { get { return maximum_charge; } }
        public Line GetOutputLine { get { return outputLine; } }

        public void DechargeBattery(double powerClaimed)
        {
            if (powerClaimed <= charge)
            {
                charge -= powerClaimed;
            }
            else
            {
                alert_message = String.Format("La batterie {0} ne peut pas satisfaire à la demande", name);
            }
        }  
        
        public void UpdateBattery()
        {
            outputLine.SetCurrentPower(outputLine.GetPowerClaimed);
            DechargeBattery(outputLine.GetPowerClaimed);
        }
    }
}
