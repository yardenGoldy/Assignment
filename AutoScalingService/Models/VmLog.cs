using System;

namespace AutoScalingService.Models
{
    public class VmLog
    {
        public DateTime Timestamp { get; set; }
        public int NumberOfVms { get; set; }

        public VmAction Action { get; set; }
    }

    public enum VmAction { 
        INCREASE,
        DECREASE
    }
}
