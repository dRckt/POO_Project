using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class DistributionNode : Node
    {
        private Line InputLine;
        private Line DissipatorLine;

        private double InputPower;
        private double MaxPower;
        private List<string> AlertMessageList;


        public DistributionNode(string name) : base(name)
        {
            string InputLineName = name + "_InputLine";
            InputLine = new Line(InputLineName);

            base.AddInputLineToList(InputLine);

            string DissipatorLineName = name + "_DissipatorLine";
            DissipatorLine = new Line(DissipatorLineName);
            DissipatorLine.SetIsDissipatorLine(true);
            DissipatorLine.SetInputNode(this);


            InputPower = GetInputPower;

            MaxPower = InputLine.GetMaxPower;
            Console.WriteLine(String.Format("Un noeud de distribution nommé {0} a été créé", name));
            InputLine.SetOutputNode(this);
        }

        public double GetInputPower { get { return InputLine.GetCurrentPower; } }

        public Line GetInputLine { get { return this.InputLine; } }
        public Line GetDissipatorLine { get {return this.DissipatorLine; } }

        //public void addOutputLine(Line newOutputLine) { this.OutputLineList.Add(newOutputLine); }


        public void SetInputLine(Line newInputLine)
        {
            this.InputLine = newInputLine;
        }


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
                //OutputLine.SetCurrentPower( /(base.OutputLineList.Count-1));
            }
        }


        public void ShowState()
        {
            string nodeStateMessage = String.Format("Noeud de distribution {0}:: Puissance d'entrée: {1}W  ; Nombre de sorties:  {2}", base.GetName, this.GetInputPower, base.OutputLineList.Count);
            Console.WriteLine(nodeStateMessage);
            foreach (Line OutputLine in base.OutputLineList)
            {
                string lineStateMessage = String.Format("   Ligne {0}:: puissance: {1} ", OutputLine.GetName, OutputLine.GetCurrentPower);
                Console.WriteLine(lineStateMessage);
            }

        }

        public Dictionary<Line, double> DividePowerClaimed()
        {
            Dictionary<Line, double> NewDictLineCoef = new Dictionary<Line, double>();
            NewDictLineCoef.Add(GetInputLine, 1);
            return NewDictLineCoef;
        }

            //public bool GetIsCentralNode { get { return this.IsCentralNode; } }
            //public void SetIsCentralNode(bool b) { this.IsCentralNode = b; }



        }
    }
