using System;

namespace POO_Project
{
    class Program
    {

        static void Main(string[] args)
        {

            //classTest_Damien test_Damien = new classTest_Damien();

            //CODE PROGRAM.CS ::

            void Accueil()
            {
                Console.WriteLine("Veuillez choisir une option:");
                Console.WriteLine("   a - Créer un nouveau réseau");
                Console.WriteLine("   b - Ouvrir le réseau de démonstration");

                string rep = Console.ReadLine();
                if (rep == "a")
                {
                    Manager newReseau = new Manager();
                    Market market = new Market();
                    Clock clock = new Clock();
                    Interface Interface = new Interface(newReseau);
                    Interface.Menu();
                }
                else if (rep == "b")
                {
                    RESEAUX_1 RESEAUX_1 = new RESEAUX_1();
                    Interface Interface = new Interface(RESEAUX_1.GetManagerInstance());
                    Interface.Menu();
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    Accueil();
                }
            }

            Accueil();
            
        }


        
    }
}
