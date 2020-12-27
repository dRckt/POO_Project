using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class concentrationNode
    {
        private string concentrationNodeName;
        private Line outputLine;
        private List<Line> inputLineList = new List<Line>{};

        private double outputPower; //peut etre inutile puisqu'on a jamais besoin de stocker la variable, a chaque fois qu'on veut savoir on recalcule la somme pour mettre a jour

        private double powerClaimed;

        private double maxPower;

        private double alertMessage;

        public concentrationNode(string name)
        {
            //créé d'abord sa ligne de sortie
            string outputLineName = name+"_outputLine";
            this.outputLine = new Line(outputLineName);
            //liste vide pour ses lignes d'entrée
            this.inputLineList = new List<Line>{};
            this.outputPower = this.getOutputPower(); //voir commentaire dans header
            this.powerClaimed = this.getPowerClaimed();
            //déclarer quelque chose qui décide sur quelle ligne il réclame combien dans ses entrées

            this.maxPower = this.outputLine.getMaxPower; //puissance max du noeud = puissance max de la ligne de sortie
            Console.WriteLine(String.Format("Une noeud de concentration nommé {0} a été créé.", name));

        }

        public Line getOutputLine{ get { return this.outputLine; }  }

        //fait la somme des courants sur ligne d'entrées
        public double getOutputPower()
        {
            double sum = 0;
            foreach (Line inputLine in this.inputLineList)
            {
                sum += inputLine.getCurrentPower;
            }
            return sum;
        }

        public double getPowerClaimed(){return this.outputLine.getPowerClaimed;}
   

        public void setClaimedPowerOfInputLines() { Console.WriteLine("pass"); }
        public void addInputLine(Line newInputLine){ this.inputLineList.Add(newInputLine);}

        public void showState()
        {
            string nodeStateMessage = String.Format("Noeud de concentration {0}:: Nombre d'entrées: {1}  ;  Puissance de sortie: {2}W  ;  AlertMessage: {3}", this.concentrationNodeName , this.inputLineList.Count , this.getOutputPower() , this.alertMessage);
            Console.WriteLine(nodeStateMessage);
            foreach (Line inputLine in this.inputLineList)
            {
                string lineStateMessage = String.Format("   Ligne {0}:: puissance: {1}  ;  alertMessage: {2}", inputLine.getName, inputLine.getCurrentPower, inputLine.getAlertMessage );
                Console.WriteLine(lineStateMessage);
            }
            
        }

    }
}