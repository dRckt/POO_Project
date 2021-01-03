using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class RESEAUX_1
    {
        Manager outsider;
        Market market;
        Clock clock;
        Weather weather_bx;
        Weather weather_os;

        protected PowerPlant doel;
        protected PowerPlant parc_eolien_os;
        protected PowerPlant esso;
        protected PowerPlant centrale_solaire_bx;

        protected Consumer bx;
        protected Consumer os;
        protected Consumer ad;
        protected Consumer bm;
        protected Consumer vw;
        protected Consumer mc;

        public RESEAUX_1()
        {
            outsider = new Manager();
            market = new Market();
            clock = new Clock();
            weather_bx = new Weather("Bruxelles", clock);
            weather_os = new Weather("Ostende", clock);

            // CREATION CENTRALES
            PowerPlant doel = outsider.CreateNewNuclearPowerPlant("doel", market);
            PowerPlant parc_eolien_os = outsider.CreateNewWindFarm("parc éolien de Ostende", weather_os);
            PowerPlant esso = outsider.CreateNewGasPowerPlant("station à gaz esso", market);
            PowerPlant centrale_solaire_bx = outsider.CreateNewSolarPowerPlant("centrale solaire de bruxelles", weather_bx);
            PowerPlant shop1 = outsider.CreateNewPurchasedAbroad("shop1", market);

            // CREATION CONSOMATEUR
            Consumer bx = outsider.CreateNewCity("Bruxelles", 1000000, weather_bx);
            Consumer os = outsider.CreateNewCity("Ostende", 70000, weather_os);

            Consumer ad = outsider.CreateNewEntreprise("Audi", 10000);
            Consumer bm = outsider.CreateNewEntreprise("BMW", 5000);
            Consumer vw = outsider.CreateNewEntreprise("VolksWagen", 3000);
            Consumer mc = outsider.CreateNewEntreprise("Mercedes", 4000);

            //CREATION DES PAIRES NOEUDS MID
            ConcentrationNode midC1 = outsider.CreateNewConcentrationNode("midC1");
            ConcentrationNode midC2 = outsider.CreateNewConcentrationNode("midC2");
            DistributionNode midD1 = outsider.CreateNewDistributionNode("midD1");
            DistributionNode midD2 = outsider.CreateNewDistributionNode("midD2");

            //Liaison des noeuds mid entre eux
            outsider.ConnectConcentrationToDistributionNode(midC1, midD1);
            outsider.ConnectConcentrationToDistributionNode(midC2, midD2);
            //Liaison des centrales aux noeuds mid
            outsider.ConnectDistributionToConcentrationNode("LINE_C1.1", doel.GetOutputNode, midC1);
            outsider.ConnectDistributionToConcentrationNode("LINE_C1.2", parc_eolien_os.GetOutputNode, midC1);
            outsider.ConnectDistributionToConcentrationNode("LINE_C2.1", parc_eolien_os.GetOutputNode, midC2);
            outsider.ConnectDistributionToConcentrationNode("LINE_C2.2", esso.GetOutputNode, midC2);
            outsider.ConnectDistributionToConcentrationNode("LINE_C2.3", centrale_solaire_bx.GetOutputNode, midC2);
                ///magasins
            outsider.ConnectDistributionToConcentrationNode("LINE_SHOP1", shop1.GetOutputNode, midC1);
            outsider.ConnectDistributionToConcentrationNode("LINE_SHOP1", shop1.GetOutputNode, midC2);

            //liaison des noeuds mid aux consumers
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.1", midD1, bx.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.2", midD1, os.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.3", midD1, ad.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.4", midD1, bm.GetInputNode);

            outsider.ConnectDistributionToConcentrationNode("LINE_D2.1", midD2, ad.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D2.2", midD2, bm.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D2.3", midD2, vw.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D2.4", midD2, mc.GetInputNode);

            

        }

        public Manager GetManagerInstance(){ return outsider; }
      

        //A supprimer ::
        public void showTest()
        {
            Console.WriteLine("CENTRALES ::: (claimedPowerActuellement sur ligne de sortie)");
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", doel.GetName, doel.GetPowerProduction, doel.GetOutPutLine.GetPowerClaimed);
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", parc_eolien_os.GetName, parc_eolien_os.GetPowerProduction, parc_eolien_os.GetOutPutLine.GetPowerClaimed);
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", esso.GetName, esso.GetPowerProduction, esso.GetOutPutLine.GetPowerClaimed);
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", centrale_solaire_bx.GetName, centrale_solaire_bx.GetPowerProduction, centrale_solaire_bx.GetOutPutLine.GetPowerClaimed);
            Console.WriteLine("Sum of power claimed : {0}", (doel.GetOutPutLine.GetPowerClaimed + parc_eolien_os.GetOutPutLine.GetPowerClaimed + esso.GetOutPutLine.GetPowerClaimed + centrale_solaire_bx.GetOutPutLine.GetPowerClaimed));

            Console.WriteLine("CONSUMERS :::  (currentPower et claimedPower actuellement sur ligne d'entrée");
            Console.WriteLine("Consumer :: {0}  ; CurrentPower : {1} ;  ClaimedPower : {2}", bx.GetName, bx.getInputLine.GetCurrentPower, bx.getInputLine.GetPowerClaimed);
            Console.WriteLine("Consumer :: {0}  ; CurrentPower : {1} ;  ClaimedPower : {2}", os.GetName, os.getInputLine.GetCurrentPower, os.getInputLine.GetPowerClaimed);
            Console.WriteLine("Consumer :: {0}  ; CurrentPower : {1} ;  ClaimedPower : {2}", ad.GetName, ad.getInputLine.GetCurrentPower, ad.getInputLine.GetPowerClaimed);
            Console.WriteLine("Consumer :: {0}  ; CurrentPower : {1} ;  ClaimedPower : {2}", bm.GetName, bm.getInputLine.GetCurrentPower, bm.getInputLine.GetPowerClaimed);
            Console.WriteLine("Consumer :: {0}  ; CurrentPower : {1} ;  ClaimedPower : {2}", vw.GetName, vw.getInputLine.GetCurrentPower, vw.getInputLine.GetPowerClaimed);
            Console.WriteLine("Consumer :: {0}  ; CurrentPower : {1} ;  ClaimedPower : {2}", mc.GetName, mc.getInputLine.GetCurrentPower, mc.getInputLine.GetPowerClaimed);
            Console.WriteLine("Sum of power claimed : {0}", (bx.getInputLine.GetPowerClaimed + os.getInputLine.GetPowerClaimed + ad.getInputLine.GetPowerClaimed + bm.getInputLine.GetPowerClaimed + vw.getInputLine.GetPowerClaimed + mc.getInputLine.GetPowerClaimed));

            /*
            Console.WriteLine("LINES :::");
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", doel.GetOutputNode.GetOutputLineList[0].GetName, doel.GetOutputNode.GetOutputLineList[0].GetCurrentPower, doel.GetOutputNode.GetOutputLineList[0].GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", parc_eolien_os.GetOutputNode.GetOutputLineList[0].GetName, parc_eolien_os.GetOutputNode.GetOutputLineList[0].GetCurrentPower, parc_eolien_os.GetOutputNode.GetOutputLineList[0].GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", parc_eolien_os.GetOutputNode.GetOutputLineList[1].GetName, parc_eolien_os.GetOutputNode.GetOutputLineList[1].GetCurrentPower, parc_eolien_os.GetOutputNode.GetOutputLineList[1].GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", esso.GetOutputNode.GetOutputLineList[0].GetName, esso.GetOutputNode.GetOutputLineList[0].GetCurrentPower, esso.GetOutputNode.GetOutputLineList[0].GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", centrale_solaire_bx.GetOutputNode.GetOutputLineList[0].GetName, centrale_solaire_bx.GetOutputNode.GetOutputLineList[0].GetCurrentPower, centrale_solaire_bx.GetOutputNode.GetOutputLineList[0].GetPowerClaimed);

            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", bx.getInputLine.GetName, bx.getInputLine.GetCurrentPower, bx.getInputLine.GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", os.getInputLine.GetName, os.getInputLine.GetCurrentPower, os.getInputLine.GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", ad.getInputLine.GetName, ad.getInputLine.GetCurrentPower, ad.getInputLine.GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", bm.getInputLine.GetName, bm.getInputLine.GetCurrentPower, bm.getInputLine.GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", vw.getInputLine.GetName, vw.getInputLine.GetCurrentPower, vw.getInputLine.GetPowerClaimed);
            Console.WriteLine("Line :: {0}      ; CurrentPower : {1}  ; ClaimedPower : {2}", mc.getInputLine.GetName, mc.getInputLine.GetCurrentPower, mc.getInputLine.GetPowerClaimed);
            */
        }
        public void funcTest()
        {

            Console.WriteLine("______________");
            Console.WriteLine("____CHECK1____");
            bx.getInputLine.SetPowerClaimed(2);

            showTest();
            Console.WriteLine("______________");


            Console.WriteLine("____CHECK2____");
            vw.getInputLine.SetPowerClaimed(3);
            showTest();
            Console.WriteLine("______________");


            Console.WriteLine("____CHECK3____");
            mc.getInputLine.SetPowerClaimed(5);
            showTest();
            Console.WriteLine("______________");


            Console.WriteLine("____CHECK4____");
            bx.getInputLine.SetPowerClaimed(7);
            showTest();
            Console.WriteLine("______________");


            Console.WriteLine("____CHECK5____");
            bm.getInputLine.SetPowerClaimed(11);
            showTest();
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK6____");
            os.getInputLine.SetPowerClaimed(13);
            showTest();
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK7____");
            vw.getInputLine.SetPowerClaimed(17);
            showTest();
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK8____");
            os.getInputLine.SetPowerClaimed(8345);
            showTest();
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK9____");
            vw.getInputLine.SetPowerClaimed(4324);
            showTest();
            Console.WriteLine("______________");





        }



    }
}
