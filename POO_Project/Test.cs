using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Test
    {
        Market market;
        Clock clock;
        Weather weather_bx;
        Weather weather_os;
        
        public Test()
        {
            market = new Market();
            clock = new Clock();
            weather_bx = new Weather("Bruxelles", clock);
            weather_os = new Weather("Ostende", clock);

            // CREATION CENTRALES
            PowerPlant doel = new NuclearPowerPlant("doel", market);
            PowerPlant parc_eolien_os = new WindFarm("parc éolien de Ostende", weather_os);
            PowerPlant esso = new GasPowerPlant("station à gaz esso", market);
            PowerPlant centrale_solaire_bx = new SolarPowerPlant("centrale solaire de bruxelles", weather_bx);

            // CREATION CONSOMATEUR
            Consumer bx = new City("Bruxelles", 1000000, weather_bx);
            Consumer os = new City("Ostende", 70000, weather_os);

            Consumer ad = new Entreprise("Audi", 10000);
            Consumer bm = new Entreprise("BMW", 5000);
            Consumer vw = new Entreprise("VolksWagen", 3000);

            Consumer diss1 = new Dissipator("Dissipateur 1");
        }
        
    }
}
