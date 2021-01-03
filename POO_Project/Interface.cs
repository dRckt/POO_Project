using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Interface
    {
        double centrale_count;
        double consom_count;

        protected Node node1;
        protected Node node2;

        private Manager Reseau;
        public Interface(Manager reseau) 
        {
            Reseau = reseau;
            consom_count = reseau.GetConsumerList.Count;
            centrale_count = reseau.GetPowerPlantList.Count;
            Reseau.UpdateConsumerClaiming();
            Reseau.UpdatePowerOfPowerPlant();
            Console.WriteLine("______________________________________________________________________");
            Console.WriteLine("------------------BIENVENUE DANS L'INTERFACE RESEAU-------------------");
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
            Console.WriteLine("------------------------------CREATION CENTRALE------------------------------");
            Console.WriteLine("Quelle genre de centrale voulez vous créer ? Appuyez sur enter pour revenur au menu.");
            Console.WriteLine("    g - Gaz Station");
            Console.WriteLine("    n - nuclear Power Plant");
            Console.WriteLine("    w - Wind Farm");
            Console.WriteLine("    s - Solar Station");
            Console.WriteLine("    p - Purchase Abroad");

            string type_central = Console.ReadLine();
            PowerPlant NewPowerPlant;

            switch (type_central)
            {
                case "g":
                    {
                        NewPowerPlant = Reseau.CreateNewGasPowerPlant(ChooseName("gaz station"), market);
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("La centrale {0} a été ajoutée. ", NewPowerPlant.GetName);
                        Console.WriteLine("Veuillez connecter son noeud {0} au réseau via l'onglet 'w' du menu.", NewPowerPlant.GetOutputNode.GetName);
                        break;
                    }
                case "n":
                    {
                        NewPowerPlant = Reseau.CreateNewNuclearPowerPlant(ChooseName("nuclear power plant"), market);
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("La centrale {0} a été ajoutée. ", NewPowerPlant.GetName);
                        Console.WriteLine("Veuillez connecter son noeud {0} au réseau via l'onglet 'w' du menu.", NewPowerPlant.GetOutputNode.GetName);
                        break;
                    }
                case "w":
                    {
                        NewPowerPlant = Reseau.CreateNewWindFarm(ChooseName("wind farm"), ChooseWeather(weather_manager, clock));
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("La centrale {0} a été ajoutée. ", NewPowerPlant.GetName);
                        Console.WriteLine("Veuillez connecter son noeud {0} au réseau via l'onglet 'w' du menu.", NewPowerPlant.GetOutputNode.GetName);
                        break;
                    }
                case "s":
                    {
                        NewPowerPlant = Reseau.CreateNewSolarPowerPlant(ChooseName("solar power plant"), ChooseWeather(weather_manager, clock));
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("La centrale {0} a été ajoutée. ", NewPowerPlant.GetName);
                        Console.WriteLine("Veuillez connecter son noeud {0} au réseau via l'onglet 'w' du menu.", NewPowerPlant.GetOutputNode.GetName);
                        break;
                    }
                case "p":
                    {
                        NewPowerPlant = Reseau.CreateNewPurchasedAbroad(ChooseName("purchased abroad"), market);
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("La centrale {0} a été ajoutée. ", NewPowerPlant.GetName);
                        Console.WriteLine("Veuillez connecter son noeud {0} au réseau via l'onglet 'w' du menu.", NewPowerPlant.GetOutputNode.GetName);
                        break;
                    }
                case "":
                    {
                        Menu();
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
            Console.WriteLine("------------------------------CREATION CONSOMMATEUR------------------------------");
            Console.WriteLine("Quelle genre de centrale voulez vous créer ? Appuyez sur enter pour revenir au menu.");
            Console.WriteLine("    c - city");
            Console.WriteLine("    e - entreprise");

            string type_centrale = Console.ReadLine();
            Consumer NewConsumer;

            switch (type_centrale)
            {
                case "c":
                    {
                        NewConsumer = Reseau.CreateNewCity(ChooseName("city"), ChooseNbr("habitants"), ChooseWeather(weather_manager, clock));
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("La ville {0} a été ajouté.", NewConsumer.GetName);
                        Console.WriteLine("Veuillez connecter son noeud {0} au réseau via l'onglet 'w' du menu.", NewConsumer.GetInputNode.GetName);
                        break;
                    }
                case "e":
                    {
                        NewConsumer = Reseau.CreateNewEntreprise(ChooseName("entreprise"), ChooseNbr("machines"));
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("L'entreprise {0} a été ajouté.", NewConsumer.GetName);
                        Console.WriteLine("Veuillez connecter son noeud {0} au réseau via l'onglet 'w' du menu.", NewConsumer.GetInputNode.GetName);
                        break;
                    }
                case "":
                    {
                        Menu();
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
            Console.WriteLine("Quel genre de noeud voulez-vous créer? Appuyez sur enter pour revenir au menu.");
            Console.WriteLine("    d - Noeud de distribution");
            Console.WriteLine("    c - Noeud de concentration");
            string type_node = Console.ReadLine();
            Node NewNode;
            switch (type_node)
            {
                case "d":
                {
                    NewNode = Reseau.CreateNewDistributionNode(ChooseName("Distribution Node"));
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("Le noeud de distribution {0} a été ajouté, veuillez le connecter au reseau via l'onglet 'w' du menu.", NewNode.GetName);    
                    break;
                }
                case "c":
                {
                    NewNode = Reseau.CreateNewConcentrationNode(ChooseName("Concentration Node"));
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("Le noeud de concentration {0} a été ajouté, veuillez le connecter au reseau via l'onglet 'w' du menu.", NewNode.GetName);
                    break;
            }
                case "":
                {
                    Menu();
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
            Console.WriteLine("Quelle liste souhaitez-vous consulter? Appuyez sur enter pour revenir au menu.");
            Console.WriteLine("   p - Afficher les centrales");
            Console.WriteLine("   c - Afficher les consomateurs");
            Console.WriteLine("   n - Afficher les noeuds");
            Console.WriteLine("   l - Afficher les lignes");
            Console.WriteLine("   u - Afficher les élément du réseau non connecté");
            string rep = Console.ReadLine();
            switch (rep)
            {
                case "p":
                    {
                        Console.WriteLine("CENTRALES ::");
                        foreach (PowerPlant pp in Reseau.GetPowerPlantList)
                        {
                            Console.WriteLine("Centrale : {0}", pp.GetName);
                            Console.WriteLine("          Production: {0}   ;   Demande: {1}", pp.GetPowerProduction, pp.GetOutPutLine.GetPowerClaimed);
                            
                        }
                        BackToShowManager();
                        break;
                    }
                case "c":
                    {
                        Console.WriteLine("CONSOMMATEURS ::");
                        foreach (Consumer c in Reseau.GetConsumerList)
                        {
                            Console.WriteLine("Consomateur : {0}", c.GetName);
                            Console.WriteLine("          Puissance reçue: {0}   ;   Demande: {1}", c.getInputLine.GetCurrentPower, c.getInputLine.GetPowerClaimed);
                        }
                        BackToShowManager();
                        break;
                    }
                case "n":
                    {
                        Console.WriteLine("NODES ::");
                        foreach (Node n in Reseau.GetNodeList)
                        {
                            Console.WriteLine("Noeud :: {0}", n.GetName);
                            Console.WriteLine("     Lignes d'entrée:");
                            foreach (Line l in n.GetInputLineList)
                            {
                                Console.WriteLine("       Ligne : {0}", l.GetName);
                                Console.WriteLine("            current power: {0} ; claimed power: {1}", l.GetCurrentPower, l.GetPowerClaimed);
                            }
                            Console.WriteLine("     Lignes de sortie:");
                            foreach (Line l in n.GetOutputLineList)
                            {
                                Console.WriteLine("       Ligne : {0}", l.GetName);
                                Console.WriteLine("            current power: {0} ; claimed power: {1}", l.GetCurrentPower, l.GetPowerClaimed);
                            }

                        }
                        BackToShowManager();
                        break;
                    }
                case "l":
                    {
                        Console.WriteLine("LINES ::");
                        foreach (Line l in Reseau.GetLineList)
                        {
                            Console.WriteLine("Ligne : {0}", l.GetName);
                            Console.WriteLine("      current power: {0} ; claimed power: {1}", l.GetCurrentPower, l.GetPowerClaimed);
                        }
                        BackToShowManager();
                        break;
                    }
                case "u":
                    {
                        Console.WriteLine("PROGRAM IS BUILDING ...");
                        BackToShowManager();
                        break;
                    }
                case "":
                    {
                        Menu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Erreur : invalid input");
                        ShowManager();
                        break;
                    }
            }
        }
        public void BackToShowManager()
        {
            Console.WriteLine("");
            Console.WriteLine("Appuyez sur b pour revenir en arrière ou sur enter pour revenir au menu.");
            string ans = Console.ReadLine();
            switch (ans)
            {
                case "b":
                    {
                        ShowManager();
                        break;
                    }
                case "":
                    {
                        Menu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Erreur: invalid input");
                        BackToShowManager();
                        break;
                    }
            }
        }
        public void exit()
        {
            Console.WriteLine("");
            Console.WriteLine("Appuyez sur enter pour revenir au menu.");
            string enter = Console.ReadLine();
            if (enter == "")
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
            Console.WriteLine("Voici la liste de consomateurs liés au réseau:");
            if (Reseau.GetConsumerList.Count == 0) { Console.WriteLine("La liste est vide."); }
            else
            {
                foreach (Consumer c in Reseau.GetConsumerList) { Console.WriteLine("   - {0};", c.GetName); }
            }
            Console.WriteLine("");
            Console.WriteLine("Entrez le nom du consommateur dont il faut modifier la demande ou appuyez sur enter pour revenir au menu:");
            string rep = Console.ReadLine();
            if (rep == "") { Menu(); }
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
                        c.SetManualClaiming(true);
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
        public void ConnectTwoNodes()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Voici la listes des noeuds du réseau. Choisissez le premier noeud à connecter ou appuyez sur enter pour revenir au menu.");
            Console.WriteLine("Remarque: Les noeuds appartenant à un consomateur ne sont pas affichés car vous ne pouvez pas modifier leur sortie.");

            for (int i = 0; i < Reseau.GetNodeList.Count; i++)
            {
                if (Reseau.GetNodeList[i].GetOutputLineList.Count > 0) 
                {
                    if (Reseau.GetNodeList[i].GetOutputLineList[0].GetIsConsumerLine) { }
                    else 
                    { 
                        Console.WriteLine("   {0} - {1}", i, Reseau.GetNodeList[i].GetName);
                        Console.WriteLine("                   Input state : {0} ; Output state : {1} ; Type : {2}", Reseau.GetNodeList[i].GetInputState(), Reseau.GetNodeList[i].GetOutputState(), Reseau.GetNodeList[i].GetType());
                    }
                }
                else
                {
                    Console.WriteLine("   {0} - {1}", i, Reseau.GetNodeList[i].GetName);
                    Console.WriteLine("                   Input state : {0} ; Output state : {1} ; Type : {2}", Reseau.GetNodeList[i].GetInputState(), Reseau.GetNodeList[i].GetOutputState(), Reseau.GetNodeList[i].GetType());
                }
            }
            Console.WriteLine("Entrez un int!");
            string rep = Console.ReadLine();
            if (rep == "") { Menu(); }
            else
            {
                int index1 = Convert.ToInt32(rep);
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("Voici la listes des noeuds du réseau. Choisissez le second noeud à connecter ou appuyez sur enter pour revenir au menu.");
                Console.WriteLine("Remarque: Les noeuds appartenant à un e centrale ne sont pas affichés car vous ne pouvez pas modifier leur entrée.");

                for (int j = 0; j < Reseau.GetNodeList.Count; j++)
                {
                    if (j == index1) { }
                    else if (Reseau.GetNodeList[j].GetInputLineList.Count > 0)
                    {
                        if (Reseau.GetNodeList[j].GetInputLineList[0].GetIsPowerPlantLine) { }
                        else
                        {
                            Console.WriteLine("   {0} - {1}", j, Reseau.GetNodeList[j].GetName);
                            Console.WriteLine("                   Input state : {0} ; Output state : {1} ; Type : {2}", Reseau.GetNodeList[j].GetInputState(), Reseau.GetNodeList[j].GetOutputState(), Reseau.GetNodeList[j].GetType());
                        }
                    }
                    else
                    {
                        Console.WriteLine("   {0} - {1}", j, Reseau.GetNodeList[j].GetName);
                        Console.WriteLine("                   Input state : {0} ; Output state : {1} ; Type : {2}", Reseau.GetNodeList[j].GetInputState(), Reseau.GetNodeList[j].GetOutputState(), Reseau.GetNodeList[j].GetType());
                    }
                }
                Console.WriteLine("Entrez un int!");
                string rep2 = Console.ReadLine();
                if (rep2 == "") { Menu(); }
                else 
                {
                    ConnectTwoNodes_part2(rep2, index1);
                }

            }
        }
        public void ConnectTwoNodes_part2(string rep2, int index1)
        {
            int index2 = Convert.ToInt32(rep2);

            Console.WriteLine("PROGRAM IS BUILDING ... ///");

            string type1 = Convert.ToString(Reseau.GetNodeList[index1].GetType());
            string type2 = Convert.ToString(Reseau.GetNodeList[index2].GetType());
            Console.WriteLine(type1);
            if (type1 == "POO_Project.ConcentrationNode")
            {
                if (type2 == "POO_Project.ConcentrationNode")
                {
                    Reseau.ConnectConcentrationToConcentrationNode((ConcentrationNode)Reseau.GetNodeList[index1], (ConcentrationNode)Reseau.GetNodeList[index2]);
                }
                else if (type2 == "POO_Project.DistributionNode")
                {
                    Reseau.ConnectConcentrationToDistributionNode((ConcentrationNode)Reseau.GetNodeList[index1], (DistributionNode)Reseau.GetNodeList[index2]);
                }
            }
            else if (type1 == "POO_Project.DistributionNode")
            {
                if (type2 == "POO_Project.ConcentrationNode")
                {
                    Console.WriteLine("Entrez le nom de la ligne qui reliera {0} à {1}", Reseau.GetNodeList[index1].GetName, Reseau.GetNodeList[index2].GetName);
                    string NewLineName = Console.ReadLine();
                    Reseau.ConnectDistributionToConcentrationNode(NewLineName, (DistributionNode)Reseau.GetNodeList[index1], (ConcentrationNode)Reseau.GetNodeList[index2]);
                }
                else if (type2 == "POO_Project.DistributionNode")
                {

                    Reseau.ConnectDistributionToDistributionNode((DistributionNode)Reseau.GetNodeList[index1], (DistributionNode)Reseau.GetNodeList[index2]);
                }
            }
            Console.WriteLine("Les noeuds {0} et {1} sont à présent connectés.", Reseau.GetNodeList[index1], Reseau.GetNodeList[index2]);
        }
        public void ShowNotificationMessage()
        {
            if(Reseau.GetAlertMessageList().Count == 0) { Console.WriteLine("Aucune notification."); }
            else
            {
                foreach(string msg in Reseau.GetAlertMessageList())
                {
                    Console.WriteLine(msg);
                }
                Reseau.ResetAlertMessageList();
            }
            
            exit();
        }
        public void Menu()
        {
            p("______________________________________________________________________");   
            p("Instruction : ");
            p("    p - Créer une nouvelle centrale");
            p("    c - Créer un nouveau consommateur");
            p("    n - Créer un nouveu noeud");
            p("    w - Connecter 2 noeuds ensemble");
            p("");
            p("    a - Afficher l'état du réseaux");
            p("    m - Modifier la demande d'un consommateur");
            p("");
            p("    u - Mettre à jour le réseau");
            p("    t - Afficher les message de notifications du réseau");
            


            string instruction = Console.ReadLine();
            switch (instruction)
            {
                case "p":
                    {
                        CreateNewPowerPlant(Reseau.GetWeatherManager, Reseau.GetClock, Reseau.GetMarket);
                        exit();
                        break;
                    }
                case "c":
                    {
                        CreateNewConsumer(Reseau.GetWeatherManager, Reseau.GetClock);
                        exit();
                        break;
                    }
                case "n":
                    {
                        CreateNewNode();
                        exit();
                        break;
                    }
                case "u":
                    {
                        Reseau.UpdateConsumerClaiming();
                        Reseau.UpdatePowerOfPowerPlant();
                        p("-------------------------------");
                        p("Le réseau a été mis à jour.");
                        p("Passez par l'onglet 't' du menu pour voir les messages de notification du réseau.");

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
                        ConnectTwoNodes();
                        exit();
                        break;
                    }
                case "t":
                    {
                        ShowNotificationMessage();
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
