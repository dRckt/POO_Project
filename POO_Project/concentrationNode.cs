using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class ConcentrationNode : Node
    {
        private string ConcentrationNodeName;
        private Line OutputLine;
        //private List<Line> inputLineList = new List<Line>{};

        private double outputPower; //peut etre inutile puisqu'on a jamais besoin de stocker la variable, a chaque fois qu'on veut savoir on recalcule la somme pour mettre a jour

        private double PowerClaimed;

        private double MaxPower;

        private List<string> AlertMessageList;

        //protected bool IsConsumerNode;

        public ConcentrationNode(string name) : base()
        {
            //créé d'abord sa ligne de sortie
            string OutputLineName = name+"_OutputLine";
            this.OutputLine = new Line(OutputLineName);
            base.AddOutputLineToList(OutputLine);
            //liste vide pour ses lignes d'entrée
            //this.inputLineList = new List<Line>{};
            this.outputPower = this.GetOutputPower(); //voir commentaire dans header
            this.PowerClaimed = this.GetPowerClaimed();
            //déclarer quelque chose qui décide sur quelle ligne il réclame combien dans ses entrées

            this.MaxPower = this.OutputLine.GetMaxPower; //puissance max du noeud = puissance max de la ligne de sortie
            Console.WriteLine(String.Format("Une noeud de concentration nommé {0} a été créé.", name));

            //this.IsConsumerNode = false;
        }

        //fait la somme des courants sur ligne d'entrées
        public double GetOutputPower()
        {
            double sum = 0;
            foreach (Line inputLine in base.InputLineList)
            {
                sum += inputLine.GetCurrentPower;
            }
            return sum;
        }

        public double GetPowerClaimed() { return this.OutputLine.GetPowerClaimed; }

        public Line GetOutputLine{ get { return this.OutputLine; }  }
        
        //public void addInputLine(Line newInputLine){ this.inputLineList.Add(newInputLine);}



        public void SetClaimedPowerOfInputLines() 
        { 
            Console.WriteLine("PROGRAMME EN CONSTRUCTION :: doit décider où est ce qu'il réclame du courant"); 
        }




        public void showState()
        {
            string nodeStateMessage = String.Format("Noeud de concentration {0}:: Nombre d'entrées: {1}  ;  Puissance de sortie: {2}W  ", this.ConcentrationNodeName , base.InputLineList.Count , this.GetOutputPower());
            Console.WriteLine(nodeStateMessage);
            foreach (Line inputLine in base.InputLineList)
            {
                string lineStateMessage = String.Format("   Ligne {0}:: puissance: {1}  ", inputLine.GetName, inputLine.GetCurrentPower);
                Console.WriteLine(lineStateMessage);
            }
            
        }

        //public bool getIsConsumerNode{ get { return this.IsConsumerNode; } }
        //public void SetIsConsumerNode(bool b) { this.IsConsumerNode = b; }

    }
}