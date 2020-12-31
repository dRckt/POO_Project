using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Line
    {
        private string name;
        private double currentPower;

        private double PowerClaimed;

        private double MaxPower;
        private string alertMessage;

        protected Node inputNode;
        protected Node OutputNode;

        private bool IsConsumerLine;
        private bool IsPowerPlantLine;
        private bool IsDissipatorLine;

        private PowerPlant myPowerPlant;
        private Consumer myConsumer;

        public Line(string name)
        {
            this.name = name;
            MaxPower = 75000;
            PowerClaimed = 0;
            SetCurrentPower(0);

            IsConsumerLine = false;
            IsPowerPlantLine = false;
            IsDissipatorLine = false;
        }

        /// Propriétés GET
        public string GetName { get { return name; } }
        public double GetMaxPower { get { return MaxPower; } }
        public double GetCurrentPower { get { return currentPower; } }
        public Node GetInputNode { get { return inputNode; } }
        public Node GetOutputNode { get { return OutputNode; } }



        public double GetPowerClaimed { get { return PowerClaimed; } }                 
        public string GetAlertMessage { get { return alertMessage; } }

        public bool GetIsConsumerLine { get { return IsConsumerLine; } }
        public bool GetIsDissipatorLine { get { return IsDissipatorLine; } }
        public bool GetIsPowerPlantLine { get { return IsPowerPlantLine; } }
        public PowerPlant GetMyPowerPlant { get { return myPowerPlant; } }
        public Consumer GetMyConsumer { get { return myConsumer; } }

        /// Methodes SET
        public void SetInputNode(Node inputNode) { this.inputNode = inputNode; }
        public void SetOutputNode(Node OutputNode) { this.OutputNode = OutputNode; }
        public void SetIsConsumerLine(bool b) { IsConsumerLine = b; }
        public void SetIsDissipatorLine(bool b) { IsDissipatorLine = b; }
        public void SetIsPowerPlantLine(bool b) 
        { 
            IsPowerPlantLine = b;
            if (b)
            {
                SetMaxPower(250000); //Les lignes des centrales sont plus grosses par défaut
            }
        }
        public void SetMyPowerPlant(PowerPlant p) { myPowerPlant = p; }
        public void SetMyConsumer(Consumer c) { myConsumer = c; }

        public void SetMaxPower(double newMaxPower) { MaxPower = newMaxPower; }
        public void SetCurrentPower(double newCurrentPower)
        {
            if (newCurrentPower > MaxPower)
            {
                alertMessage = String.Format("Required power on {0} is too high", name);
                currentPower = MaxPower;    
            }
            else
            {
                currentPower = newCurrentPower;
            }
        }
        /////////////////////////////////////////////////////
        public void SetPowerClaimed(double newPowerClaimed)  //ne doit etre appellé que depuis les lignes qui rentrent dans consumer
        { 
            PowerClaimed = newPowerClaimed;
            if (GetIsPowerPlantLine)
            {
                Console.WriteLine(""); //pass
            }
            else
            {
                GetInputNode.UpdatePowerClaimed();
            }   
            if (GetIsConsumerLine)
            {
                //MESSAGE DE NOTIF:: UN UTILISATEUR A CHANGE DE CLAIMING
            }
        }     
        public double GetDisponiblePower()
        {
            if (GetIsPowerPlantLine)
            {
                return 1000;  //POUR TEST
                //return GetMyPowerPlant.DisponibleProduction();

            }
            else
            {
                return GetInputNode.GetDisponiblePower(this);
            }
            
        }

        /////////////////////////////////////////////////////
      
        public void UpdateCurrentPower(double newCurrentPower)  //ne doit etre appellée que pour les lignes qui sortent de central
        {
            if (GetDisponiblePower() > newCurrentPower)
            {
                SetCurrentPower(newCurrentPower);
            }
            else
            {
                SetCurrentPower(GetDisponiblePower());
                ////MESSAGE D'ALERTE :: Il manque du courant (newCurrentPower-GetDisponiblePower()) sur une des lignes
            }

            if (GetIsConsumerLine)
            {
                ///MESSAGE DE NOTIF? changement de puissance d'entrée pour le consumer
            }
            else
            {
                GetOutputNode.UpdateCurrentPower();
            }
            

        }

        /////////////////////////////////////////////////////


    }
}
