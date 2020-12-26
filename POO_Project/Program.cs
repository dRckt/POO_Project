using System;

namespace POO_Project
{
    class Program
    {
        static void Main(string[] args)
        {

            Line L1 = new Line("Ligne 1");
            Weather meteo_bx = new Weather("Bruxelles");
            Market bourse = new Market();

            NuclearPowerPlant doel         = new NuclearPowerPlant("Centrale nucléaire", L1, bourse);
            GasPowerPlant esso                 = new GasPowerPlant("Gas Station       ", L1, bourse);
            WindFarm eol                            = new WindFarm("Parc Eolien       ", L1, meteo_bx);
            SolarPowerPlant champdepanneau   = new SolarPowerPlant("panneau solaire   ", L1, meteo_bx);
            PurchaseAbroad achat              = new PurchaseAbroad("achat à l'étranger", L1, 10000, bourse);

            Console.WriteLine(doel.Resume());
            Console.WriteLine(esso.Resume());
            Console.WriteLine(eol.Resume());
            Console.WriteLine(champdepanneau.Resume());
            Console.WriteLine(achat.Resume());

            /*
            for (int h = 0; h < 15; h++)
            {
                Console.WriteLine(doel.Production());
                if (doel.IsWorking == true)
                {
                    Console.WriteLine("doel is working");
                }
                else if (doel.IsWorking == false)
                {
                    Console.WriteLine("doel is not working");
                }
            }
            Console.WriteLine("################################");
            doel.Stop();
            Console.WriteLine("doel stopped");
            for (int h = 0; h < 15; h++)
            {
                Console.WriteLine(doel.Production());
                if (doel.IsWorking == true)
                {
                    Console.WriteLine("doel is working");
                }
                else if (doel.IsWorking == false)
                {
                    Console.WriteLine("doel is not working");
                }
            }
            Console.WriteLine("################################");
            doel.Start();
            Console.WriteLine("doel started");
            for (int h = 0; h < 15; h++)
            {
                Console.WriteLine(doel.Production());
                if (doel.IsWorking == true)
                {
                    Console.WriteLine("doel is working");
                }
                else if (doel.IsWorking == false)
                {
                    Console.WriteLine("doel is not working");
                }
            }
            Console.WriteLine("################################");
            doel.Stop();
            Console.WriteLine("doel stopped");
            for (int h = 0; h < 15; h++)
            {
                Console.WriteLine(doel.Production());
                if (doel.IsWorking == true)
                {
                    Console.WriteLine("doel is working");
                }
                else if (doel.IsWorking == false)
                {
                    Console.WriteLine("doel is not working");
                }
            }
            */

        }
    }
}
