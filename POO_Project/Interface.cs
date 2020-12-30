using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Interface
    {
        double centrale_count;
        double consom_count;

        public Interface()
        {

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
        private Weather ChooseWeather(WeatherManager weather_manager)
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
                    return ChooseWeather(weather_manager);
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
                        return ChooseWeather(weather_manager);
                    }
                }
            }
            else
            {
                Console.WriteLine("erreur : entrée incorrecte");
                return ChooseWeather(weather_manager);
            }
        }

        public PowerPlant CreateNewPowerPlant(WeatherManager weather_manager, Market market)
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
                        NewPowerPlant = new GasPowerPlant(ChooseName("gaz station"), market);
                        break;
                    }
                case "n":
                    {
                        NewPowerPlant = new NuclearPowerPlant(ChooseName("nuclear power plant"), market);
                        break;
                    }
                case "w":
                    {
                        NewPowerPlant = new WindFarm(ChooseName("wind farm"), ChooseWeather(weather_manager));
                        break;
                    }
                case "s":
                    {
                        NewPowerPlant = new SolarPowerPlant(ChooseName("solar power plant"), ChooseWeather(weather_manager));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Entrée incorrecte");
                        return CreateNewPowerPlant(weather_manager, market);
                    }
            }

            centrale_count++;
            return NewPowerPlant;
        }

        public Consumer CreateNewConsumer(WeatherManager weather_manager)
        {
            Console.WriteLine(String.Format("------------------------------CREATION CONSOMMATEUR n°{0}------------------------------", consom_count));
            Console.WriteLine("Quelle genre de centrale voulez vous créer ? Entrez :");
            Console.WriteLine("    c - city");
            Console.WriteLine("    e - entreprise");
            Console.WriteLine("    d - dissipator");

            string type_centrale = Console.ReadLine();
            Consumer NewConsumer;

            switch (type_centrale)
            {
                case "c":
                    {
                        NewConsumer = new City(ChooseName("city"), ChooseNbr("habitants"), ChooseWeather(weather_manager));
                        break;
                    }
                case "e":
                    {
                        NewConsumer = new Entreprise(ChooseName("entreprise"), ChooseNbr("machines"));
                        break;
                    }
                case "d":
                    {
                        NewConsumer = new dissipator(ChooseName("dissipator"));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("ERROR : input invalide");
                        return CreateNewConsumer(weather_manager);
                    }
            }

            consom_count++;
            return NewConsumer;
        }

    }
}
