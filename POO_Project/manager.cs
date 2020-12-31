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
            Console.WriteLine("");
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
            //NewPowerPlant.GetOutPutLine.SetMyPowerPlant(NewPowerPlant);
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



        public void UpdatePowerOfPowerPlant()
        {
            foreach (PowerPlant PowerPlant in PowerPlantList)
            {
                if (PowerPlant.GetOutPutLine.GetPowerClaimed < PowerPlant.DisponibleProduction())
                {
                    ///// -- DAMIEN -- /////
                    ///// -- DAMIEN -- /////
                    ///
                    ///  ici augmenter/diminuer la puissance de la centrale
                    ///  la central devrait produire:  PowerPlant.GetOutPutLine.GetPowerClaimed
                    ///  
                    ///  +/MESSAGE DE NOTIF si la production de la centrale change
                    ///  
                    ///// -- DAMIEN -- /////
                    ///// -- DAMIEN -- /////
                    PowerPlant.GetOutPutLine.SetCurrentPower(PowerPlant.Production());
                   
                }
                List<Line> LineList = new List<Line> { PowerPlant.GetOutPutLine };
    
                
                
                ///
                /// MESSAGE NOTIFICATION
                /// message de modification de production à une centrale,
                /// message d'arrêt d'une centrale
                /// message de démarrage d'une centrale,  (missing power trop élevé = démarrage d'une centrale?)
                
            }

        }


        }

        



       


    }
}
