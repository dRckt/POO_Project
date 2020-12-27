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

        protected string alertMessage;

        protected bool isConsuming = true;

        public Consumer(string name, Line inputLine)
        {
            this.name = name;
            this.inputLine = inputLine;

            alertMessage = "";
        }
        
        // permet de récupérer le nom du consommateur
        public string GetName { get { return name; } }
        // permet de récupérer la ligne d'entrée
        public Line getInputLine { get { return inputLine; } }
        
        // si besoin éventuel, permet de brancher le consommateur sur une autre ligne passée en paramètre
        public void SetInputLine(Line newInputLine) { inputLine = newInputLine; }

        // générer une demande de puissance
        public virtual void LaunchClaimingPower()
        {
            SetMissingPower();
        }
        
        // permet de récupérer la demande de puissance actuelle
        public double GetClaimingPower { get { return claimingPower; } }
        
        // va chercher la puissance sur inputLine
        public void SetMissingPower()
        {
            double a = claimingPower;
            double b = inputLine.getCurrentPower();
            missingPower = a - b;  // puissance manquante = puissance demandée - puissance disponible sur inputLine

            if (missingPower > 0)   // pas assez de puissance sur la lige
            {
                alertMessage = String.Format("The consumer {0} is missing {1}W.", name, missingPower);
            }
            else if (missingPower == 0)     // bonne quantité de puissance sur la ligne
            {
                alertMessage = String.Format("The consumer {0} has received all the available power", name);
                Consuming();
            }
            else        // trop de puissance sur la ligne
            {
                alertMessage = String.Format("The consumer {0} is receiving {1}W in excess.", name, Math.Abs(missingPower));
            }
        }
        
        public double GetMissingPower { get { return missingPower; } }
        public string GetAlertMessage { get { return alertMessage; } }
        public string CleanAlertMessage { set { alertMessage = ""; } }

        public void Consuming()
        {
            // Le consomateur vide inputLine
            claimingPower -= inputLine.getCurrentPower();
            inputLine.setCurrentPower(0);
        }
    }

    public class City : Consumer
    {
        private double temperature;
        private double nbr_hab;

        public City(string name, Line inputLine, double nbr_hab, Weather meteo) : base(name, inputLine)
        {
            // La ville se différencie par un nombre d'habitant et une certaine température donnée par la météo sur laquelle elle est focalisée
            temperature = meteo.getTemperature;
            this.nbr_hab = nbr_hab;
        }

        public override void LaunchClaimingPower()
        {
            // température peut prendre une valeur entre -5 et +35°C
            if (temperature < 10)
            {
                claimingPower = 1000;
            }
            else if (temperature >= 5 & temperature < 20)
            {
                claimingPower = 500;
            }
            else  // temperature >= 20°C
            {
                claimingPower = 50;     // faible consomation
            }

            claimingPower *= nbr_hab;   // * le nombre d'habitant de la ville

            SetMissingPower();
        }
    }

    public class Entreprise : Consumer
    {
        double nbr_machines;
        public Entreprise(string name, Line inputLine, double nbr_machines) : base(name, inputLine)
        {
            this.nbr_machines = nbr_machines;
        }
        public override void LaunchClaimingPower()
        {
            claimingPower = nbr_machines * 10000; // chaque machine consomme 10000W
            SetMissingPower();
        }
    }
    
    public class SaleAbroad : Consumer
    {
        Market market;
        double wattPrice;
        double benefices;
        public SaleAbroad(string name, Line inputLine, Market market) : base(name, inputLine)
        {
            this.market = market;    
        }
        public override void LaunchClaimingPower()
        {
            wattPrice = market.getWattPrice;
            claimingPower = inputLine.getCurrentPower();
            benefices = claimingPower * wattPrice;
            SetMissingPower();
        }
    }

    public class dissipator : Consumer
    {
        public dissipator(string name, Line inputLine) : base(name, inputLine)
        {
            isConsuming = false;
        }
        public override void LaunchClaimingPower()
        {
            claimingPower = inputLine.getCurrentPower() ;
            SetMissingPower();
        }

    }
}
