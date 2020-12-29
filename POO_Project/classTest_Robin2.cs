using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class classTest_Robin2
    {
        public classTest_Robin2()
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
            /*
            List<Line> consumerLine3 = consumerNode3.GetOutputLineList;
            consumerLine3[0].SetIsConsumerLine(true);
            consumerLine3[0].SetPowerClaimed(42);
            consumerLine3[0].SetInputNode(consumerNode3);
            */
            List<Line> centralLineList = new List<Line> { centralNode1.GetInputLineList[0] };//, centralNode2.GetInputLineList[0], centralNode3.GetInputLineList[0] };
            centralNode1.GetInputLineList[0].SetOutPutNode(centralNode1);
            //centralNode2.GetInputLineList[0].SetOutPutNode(centralNode2);
            //centralNode3.GetInputLineList[0].SetOutPutNode(centralNode3);




            DistributionNode midDistributionNode = new DistributionNode("midDistributionNode");
            ConcentrationNode midConcentrationNode = new ConcentrationNode("midConcentrationNode");

            Manager outsider = new Manager();

            
            //outsider.ConnectTwoNode("mid-consumer2", midDistributionNode, consumerNode2);


            //midConcentrationNode.AddInputLineToList(centralNode1.GetOutputLineList[0]);
            //centralNode1.GetInputLineList[0].SetOutPutNode(midConcentrationNode);

            //midDistributionNode.AddOutputLineToList(consumerLine1[0]);
            //consumerLine1[0].SetInputNode(midDistributionNode);
            //midDistributionNode.AddOutputLineToList(consumerLine2[0]);
            //consumerLine2[0].SetInputNode(midDistributionNode);
            //midDistributionNode.AddOutputLineToList(consumerLine3[0]);
            //consumerLine3[0].SetInputNode(midDistributionNode);

            ////////
            ///
            //outsider.ConnectTwoNode("Line420", centralNode1, consumerNode1);


            Console.WriteLine("Pour l'instant tout est OKKKKKKK");

            consumerNode1.ShowState();
            Console.WriteLine(consumerLine1[0].GetPowerClaimed);
            Console.WriteLine("________");
            Console.WriteLine(String.Format("Resultat de la requete :: {0}", outsider.GetPowerClaimed(centralLineList)));



        }
    }
}