using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    // Classe mere
    public class PowerPlant
    {
        protected string name;
        protected Line outputLine;
        protected bool isWorking = false;
        protected string alertMessage = "";

        //protected string type;

        protected double productionCost;
        protected double CO2emission;
        protected double powerProduction;

        //protected bool constantProduction;
        //protected bool adjustableProduction;

        //protected double startTime;
        //protected double stopTime;

        public PowerPlant(string name, Line outputLine)
        {
            this.name = name;
            this.outputLine = outputLine;
        }

        public virtual void Start() { isWorking = true; }
        public virtual void Stop() { isWorking = false; }
        public bool IsWorking { get { return isWorking; } }
        public virtual double Production()
        {
            Console.WriteLine("Si ce message s'affiche, ca pue, faut pas arriver ici");
            return powerProduction;
        }
        public virtual double Cost()
        {
            return productionCost;
        }
        public virtual double C02()
        {
            return CO2emission;
        }
        public string Resume()
        {
            string resume = string.Format("{0} : Production = {1} W, cout = {2} $, emission = {3} grammes", name, Production(), Cost(), C02());
            return resume;
        }
    }

    // Classes filles héritées
    public class GasPowerPlant : PowerPlant
    {
        Market market;
        public GasPowerPlant(string name, Line outputLine, Market market) : base(name, outputLine)
        {
            this.market = market;
        }

        public override double Production()
        {
            var ran = new Random();
            powerProduction = ran.Next(5000, 50000);
            return powerProduction;
        }
        public override double Cost()
        {
            double gasPrice = market.getGasPrice;
            productionCost = gasPrice * powerProduction;
            return productionCost;
        }
        public override double C02()
        {
            CO2emission = 0.2 * powerProduction;
            return CO2emission;
        }
    }

    public class NuclearPowerPlant : PowerPlant
    {
        private double productionGoal = 100;          // objectif de puissance à atteindre
        private double productionState;               // 0 = en route, 1 = a l'arret, 2 = mise en route, 3, mise a l'arret
        private double count_marche = 1;
        private double count_arret = 10;
        Market market;
        public NuclearPowerPlant(string name, Line outputLine, Market market) : base(name, outputLine)
        {
            this.market = market;
            Start();                // mise en marche automatique au moment de la création de la centrale
        }
        public override void Start() { productionState = 2; isWorking = true; }
        public override void Stop() { productionState = 3; }
        public override double Production()
        {

            switch (productionState)
            {
                // production stable
                case 0:
                    {
                        powerProduction = productionGoal;
                        break;
                    }
                // production nulle
                case 1:
                    {
                        isWorking = false;
                        powerProduction = 0;
                        break;
                    }
                // mise en route
                case 2:
                    {
                        if (count_marche <= 10)
                        {
                            powerProduction = productionGoal * (count_marche / 10);
                            //Console.WriteLine(" en mise en marche : " + count_marche + "---" + powerProduction);
                            count_marche ++;
                        }
                        else
                        {
                            count_marche = 0;
                            productionState = 0;
                        }
                        break;
                    }
                // mise a l'arret
                case 3:
                    {
                        if (count_arret > 0 )
                        {
                            powerProduction = productionGoal * (count_arret / 10);
                            //Console.WriteLine(" en cours d'arret : " + count_arret + "---" + powerProduction);
                            count_arret --;
                        }
                        else
                        {
                            count_arret = 10;
                            productionState = 1;
                        }
                        break;
                    }
            }
            return powerProduction;
        }
        public override double Cost()
        {
            double nuclearPrice = market.getNuclearPrice;
            productionCost = nuclearPrice * powerProduction;
            return productionCost;
        }

    }

    public class WindFarm : PowerPlant
    {
        Weather meteo;
        double windspeed;
        bool reduceProduction = false;
        public WindFarm(string name, Line outputLine, Weather meteo) : base(name, outputLine)
        {
            this.meteo = meteo;
        }
        public override double Production()
        {
            windspeed = meteo.getWindspeed;
            if (reduceProduction)
            {
                powerProduction = 1000 * windspeed / 2;
            }
            else
            {
                powerProduction = 1000 * windspeed;
            }
            return powerProduction;
        }

        public void ReduceProduction() { reduceProduction = true; }
        public void StopReduceProduction() { reduceProduction = false; }
      
    }

    public class SolarPowerPlant : PowerPlant
    {
        Weather meteo;
        double sunlight;
        public SolarPowerPlant(string name, Line outputLine, Weather meteo) : base(name, outputLine)
        {
            this.meteo = meteo;
        }
        public override double Production()
        {
            sunlight = meteo.getSunlight;
            powerProduction = 2000 * sunlight;
            return powerProduction;
        }
    }

    public class PurchaseAbroad : PowerPlant
    {
        double purchasedPower;
        Market market;
        public PurchaseAbroad(string name, Line outputLine, double purchasedPower, Market market) : base(name, outputLine)
        {
            this.purchasedPower = purchasedPower;
            this.market = market;
        }
        public override double Production()
        {
            powerProduction = purchasedPower;
            return powerProduction;
        }
        public override double Cost()
        {
            double wattPrice = market.getWattPrice;
            productionCost = wattPrice * powerProduction;
            return productionCost;
        }
    }
}
