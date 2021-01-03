using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Manager
    {

        protected List<PowerPlant> PowerPlantList;
        protected List<Consumer> ConsumerList;

        protected List<PurchaseAbroad> MarketList;

        protected List<Line> LineList;
        protected List<Node> NodeList;

        List<string> AlertMessageList;
        private Market market;
        private WeatherManager weather_manager;
        private Clock clock;

        public Manager()
        {
            PowerPlantList = new List<PowerPlant> { };
            ConsumerList = new List<Consumer> { };

            LineList = new List<Line> { };
            NodeList = new List<Node> { };

            //i = new Interface();  //a retirer
            weather_manager = new WeatherManager();
            market = new Market();
            clock = new Clock();

            AlertMessageList = new List<string> { };
        }

        ///RECUPERATION DES OBJETS/LISTES D'OBJETS DU RESEAU
        public  WeatherManager GetWeatherManager { get { return weather_manager; } }
        public Market GetMarket { get { return market; } }
        public Clock GetClock { get { return clock; } }
        public List<PowerPlant> GetPowerPlantList { get { return PowerPlantList; } }
        public List<Consumer> GetConsumerList { get { return ConsumerList; } }
        public List<Line> GetLineList { get { return LineList; } }
        public List<Node> GetNodeList { get { return NodeList; } }


        ///CREATION DE NOUVELLE CENTRALE
        public PowerPlant CreateNewPowerPlant(string name)
        {
            foreach (PowerPlant pp in PowerPlantList)
            {
                if (pp.GetName == name)
                {
                    Console.WriteLine("La centrale nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewPowerPlant(newName);
                }
            }
            PowerPlant NewPowerPlant = new PowerPlant(name);
            PowerPlantList.Add(NewPowerPlant);
            NodeList.Add(NewPowerPlant.GetOutputNode);
            LineList.Add(NewPowerPlant.GetOutPutLine);
            return NewPowerPlant;
        }
        public PowerPlant CreateNewGasPowerPlant(string name, Market market)
        {
            
            foreach (PowerPlant pp in PowerPlantList)
            {
                if (pp.GetName == name)
                {
                    Console.WriteLine("La centrale nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewGasPowerPlant(newName, market);
                }
            }
            PowerPlant NewPowerPlant = new GasPowerPlant(name, market);
            PowerPlantList.Add(NewPowerPlant);
            NodeList.Add(NewPowerPlant.GetOutputNode);
            LineList.Add(NewPowerPlant.GetOutPutLine);
            return NewPowerPlant;
        }
        public PowerPlant CreateNewNuclearPowerPlant(string name, Market market)
        {
            foreach (PowerPlant pp in PowerPlantList)
            {
                if (pp.GetName == name)
                {
                    Console.WriteLine("La centrale nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewNuclearPowerPlant(newName, market);
                }
            }
            PowerPlant NewPowerPlant = new NuclearPowerPlant(name, market);
            PowerPlantList.Add(NewPowerPlant);
            NodeList.Add(NewPowerPlant.GetOutputNode);
            LineList.Add(NewPowerPlant.GetOutPutLine);
            return NewPowerPlant;
        }
        public PowerPlant CreateNewWindFarm(string name, Weather meteo)
        {
            foreach (PowerPlant pp in PowerPlantList)
            {
                if (pp.GetName == name)
                {
                    Console.WriteLine("La centrale nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewWindFarm(newName, meteo);
                }
            }
            PowerPlant NewPowerPlant = new WindFarm(name, meteo);
            PowerPlantList.Add(NewPowerPlant);
            NodeList.Add(NewPowerPlant.GetOutputNode);
            LineList.Add(NewPowerPlant.GetOutPutLine);
            return NewPowerPlant;
        }
        public PowerPlant CreateNewSolarPowerPlant(string name, Weather meteo)
        {
            foreach (PowerPlant pp in PowerPlantList)
            {
                if (pp.GetName == name)
                {
                    Console.WriteLine("La centrale nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewSolarPowerPlant(newName, meteo);
                }
            }
            PowerPlant NewPowerPlant = new SolarPowerPlant(name, meteo);
            PowerPlantList.Add(NewPowerPlant);
            NodeList.Add(NewPowerPlant.GetOutputNode);
            LineList.Add(NewPowerPlant.GetOutPutLine);
            return NewPowerPlant;
        }

        public PowerPlant CreateNewPurchasedAbroad(string name, Market market)
        {
            foreach (PowerPlant pp in PowerPlantList)
            {
                if (pp.GetName == name)
                {
                    Console.WriteLine("La centrale nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewPurchasedAbroad(newName, market);
                }
            }
            PowerPlant NewPowerPlant = new PurchaseAbroad(name, market);
            PowerPlantList.Add(NewPowerPlant);
            NodeList.Add(NewPowerPlant.GetOutputNode);
            LineList.Add(NewPowerPlant.GetOutPutLine);
            MarketList.Add((PurchaseAbroad)NewPowerPlant);
            return NewPowerPlant;
        }
        ///CREATION DE NOUVEAU CONSUMER
        public Consumer CreateNewConsumer(string name)
        {
            foreach (Consumer c in ConsumerList)
            {
                if (c.GetName == name)
                {
                    Console.WriteLine("Le consomateur nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewConsumer(newName);
                }
            }
            Consumer NewConsumer = new Consumer(name);
            ConsumerList.Add(NewConsumer);
            NodeList.Add(NewConsumer.GetInputNode);
            LineList.Add(NewConsumer.getInputLine);

            return NewConsumer;
        }
        public Consumer CreateNewCity(string name, double nbr_hab, Weather meteo)
        {
            foreach (Consumer c in ConsumerList)
            {
                if (c.GetName == name)
                {
                    Console.WriteLine("La ville nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewCity(newName, nbr_hab, meteo);
                }
            }
            Consumer NewConsumer = new City(name, nbr_hab, meteo);
            ConsumerList.Add(NewConsumer);
            NodeList.Add(NewConsumer.GetInputNode);
            LineList.Add(NewConsumer.getInputLine);
            return NewConsumer;
        }
        public Consumer CreateNewEntreprise(string name, double nbr_machines)
        {
            foreach (Consumer c in ConsumerList)
            {
                if (c.GetName == name)
                {
                    Console.WriteLine("L'entreprise nommée {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewEntreprise(newName, nbr_machines);
                }
            }
            Consumer NewConsumer = new Entreprise(name, nbr_machines);
            ConsumerList.Add(NewConsumer);
            NodeList.Add(NewConsumer.GetInputNode);
            LineList.Add(NewConsumer.getInputLine);
            return NewConsumer;
        }

        ///CREATION DE NOUVEAU NOEUD
        public DistributionNode CreateNewDistributionNode(string name)
        {
            foreach (Node n in NodeList)
            {
                if (n.GetName == name)
                {
                    Console.WriteLine("Le noeud nommé {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewDistributionNode(newName);
                }
            }
            DistributionNode NewDistributionNode = new DistributionNode(name);
            NodeList.Add(NewDistributionNode);
            LineList.Add(NewDistributionNode.GetInputLine);
            return NewDistributionNode;
        }
        public ConcentrationNode CreateNewConcentrationNode(string name)
        {
            foreach (Node n in NodeList)
            {
                if (n.GetName == name)
                {
                    Console.WriteLine("Le noeud nommé {0} existe déjà. Entrez un nouveau nom :", name);
                    string newName = Console.ReadLine();
                    return CreateNewConcentrationNode(newName);
                }
            }

            ConcentrationNode NewConcentrationNode = new ConcentrationNode(name);
            NodeList.Add(NewConcentrationNode);
            LineList.Add(NewConcentrationNode.GetOutputLine);

            PowerPlant shop = new PurchaseAbroad("shop_" + name, market);
            NewConcentrationNode.AddInputLineToList(shop.GetOutPutLine);
            shop.GetOutPutLine.SetOutputNode(NewConcentrationNode);
            shop.GetOutPutLine.SetIsMarketLine(true);

            return NewConcentrationNode;
        }



        ///CONNEXION DE 2 NOEUDS
        
        // Connexion :: ConcentrationNode -> DistributionNode
        public void ConnectConcentrationToDistributionNode(ConcentrationNode ConcentrationNode, DistributionNode DistributionNode)
        {
            // crée la ligne de connexion qui va etre la premiere ligne de la liste des lignes de sortie du noeud de concentration (la seule)
            Line ConnexionLine = ConcentrationNode.GetOutputLineList[0];

            
            DistributionNode.ResetInputLineList(ConnexionLine);
            DistributionNode.SetInputLine(ConnexionLine);
            ConnexionLine.SetInputNode(ConcentrationNode);
            ConnexionLine.SetOutputNode(DistributionNode);
        }
        
        // Connexion :: DistributionNode -> ConcentrationNode
        public void ConnectDistributionToConcentrationNode(string ConnexionLineName, DistributionNode DistributionNode, ConcentrationNode ConcentrationNode)
        {
            foreach (Line l in LineList)
            {
                if (l.GetName == ConnexionLineName)
                {
                    Console.WriteLine("Le noeud nommé {0} existe déjà. Entrez un nouveau nom :", ConnexionLineName);
                    string newName = Console.ReadLine();
                    ConnectDistributionToConcentrationNode(newName, DistributionNode, ConcentrationNode);
                    break;
                }
            }
            // creation d'une nouvelle ligne
            Line ConnexionLine = new Line(ConnexionLineName);
            LineList.Add(ConnexionLine);

            if (DistributionNode.GetHasMarket) { ConnexionLine.SetIsMarketLine(true); }//MarketList.Add(DistributionNode.GetMyMarket); }

            ConcentrationNode.AddInputLineToList(ConnexionLine);
            DistributionNode.AddOutputLineToList(ConnexionLine);

            ConnexionLine.SetInputNode(DistributionNode);
            ConnexionLine.SetOutputNode(ConcentrationNode);

        }
        
        // Connexion :: DistributionNode -> DistributuonNode
        public void ConnectDistributionToDistributionNode(DistributionNode amontNode, DistributionNode avalNode)
        {
            Line ConnexionLine = avalNode.GetInputLine;

            amontNode.AddOutputLineToList(ConnexionLine);

            ConnexionLine.SetInputNode(amontNode);
            ConnexionLine.SetOutputNode(avalNode);

        }
       
        // Connexion :: ConcentrationNode -> ConcentrationNode
        public void ConnectConcentrationToConcentrationNode(ConcentrationNode amontNode, ConcentrationNode avalNode)
        {
            Line ConnexionLine = amontNode.GetOutputLine;

            avalNode.AddInputLineToList(ConnexionLine);

            ConnexionLine.SetInputNode(amontNode);
            ConnexionLine.SetOutputNode(avalNode);

        }


        // UPDATE CONSUMER
        public void UpdateConsumerClaiming()
        {
            foreach (Consumer c in GetConsumerList)
            {
                Line line = c.getInputLine;
                line.SetPowerClaimed(line.GetPowerClaimed);
            }
        }

        // UPDATE POWER PLANT
        public void UpdatePowerOfPowerPlant()
        {
            foreach (PowerPlant PowerPlant in PowerPlantList)
            {
                

                Console.WriteLine(PowerPlant.GetAlertMessage);

                /*
                // si la demande est inférieure à ce que peux fournir la centrale
                if (powerClaimed < PowerPlant.DisponibleProduction())
                {
                    // si la production actuelle de la centrale est differente de la demande
                    if (PowerPlant.() != PowerPlant.GetOutPutLine.GetPowerClaimed)
                    {
                        //      set powerplant production = outputline.getClaimedPower
                        //      alertMessage :: la centrale a modifié sa production


                        ////  ON NE PEUT PAS DECIDER DE LA PRODUCTION DE LA CENTRALE 
                        AlertMessageList.Add(String.Format("La centrale {0} envoie à présent {1}W de sa production dans le réseau.", PowerPlant.GetName, PowerPlant.Production()));
                        AlertMessageList.Add(String.Format("ALERTE :: La centrale {0} est incapable de fournir {1}W, il lui manque {2}W pour satisfaire la demande.", PowerPlant.GetName, PowerPlant.GetOutPutLine.GetPowerClaimed, (PowerPlant.GetOutPutLine.GetPowerClaimed- PowerPlant.Production())));

                        //Console.WriteLine("La centrale a modifié sa production");                        
                    }

                    // La production de la centrale est copiée sur la ligne de sortie
                    PowerPlant.GetOutPutLine.SetCurrentPower(PowerPlant.Production());
                }
                else
                {
                    //if powerlant production != powerplant disponible production:
                    //      set innjecter power claimed dans le réseau et Production()-powerclaimed dans la batterie, sauf si elle est pleine
                    //      message d'alerte:: la centrale a mis a jour sa poduction
                    //Dans tous les cas:
                    PowerPlant.GetOutPutLine.SetCurrentPower(PowerPlant.Production());
                    AlertMessageList.Add(String.Format("La centrale {0} envoie à présent {1}W de sa production dans le réseau.", PowerPlant.GetName, PowerPlant.GetOutPutLine.GetPowerClaimed));
                    //D :: Y a un stuut ici, ca pas tout encoyer
                }
                */
                PowerPlant.GetOutPutLine.SetCurrentPower(PowerPlant.UpdatePowerPlant());

            }
            
            foreach (PurchaseAbroad m in MarketList)
            {
                // DEFINIR PUISSANCE DE SORTIE DU MARKET = le power claimed sur sa ligne
                m.GetOutPutLine.SetCurrentPower(m.UpdatePowerPlant());
            }
            
            foreach (Node n in NodeList) { n.UpdateCurrentPower(); }
        }   
    
        
    }
}
