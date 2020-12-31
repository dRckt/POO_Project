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

        private PowerPlant myPowerPlant;

        
        public Node(string name)
        {
            Name = name;
            Console.WriteLine("");
            InputLineList = new List<Line> { };
            OutputLineList = new List<Line> { };


        }

        // recupere la liste des lignes d'entrées
        public List<Line> GetInputLineList { get { return InputLineList; } }

        // recupere la liste des lignes de sorties
        public List<Line> GetOutputLineList { get { return OutputLineList; } }

        
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



        /////////////////////////////////////////////////////

        public void UpdatePowerClaimed()
        {
            double PowerClaimedOnNode = 0;
            foreach (Line line in OutputLineList)
            {
                PowerClaimedOnNode += line.GetPowerClaimed;
            }
            Dictionary<Line, double> n_set = new Dictionary<Line, double> { };
            foreach (Line line in InputLineList)
            {
                n_set.Add(line, line.GetDisponiblePower());
            }

            foreach (Line line in InputLineList)
            {
                if (PowerClaimedOnNode > n_set[line])
                {
                    line.SetPowerClaimed(n_set[line]);
                    PowerClaimedOnNode -= n_set[line];
                    ///MESSAGE :: cette ligne prend tout ce qu'elle peut
                }
                else
                {
                    line.SetPowerClaimed(PowerClaimedOnNode);
                    PowerClaimedOnNode = 0;
                }
            }
            //après ce dernier foreach, si PowerClaimedOnNode != 0 => il manque une centrale
        }

        public double GetDisponiblePower(Line avalLine)
        {
            double disponiblePower = 0;
            foreach (Line line in InputLineList)
            {
                disponiblePower += line.GetDisponiblePower();
            }
            foreach (Line line in InputLineList)
            {
                if (line == avalLine)
                {
                    Console.WriteLine(""); //pass
                }
                else
                {
                    disponiblePower -= line.GetPowerClaimed;
                }
            }
            return disponiblePower;
        }

        /////////////////////////////////////////////////////

        public void UpdateCurrentPower()
        {
            double CurrentPowerOfNode = 0;
            foreach (Line line in InputLineList){ CurrentPowerOfNode += line.GetCurrentPower;}
            foreach (Line line in OutputLineList)
            {
                if (line.GetIsDissipatorLine) { Console.WriteLine(""); } //pass
                else
                {
                    if (line.GetPowerClaimed <= CurrentPowerOfNode)
                    {
                        line.UpdateCurrentPower(line.GetPowerClaimed);
                        CurrentPowerOfNode -= line.GetPowerClaimed;
                    }
                    else
                    {
                        line.UpdateCurrentPower(CurrentPowerOfNode);
                        ////MESSAGE D'ALERTE:: il manque du courant (line.GetPowerClaimed-CurrentPowerOfNode) sur une des lignes
                    }

                }
            }

            //S'il y a une ligne de dissipation on lui envoie ce qu'il reste de courant non-distribué (normalement 0)
            foreach (Line line in OutputLineList)
            {
                if (line.GetIsDissipatorLine){line.SetCurrentPower(CurrentPowerOfNode);}
            }

        }

        /////////////////////////////////////////////////////
    }
}
