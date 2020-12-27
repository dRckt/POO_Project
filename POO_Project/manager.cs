using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    class manager
    {
        public manager()
        {
            Console.WriteLine("");
        }

        public PowerPlant createNewPowerPlant()
        {
            //interction avec le terminal pour donner les paramètres de la centrale (type, production, etc..)
            //!!centrale éteinte quand elle est créé

            PowerPlant newPowerPlant = new PowerPlant("<nom choisi par input terminal>"); 
        }
    }
}
