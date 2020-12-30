using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class PowerPlant
    {
        protected string name;
        protected Line OutputLine;
        protected bool IsWorking = false;
        protected string alertMessage = "";

        //protected string type;

        protected double productionCost;
        protected double CO2emission;
        protected double powerProduction;

        //protected bool constantProduction;
        //protected bool adjustableProduction;

        //protected double startTime;
        //protected double stopTime;

        protected DistributionNode OutputNode;

        public PowerPlant(string name)//, Line OutputLine)
        {
            this.name = name;
            //this.OutputLine = OutputLine;

            this.OutputNode = new DistributionNode(String.Format(name + "_OutputNode"));
            this.OutputLine = this.OutputNode.GetInputLine; //ligne de sortie de la centrale = ligne d'entrée de son noeud de distribution
            OutputLine.SetIsPowerPlantLine(true);  //je précise que cette ligne est reliée a une centrale
            OutputLine.SetOutputNode(OutputNode);  //je précise à la ligne qui est mon noeud de sortie (pour pouvoir le récupérer par après)
        }
        public Line GetOutPutLine { get { return this.OutputLine; } }
        public Node GetOutputNode{get {return this.OutputNode; } } 
        public string GetName { get { return name; } }

        public virtual void Start() { IsWorking = true; }
        public virtual void Stop() { IsWorking = false; }
        public bool GetIsWorking { get { return IsWorking; } } //j'ai ajouté le Get devant parce que build plantait sinon (ambiguité entre la methode et le bool)
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
        //public GasPowerPlant(string name, Line OutputLine, Market market) : base(name, OutputLine)
        public GasPowerPlant(string name, Market market) : base(name)
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
            double gasPrice = market.GetGasPrice;
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
        //public NuclearPowerPlant(string name, Line OutputLine, Market market) : base(name, OutputLine)
        public NuclearPowerPlant(string name, Market market) : base(name)
        {
            this.market = market;
            Start();                // mise en marche automatique au moment de la création de la centrale
        }
        public override void Start() { productionState = 2; IsWorking = true; }
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
                        IsWorking = false;
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
            double nuclearPrice = market.GetNuclearPrice;
            productionCost = nuclearPrice * powerProduction;
            return productionCost;
        }

    }

    public class WindFarm : PowerPlant
    {
        Weather meteo;
        double windspeed;
        bool reduceProduction = false;
        //public WindFarm(string name, Line OutputLine, Weather meteo) : base(name, OutputLine)
        public WindFarm(string name, Weather meteo) : base(name)
        {
            this.meteo = meteo;
        }
        public override double Production()
        {
            windspeed = meteo.GetWindspeed;
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
        //public SolarPowerPlant(string name, Line OutputLine, Weather meteo) : base(name, OutputLine)
        public SolarPowerPlant(string name, Weather meteo) : base(name)
        {
            this.meteo = meteo;
        }
        public override double Production()
        {
            sunlight = meteo.GetSunlight;
            powerProduction = 2000 * sunlight;
            return powerProduction;
        }
    }

    public class PurchaseAbroad : PowerPlant
    {
        double purchasedPower;
        Market market;
        //public PurchaseAbroad(string name, Line OutputLine, double purchasedPower, Market market) : base(name, OutputLine)
        public PurchaseAbroad(string name, double purchasedPower, Market market) : base(name)
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
            double wattPrice = market.GetWattPrice;
            productionCost = wattPrice * powerProduction;
            return productionCost;
        }
    }
}
