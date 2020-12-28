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
        protected Node OutPutNode;

        //private double inputLink;
        //private double outputLink;


        //private bool inputIsConnected;
        //private bool outputIsConnected;

        private bool IsConsumerLine;
        private bool IsPowerPlantLine;

        private bool IsDissipatorLine;

        public Line(string name)
        {
            this.name = name;
            MaxPower = 142;
            //this.inputIsConnected = false;
            //this.outputIsConnected = false;
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
        public Node GetOutputNode { get { return OutPutNode; } }

        public double GetPowerClaimed { get { return PowerClaimed; } }                        // D : inutile ici 
        public string GetAlertMessage { get { return alertMessage; } }
        public bool GetIsConsumerLine { get { return IsConsumerLine; } }
        public bool GetIsPowerPlantLine { get { return IsPowerPlantLine; } }  
        public bool GetIsDissipatorLine { get { return IsDissipatorLine; } }

        /// Methodes SET
        public void SetInputNode(Node inputNode) { this.inputNode = inputNode; }
        public void SetOutPutNode(Node OutPutNode) { this.OutPutNode = OutPutNode; }
        public void SetPowerClaimed(double newPowerClaimed) { PowerClaimed = newPowerClaimed; }
        public void SetIsConsumerLine(bool b) { IsConsumerLine = b; }
        public void SetIsDissipatorLine(bool b) { IsDissipatorLine = b; }
        public void SetIsPowerPlantLine(bool b) { IsPowerPlantLine = b; }
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


        /*
        public void SetInputLink(double newInputLink)
        {
            this.inputLink = newInputLink;
            this.inputIsConnected = true;
        }
        public void Set
        
        
        
        
        OutputLink(double newOutputLink)
        {
            this.outputLink = newOutputLink;
            this.outputIsConnected = true;
        }

        public double GetInputLink()
        {
            return this.inputLink;
        }
        public double GetOutputLink()
        {
            return this.outputLink;
        }
        */
        
    }
}
