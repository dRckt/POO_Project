﻿using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Line
    {
        private string name;
        private double currentPower;

        private double PowerClaimed;

        private double MaxPower;
        private string alertMessage;

        protected Node inputNode;
        protected Node OutputNode;
        private double PriorityLevel;

        private bool IsConsumerLine;
        private bool IsPowerPlantLine;
        private bool IsDissipatorLine;
        private bool OutputConnectedToNode;
        private bool InputConnectedToNode;
        private bool IsMarketLine;
        private bool IsBatteryLine;

        private PowerPlant myPowerPlant;
        private Consumer myConsumer;
        private Battery myBattery;

        public Line(string name)
        {
            this.name = name;
            MaxPower = 75000000;
            PowerClaimed = 0;
            SetCurrentPower(0);

            IsConsumerLine = false;
            IsPowerPlantLine = false;
            IsDissipatorLine = false;
            OutputConnectedToNode = false;
            InputConnectedToNode = false;
            IsMarketLine = false;
            IsBatteryLine = false;
            PriorityLevel = 0;
        }

        /// Propriétés GET
        public string GetName { get { return name; } }
        public double GetMaxPower { get { return MaxPower; } }
        public double GetCurrentPower { get { return currentPower; } }
        public Node GetInputNode { get { return inputNode; } }
        public Node GetOutputNode { get { return OutputNode; } }

        public bool GetIsMarketLine { get { return IsMarketLine; } }
        public double GetPriorityLevel { get { return PriorityLevel; } }
        public bool GetInputConnectedToNode { get { return InputConnectedToNode; } }
        public bool GetOutputConnectedToNode { get { return OutputConnectedToNode; } }

        public double GetPowerClaimed { get { return PowerClaimed; } }                 
        public string GetAlertMessage { get { return alertMessage; } }

        public bool GetIsConsumerLine { get { return IsConsumerLine; } }
        public bool GetIsDissipatorLine { get { return IsDissipatorLine; } }
        public bool GetIsBatteryLine { get { return IsBatteryLine; } }
        public bool GetIsPowerPlantLine { get { return IsPowerPlantLine; } }
        public PowerPlant GetMyPowerPlant { get { return myPowerPlant; } }
        public Consumer GetMyConsumer { get { return myConsumer; } }

        /// Methodes SET
        public void SetName(string Name) { name = Name; }
        public void SetInputNode(Node inputNode)
        {
            this.inputNode = inputNode;
            InputConnectedToNode = true;
        }
        public void SetOutputNode(Node OutputNode) 
        { 
            this.OutputNode = OutputNode;
            OutputConnectedToNode = true;
        }
        public void SetIsMarketLine(bool b) { IsMarketLine = b; }
        public void SetIsConsumerLine(bool b) { IsConsumerLine = b; }
        public void SetIsDissipatorLine(bool b) { IsDissipatorLine = b; }
        public void SetIsBatteryLine(bool b) { IsBatteryLine = b; }
        public void SetMyBattery(Battery myBattery) { this.myBattery = myBattery; }
        public void SetIsPowerPlantLine(bool b) //, PriorityLevel)
        { 
            IsPowerPlantLine = b;
            if (b)
            {
                SetMaxPower(250000000); //Les lignes des centrales sont plus grosses par défaut
            }
        }
        public void SetMyPowerPlant(PowerPlant p) { myPowerPlant = p; }
        public void SetMyConsumer(Consumer c) 
        { 
            myConsumer = c;
            SetIsConsumerLine(true);
        }

        public void SetMaxPower(double newMaxPower) { MaxPower = newMaxPower; }
        public void SetPriorityLevel(double priorityLevel) 
        {
            PriorityLevel = priorityLevel;
            if (OutputConnectedToNode)
            {
                OutputNode.UpdatePriorityLevel();
            }
        }
        public void SetCurrentPower(double newCurrentPower)
        {
            if (newCurrentPower > MaxPower)
            {
                alertMessage = String.Format("Required power on {0} is too high", name);
                currentPower = MaxPower;    
            }
            else
            {
                currentPower = newCurrentPower;
            }
        }
        public void SetPowerClaimed(double newPowerClaimed) 
        {
            if (GetIsConsumerLine)
            {
                if (GetPowerClaimed != newPowerClaimed)
                {
                    alertMessage = (String.Format("Mise à jour de la demande de {0}:: Ancienne demande: {1} ; Nouvelle demande: {2}", GetMyConsumer.GetName, GetPowerClaimed, newPowerClaimed));
                }
            }

            PowerClaimed = newPowerClaimed;
            if (GetIsPowerPlantLine) {/*pass*/}
            else
            {
                if (IsBatteryLine) {/*pass*/ }
                else { GetInputNode.UpdatePowerClaimed(); }
                
            }   
        }     
        public double GetDisponiblePower()
        {
            if (GetIsPowerPlantLine)
            {
                //return 1000;  POUR TEST
                return GetMyPowerPlant.DisponibleProduction();

            }
            else if (GetIsBatteryLine)
            {
                return myBattery.charge;
            }
            else
            {
                return GetInputNode.GetDisponiblePower(this);
            }            
        }
        public void UpdateCurrentPower(double newCurrentPower) 
        {
            if (GetDisponiblePower() > newCurrentPower){SetCurrentPower(newCurrentPower);}
            else
            {
                SetCurrentPower(GetDisponiblePower());
                alertMessage = String.Format("La ligne {0} requiert {1}W supplémentaire", GetName, (newCurrentPower - GetDisponiblePower()));
            }

            if (GetIsConsumerLine){/*pass*/}
            else{GetOutputNode.UpdateCurrentPower();}
        }
    }
}
