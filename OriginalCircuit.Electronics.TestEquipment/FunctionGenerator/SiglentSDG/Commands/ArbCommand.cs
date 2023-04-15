using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents an arbitrary waveform command for a Siglent SDG series function generator.
    /// </summary>
    public class ArbCommand
    {
        NetworkTestInstrument? equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArbCommand"/> class with the specified <see cref="NetworkTestInstrument"/>.
        /// </summary>
        /// <param name="instrument">The network test instrument to use for the arbitrary waveform command.</param>

        public ArbCommand(NetworkTestInstrument instrument)
        {
            equipment = instrument;
        }

        /// <summary>
        /// Sets the sampling rate, mode, and interpolation method for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sampling rate for (C1 or C2).</param>
        /// <param name="mode">The mode to set (DDS or TARB).</param>
        /// <param name="sampleRate">The desired sample rate in Sa/s.</param>
        /// <param name="interpolation">The desired interpolation method (LINE or HOLD).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetSampleRate(string channel, SampleRateMode mode, double sampleRate, InterpolationMethod interpolation)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:SampleRATE MODE,{mode},VALUE,{sampleRate},INTER,{interpolation}");
        }

        /// <summary>
        /// Queries the sampling rate, mode, and interpolation method for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the sampling rate for (C1 or C2).</param>
        /// <returns>A tuple containing the mode, sample rate (in Sa/s), and interpolation method.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<(SampleRateMode Mode, double SampleRate, InterpolationMethod Interpolation)> GetSampleRate(string channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"{channel}:SRATE?");

            var response = await equipment.ReadString();
            var parts = response.Split(',');

            var mode = Enum.Parse<SampleRateMode>(parts[1]);
            double sampleRate = mode == SampleRateMode.TARB ? double.Parse(parts[3]) : 0;
            var interpolation = mode == SampleRateMode.TARB ? Enum.Parse<InterpolationMethod>(parts[5]) : default;

            return (mode, sampleRate, interpolation);
        }


        /// <summary>
        /// Represents the mode for the sampling rate command.
        /// </summary>
        public enum SampleRateMode
        {
            /// <summary>
            /// Direct Digital Synthesis mode.
            /// </summary>
            DDS,

            /// <summary>
            /// TrueArb mode.
            /// </summary>
            TARB
        }

        /// <summary>
        /// Represents the interpolation method for the sampling rate command.
        /// </summary>
        public enum InterpolationMethod
        {
            /// <summary>
            /// Linear interpolation.
            /// </summary>
            LINE,

            /// <summary>
            /// Zero-order hold interpolation.
            /// </summary>
            HOLD
        }

    }
}
