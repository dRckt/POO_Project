using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Manager
    {

        protected List<PowerPlant> PowerPlantList;
        protected List<Consumer> ConsumerList;




        public Manager()
        {
            Console.WriteLine("");
            this.PowerPlantList = new List<PowerPlant> { };
            this.ConsumerList = new List<Consumer> { };
        }

        public PowerPlant CreateNewPowerPlant()
        {
            //interction avec le terminal pour donner les paramètres de la centrale (type, production, etc..)
            //!!centrale éteinte quand elle est créé

            //voir type de centrale qu'on crée (nucleaire, gaz, ..) mais cas général:
            PowerPlant NewPowerPlant = new PowerPlant("<nom choisi par input terminal>");
            this.PowerPlantList.Add(NewPowerPlant);
            
            return NewPowerPlant;
        }

        public Consumer CreateNewConsumer()
        {
            //interction avec le terminal pour donner les paramètres du consommateur

            //voir type de consommateur qu'on créé, mais cas général:
            Consumer NewConsumer = new Consumer("<nom choisi par input terminal>");
            this.ConsumerList.Add(NewConsumer); 

            return NewConsumer;
        }

        public void ConnectDistributionToConcentrationNode(string ConnexionLineName, Node DistributionNode, Node ConcentrationNode)
        {
            Line ConnexionLine = new Line(ConnexionLineName);
            DistributionNode.AddOutputLineToList(ConnexionLine);
            ConcentrationNode.AddInputLineToList(ConnexionLine);

        }

        public void UpdateClaimingOfConsumer() //devra probablement prendre un dictionnaire en param, voir interaction avec terminal pour récupérer ce dict
        {
            //parcourir tous les consumer (clé) du dict et leur attribuer un nouveau claimingPower (valeur)
            
            //
        }
        public void UpdatePowerOfPowerPlant()
        {
            //(éventuellement d'abord appel de la mise a jour des puissances réclamées par les clients (UpdateClaimingOfConsumer), voir interactions avc terminal)
            //UpdateClaimingOfConsumer();

            foreach (PowerPlant PowerPlant in this.PowerPlantList)
            {

                List<Line> LineList = new List<Line> { PowerPlant.GetOutPutLine };
                double NewPowerClaimed = GetPowerClaimed(LineList);

                ///// -- DAMIEN -- /////
                ///// -- DAMIEN -- /////
                ///
                ///   ici mettre a jour la puissance de la centrale, nouvelle puissance requise = NewPowerClaimed
                ///
                ///// -- DAMIEN -- /////
                ///// -- DAMIEN -- /////
            }

        }

        public double GetPowerClaimed(List<Line> LineList)
        {
            double sum = 0;
            foreach (Line line in LineList)
            {
                if (line.GetIsConsumerLine )
                {
                    sum += line.GetPowerClaimed;
                }
                else if (line.GetIsDissipatorLine) 
                {
                    sum -= line.GetCurrentPower;  //Ce qui etait en trop sur la ligne dissipative est récupéré
                    line.SetCurrentPower(0);  //puissance de la ligne dissipative mise a 0
                }
                else
                {
                    Node node = line.GetOutputNode;
                    sum += GetPowerClaimed(node.GetOutputLineList);
                }
            }
            return sum;
        }
    }
}
