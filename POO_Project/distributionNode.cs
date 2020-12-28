using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class DistributionNode : Node
    {
        private Line InputLine;
        //private List<Line> OutputLineList = new List<Line> { };

        private Line DissipatorLine;

        private double InputPower;
        private double PowerClaimed;
        private double MaxPower;
        private List<string> AlertMessageList;
        private string DistributionNodeName;

        protected bool IsCentralNode;


        public DistributionNode(string name) : base()
        {
            string InputLineName = name + "_InputLine";
            this.InputLine = new Line(InputLineName);
            base.AddInputLineToList(InputLine);
            //this.OutputLineList = new List<Line> { };



            string DissipatorLineName = name + "_DissipatorLine";
            this.DissipatorLine = new Line(DissipatorLineName);

            this.InputPower = this.GetInputPower;
            this.PowerClaimed = this.GetPowerClaimed();

            this.MaxPower = this.InputLine.GetMaxPower;

            this.IsCentralNode = false;
        }

        public double GetInputPower { get { return InputLine.GetCurrentPower; } }
        public double GetPowerClaimed()
        {
            double sum = 0;
            foreach (Line OutputLine in this.OutputLineList)
            {
                sum += OutputLine.GetCurrentPower;
            }
            return sum;
        }

        public Line GetInputLine { get { return this.InputLine; } }
        public Line GetDissipatorLine { get {return this.DissipatorLine; } }

        //public void addOutputLine(Line newOutputLine) { this.OutputLineList.Add(newOutputLine); }



        public void SetCurrentPowerOfOutputLines()
        {
            foreach (Line OutputLine in base.OutputLineList)
            {
                foreach (string alertMessage in this.AlertMessageList)
                {
                    if (String.Equals(alertMessage, "Trouver un template pour ici"))
                    {
                        Console.WriteLine("");
                    }
                }
                //OutputLine.SetCurrentPower("A COMPLETER");
            }
        }


        public void showState()
        {
            string nodeStateMessage = String.Format("Noeud de distribution {0}:: Puissance d'entrée: {1}W  ; Nombre de sorties:  {2}", this.DistributionNodeName, this.GetInputPower, base.OutputLineList.Count);
            Console.WriteLine(nodeStateMessage);
            foreach (Line OutputLine in base.OutputLineList)
            {
                string lineStateMessage = String.Format("   Ligne {0}:: puissance: {1} ", OutputLine.GetName, OutputLine.GetCurrentPower);
                Console.WriteLine(lineStateMessage);
            }

        }

        public bool GetIsCentralNode { get { return this.IsCentralNode; } }
        public void SetIsCentralNode(bool b) { this.IsCentralNode = b; }

       

    }
}
