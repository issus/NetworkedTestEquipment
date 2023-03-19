using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data
{
    /// <summary>
    /// The ChannelSettings class represents settings for a DP800 channel, including the channel number, voltage, current, and their maximums.
    /// </summary>
    public class ChannelSettings
    {
        /// <summary>
        /// The channel number of the DP800Channel instance.
        /// </summary>
        public DP800Channel Channel { get; set; }

        /// <summary>
        /// The voltage value to be set on the channel.
        /// </summary>
        public double Voltage { get; set; }

        /// <summary>
        /// The current value to be set on the channel.
        /// </summary>
        public double Current { get; set; }

        /// <summary>
        /// The maximum voltage allowed for the channel.
        /// </summary>
        public double MaxVoltage { get; set; }

        /// <summary>
        /// The maximum current allowed for the channel.
        /// </summary>
        public double MaxCurrent { get; set; }

        /// <summary>
        /// Initializes a new instance of the ChannelSettings class with the specified channel number, voltage, current, maximum voltage, and maximum current.
        /// </summary>
        /// <param name="channel">The channel number of the DP800Channel instance.</param>
        /// <param name="voltage">The voltage value to be set on the channel.</param>
        /// <param name="current">The current value to be set on the channel.</param>
        /// <param name="maxVoltage">The maximum voltage allowed for the channel.</param>
        /// <param name="maxCurrent">The maximum current allowed for the channel.</param>
        public ChannelSettings(DP800Channel channel, double voltage, double current, double maxVoltage, double maxCurrent)
        {
            Channel = channel;
            Voltage = voltage;
            Current = current;
            MaxVoltage = maxVoltage;
            MaxCurrent = maxCurrent;
        }

        /// <summary>
        /// Returns a string representation of the ChannelSettings instance.
        /// </summary>
        /// <returns>A string representation of the ChannelSettings instance.</returns>
        public override string ToString()
        {
            return $"{Channel}: {Voltage}V [{MaxVoltage}V MAX] {Current}A [{MaxCurrent}A MAX]";
        }
    }

}
