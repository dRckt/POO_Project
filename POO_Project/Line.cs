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
            this.SetName(name);
            this.MaxPower = 142;
            //this.inputIsConnected = false;
            //this.outputIsConnected = false;
            this.SetPowerClaimed(0);
            this.SetCurrentPower(0);

            this.IsConsumerLine = false;
            this.IsPowerPlantLine = false;
            this.IsDissipatorLine = false;
    }

        public void SetInputNode(Node inputNode) { this.inputNode = inputNode; }
        public void SetOutPutNode(Node OutPutNode) { this.OutPutNode = OutPutNode; }

        public Node GetInputNode { get { return this.inputNode; } }
        public Node GetOutputNode { get { return this.OutPutNode; } }


        public void SetName(string newName){this.name = newName;}
        public string GetName { get { return name; } }       

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
        public double GetCurrentPower{ get { return currentPower; } }

        public void SetPowerClaimed(double newPowerClaimed){this.PowerClaimed = newPowerClaimed;}
        public double GetPowerClaimed{ get { return PowerClaimed; } }

        public double GetMaxPower{ get { return MaxPower; } }

        public string GetAlertMessage { get { return alertMessage; } }

        public bool GetIsConsumerLine { get { return this.IsConsumerLine; } }
        public bool GetIsPowerPlantLine { get { return this.IsPowerPlantLine; } }
        public void SetIsConsumerLine(bool b)
        {
            this.IsConsumerLine = b;
        }
        public void SetIsPowerPlantLine(bool b)
        {
            this.IsPowerPlantLine = b;
        }

        public void SetIsDissipatorLine(bool b)
        {
            this.IsDissipatorLine = b;
        }
        public bool GetIsDissipatorLine { get { return this.IsDissipatorLine; } }

        

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
