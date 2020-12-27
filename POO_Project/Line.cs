using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Line
    {
        private string name;
        private double currentPower;

        private double powerClaimed;

        private double maxPower;
        private string alertMessage;

        //private double inputLink;
        //private double outputLink;


        //private bool inputIsConnected;
        //private bool outputIsConnected;

        public Line(string name)
        {
            this.setName(name);
            this.maxPower = 142;
            //this.inputIsConnected = false;
            //this.outputIsConnected = false;
            this.setPowerClaimed(0);
            this.setCurrentPower(0);
        }



        public void setName(string newName){this.name = newName;}
        public string getName { get { return name; } }       

        public void setCurrentPower(double newCurrentPower)
        {
            if (newCurrentPower > this.maxPower)
            {
                this.alertMessage = String.Format("Required power on {0} is too high", this.name);
            }
            else
            {
                this.currentPower = newCurrentPower;
            }
        }
        public double getCurrentPower{ get { return currentPower; } }

        public void setPowerClaimed(double newPowerClaimed){this.powerClaimed = newPowerClaimed;}
        public double getPowerClaimed{ get { return powerClaimed; } }

        public double getMaxPower{get { return maxPower; } }

        public string getAlertMessage { get { return alertMessage; } }

        /*
        public void setInputLink(double newInputLink)
        {
            this.inputLink = newInputLink;
            this.inputIsConnected = true;
        }
        public void setOutputLink(double newOutputLink)
        {
            this.outputLink = newOutputLink;
            this.outputIsConnected = true;
        }

        public double getInputLink()
        {
            return this.inputLink;
        }
        public double getOutputLink()
        {
            return this.outputLink;
        }
        */
        
    }
}
