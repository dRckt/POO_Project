using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Project
{
    public class Clock
    {
        double StartTime;
        double hour;
        public Clock()
        {
            StartTime = 0 ;
            hour = StartTime ;
        }
        public void UpdateClock(double pas)
        {
            if (hour == 23)
            {
                hour = 0 ;
            }
            else
            {
                hour += pas;
            }
        }

        public double GetHour { get { return hour; } }
    }
}
