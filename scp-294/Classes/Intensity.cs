using System.ComponentModel;
using System;

namespace scp_294.Classes
{
    public class Intensity
    {
        [Description("If you want a random intensity in a specific range, set this to -1")]
        public int FixedAmount { get; set; }

        [Description("This is the lowest amount of the range of intensity possible. If Fixed Amount is 0 or above these will be ignored")]
        public int LowestAmount { get; set; }

        [Description("This is the highest amount of the range of intensity possible. If Fixed Amount is 0 or above these will be ignored")]
        public int HighestAmount { get; set; }

        public int GetIntensity()
        {
            if(FixedAmount >= 0) return FixedAmount;

            return new Random().Next(LowestAmount, HighestAmount+1);
        }
    }
}
