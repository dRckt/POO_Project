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
        

        public DistributionNode(string name) : base(name)
        {
            string InputLineName = name + "_InputLine";
            InputLine = new Line(InputLineName);

            base.AddInputLineToList(InputLine);

            string DissipatorLineName = name + "_DissipatorLine";
            DissipatorLine = new Line(DissipatorLineName);
            DissipatorLine.SetIsDissipatorLine(true);
            DissipatorLine.SetInputNode(this);
            base.AddOutputLineToList(DissipatorLine);

            Consumer dissipator = new Dissipator("dissipator_" + name);
            dissipator.getInputLine = DissipatorLine;
            InputPower = GetInputPower;

            MaxPower = InputLine.GetMaxPower;
            //Console.WriteLine(String.Format("Un noeud de distribution nommé {0} a été créé", name));
            InputLine.SetOutputNode(this);
        }

        public double GetInputPower { get { return InputLine.GetCurrentPower; } }
        public Line GetInputLine { get { return this.InputLine; } }
        public Line GetDissipatorLine { get {return this.DissipatorLine; } }

        //public void addOutputLine(Line newOutputLine) { this.OutputLineList.Add(newOutputLine); }

        public override string GetInputState()
        {
            if (GetInputLineList[0].GetInputConnectedToNode || GetInputLineList[0].GetIsPowerPlantLine)
            {
                return "connected";
            }
            else { return "not connected"; }
        }
        public override string GetOutputState()
        {
            if (OutputLineList.Count > 1)
            {
                return "connected";
            }
            else { return "not connected"; }
        }
        public void SetInputLine(Line newInputLine)
        {
            InputLine = newInputLine;
        }


        ///PROGRAM IS BUILDING ...
        public void ShowState()
        {
            string nodeStateMessage = String.Format("Noeud de distribution {0}:: Puissance d'entrée: {1}W  ; Nombre de sorties:  {2}", GetName, GetInputPower, OutputLineList.Count-1);
            Console.WriteLine(nodeStateMessage);
            foreach (Line OutputLine in OutputLineList)
            {
                string lineStateMessage = String.Format("   Ligne {0}:: puissance: {1} ", OutputLine.GetName, OutputLine.GetCurrentPower);
                Console.WriteLine(lineStateMessage);
            }

        }


            //public bool GetIsCentralNode { get { return this.IsCentralNode; } }
            //public void SetIsCentralNode(bool b) { this.IsCentralNode = b; }



        }
    }
