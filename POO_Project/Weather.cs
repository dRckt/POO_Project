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
            temperature = random.Next(-5, 35);  // varie entre -5 et +35°C
        }

        public double GetSunlight { get { return sunlight; } }
        public double GetWindspeed { get { return windSpeed; } }
        public double GetTemperature { get { return temperature; } }
        public string GetLocalisation { get { return localisation; } }

        public string GetMeteo()
        {
            string meteo = String.Format("Météo de {0} : Ensoleillement = {1}, Force du vent = {2}, Température = {3}", localisation, sunlight, windSpeed, temperature);

            return meteo;
        }
    }

    public class WeatherManager
    {
        private List<Weather> WeatherList;
        
        public WeatherManager()
        {
            WeatherList = new List<Weather>();
        }

        public void CreateWeather(string localisation)
        {
            Weather weather = new Weather(localisation);
            AddWeather(weather);
        }

        public void AddWeather(Weather weather)
        {
            WeatherList.Add(weather);
        }
        public double GetWeatherListCount { get { return WeatherList.Count; } }
        public List<Weather> GetWeatherList { get { return WeatherList; } }
    }
}
