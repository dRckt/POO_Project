using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class manager
    {

        protected List<PowerPlant> PowerPlantList;
        protected List<Consumer> ConsumerList;


        public manager()
        {
            Console.WriteLine("");
            this.PowerPlantList = new List<PowerPlant> { };
            this.ConsumerList = new List<Consumer> { };
        }

        public PowerPlant CreateNewPowerPlant()
        {
            //interction avec le terminal pour donner les paramètres de la centrale (type, production, etc..)
            //!!centrale éteinte quand elle est créé

            //voir type de centrale qu'on crée (nucleaire, gaz, ..) mais cas général:
            PowerPlant NewPowerPlant = new PowerPlant("<nom choisi par input terminal>");
            this.PowerPlantList.Add(NewPowerPlant);
            
            return NewPowerPlant;
        }

        public Consumer CreateNewConsumer()
        {
            //interction avec le terminal pour donner les paramètres du consommateur

            //voir type de consommateur qu'on créé, mais cas général:
            Consumer NewConsumer = new Consumer("<nom choisi par input terminal>");
            this.ConsumerList.Add(NewConsumer); 

            return NewConsumer;
        }

        public void ConnectDistributionToConcentrationNode(string ConnexionLineName, distributionNode DistributionNode, concentrationNode ConcentrationNode)
        {
            Line ConnexionLine = new Line(ConnexionLineName);
            DistributionNode.addOutputLine(ConnexionLine);
            ConcentrationNode.addInputLine(ConnexionLine);

        }

        public void UpdateClaimingOfConsumer() //devra probablement prendre un dictionnaire en param, voir interaction avec terminal pour récupérer ce dict
        {
            //parcourir tous les consumer (clé) du dict et leur attribuer un nouveau claimingPower (valeur)
            
            //
        }
        public void UpdatePowerOfPowerPlant()
        {
            //(éventuellement d'abord appel de la mise a jour des puissances réclamées par les clients (UpdateClaimingOfConsumer), voir interactions avc terminal)

            //parcour les centrales
                
                //METHODE RECURSIVE qui part d'une LISTE de noeud (return double sum)
                //variable sum=0
                //parcourir les noeuds de la liste
                    //demander au noeud si il appartient a un consumer (getIsConsumerNode = true;)
                        //si oui:
                            //recupérer la puissance réclamée du consumer et l'ajouter a sum (sum += ...)
                        //si non:
                            //récupérer la liste de noeuds (ou l'unique noeud) auquel on est connecté
                            //APPEL RECURSIF
                            //sum += RETURN de l'appel recursif
                 //RETURN SUM (quand on a fini de parcourir les noeuds)
                 //La centrale qu'on parcourait attribue 'sum' comme étant sa puissance a fournir (DAMIEN)
            //fin du parcour =>toutes les centrales devraient avoir mis a jour la puissance

        }
    }
}
