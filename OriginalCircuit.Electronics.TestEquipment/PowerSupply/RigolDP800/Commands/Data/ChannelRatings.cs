using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data
{
    /// <summary>
    /// Represents the ratings for a DP800 power supply channel.
    /// </summary>
    public class ChannelRatings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelRatings"/> class with the specified channel, maximum voltage, and maximum current.
        /// </summary>
        /// <param name="channel">The channel for which to set the ratings.</param>
        /// <param name="maxVoltage">The maximum voltage allowed on the channel, in volts.</param>
        /// <param name="maxCurrent">The maximum current allowed on the channel, in amps.</param>
        public ChannelRatings(DP800Channel channel, int maxVoltage, int maxCurrent)
        {
            Channel = channel;
            MaxVoltage = maxVoltage;
            MaxCurrent = maxCurrent;
        }

        /// <summary>
        /// Gets or sets the channel for which the ratings are set.
        /// </summary>
        public DP800Channel Channel { get; set; }

        /// <summary>
        /// Gets or sets the maximum voltage allowed on the channel, in volts.
        /// </summary>
        public int MaxVoltage { get; set; }

        /// <summary>
        /// Gets or sets the maximum current allowed on the channel, in amps.
        /// </summary>
        public int MaxCurrent { get; set; }

        /// <summary>
        /// Returns a string that represents the current <see cref="ChannelRatings"/> object.
        /// </summary>
        /// <returns>A string that represents the current <see cref="ChannelRatings"/> object.</returns>
        public override string ToString()
        {
            return $"{Channel}: {MaxVoltage}V {MaxCurrent}A";
        }
    }

}
