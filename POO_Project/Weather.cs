using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Weather
    {
        private string localisation;

        private double sunlight;     // sunlight  takes a value between 0 and 10, depending on the intensity of the sun
        private double windSpeed;    // windSpeed takes a value between 0 and 10
        private double temperature;  

        public Weather(string localisation)
        {
            this.localisation = localisation;

            changeWeather();        // initialise à des valeurs aléatoires
        }

        public void changeWeather() 
        {
            var random = new Random();
            sunlight = random.Next(0, 10);
            windSpeed = random.Next(0, 10);
            temperature = random.Next(-5, 35);
        }

        public double getSunlight { get { return sunlight; } }
        public double getWindspeed { get { return windSpeed; } }
        public double getTemperature { get { return temperature; } }

        public string getMeteo()
        {
            string meteo = String.Format("Météo de {0} : Ensoleillement = {1}, Force du vent = {2}, Température = {3}", localisation, sunlight, windSpeed, temperature);

            return meteo;
        }
    }
}
