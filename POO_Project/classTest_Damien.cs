using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    class classTest_Damien
    {
        private Manager m = new Manager();


        public classTest_Damien()
        {
            Market market = new Market();
            PowerPlant powerplant = new GasPowerPlant("Centrale", market);

            Console.WriteLine(powerplant.GetType());
            Console.WriteLine("----------------------------------");
            if (powerplant is GasPowerPlant)
            {
                Console.WriteLine("power plant est de type GasPowerPlant");
            }
            else
            {
                Console.WriteLine("power plant n'est pas de type GasPowerPlant");
            }
        }

    }
}
