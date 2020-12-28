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

        //private double inputLink;
        //private double outputLink;


        //private bool inputIsConnected;
        //private bool outputIsConnected;

        protected Node inputNode;
        protected Node OutPutNode;

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
        public Node GetInputNode { get { return this.inputNode; } }
        public Node GetOutputNode { get { return this.OutPutNode; } }
        public double GetPowerClaimed { get { return PowerClaimed; } }
        public double GetMaxPower { get { return MaxPower; } }
        public string GetAlertMessage { get { return alertMessage; } }
        public bool GetIsConsumerLine { get { return this.IsConsumerLine; } }
        public bool GetIsPowerPlantLine { get { return this.IsPowerPlantLine; } }
        public double GetCurrentPower { get { return currentPower; } }
        public bool GetIsDissipatorLine { get { return this.IsDissipatorLine; } }

        /// Propiétés SET
        public void SetInputNode(Node inputNode) { this.inputNode = inputNode; }
        public void SetOutPutNode(Node OutPutNode) { this.OutPutNode = OutPutNode; }
        public void SetPowerClaimed(double newPowerClaimed) { PowerClaimed = newPowerClaimed; }
        public void SetIsConsumerLine(bool b) { IsConsumerLine = b; }
        public void SetIsDissipatorLine(bool b) { IsDissipatorLine = b; }
        public void SetIsPowerPlantLine(bool b) { IsPowerPlantLine = b; }
        public void SetCurrentPower(double newCurrentPower)
        {
            if (newCurrentPower > this.MaxPower)
            {
                this.alertMessage = String.Format("Required power on {0} is too high", this.name);
            }
            else
            {
                this.currentPower = newCurrentPower;
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
