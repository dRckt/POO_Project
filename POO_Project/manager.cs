using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Manager
    {

        protected List<PowerPlant> PowerPlantList;
        protected List<Consumer> ConsumerList;

        private Market market;
        private WeatherManager weather_manager;

        private double count;

        public Manager()
        {
            Console.WriteLine("");
            PowerPlantList = new List<PowerPlant> { };
            ConsumerList = new List<Consumer> { };

            weather_manager = new WeatherManager();
            market = new Market();
        }
        // creation Meteo
        private Weather ChooseWeather()
        {
            Console.WriteLine("Creer une nouvelle meteo (0) ou utiliser une meteo disponnible ? (1)");
            string reponse = Console.ReadLine();

            if (reponse == "0")
            {
                Console.WriteLine("Quelle est la localisation de la nouvelle meteo ?");
                string localisation = Console.ReadLine();
                Weather new_weather = new Weather(localisation);
                weather_manager.AddWeather(new_weather);
                return new_weather;
            }
            else if (reponse == "1")
            {
                if (weather_manager.GetWeatherListCount == 0)
                {
                    Console.WriteLine("erreur : La liste est vide");
                    return ChooseWeather();
                }
                else
                {
                    Dictionary<int, Weather> dict_Weather = new Dictionary<int, Weather>();
                    int id = 0;
                    foreach (Weather weather in weather_manager.GetWeatherList)
                    {
                        dict_Weather.Add(id, weather);
                        Console.WriteLine(String.Format("   {0} - Meteo de {1}",id, weather.GetLocalisation));
                        id++;
                    }

                    Console.WriteLine("Entrez l'id de la meteo souhaitée");
                    int weather_id = Convert.ToInt32(Console.ReadLine());

                    try
                    {
                        Weather new_weather = dict_Weather[weather_id];
                        Console.WriteLine(String.Format("meteo choisie : meteo de {0}", new_weather.GetLocalisation));
                        return new_weather;

                    }
                    catch
                    {
                        Console.WriteLine("error : id mauvais");
                        return ChooseWeather();
                    }                  
                }
            }
            else
            {
                Console.WriteLine("erreur : entrée incorrecte");
                return ChooseWeather();
            }
        }
        // creation d'une centrale
        public PowerPlant CreateNewPowerPlant()
        {
            Console.WriteLine(String.Format("------------------------------CREATION CENTRALE n°{0}------------------------------", count));
            Console.WriteLine("Quelle genre de centrale voulez vous créer ? Entrez :");
            Console.WriteLine("    g - Gaz Station");
            Console.WriteLine("    n - nuclear Power Plant");
            Console.WriteLine("    w - Wind Farm");
            Console.WriteLine("    s - Solar Station");

            string type_central = Console.ReadLine();
            PowerPlant NewPowerPlant;

            if (type_central == "g")
            {
                NewPowerPlant = new GasPowerPlant(ChooseName() , market);
            }
            else if (type_central == "n")
            {
                NewPowerPlant = new NuclearPowerPlant(ChooseName(), market);
            }
            else if (type_central == "w")
            {
                NewPowerPlant = new WindFarm(ChooseName(), ChooseWeather());
            }
            else if (type_central == "s")
            {
                NewPowerPlant = new SolarPowerPlant(ChooseName(), ChooseWeather());
            }
            else
            {
                Console.WriteLine("Entrée incorrecte");
                return CreateNewPowerPlant();
            }

            PowerPlantList.Add(NewPowerPlant);
            Console.WriteLine(String.Format("La centrale {0} a bien été créée.", NewPowerPlant.GetName));

            count++;

            return CreateNewPowerPlant();

            // interction avec le terminal pour donner les paramètres de la centrale (type, production, etc..)
            // !!centrale éteinte quand elle est créé
        }

        public Consumer CreateNewConsumer()
        {
            //interction avec le terminal pour donner les paramètres du consommateur

            //voir type de consommateur qu'on créé, mais cas général:
            Consumer NewConsumer = new Consumer("<nom choisi par input terminal>");
            ConsumerList.Add(NewConsumer); 

            return NewConsumer;
        }

        private string ChooseName()
        {
            Console.WriteLine("Entrez le nom de la centrale :");
            string name = Console.ReadLine();
            return name;
        }

        public void ConnectDistributionToConcentrationNode(string ConnexionLineName, Node DistributionNode, Node ConcentrationNode)
        {
            Line ConnexionLine = new Line(ConnexionLineName);
            DistributionNode.AddOutputLineToList(ConnexionLine);
            ConcentrationNode.AddInputLineToList(ConnexionLine);

            ConnexionLine.SetInputNode(DistributionNode);
            ConnexionLine.SetOutPutNode(ConcentrationNode);

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

            foreach (PowerPlant PowerPlant in PowerPlantList)
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
                /* //Pfinalement pas nécessaire mais aura peut etre besoin de ce bloc de code ailleurs
                else if (line.GetIsDissipatorLine) 
                {
                    sum -= line.GetCurrentPower;  //Ce qui etait en trop sur la ligne dissipative est récupéré
                    line.SetCurrentPower(0);  //puissance de la ligne dissipative mise a 0
                }*/
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
