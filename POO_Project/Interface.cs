using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Interface
    {
        double centrale_count; 
        double consom_count;  

        private Manager Reseau;

        public Interface(Manager reseau) 
        {
            Reseau = reseau;
            consom_count = reseau.GetConsumerList.Count;
            centrale_count = reseau.GetPowerPlantList.Count;
        }
        private void p(string value)
        {
            Console.WriteLine(value);
        }

        private double ChooseNbr(string obj) 
        {
            Console.WriteLine(String.Format("Number of {0} :", obj));
            double nbr_obj = Convert.ToDouble(Console.ReadLine());
            return nbr_obj;
        }
        private string ChooseName(string obj)
        {
            Console.WriteLine(String.Format("Enter name of the new {0} :", obj));
            string name = Console.ReadLine();
            return name;
        }
        private Weather ChooseWeather(WeatherManager weather_manager, Clock clock)
        {
            Console.WriteLine("Creer une nouvelle meteo (0) ou utiliser une meteo disponnible ? (1)");
            string reponse = Console.ReadLine();

            if (reponse == "0")
            {
                Console.WriteLine("Quelle est la localisation de la nouvelle meteo ?");
                string localisation = Console.ReadLine();
                Weather new_weather = new Weather(localisation, clock);
                weather_manager.AddWeather(new_weather);
                return new_weather;
            }
            else if (reponse == "1")
            {
                if (weather_manager.GetWeatherListCount == 0)
                {
                    Console.WriteLine("erreur : La liste est vide");
                    return ChooseWeather(weather_manager, clock);
                }
                else
                {
                    Dictionary<int, Weather> dict_Weather = new Dictionary<int, Weather>();
                    int id = 0;
                    foreach (Weather weather in weather_manager.GetWeatherList)
                    {
                        dict_Weather.Add(id, weather);
                        Console.WriteLine(String.Format("   {0} - Meteo de {1}", id, weather.GetLocalisation));
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
                        return ChooseWeather(weather_manager, clock);
                    }
                }
            }
            else
            {
                Console.WriteLine("erreur : entrée incorrecte");
                return ChooseWeather(weather_manager, clock);
            }
        }

        public void CreateNewPowerPlant(WeatherManager weather_manager, Clock clock, Market market)
        {
            Console.WriteLine(String.Format("------------------------------CREATION CENTRALE n°{0}------------------------------", centrale_count));
            Console.WriteLine("Quelle genre de centrale voulez vous créer ? Entrez :");
            Console.WriteLine("    g - Gaz Station");
            Console.WriteLine("    n - nuclear Power Plant");
            Console.WriteLine("    w - Wind Farm");
            Console.WriteLine("    s - Solar Station");

            string type_central = Console.ReadLine();
            PowerPlant NewPowerPlant;

            switch (type_central)
            {
                case "g":
                    {
                        NewPowerPlant = Reseau.CreateNewGasPowerPlant(ChooseName("gaz station"), market);
                        
                        break;
                    }
                case "n":
                    {
                        NewPowerPlant = Reseau.CreateNewNuclearPowerPlant(ChooseName("nuclear power plant"), market);
                        break;
                    }
                case "w":
                    {
                        NewPowerPlant = Reseau.CreateNewWindFarm(ChooseName("wind farm"), ChooseWeather(weather_manager, clock));
                        break;
                    }
                case "s":
                    {
                        NewPowerPlant = Reseau.CreateNewSolarPowerPlant(ChooseName("solar power plant"), ChooseWeather(weather_manager, clock));
                        break;
                    }
                default:
                    {
                        p("Erreur : Invalid Input");
                        CreateNewPowerPlant(weather_manager, clock, market);
                        break;
                    }
            }

            
        }
        public void CreateNewConsumer(WeatherManager weather_manager, Clock clock)
        {
            Console.WriteLine(String.Format("------------------------------CREATION CONSOMMATEUR n°{0}------------------------------", consom_count));
            Console.WriteLine("Quelle genre de centrale voulez vous créer ? Entrez :");
            Console.WriteLine("    c - city");
            Console.WriteLine("    e - entreprise");
            Console.WriteLine("    d - dissipator");   //inutile de proposer d'ajouter un dissipateur au réseau puisque chaque noeud de distribution en a déja un

            string type_centrale = Console.ReadLine();
            Consumer NewConsumer;

            switch (type_centrale)
            {
                case "c":
                    {
                        NewConsumer = Reseau.CreateNewCity(ChooseName("city"), ChooseNbr("habitants"), ChooseWeather(weather_manager, clock));
                        break;
                    }
                case "e":
                    {
                        NewConsumer = Reseau.CreateNewEntreprise(ChooseName("entreprise"), ChooseNbr("machines"));
                        break;
                    }

                ///A retirer
                case "d":
                    {
                        NewConsumer = new Dissipator(ChooseName("dissipator"));
                        break;
                    }
                default:
                    {
                        p("Erreur : Invalid Input");

                        CreateNewConsumer(weather_manager, clock);
                        break;
                    }
            }
        }
        public void CreateNewNode()
        {
            Console.WriteLine(String.Format("------------------------------CREATION NOEUD n°{0}------------------------------", Reseau.GetNodeList.Count));
            Console.WriteLine("Quel genre de noeud voulez-vous créer? Entrez :");
            Console.WriteLine("    d - Noeud de distribution");
            Console.WriteLine("    c - Noeud de concentration");
            string type_node = Console.ReadLine();
            Node NewNode;
            switch (type_node)
            {
                case "d":
                {
                    NewNode = Reseau.CreateNewDistributionNode(ChooseName("Distribution Node"));
                    break;
                }
                case "c":
                {
                    NewNode = Reseau.CreateNewConcentrationNode(ChooseName("Concentration Node"));
                    break;
                }
                default:
                {
                    p("Erreur : Invalid Input");
                    CreateNewNode();
                    break;
                }

            }
            
        }

        public void ShowManager()
        {
            Console.WriteLine("CENTRALES ::");
            foreach (PowerPlant pp in Reseau.GetPowerPlantList)
            {
                Console.WriteLine("PROGRAM IS BUILDING ...");
            }
            Console.WriteLine("CONSOMMATEURS ::");
            foreach (Consumer c in Reseau.GetConsumerList)
            {
                Console.WriteLine("PROGRAM IS BUILDING ...");
            }
            Console.WriteLine("NODES ::");
            foreach (Node n in Reseau.GetNodeList)
            {
                Console.WriteLine("PROGRAM IS BUILDING ...");
            }
            Console.WriteLine("LINES ::");
            foreach (Line l in Reseau.GetLineList)
            {
                Console.WriteLine("PROGRAM IS BUILDING ...");
                Console.WriteLine("Line : {0}   ;   Current power : {1}   ;   Power claimed : {2}", l.GetName, l.GetCurrentPower, l.GetPowerClaimed);
            }
            ////AFFICHER AUSSI LES MESSAGES D ALERTE
            exit();

        }
        public void exit()
        {
            Console.WriteLine("Appuyez sur la barre d'espace + enter pour revenir au menu.");
            string enter = Console.ReadLine();
            if (enter == " ")
            {
                Menu();
            }
            else
            {
                exit();
            }

        }

        public void SetPowerClaimedByConsumer()
        {
            Console.WriteLine("PROGRAM IS BUILDING ...");

            Console.WriteLine("Entrez le nom du consommateur dont il faut modifier la demande ou appuyez sur la barre d'espace + enter pour quitter:");
            string rep = Console.ReadLine();
            if (rep == " ") { Menu(); }
            else
            {
                bool found = false;
                foreach (Consumer c in Reseau.GetConsumerList)
                {
                    if (c.GetName == rep)
                    {
                        Console.WriteLine("PROGRAM IS BUILDING ...");
                        Console.WriteLine("Le consommateur réclame actuellement {0}W, entrez la nouvelle puissance réclamée:", c.getInputLine.GetPowerClaimed);
                        double reponse = Convert.ToDouble(Console.ReadLine());
                        ///
                        ////// ICI MODIFIER POWER CLAIMED DU CONSUMER EN reponse
                        c.getInputLine.SetPowerClaimed(reponse);
                        ///
                        Console.WriteLine("La puissance réclamée par {0} à été mise à jour.", c.GetName);
                        found = true;
                        Menu();
                        break;
                    }
                }
                if (found == false)
                {
                     Console.WriteLine("Aucune correspondance trouvée.");
                     SetPowerClaimedByConsumer();
                }
           
            }
            
            
        }

        public void Menu()
        {
            p("______________________________________________________________________");   
            p("Instruction : ");
            p("    p - Créer une nouvelle centrale");
            p("    c - Créer un nouveau consommateur");
            p("    n - Créer un nouveu noeud");
            p("    w - Connecter 2 noeuds ensemble");
            p("    a - Afficher l'état du réseaux");
            p("    m - Modifier la demande d'un consommateur");
            p("    u - Mettre à jour le réseau");


            string instruction = Console.ReadLine();
            switch (instruction)
            {
                case "p":
                    {
                        CreateNewPowerPlant(Reseau.GetWeatherManager, Reseau.GetClock, Reseau.GetMarket);
                        p("Centrale ajoutée!");
                        Menu();
                        break;
                    }
                case "c":
                    {
                        CreateNewConsumer(Reseau.GetWeatherManager, Reseau.GetClock);
                        p("Consommateur ajouté!");
                        Menu();
                        break;
                    }
                case "n":
                    {
                        CreateNewNode();
                        p("Noeud ajouté!");
                        Menu();
                        break;
                    }
                case "u":
                    {
                        Console.WriteLine("PROGRAM IS BUILDING ...");
                        foreach (PowerPlant pp in Reseau.GetPowerPlantList)
                        {
                            // ICI METTRE A JOUR LA PUISSANCE DE 'pp
                            // pp.CurrentPower doit devenir égal à pp.GetOutputLine.GetPowerClaimed
                        }
                        Console.WriteLine("Le réseau a été mis à jour.");
                        
                        Menu();
                        break;
                    }
                case "a":
                    {
                        ShowManager();
                        break;
                    }
                case "m":
                    {
                        SetPowerClaimedByConsumer();
                        break;
                    }
                case "w":
                    {
                        //////
                        ///ICI interraction console pour lier 2 noeuds ensembles
                        Menu();
                        break;
                    }
                default:
                    {
                        p("Erreur : Invalid Input");
                        Menu();
                        break;
                    }
            }         
        }
        
        public void InvalideInput()
        {
            p("Input invalide");
        }
    }
}
