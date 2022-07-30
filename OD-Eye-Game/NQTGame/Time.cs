using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQTGame
{
    public class Time
    {
        public int Seconds { get; set; } = 0;
        public int Minutes { get; set; } = 0;
        public int Hours { get; set; } = 0;
        public Time(int time) : this(time + 0.0) { }
        public Time(double time)
        {
            this.Seconds = (int)time % 60;
            this.Minutes = (int)(time / 60)%60;
            this.Hours = (int)(time / 360);
        }
        public Time(int Secounds, int Minutes, int Hours)
        {
            this.Seconds = Secounds;
            this.Minutes = Minutes;
            this.Hours = Hours;
        }
        public override string ToString()
        {
            string h = (Hours > 10) ? Hours + "" : "0" + Hours;
            string m = (Minutes > 10) ? Minutes + "" : "0" + Minutes;
            string s = (Seconds > 10) ? Seconds + "" : "0" + Seconds;

            if (Hours > 0)
                return h + ":" + m + ":" + s;
            else
                return m + ":" + s;
        }
    }
}
