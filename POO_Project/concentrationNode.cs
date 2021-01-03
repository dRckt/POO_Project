using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class ConcentrationNode : Node
    {
        private Line OutputLine;
        private double OutputPower; //peut etre inutile puisqu'on a jamais besoin de stocker la variable, a chaque fois qu'on veut savoir on recalcule la somme pour mettre a jour
        private double PowerClaimed;
        private double MaxPower;
        private List<string> AlertMessageList;

        public ConcentrationNode(string name) : base(name)
        {
            string OutputLineName = name+"_OutputLine";
            OutputLine = new Line(OutputLineName);
            
            base.AddOutputLineToList(OutputLine);

            OutputPower = GetOutputPower(); //voir commentaire dans header

            MaxPower = OutputLine.GetMaxPower; 
            //Console.WriteLine(String.Format("Une noeud de concentration nommé {0} a été créé.", name));
            OutputLine.SetInputNode(this); //Ce noeud est le noeud d'entrée de la ligne de sortie

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
        
        public Line GetOutputLine{ get { return OutputLine; }  }


        public override string GetOutputState()
        {
            if (GetOutputLineList[0].GetOutputConnectedToNode || GetOutputLineList[0].GetIsConsumerLine)
            {
                return "connected";
            }
            else { return "not connected"; }
        }

        public override string GetInputState()
        {
            if (GetInputLineList.Count > 0)
            {
                return "connected";
            }
            else { return "not connected"; }
        }
        ///PROGRAM IS BUILDING ...
        public void ShowState()
        {
            string nodeStateMessage = String.Format("Noeud de concentration {0}:: Nombre d'entrées: {1}  ;  Puissance de sortie: {2}W /// claimed on output {3} ", base.GetName, base.InputLineList.Count, this.GetOutputPower(), base.OutputLineList[0].GetPowerClaimed);
            Console.WriteLine(nodeStateMessage);
            foreach (Line inputLine in base.InputLineList)
            {
                string lineStateMessage = String.Format("   Ligne {0}:: puissance: {1}  /// claimed on input {2} ", inputLine.GetName, inputLine.GetCurrentPower, inputLine.GetPowerClaimed);
                Console.WriteLine(lineStateMessage);
            }
            
        }

 



    }
}