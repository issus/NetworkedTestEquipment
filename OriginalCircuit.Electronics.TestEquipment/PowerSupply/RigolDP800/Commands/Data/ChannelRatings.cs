using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data
{
    public class ChannelRatings
    {
        public ChannelRatings(DP800Channel channel, int maxVoltage, int maxCurrent)
        {
            Channel = channel;
            MaxVoltage = maxVoltage;
            MaxCurrent = maxCurrent;
        }

        public DP800Channel Channel { get; set; }
        public int MaxVoltage { get; set; }
        public int MaxCurrent { get; set; }

        public override string ToString()
        {
            return $"{Channel}: {MaxVoltage}V {MaxCurrent}A";
        }
    }
}
