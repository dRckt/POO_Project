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

            //Market market = new Market();

            //PowerPlant centrale = new NuclearPowerPlant("doel", market);

            //Console.WriteLine(centrale.GetType());

        }

        public void TestWeatherClock()
        {
            Clock clock = new Clock();
            Weather weather = new Weather("bruxelles", clock);

            Console.WriteLine("-------------ensoleillement-------------");
            weather.changeWeather();
            Console.WriteLine(clock.GetHour + "h : " + weather.GetSunlight);

            for (int i=0; i < 24; i++)
            {
                clock.UpdateClock(1);
                weather.changeWeather();
                Console.WriteLine(clock.GetHour + "h : " + weather.GetSunlight);
            }

            Console.WriteLine("-------------force du vent-------------");
            weather.changeWeather();
            Console.WriteLine(clock.GetHour + "h : " + weather.GetWindspeed);

            for (int i = 0; i < 24; i++)
            {
                clock.UpdateClock(1);
                weather.changeWeather();
                Console.WriteLine(clock.GetHour + "h : " + weather.GetWindspeed);
            }

            Console.WriteLine("-------------temperature-------------");
            weather.changeWeather();
            Console.WriteLine(clock.GetHour + "h : " + weather.GetTemperature);

            for (int i = 0; i < 24; i++)
            {
                clock.UpdateClock(1);
                weather.changeWeather();
                Console.WriteLine(clock.GetHour + "h : " + weather.GetTemperature);
            }

        }

    }
}
