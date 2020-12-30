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
            SetPowerClaimed(0);
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
        public void SetPowerClaimed(double newPowerClaimed) { PowerClaimed = newPowerClaimed; }
        public void SetIsConsumerLine(bool b) { IsConsumerLine = b; }
        public void SetIsDissipatorLine(bool b) { IsDissipatorLine = b; }
        public void SetIsPowerPlantLine(bool b)
        {
            IsPowerPlantLine = b;
            GetOutputNode.SetIsPowerPlantNode(b);
        }
        public void SetMyPowerPlant(PowerPlant p)
        {
            powerPlant = p;
            GetOutputNode.SetMyPowerPlant(p);
        }
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


        public double AskDisponiblePower()
        {
            double DisponiblePower;
            if (IsPowerPlantLine)
            {
                //////Demander a la centrale combien elle peut me fournir (Damien)
                ///double DisponiblePower = myPowerPlant.AskDisponiblePower();
                DisponiblePower = 0; // stocjer la réponser dans cette variable
            }
            else
            {
                Node InputNode = GetInputNode;
                DisponiblePower = InputNode.AskDisponiblePower(); 
            }
            
            return DisponiblePower;
        }
    }
}
