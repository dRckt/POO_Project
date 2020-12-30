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

        Clock clock;

        public Weather(string localisation, Clock clock)
        {
            this.localisation = localisation;
            this.clock = clock;
            changeWeather();        // initialisation
        }

        public void changeWeather() 
        {
            double hour = clock.GetHour;
            
            if (6 < hour & hour < 22 )
            {
                sunlight = 2*Math.Sin((Math.PI / 24) * (hour - 2)) - 1;
            }
            else
            {
                sunlight = 0;
            }
            windSpeed = 0.5*Math.Sin((Math.PI/12)*hour)+0.5 ;
            temperature = 20*Math.Sin((Math.PI/24)*hour)+3 ;

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

        public void CreateWeather(string localisation, Clock clock)
        {
            Weather weather = new Weather(localisation, clock);
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
