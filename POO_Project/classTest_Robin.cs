using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class classTest_Robin
    {

        public classTest_Robin()
        {
            Console.WriteLine("Hello World!");
            ConcentrationNode consumerNode1 = new ConcentrationNode("consumerNode1");
            ConcentrationNode consumerNode2 = new ConcentrationNode("consumerNode2");
            ConcentrationNode consumerNode3 = new ConcentrationNode("consumerNode3");
            ConcentrationNode consumerNode4 = new ConcentrationNode("consumerNode4");

            DistributionNode centralNode1 = new DistributionNode("centralNode1");
            //DistributionNode centralNode2 = new DistributionNode("centralNode2");
            //DistributionNode centralNode3 = new DistributionNode("centralNode3");

            List<Line> consumerLine1 = consumerNode1.GetOutputLineList;  //OUTPUT car noeud, input pour consomateur dans v final
            consumerLine1[0].SetIsConsumerLine(true);
            consumerLine1[0].SetPowerClaimed(5);
            consumerLine1[0].SetInputNode(consumerNode1);
            
            List<Line> consumerLine2 = consumerNode2.GetOutputLineList;
            consumerLine2[0].SetIsConsumerLine(true);
            consumerLine2[0].SetPowerClaimed(4);
            consumerLine2[0].SetInputNode(consumerNode2);

            List<Line> consumerLine3 = consumerNode3.GetOutputLineList;
            consumerLine3[0].SetIsConsumerLine(true);
            consumerLine3[0].SetPowerClaimed(3);
            consumerLine3[0].SetInputNode(consumerNode2);

            List<Line> consumerLine4 = consumerNode4.GetOutputLineList;
            consumerLine3[0].SetIsConsumerLine(true);
            consumerLine3[0].SetPowerClaimed(2);
            consumerLine3[0].SetInputNode(consumerNode4);

            List<Line> centralLineList = new List<Line> { centralNode1.GetInputLineList[0] };//, centralNode2.GetInputLineList[0], centralNode3.GetInputLineList[0] };
            centralNode1.GetInputLineList[0].SetOutputNode(centralNode1);


            

            DistributionNode midDistributionNode = new DistributionNode("midDistributionNode");
            ConcentrationNode midConcentrationNode = new ConcentrationNode("midConcentrationNode");

            ConcentrationNode midConcentrationNode_bis = new ConcentrationNode("midConcentrationNode_bis"); 
            DistributionNode midDistributionNode_bis = new DistributionNode("midDistributionNode_bis");



            Manager outsider = new Manager();
   

            //ETAGE 1
            outsider.ConnectDistributionToDistributionNode(centralNode1, midDistributionNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_FLOOR1", centralNode1, consumerNode1);
            //outsider.ConnectDistributionToConcentrationNode("LINE_BIS", centralNode1, midConcentrationNode_bis);
            //outsider.ConnectConcentrationToDistributionNode(midConcentrationNode_bis, midDistributionNode_bis);
            //outsider.ConnectDistributionToConcentrationNode("LINE_B", midDistributionNode_bis, consumerNode4);
            //ETAGE 2
            outsider.ConnectDistributionToConcentrationNode("LINE_FLOOR2.2", midDistributionNode, consumerNode2);
            outsider.ConnectDistributionToConcentrationNode("LINE_MID", midDistributionNode, midConcentrationNode);
            //ETAGE 3
            outsider.ConnectConcentrationToConcentrationNode(midConcentrationNode, consumerNode3);


         



            Node node = centralNode1.GetInputLine.GetOutputNode;
            Console.WriteLine("-------------------");
            Console.WriteLine(node);
            Console.WriteLine(node.GetOutputLineList[0]);
            Console.WriteLine("-------------------");


            Console.WriteLine("Pour l'instant tout est OKKKKKKK");

            consumerNode1.ShowState();
            Console.WriteLine(consumerLine1[0].GetPowerClaimed);
            Console.WriteLine("________");




        }



    }


    public class classTest_Robin2
    {
        public classTest_Robin2()
        {
            Manager outsider = new Manager();
            PowerPlant central1 = new NuclearPowerPlant("central1", new Market());
            central1.GetOutPutLine.SetMyPowerPlant(central1);
            outsider.GetPowerPlantList.Add(central1);

            Consumer consumer1 = new City("consumer1", 10000, new Weather("bx", new Clock()));
            outsider.GetConsumerList.Add(consumer1);
            Consumer consumer2 = new City("consumer2", 10000, new Weather("ch", new Clock()));
            outsider.GetConsumerList.Add(consumer2);


            outsider.ConnectDistributionToConcentrationNode("LINE1", central1.GetOutputNode, consumer1.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE2", central1.GetOutputNode, consumer2.GetInputNode);

            consumer1.UpdateClaimingPower();

            //consumer1.getInputLine.SetPowerClaimed(3);
            //consumer2.getInputLine.SetPowerClaimed(4);

            Console.WriteLine("______________________");
            Console.WriteLine(consumer1.getInputLine.GetPowerClaimed);
            Console.WriteLine(consumer1.GetClaimingPower);






        }
    }
}
