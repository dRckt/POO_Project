using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Line
    {
        private string name;
        private double currentPower;

        private double maxPower;
        private string alertMessage;

        private double inputLink;
        private double outputLink;
        private bool inputIsConnected;
        private bool outputIsConnected;

        public Line(string name)
        {
            this.setName(name);
            this.inputIsConnected = false;
            this.outputIsConnected = false;
        }


        public void setName(string newName)
        {
            this.name = newName;
        }
        public string getName { get { return name; } }       



        public void setCurrentPower(double newCurrentPower)
        {
            this.currentPower = newCurrentPower;
        }
        public double getCurrentPower()
        {
            return this.currentPower;
        }



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
    }
}
