using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Node
    {
        protected double PriorityLevel;

        protected List<Line> InputLineList;
        protected List<Line> OutputLineList;
        private string Name;

        private Line MarketLine;
        private bool HasMarket;
        
        private PowerPlant myPowerPlant;
        private PurchaseAbroad myMarket;

        protected List<string> AlertMessageList;

        
        public Node(string name)
        {
            Name = name;
            InputLineList = new List<Line> { };
            OutputLineList = new List<Line> { };
            PriorityLevel = 0;
            AlertMessageList = new List<string> { };


        }

        // recupere la liste des lignes d'entrées
        public List<Line> GetInputLineList { get { return InputLineList; } }

        // recupere la liste des lignes de sorties
        public List<Line> GetOutputLineList { get { return OutputLineList; } }

        public virtual string GetInputState()
        {
            return "PROGRAM IS BUILDING ...";
        }
        public virtual string GetOutputState()
        {
            return "PROGRAM IS BUILDING ...";
        }
        public List<string> GetAlertMessageList() { return AlertMessageList;  }
        public void ResetAlertMessageList() { AlertMessageList = new List<string> { }; }

        public bool GetHasMarket { get { return HasMarket; } }
        public void SetHasMarket(bool b) { HasMarket = b; }

        public PurchaseAbroad GetMyMarket { get { return myMarket; } }
        public void SetMyMarket(PurchaseAbroad m) { myMarket = m; }

        // recupere le nom du noeud
        public string GetName { get { return Name; } }

        // ajoute une ligne d'entrée
        public void AddInputLineToList(Line newInputLine)
        {
            InputLineList.Add(newInputLine);
            UpdatePriorityLevel();
        }

        // ajoute une ligne de sortie
        public void AddOutputLineToList(Line newOutputLine)
        {
            OutputLineList.Add(newOutputLine);
            UpdatePriorityLevel();
        }


        //pour les noeuds de distribution
        public void ResetInputLineList(Line newInputLine)
        {
            InputLineList = new List<Line> { newInputLine };
            UpdatePriorityLevel();

        }
        //pour les noeuds de concentration
        public void ResetOutputLineList(Line newOutputLine)
        {
            OutputLineList = new List<Line> { newOutputLine };
            UpdatePriorityLevel();
        }

        public void UpdatePriorityLevel()
        {
            double priorityLevel = 0;
            foreach (Line line in InputLineList)
            {
                if (line.GetPriorityLevel > priorityLevel)
                {
                    priorityLevel = line.GetPriorityLevel;
                }
            }
            PriorityLevel = priorityLevel;
            foreach (Line line in OutputLineList)
            {
                line.SetPriorityLevel(PriorityLevel);
            }
        }
        public double GetPriorityLevel { get { return PriorityLevel; } }

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
                if (line.GetIsMarketLine) { MarketLine = line; }
                else { n_set.Add(line, line.GetDisponiblePower()); }
            }

            bool maj = false;
            for (double i = 0; i<10; i++)
            {
                foreach (Line line in InputLineList)
                {
                    if (line.GetIsMarketLine){}
                    else
                    {
                        if (line.GetPriorityLevel == i)
                        {
                            if (PowerClaimedOnNode > n_set[line])
                            {
                                if (line.GetPowerClaimed != n_set[line])
                                {
                                    maj = true;
                                    line.SetPowerClaimed(n_set[line]);
                                }
                                PowerClaimedOnNode -= n_set[line];
                            }
                            else
                            {
                                if (line.GetPowerClaimed != PowerClaimedOnNode)
                                {
                                    maj = true;
                                    line.SetPowerClaimed(PowerClaimedOnNode);
                                }
                                PowerClaimedOnNode = 0;
                            }
                        }
                    }
                    
                    
                }
            }
            if (maj) { AlertMessageList.Add(String.Format("Mise à jour:: {0} à modifié la distribution de ses requêtes de puissance", GetName)); }
            if (PowerClaimedOnNode != 0)
            {
                if (GetHasMarket) 
                {
                    MarketLine.SetPowerClaimed(PowerClaimedOnNode);
                    AlertMessageList.Add(String.Format("ALERTE :: Le noeud {0} à du acheter {1}W.", GetName, PowerClaimedOnNode));
                }
                else 
                {
                    AlertMessageList.Add(String.Format("ALERTE :: Le noeud {0} ne reçoit pas assez de puissance pour satisfaire la demande. Il lui manque {1}W.", GetName, PowerClaimedOnNode));
                }
                
            }
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
                if (line == avalLine){/*pass*/}
                else{disponiblePower -= line.GetPowerClaimed;}
            }
            return disponiblePower;
        }


        public void UpdateCurrentPower()
        {
            double CurrentPowerOfNode = 0;
            bool maj = false;
            foreach (Line line in InputLineList){ CurrentPowerOfNode += line.GetCurrentPower;}
            foreach (Line line in OutputLineList)
            {
                if (line.GetIsDissipatorLine) {/*pass*/}
                else
                {
                    if (CurrentPowerOfNode>0)
                    {
                        if (line.GetPowerClaimed <= CurrentPowerOfNode)
                        {
                            if (line.GetPowerClaimed != line.GetCurrentPower)
                            {
                                line.UpdateCurrentPower(line.GetPowerClaimed);
                                maj = true;
                            }

                            CurrentPowerOfNode -= line.GetPowerClaimed;
                        }
                        else
                        {
                            if (CurrentPowerOfNode != line.GetCurrentPower)
                            {
                                line.UpdateCurrentPower(CurrentPowerOfNode);
                                maj = true;
                            }
                            CurrentPowerOfNode = 0;
                            AlertMessageList.Add(String.Format("ALERTE :: Il manque {0}W sur le noeud {1} ", (line.GetPowerClaimed - CurrentPowerOfNode), GetName));
                        }
                    }
                    else { line.UpdateCurrentPower(0); }
                    
                }
            }

            if (maj)
            {
                AlertMessageList.Add(String.Format("Le noeud {0} a mis à jour sa distribution de puissance", GetName));
            }
            foreach (Line line in OutputLineList)
            {
                if (line.GetIsDissipatorLine){
                    line.SetCurrentPower(CurrentPowerOfNode);
                    if (CurrentPowerOfNode > 0) 
                    { 
                        AlertMessageList.Add(String.Format("La ligne de dissipation {0} est active.", line.GetName));
                    }
                }
            }
        }
    }
}
