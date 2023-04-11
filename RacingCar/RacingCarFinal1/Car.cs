using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingCarFinal1
{
    public class Car
    {
        public int Id { get; set; }
        public string? CarName { get; set; }
        public int SpeedPerHour { get; set; }
        public decimal SpeedPerSecond { get; set; }
        public int ReachTime { get; set; }
        public float DistanceStart { get; set; }
        public int DistanceEnd { get; set; }

    }
}
