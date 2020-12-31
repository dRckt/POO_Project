using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Manager
    {

        protected List<PowerPlant> PowerPlantList;
        protected List<Consumer> ConsumerList;

        private Interface i;
        private Market market;
        private WeatherManager weather_manager;
        private Clock clock;
              
        public Manager()
        {
            PowerPlantList = new List<PowerPlant> { };
            ConsumerList = new List<Consumer> { };

            i = new Interface();
            weather_manager = new WeatherManager();
            market = new Market();
            clock = new Clock();
        }
        
        // creation d'une centrale
        public PowerPlant CreateNewPowerPlant()
        {
            // Communication avec la console pour créer la centrale
            PowerPlant NewPowerPlant = i.CreateNewPowerPlant(weather_manager, clock, market);
            NewPowerPlant.GetOutPutLine.SetMyPowerPlant(NewPowerPlant);
            NewPowerPlant.GetOutputNode.SetIsPowerPlantNode(true);
            //NewPowerPlant.

            PowerPlantList.Add(NewPowerPlant);
            Console.WriteLine(String.Format("La centrale {0} a bien été créée.", NewPowerPlant.GetName));

            return NewPowerPlant;
        }
       
        public Consumer CreateNewConsumer()
        {
            // Communication avec la console pour créer le consomateur
            Consumer NewConsumer = i.CreateNewConsumer(weather_manager, clock);

            Console.WriteLine(String.Format("Le consommateur {0} a bien été créée.", NewConsumer.GetName));
            ConsumerList.Add(NewConsumer);
            
            return NewConsumer;
        }

        public List<PowerPlant> GetPowerPlantList { get { return PowerPlantList; } }
        public List<Consumer> GetConsumerList { get { return ConsumerList; } }



        // Connecter un noeud de concentration à un noeud de distribution
        public void ConnectConcentrationToDistributionNode(ConcentrationNode ConcentrationNode, DistributionNode DistributionNode)
        {
            // crée la ligne de connexion qui va etre la premiere ligne de la liste des lignes de sortie du noeud de concentration (la seule)
            Line ConnexionLine = ConcentrationNode.GetOutputLineList[0];

            DistributionNode.SetInputLine(ConnexionLine);
            DistributionNode.ResetInputLineList(ConnexionLine);

            ConnexionLine.SetInputNode(ConcentrationNode);
            ConnexionLine.SetOutputNode(DistributionNode);
        }

        // Connecter un noeud de distribution à un noeud de concentration
        public void ConnectDistributionToConcentrationNode(string ConnexionLineName, DistributionNode DistributionNode, ConcentrationNode ConcentrationNode)
        {
            // creation d'une nouvelle ligne
            Line ConnexionLine = new Line(ConnexionLineName);

            ConcentrationNode.AddInputLineToList(ConnexionLine);
            DistributionNode.AddOutputLineToList(ConnexionLine);

            ConnexionLine.SetInputNode(DistributionNode);
            ConnexionLine.SetOutputNode(ConcentrationNode);

        }

        public void ConnectDistributionToDistributionNode(DistributionNode amontNode, DistributionNode avalNode)
        {
            Line ConnexionLine = avalNode.GetInputLine;

            amontNode.AddOutputLineToList(ConnexionLine);

            ConnexionLine.SetInputNode(amontNode);
            ConnexionLine.SetOutputNode(avalNode);

        }

        public void ConnectConcentrationToConcentrationNode(ConcentrationNode amontNode, ConcentrationNode avalNode)
        {
            Line ConnexionLine = amontNode.GetOutputLine;

            avalNode.AddInputLineToList(ConnexionLine);

            ConnexionLine.SetInputNode(amontNode);
            ConnexionLine.SetOutputNode(avalNode);

        }




        public void UpdateClaimingOfConsumer() //devra probablement prendre un dictionnaire en param, voir interaction avec terminal pour récupérer ce dict
        {
            //parcourir tous les consumer (clé) du dict et leur attribuer un nouveau claimingPower (valeur)
            foreach (Consumer consumer in ConsumerList)
            {
                consumer.getInputLine.GetInputNode.SetClaimedPowerOfInputLines_Concentration();
            }
            //
        }
        public void UpdatePowerOfPowerPlant()
        {
            //(éventuellement d'abord appel de la mise a jour des puissances réclamées par les clients (UpdateClaimingOfConsumer), voir interactions avc terminal)
            //UpdateClaimingOfConsumer();

            foreach (PowerPlant PowerPlant in PowerPlantList)
            {

                List<Line> LineList = new List<Line> { PowerPlant.GetOutPutLine };
                double NewPowerClaimed = GetPowerClaimed(LineList);
                //double MissingPower = NewPowerClaimed - PowerPlant.Production(); //Ca va puer???

                                
                
                ///// -- DAMIEN -- /////
                ///// -- DAMIEN -- /////
                ///
                ///  ici mettre a jour la puissance de la centrale
                ///
                ///// -- DAMIEN -- /////
                ///// -- DAMIEN -- /////
                ///
                /// MESSAGE NOTIFICATION
                /// message de modification de production à une centrale,
                /// message d'arrêt d'une centrale
                /// message de démarrage d'une centrale,  (missing power trop élevé = démarrage d'une centrale?)
                
                UpdateClaimingOfConsumer();
            }

        }

        
        public double GetPowerClaimed(List<Line> LineList)
        {
            /* !!! Pour l'instant récupère la somme de toutes les puissances demandées sur une liste de ligne
             *  si plusieurs centrales alimentent un meme consommateur, ca pourrait poser probleme 
             *  =>demander au max a une centrale
             *  
             * 
             */
            double sum = 0;
            foreach (Line line in LineList)
            {
                if (line.GetIsConsumerLine )
                {
                    sum += line.GetPowerClaimed;
                    Console.WriteLine("LineConsumer ::: {0}, {1}", line, line.GetName);
                }
                else if (line.GetIsDissipatorLine) 
                {
                    Console.WriteLine(""); //pass
                }
                else
                {
                    Console.WriteLine("Line ::: {0}, {1}", line, line.GetName);
                    Node node = line.GetOutputNode;
                    Console.WriteLine("Node ::: {0}", node, node.GetName);
                    sum += GetPowerClaimed(node.GetOutputLineList);  
                    //probleme?? un ligne n'a pas de OutputNode?!
                    // OU un noeud n'a pas de outputlineList
                }
            }
            return sum;
        }

        public void PropagatePowerClaimed(Consumer consumer)
        {
            Line inputLine = consumer.GetInputNode.GetOutputLine;
            double DisponiblePower = inputLine.AskDisponiblePower();

            consumer.GetInputNode.DividePowerClaimed();
        }

        public  void Menu()
        {
            string instruction = i.Menu();
            switch (instruction)
            {
                case "p":
                    {
                        CreateNewPowerPlant();
                        break;
                    }
                case "c":
                    {
                        CreateNewConsumer();
                        break;
                    }
                default:
                    {
                        i.InvalideInput();
                        break;
                    }
            }
            
            Menu();
            
        }

    }
}
