using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Consumer
    {
        protected string name;
        protected Line inputLine;

        protected double claimingPower;
        protected double missingPower;
        protected double inputLinePower;

        protected string alertMessage;

        protected bool isConsuming = true;
        private bool ManualClaiming = false;

        protected ConcentrationNode inputNode;

        public Consumer(string name)
        {            
            this.name = name;

            inputNode = new ConcentrationNode(name + "_inputNode");
            inputLine = inputNode.GetOutputLine; 
            inputLine.SetIsConsumerLine(true);
            inputLine.SetInputNode(inputNode);
            inputLine.SetName(name + "_line");
            
            inputLine.SetMyConsumer(this);

            alertMessage = "";
        }

        public string GetName { get { return name; } }
        public Line getInputLine { get { return inputLine; } }
        public ConcentrationNode GetInputNode { get { return inputNode; } }
        public double GetMissingPower { get { return missingPower; } }
        public string GetAlertMessage { get { return alertMessage; } }
        public string ResetAlertMessage { set { alertMessage = ""; } }
        public void SetInputLine(Line newInputLine) { inputLine = newInputLine; }     
        public double GetClaimingPower { get { return claimingPower; } }

        public void SetManualClaiming(bool b) { ManualClaiming = b; }
        public bool GetManualClaiming { get { return ManualClaiming; } } 

        // UPDATE CLAIMED POWER
        public virtual double UpdateClaimingPower()
        {
            inputLine.SetPowerClaimed(claimingPower);
            return claimingPower;
        }

        
      
        
    }

    public class City : Consumer
    {
        private double temperature;
        private double nbr_hab;

        public City(string name, double nbr_hab, Weather meteo) : base(name)
        {
            // La ville se différencie par un nombre d'habitant et une certaine température donnée par la météo sur laquelle elle est focalisée
            temperature = meteo.GetTemperature;
            this.nbr_hab = nbr_hab;
        }

        public override double UpdateClaimingPower()
        {
            // température peut prendre une valeur entre -5 et +35°C
            if (temperature < 10)
            {
                claimingPower = 5;
            }
            else if (temperature >= 5 & temperature < 20)
            {
                claimingPower = 2;
            }
            else  // temperature >= 20°C
            {
                claimingPower = 1;     // faible consomation
            }

            claimingPower *= nbr_hab;   // * le nombre d'habitant de la ville

            inputLine.SetPowerClaimed(claimingPower);
            return claimingPower;           
        }
    }

    public class Entreprise : Consumer
    {
        double nbr_machines;
        public Entreprise(string name, double nbr_machines) : base(name)
        {
            this.nbr_machines = nbr_machines;
        }
        public override double UpdateClaimingPower()
        {
            claimingPower = nbr_machines * 1; // chaque machine consomme 10000W

            inputLine.SetPowerClaimed(claimingPower);
            return claimingPower;
        }
    }

    public class SaleAbroad : Consumer
    {
        Market market;
        double wattPrice;
        double benefices;
        double tot_benef;
        public SaleAbroad(string name, Market market) : base(name)
        {
            this.market = market;
        }
        public override double UpdateClaimingPower()
        {
            wattPrice = market.GetWattPrice;
            claimingPower = inputLine.GetCurrentPower;
            benefices = claimingPower * wattPrice;
            tot_benef += benefices;

            inputLine.SetPowerClaimed(claimingPower);
            return claimingPower;
        }
    }
    public class Dissipator : Consumer
    {
        public Dissipator(string name) : base(name)
        {
            isConsuming = false;
        }
        public override double UpdateClaimingPower()
        {
            claimingPower = inputLine.GetCurrentPower;
            inputLine.SetPowerClaimed(claimingPower);
            return claimingPower;
        }
    }
}
