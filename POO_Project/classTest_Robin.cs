using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    class classTest_Robin
    {
        public string opera;
        public string apero;
        public classTest_Robin()
        {
            ConcentrationNode test_node = new ConcentrationNode("noeud test");
            //Console.WriteLine("Noeud de concentration créé avec succès.");

            Line line_test0 = new Line("line_test0");
            Line line_test1 = new Line("line_test1");
            //Console.WriteLine("lignes de test créés avec succès.");



            line_test0.SetPowerClaimed(32);
            line_test0.SetCurrentPower(32);

            line_test1.SetPowerClaimed(42);
            line_test1.SetCurrentPower(42);

            //Console.WriteLine("puissance définies avec succès.");


            test_node.AddInputLineToList(line_test0);
            test_node.AddInputLineToList(line_test1);
            //Console.WriteLine("lignes ajoutées au noeud avec succès.");


            Console.WriteLine(String.Format("Le noeud véhicules une puissance de  {0}", test_node.GetOutputPower() )); //devrait etre égal a 32+42 = 74
            //Console.WriteLine("puissnace récupérée avec succès.");
            Console.WriteLine("_________");
            test_node.showState();


            string apero = "blablabla";
            string opera = "blablabla";
            if (String.Equals(apero, opera))
            {
                Console.WriteLine("Ah oui oui");
            }
            else { Console.WriteLine("Ah non non"); }

        }
    }
}
