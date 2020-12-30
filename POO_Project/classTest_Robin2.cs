using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
   public class thisIsATest
    {
        public thisIsATest()
        {

            ///CENTRALES//
            DistributionNode centralNode1 = new DistributionNode("centralNode1");
            Line centralLine1 = centralNode1.GetInputLineList[0];
            centralLine1.SetIsPowerPlantLine(true);
            centralLine1.SetOutputNode(centralNode1);

            DistributionNode centralNode2 = new DistributionNode("centralNode2");
            Line centralLine2 = centralNode2.GetInputLineList[0];
            centralLine2.SetIsPowerPlantLine(true);
            centralLine2.SetOutputNode(centralNode2);

            //CONSUMERS//
            ConcentrationNode consumerNode1 = new ConcentrationNode("consumerNode1");
            Line consumerLine1 = consumerNode1.GetOutputLine;
            consumerLine1.SetIsConsumerLine(true);
            consumerLine1.SetInputNode(consumerNode1);

            ConcentrationNode consumerNode2 = new ConcentrationNode("consumerNode2");
            Line consumerLine2 = consumerNode2.GetOutputLine;
            consumerLine2.SetIsConsumerLine(true);
            consumerLine2.SetInputNode(consumerNode2);

            ConcentrationNode consumerNode3 = new ConcentrationNode("consumerNode3");
            Line consumerLine3 = consumerNode3.GetOutputLine;
            consumerLine3.SetIsConsumerLine(true);
            consumerLine3.SetInputNode(consumerNode3);


            ConcentrationNode midConcentrationNode1 = new ConcentrationNode("midConcentrationNode1");
            DistributionNode midDistributionNode1 = new DistributionNode("midDistributionNode1");

            Manager outsider = new Manager();
            // outsider.ConnectDistributionToConcentrationNode("LINE1", centralNode1, midConcentrationNode1);

            // outsider.ConnectConcentrationToDistributionNode(midConcentrationNode1, midDistributionNode1);

            //outsider.ConnectDistributionToConcentrationNode("LINE2.1",midDistributionNode1, consumerNode1);
            //outsider.ConnectDistributionToConcentrationNode("LINE2.2", midDistributionNode1, consumerNode2);

            //outsider.ConnectDistributionToConcentrationNode("LINE2.2", midDistributionNode1, consumerNode3);
            //outsider.ConnectDistributionToConcentrationNode("LINE 3",centralNode2, consumerNode3);


            outsider.ConnectDistributionToConcentrationNode("LINE-A", centralNode1, consumerNode1);
            outsider.ConnectDistributionToConcentrationNode("LINE-B", centralNode2, consumerNode1);

            outsider.ConnectDistributionToConcentrationNode("LINE-A", centralNode1, consumerNode2);
            outsider.ConnectDistributionToConcentrationNode("LINE-B", centralNode2, consumerNode2);

            consumerLine1.SetPowerClaimed(500);

            Console.WriteLine("____CHECK1____");

            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", centralLine1.GetName, centralLine1.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", centralLine2.GetName, centralLine2.GetPowerClaimed);

            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode1.GetOutputLine.GetName, consumerNode1.GetOutputLine.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode2.GetOutputLine.GetName, consumerNode2.GetOutputLine.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode3.GetOutputLine.GetName, consumerNode3.GetOutputLine.GetPowerClaimed);
            
            consumerLine1.SetPowerClaimed(1100);

            Console.WriteLine("____CHECK2____");


            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", centralLine1.GetName, centralLine1.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", centralLine2.GetName, centralLine2.GetPowerClaimed);

            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode1.GetOutputLine.GetName, consumerNode1.GetOutputLine.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode2.GetOutputLine.GetName, consumerNode2.GetOutputLine.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode3.GetOutputLine.GetName, consumerNode3.GetOutputLine.GetPowerClaimed);

            consumerLine2.SetPowerClaimed(450);
            //consumerLine1.SetPowerClaimed(1900);

            Console.WriteLine("____CHECK3____");

            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", centralLine1.GetName, centralLine1.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", centralLine2.GetName, centralLine2.GetPowerClaimed);

            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode1.GetOutputLine.GetName, consumerNode1.GetOutputLine.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode2.GetOutputLine.GetName, consumerNode2.GetOutputLine.GetPowerClaimed);
            Console.WriteLine("Ligne :: {0} ; powerClaimed : {1}", consumerNode3.GetOutputLine.GetName, consumerNode3.GetOutputLine.GetPowerClaimed);


            
            
            //consumerLine3.SetPowerClaimed(400);


            Console.WriteLine("C'est l'heure de vérité:: {0} for first one and {1} for second one", centralLine1.GetPowerClaimed, centralLine2.GetPowerClaimed);
        }
    }
}