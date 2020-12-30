using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Node
    {
        protected List<Line> InputLineList;
        protected List<Line> OutputLineList;
        private string Name;

        private bool IsPowerPlantNode;
        private PowerPlant myPowerPlant;

        
        public Node(string name)
        {
            Name = name;
            Console.WriteLine("");
            InputLineList = new List<Line> { };
            OutputLineList = new List<Line> { };
            IsPowerPlantNode = false;

        }

        // recupere la liste des lignes d'entrées
        public List<Line> GetInputLineList { get { return InputLineList; } }

        // recupere la liste des lignes de sorties
        public List<Line> GetOutputLineList { get { return OutputLineList; } }

        public void SetIsPowerPlantNode(bool b)
        {
            IsPowerPlantNode = b;
        }
        public void SetMyPowerPlant(PowerPlant p)
        {
            myPowerPlant = p;
        }
        public PowerPlant GetPowerPlant { get { return myPowerPlant; } }
        
        // recupere le nom du noeud
        public string GetName { get { return Name; } }

        // ajoute une ligne d'entrée
        public void AddInputLineToList(Line newInputLine)
        {
            InputLineList.Add(newInputLine);
        }

        // ajoute une ligne de sortie
        public void AddOutputLineToList(Line newOutputLine)
        {
            OutputLineList.Add(newOutputLine);
        }


        //pour les noeuds de distribution
        public void ResetInputLineList(Line newInputLine)
        {
            InputLineList = new List<Line> { newInputLine };
        }
        //pour les noeuds de concentration
        public void ResetOutputLineList(Line newOutputLine)
        {
            OutputLineList = new List<Line> { newOutputLine };
        }



        public double AskDisponiblePower()
        {
            double DisponiblePower = 0;
            foreach (Line line in InputLineList)
            {

                DisponiblePower += line.AskDisponiblePower();
            }
            return DisponiblePower;
        }


        ////////////////////////////////////// UNIQUEMENT POUR LES NOEUDS DE CONCENTRATION ? //////////////////////////////////////

        // permet de faire une demande aux lignes d'entrées
        public void SetClaimedPowerOfInputLines_Concentration()
        {
            Console.WriteLine("PROGRAMME EN CONSTRUCTION :: doit décider où est ce qu'il réclame du courant");
            foreach (Line line in this.InputLineList)
            {
                // pour chaque ligne d'entrée, declare la demande en prenant la demande de puissance de la premiere ligne de sortie de la liste "ligne de sortie" et la divise par le nombre de ligne d'entrées
                line.SetPowerClaimed(OutputLineList[0].GetPowerClaimed / InputLineList.Count);  //COEFF ?????
            }
        }



    }
}
