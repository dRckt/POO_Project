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

        public Line(string name)
        {
            this.name = name;
            MaxPower = 142;
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



        public double GetPowerClaimed { get { return PowerClaimed; } }                        // D : inutile ici //R: utile dans manager
        public string GetAlertMessage { get { return alertMessage; } }

        public bool GetIsConsumerLine { get { return IsConsumerLine; } }
        public bool GetIsPowerPlantLine { get { return IsPowerPlantLine; } }
        public PowerPlant GetMyPowerPlant { get { return myPowerPlant;  } }
        public bool GetIsDissipatorLine { get { return IsDissipatorLine; } }

        /// Methodes SET
        public void SetInputNode(Node inputNode) { this.inputNode = inputNode; }
        public void SetOutputNode(Node OutputNode) { this.OutputNode = OutputNode; }


        public void SetIsConsumerLine(bool b) { IsConsumerLine = b; }
        public void SetIsDissipatorLine(bool b) { IsDissipatorLine = b; }
        public void SetIsPowerPlantLine(bool b) { IsPowerPlantLine = b; }
        public void SetMyPowerPlant(PowerPlant p) { myPowerPlant = p; }
        public void SetCurrentPower(double newCurrentPower)
        {
            if (newCurrentPower > MaxPower)
            {
                alertMessage = String.Format("Required power on {0} is too high", name);
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
                ////MESSAGE D'ALERTE :: Il manque du courant sur une des lignes
            }

            if (GetIsConsumerLine)
            {
                
            }
            else
            {
                GetOutputNode.UpdateCurrentPower();
            }
            

        }

        /////////////////////////////////////////////////////


    }
}
