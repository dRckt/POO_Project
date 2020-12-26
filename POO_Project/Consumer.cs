using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    class Consumer
    {
        private string name;

        private Line inputLine;

        private double claimingPower;
        private double missingPower;

        private string alertMessage;
        private double buyingPrice;

        public Consumer(string name, Line inputLine, double buyingPrice)
        {
            this.setName(name);
            this.setInputLine(inputLine);
            this.setBuyingPrice(buyingPrice);
            this.cleanAlertMessage();

            this.setClaimingPower(0); //consomme 0 � la cr�ation

            this.setMissingPower();
        }


        public void setName(string newName)
        {
            this.name = newName;
        }
        public string getName()
        {
            return this.name;
        }


        public void setInputLine(Line newInputLine)
        {
            this.inputLine = newInputLine;
        }
        public Line getInputLine()
        {
            return this.inputLine;
        }


        public void setClaimingPower(double newClaimingPower)
        {
            this.claimingPower = newClaimingPower;
        }
        public double getClaimingPower()
        {
            return this.claimingPower;
        }

        public void setMissingPower()
        {
            double a = this.claimingPower;
            double b = this.inputLine.getCurrentPower();
            this.missingPower = a - b;  //energie manquante = energie demand�e - energie recue

            if (this.missingPower > 0)
            {
                this.alertMessage = String.Format("The consumer {0} is missing {1}W.", this.name, this.missingPower);
            }
            else
            {
                this.alertMessage = String.Format("The consumer {0} is receiving {1}W in excess.", this.name, (-this.missingPower));
            }
        }

        public double getMissingPower()
        {
            return this.missingPower;
        }

        public void cleanAlertMessage()
        {
            this.alertMessage = "";
        }
        public string getAlertMessage()
        {
            return this.alertMessage;
        }



        public void setBuyingPrice(double newBuyingPrice)
        {
            this.buyingPrice = newBuyingPrice;
        }
        public double getBuyingPrice()
        {
            return this.buyingPrice;
        }
    }
}
