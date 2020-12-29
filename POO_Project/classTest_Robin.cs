﻿using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    class classTest_Robin
    {

        public classTest_Robin()
        {
            Console.WriteLine("Hello World!");
            ConcentrationNode consumerNode1 = new ConcentrationNode("consumerNode1");
            ConcentrationNode consumerNode2 = new ConcentrationNode("consumerNode2");
            //ConcentrationNode consumerNode3 = new ConcentrationNode("consumerNode3");

            DistributionNode centralNode1 = new DistributionNode("centralNode1");
            //DistributionNode centralNode2 = new DistributionNode("centralNode2");
            //DistributionNode centralNode3 = new DistributionNode("centralNode3");



            List<Line> consumerLine1 = consumerNode1.GetOutputLineList;  //OUTPUT car noeud, input pour consomateur dans v final
            consumerLine1[0].SetIsConsumerLine(true);
            consumerLine1[0].SetPowerClaimed(42);
            consumerLine1[0].SetInputNode(consumerNode1);
            
            List<Line> consumerLine2 = consumerNode2.GetOutputLineList;
            consumerLine2[0].SetIsConsumerLine(true);
            consumerLine2[0].SetPowerClaimed(42);
            consumerLine2[0].SetInputNode(consumerNode2);

            List<Line> centralLineList = new List<Line> { centralNode1.GetInputLineList[0] };//, centralNode2.GetInputLineList[0], centralNode3.GetInputLineList[0] };
            centralNode1.GetInputLineList[0].SetOutPutNode(centralNode1);


            

            DistributionNode midDistributionNode = new DistributionNode("midDistributionNode");
            ConcentrationNode midConcentrationNode = new ConcentrationNode("midConcentrationNode");

            Manager outsider = new Manager();
            //outsider.ConnectDistributionToConcentrationNode("TEST-LINE", centralNode1, consumerNode1);

            
            outsider.ConnectDistributionToDistributionNode(centralNode1, midDistributionNode);
            //outsider.ConnectDistributionToConcentrationNode("Distr/Conc_Line", midDistributionNode, midConcentrationNode);
            //concentrationToDistribution a tester
            outsider.ConnectDistributionToConcentrationNode("BLAKLALA", midDistributionNode, consumerNode1);
            outsider.ConnectDistributionToConcentrationNode("ssssss", midDistributionNode, midConcentrationNode);
            outsider.ConnectConcentrationToConcentrationNode(midConcentrationNode, consumerNode2);


         



            Node node = centralNode1.GetInputLine.GetOutputNode;
            Console.WriteLine("-------------------");
            Console.WriteLine(node);
            Console.WriteLine(node.GetOutputLineList[0]);
            Console.WriteLine("-------------------");


            Console.WriteLine("Pour l'instant tout est OKKKKKKK");

            consumerNode1.showState();
            Console.WriteLine(consumerLine1[0].GetPowerClaimed);
            Console.WriteLine("________");
            Console.WriteLine(String.Format("Resultat de la requete :: {0}", outsider.GetPowerClaimed(centralLineList)));



        }
    }
}
