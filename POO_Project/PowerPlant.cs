using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class PowerPlant
    {
        //NIVEAU 1 = PASSE EN PRIORITE, c'est a lui qu'on demandera en premier le courant
        //          par exemple si un consumer a le choix entre demander du courant sur ligne de niveau 1 ou de niveau 3, le consumer prendra d'abord le courant sur ligne de niveau 1 !!
        //          Logiquement PLsolarpowerplant = 1 et PLgaspowerplant est plus grand vu qu'il pollue

        //D: stp détermine qui vaut combien dans les variables ci dessous:  (minimum= 0  ; maximum=6);
       
        public double PLGasPowerPlant = 1;          
        public double PLNuclearPowerPlant = 0;      
        public double PLWindFarm = 2;               
        public double PLSolarPowerPlant = 3;      
        public double PLPurchaseAbroad = 7;         
        public double PLNeutre = 4;                 

        protected double MyPriorityLevel;

        protected string name;
        protected Line OutputLine;
        protected bool IsWorking = false;
        protected string alertMessage = "";

        //protected string type;

        protected double productionCost;
        protected double CO2emission;
        protected double powerProduction;
        protected double disponibleProduction;
        protected double claimedPower;

        protected bool constantProduction;
        protected bool adjustableProduction;

        protected DistributionNode OutPutNode;

        // STOCKAGE 
        protected Battery battery;
     

        public PowerPlant(string Name)
        {
            name = Name;

            OutPutNode = new DistributionNode(String.Format(name + "_OutPutNode"));
            OutputLine = OutPutNode.GetInputLine; 

            OutputLine.SetIsPowerPlantLine(true);
            OutputLine.SetOutputNode(OutPutNode); 
            OutputLine.SetMyPowerPlant(this);
            OutputLine.SetName(Name + "_line");
            OutputLine.SetPriorityLevel(4);

            battery = new Battery(Name + "_battery", this);
        }

        // PROPRIETES GET
        public Line GetOutPutLine { get { return this.OutputLine; } }
        public DistributionNode GetOutputNode{get {return this.OutPutNode; } } 
        public string GetName { get { return name; } }
        public double GetPriorityLevel { get { return MyPriorityLevel; } }
        public bool GetIsWorking { get { return IsWorking; } }
        public string GetAlertMessage { get { return alertMessage; } }
        public double GetPowerProduction { get { return powerProduction; } }

        // CHANGEMENT PRIORITY LEVEL
        public void SetPriorityLevel(double PL) { MyPriorityLevel = PL; }

        // START - STOP 
        public virtual void Start() 
        { 
            IsWorking = true;
            OutputLine.SetPriorityLevel(GetPriorityLevel);
            alertMessage = String.Format("La centrale {0} a été démarrée", GetName);
        }
        public virtual void Stop() 
        { 
            IsWorking = false;
            OutputLine.SetPriorityLevel(7);
            alertMessage = String.Format("La centrale {0} a été stoppée", GetName);
        }         

        // RENVOIE LA PRODUCTION DISPONNIBLE
        public virtual double DisponibleProduction() { return 0; } // Fonction toujours overridée  --> passage en abstract     
        public double UpdateClaimedPower() { return OutputLine.GetPowerClaimed; }
        
        //public abstract double DisponibleProduction();           // Passage de la fonction en abstract, pour ca il faut que la classe PowerPlant soit abstract, mais erreur dans le manager

        public virtual double Cost() { return productionCost; }
        public virtual double C02() { return CO2emission; }
        public double AskDisponiblePower() { return disponibleProduction; }
         
        

        public virtual double UpdatePowerPlant()
        {
            if (IsWorking == true)
            {
                powerProduction = DisponibleProduction();
                claimedPower = UpdateClaimedPower();

                double surplus = powerProduction - claimedPower;

                if (surplus > 0)
                {
                    if (battery.charge + surplus < battery.GetMaximumCharge)
                    {
                        battery.charge += surplus;
                        return claimedPower;
                    }
                    else
                    {
                        return powerProduction;
                    }
                }
                else
                {
                    return powerProduction;
                }
            }
            else
            {
                powerProduction = 0;
                return powerProduction;
            }            
        }
    }

    // Classes filles héritées
    public class GasPowerPlant : PowerPlant
    {
        private readonly Market market;
       
        public GasPowerPlant(string name, Market market) : base(name)
        {
            SetPriorityLevel(PLGasPowerPlant);
            OutputLine.SetPriorityLevel(GetPriorityLevel);
            this.market = market;
            constantProduction = true;
            adjustableProduction = false;
            Start();
        }

        public override double DisponibleProduction()
        {
            var ran = new Random();
            disponibleProduction = ran.Next(5000, 50000);
            return disponibleProduction;
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

        public NuclearPowerPlant(string name, Market market) : base(name)
        {
            SetPriorityLevel(PLGasPowerPlant);
            OutputLine.SetPriorityLevel(GetPriorityLevel);
            this.market = market;
            constantProduction = true;
            adjustableProduction = false;
            Start();                                 // mise en marche automatique au moment de la création de la centrale
        }
        public override void Start() 
        { 
            productionState = 2;
            IsWorking = true;
            OutputLine.SetPriorityLevel(GetPriorityLevel);
        }
        public override void Stop()
        {
            productionState = 3;
            OutputLine.SetPriorityLevel(7);
        }
        public override double DisponibleProduction()
        {
            double dispopowerProduction=0;
            switch (productionState)
            {
                // production stable
                case 0:
                    {
                        dispopowerProduction = productionGoal;
                        break;
                    }
                // production nulle
                case 1:
                    {
                        IsWorking = false;
                        dispopowerProduction = 0;
                        break;
                    }
                // mise en route
                case 2:
                    {
                        if (count_marche <= 10)
                        {
                            dispopowerProduction = productionGoal * (count_marche / 10);
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
                            dispopowerProduction = productionGoal * (count_arret / 10);
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
            return dispopowerProduction;
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
   
        public WindFarm(string name, Weather meteo) : base(name)
        {
            SetPriorityLevel(PLGasPowerPlant);
            OutputLine.SetPriorityLevel(GetPriorityLevel);
            this.meteo = meteo;
            constantProduction = false;
            adjustableProduction = true;
            Start();
        }
        public override double DisponibleProduction()
        {
            windspeed = meteo.GetWindspeed;
            powerProduction = 1000 * windspeed;
            return powerProduction;
        }

        public override double UpdatePowerPlant()
        {
            if (IsWorking == true)
            {
                powerProduction = DisponibleProduction();
                claimedPower = UpdateClaimedPower();

                if (reduceProduction == true)
                {
                    powerProduction /= 2;
                }
 
                double surplus = powerProduction - claimedPower;

                if (surplus > 0)
                {
                    if (battery.charge + surplus < battery.GetMaximumCharge)
                    {
                        battery.charge += surplus;
                        return claimedPower;
                    }
                    else
                    {
                        return powerProduction;
                    }
                }
                else
                {
                    return powerProduction;
                }
            }
            else
            {
                powerProduction = 0;
                return powerProduction;
            }
        }

        public bool ReduceProduction { set { reduceProduction = true; } }
        public bool StopReduceProduction { set { reduceProduction = false; } }
      
    }

    public class SolarPowerPlant : PowerPlant
    {
        Weather meteo;
        double sunlight;

        public SolarPowerPlant(string name, Weather meteo) : base(name)
        {
            SetPriorityLevel(PLGasPowerPlant);
            OutputLine.SetPriorityLevel(GetPriorityLevel);
            this.meteo = meteo;
            constantProduction = false;
            adjustableProduction = false;
            Start();
        }
        public override double DisponibleProduction()
        {
            sunlight = meteo.GetSunlight;
            powerProduction = 2000 * sunlight;
            return powerProduction;
        }
    }

    public class PurchaseAbroad : PowerPlant
    {
        Market market;
        
        public PurchaseAbroad(string name, Market market) : base(name)
        {
            SetPriorityLevel(PLGasPowerPlant);
            OutputLine.SetPriorityLevel(GetPriorityLevel);
            OutputLine.SetIsMarketLine(true);
            //OutputLine.GetInputNode.SetHasMarket(true);
            //OutputLine.GetOutputeNode.SetHasMarket(true);
            OutPutNode.SetHasMarket(true);
            //OutputLine.GetInputNode.SetMyMarket(this); 
            OutPutNode.SetMyMarket(this);
            this.market = market;
            Start();
        }
        public override double UpdatePowerPlant()
        {
            claimedPower = UpdateClaimedPower();
            powerProduction = claimedPower;
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
