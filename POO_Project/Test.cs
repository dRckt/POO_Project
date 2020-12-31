using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Test
    {
        Market market;
        Clock clock;
        Weather weather_bx;
        Weather weather_os;



        
        public Test()
        {
            market = new Market();
            clock = new Clock();
            weather_bx = new Weather("Bruxelles", clock);
            weather_os = new Weather("Ostende", clock);

            // CREATION CENTRALES
            PowerPlant doel = new NuclearPowerPlant("doel", market);
            PowerPlant parc_eolien_os = new WindFarm("parc éolien de Ostende", weather_os);
            PowerPlant esso = new GasPowerPlant("station à gaz esso", market);
            PowerPlant centrale_solaire_bx = new SolarPowerPlant("centrale solaire de bruxelles", weather_bx);

            // CREATION CONSOMATEUR
            Consumer bx = new City("Bruxelles", 1000000, weather_bx);
            Consumer os = new City("Ostende", 70000, weather_os);

            Consumer ad = new Entreprise("Audi", 10000);
            Consumer bm = new Entreprise("BMW", 5000);
            Consumer vw = new Entreprise("VolksWagen", 3000);
            Consumer mc = new Entreprise("Mercedes", 4000);

            Consumer diss1 = new Dissipator("Dissipateur 1");


            //CREATION DES PAIRES NOEUDS MID
            ConcentrationNode midC1 = new ConcentrationNode("midC1");
            ConcentrationNode midC2 = new ConcentrationNode("midC2");
            DistributionNode midD1 = new DistributionNode("midD1");
            DistributionNode midD2 = new DistributionNode("midD2");
            //Liaison des noeuds mid entre eux
            Manager outsider = new Manager();
            outsider.ConnectConcentrationToDistributionNode(midC1, midD1);
            outsider.ConnectConcentrationToDistributionNode(midC2, midD2);
            //Liaison des centrales aux noeuds mid
            outsider.ConnectDistributionToConcentrationNode("LINE_C1.1", doel.GetOutputNode, midC1);
            outsider.ConnectDistributionToConcentrationNode("LINE_C1.2", parc_eolien_os.GetOutputNode, midC1);
            outsider.ConnectDistributionToConcentrationNode("LINE_C2.1", parc_eolien_os.GetOutputNode, midC2);
            outsider.ConnectDistributionToConcentrationNode("LINE_C2.2", esso.GetOutputNode, midC2);
            outsider.ConnectDistributionToConcentrationNode("LINE_C2.3", centrale_solaire_bx.GetOutputNode, midC2);

            //liaison des noeuds mid aux consumers
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.1", midD1, bx.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.2", midD1, os.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.3", midD1, ad.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D1.4", midD1, bm.GetInputNode);

            outsider.ConnectDistributionToConcentrationNode("LINE_D2.1", midD2, ad.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D2.2", midD2, bm.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D2.3", midD2, vw.GetInputNode);
            outsider.ConnectDistributionToConcentrationNode("LINE_D2.4", midD2, mc.GetInputNode);

            ///TEST
            ///

            Console.WriteLine("______________");
            Console.WriteLine("____CHECK1____");
            bx.getInputLine.SetPowerClaimed(2);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");


            Console.WriteLine("____CHECK2____");
            vw.getInputLine.SetPowerClaimed(3);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");


            Console.WriteLine("____CHECK3____");
            mc.getInputLine.SetPowerClaimed(5);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");

            
            Console.WriteLine("____CHECK4____");
            bx.getInputLine.SetPowerClaimed(7);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");
            

            Console.WriteLine("____CHECK5____");
            bm.getInputLine.SetPowerClaimed(11);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK6____");
            os.getInputLine.SetPowerClaimed(13);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK7____");
            vw.getInputLine.SetPowerClaimed(17);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK8____");
            os.getInputLine.SetPowerClaimed(8345);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");

            Console.WriteLine("____CHECK9____");
            vw.getInputLine.SetPowerClaimed(4324);
            showTest(doel, parc_eolien_os, esso, centrale_solaire_bx, bx, os, ad, bm, vw, mc);
            Console.WriteLine("______________");





        }

        public void showTest(PowerPlant doel, PowerPlant parc_eolien_os, PowerPlant esso, PowerPlant centrale_solaire_bx, Consumer bx, Consumer os, Consumer ad, Consumer bm, Consumer vw, Consumer mc)
        {
            Console.WriteLine("CENTRALES ::: (claimedPowerActuellement sur ligne de sortie)");
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", doel.GetName, doel.Production(), doel.GetOutPutLine.GetPowerClaimed);
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", parc_eolien_os.GetName, parc_eolien_os.Production(), parc_eolien_os.GetOutPutLine.GetPowerClaimed);
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", esso.GetName, esso.Production(), esso.GetOutPutLine.GetPowerClaimed);
            Console.WriteLine("Centrale :: {0}  ;  Production : {1}  ;  ClaimedPower : {2}", centrale_solaire_bx.GetName, centrale_solaire_bx.Production(), centrale_solaire_bx.GetOutPutLine.GetPowerClaimed);
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


    }
}
