using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    class classTest_Damien
    {

        public classTest_Damien()
        {

            Manager reseaux = new Manager();

            //reseaux.CreateNewPowerPlant();
            //reseaux.CreateNewConsumer();

            Market market = new Market();

            PowerPlant centrale = new NuclearPowerPlant("doel", market);

            Console.WriteLine(centrale.GetType());

        }

        public void TestWeatherClock()
        {
            //Weather weather = 
        }

    }
}
