using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class classTest_Robin2
    {
        public classTest_Robin2()
        {
            Manager outsider = new Manager();
            PowerPlant central1 = new NuclearPowerPlant("central1", new Market());
            central1.GetOutPutLine.SetMyPowerPlant(central1);
            central1.GetOutputNode.SetIsPowerPlantNode(true);
            outsider.GetPowerPlantList.Add(central1);

            Consumer consumer1 = new Consumer("consumer1");
            outsider.GetConsumerList.Add(consumer1);
            Consumer consumer2 = new Consumer("consumer2");
            outsider.GetConsumerList.Add(consumer2);


            outsider.ConnectDistributionToConcentrationNode("LINE1", central1.GetOutputNode, consumer1.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE2", central1.GetOutputNode, consumer2.GetInputNode);

            consumer1.getInputLine.SetPowerClaimed(3);
            consumer2.getInputLine.SetPowerClaimed(4);






        }
    }
}