using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class classTest_Robin2
    {
        public classTest_Robin2()
        {
            Consumer consumer1 = new Consumer("consumer1");
            Consumer consumer2 = new Consumer("consumer2");
            Consumer consumer3 = new Consumer("consumer3");
            Consumer consumer4 = new Consumer("consumer4");


            Console.WriteLine("Hello World!");
           
            ConcentrationNode consumerNode1 = new ConcentrationNode("consumerNode1");
            ConcentrationNode consumerNode2 = new ConcentrationNode("consumerNode2");
            ConcentrationNode consumerNode3 = new ConcentrationNode("consumerNode3");
            ConcentrationNode consumerNode4 = new ConcentrationNode("consumerNode4");


            PowerPlant central1 = new PowerPlant("central1");
            DistributionNode centralNode1 = central1.GetOutputNode;

            /*
            List<Line> consumerLine1 = consumerNode1.GetOutputLineList;  //OUTPUT car noeud, input pour consomateur dans v final
            consumerLine1[0].SetIsConsumerLine(true);
            consumerLine1[0].SetPowerClaimed(5);
            consumerLine1[0].SetInputNode(consumerNode1);
            */

            Line consumerLine1 = consumerNode1.GetOutputLine;
            consumerLine1.SetPowerClaimed(5);

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
            //Console.WriteLine(consumerLine1[0].GetPowerClaimed);
            Console.WriteLine("________");
            Console.WriteLine(String.Format("Resultat de la requete :: {0}", outsider.GetPowerClaimed(centralLineList)));



        }
    }
}