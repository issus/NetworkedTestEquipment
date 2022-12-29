using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data
{
    public class ChannelSettings
    {
        public DP800Channel Channel { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }

        public double MaxVoltage { get; set; }
        public double MaxCurrent { get; set; }

        public ChannelSettings(DP800Channel channel, double voltage, double current, double maxVoltage, double maxCurrent)
        {
            Channel = channel;
            Voltage = voltage;
            Current = current;
            MaxVoltage = maxVoltage;
            MaxCurrent = maxCurrent;
        }

        public override string ToString()
        {
            return $"{Channel}: {Voltage}V [{MaxVoltage}V MAX] {Current}A [{MaxCurrent}A MAX]";
        }
    }
}
