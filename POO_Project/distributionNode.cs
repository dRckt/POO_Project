using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class distributionNode
    {
        private Line inputLine;
        private List<Line> outputLineList = new List<Line> { };

        private Line dissipatorLine;

        private double inputPower;
        private double powerClaimed;
        private double maxPower;
        private List<string> alertMessageList;
        private string distributionNodeName;

        protected bool IsCentralNode;


        public distributionNode(string name)
        {
            string inputLineName = name + "_inputLine";
            this.inputLine = new Line(inputLineName);
            this.outputLineList = new List<Line> { };

            string dissipatorLineName = name + "_dissipatorLine";
            this.dissipatorLine = new Line(dissipatorLineName);

            this.inputPower = this.getInputPower;
            this.powerClaimed = this.getPowerClaimed();

            this.maxPower = this.inputLine.getMaxPower;

            this.IsCentralNode = false;
        }

        public double getInputPower { get { return inputLine.getCurrentPower; } }
        public double getPowerClaimed()
        {
            double sum = 0;
            foreach (Line outputLine in this.outputLineList)
            {
                sum += outputLine.getCurrentPower;
            }
            return sum;
        }

        public Line getInputLine { get { return this.inputLine; } }
        public Line getDissipatorLine { get {return this.dissipatorLine; } }

        public void addOutputLine(Line newOutputLine) { this.outputLineList.Add(newOutputLine); }




        public void setCurrentPowerOfOutputLines()
        {
            foreach (Line outputLine in this.outputLineList)
            {
                foreach (string alertMessage in this.alertMessageList)
                {
                    if (String.Equals(alertMessage, "Trouver un template pour ici"))
                    {
                        Console.WriteLine("");
                    }
                }
                //outputLine.setCurrentPower("A COMPLETER");
            }
        }


        public void showState()
        {
            string nodeStateMessage = String.Format("Noeud de distribution {0}:: Puissance d'entrée: {1}W  ; Nombre de sorties:  {2}", this.distributionNodeName, this.getInputPower, this.outputLineList.Count);
            Console.WriteLine(nodeStateMessage);
            foreach (Line outputLine in this.outputLineList)
            {
                string lineStateMessage = String.Format("   Ligne {0}:: puissance: {1} ", outputLine.getName, outputLine.getCurrentPower);
                Console.WriteLine(lineStateMessage);
            }

        }

        public bool getIsCentralNode { get { return this.IsCentralNode; } }

    }
}
