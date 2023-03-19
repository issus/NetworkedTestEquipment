using OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands;
using OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800
{
    /// <summary>
    /// Represents the questionable status of the DP800 series power supply instrument.
    /// </summary>
    [Flags]
    public enum QuestionableStatus
    {
        /// <summary>
        /// Voltage fault. Overvoltage and reverse voltage occurred; the OV
        /// or the RV condition is removed.
        /// </summary>
        VoltageFault = 1,
        /// <summary>
        /// Overcurrent occurred.
        /// </summary>
        Overcurrent = 2,
        /// <summary>
        /// Remote Sense terminal connection
        /// </summary>
        RemoteSenseTerminal = 4,
        /// <summary>
        /// Overpower occurred
        /// </summary>
        Overpower = 8,
        /// <summary>
        /// Runs in List mode
        /// </summary>
        Run = 128,
        /// <summary>
        /// Remote Reverse Voltage. When reverse voltage occurs to the
        /// remote terminals, the bit and VF bit are set. When the reverse
        /// voltage is removed, the RRV bit is cleared, but the VF bit will not
        /// be cleared.
        /// </summary>
        RemoteReverseVoltage = 512,
        /// <summary>
        /// Unregulated. The input is unregulated. When the input has been
        /// regulated, the bit is cleared
        /// </summary>
        Unregulated = 1024,
        /// <summary>
        /// Local Reverse Voltage. When reverse voltage occurs to the input
        /// terminals, the bit and VF bit are set. When the reverse voltage is
        /// removed, the LRV bit is cleared, but the VF bit will not be cleared.
        /// </summary>
        LocalReverseVoltage = 2048,
        /// <summary>
        /// Overvoltage. When OV occurs, the OV bit and VF bit are set, and
        /// the load is turned off.
        /// </summary>
        Overvoltage = 4096,
        /// <summary>
        /// Protection Shutdown. When overcurrent, overpower, or
        /// overtemperature occurred, the load's input is turned off
        /// (protection shutdown).
        /// </summary>
        ProtectiveShutdown = 8192,
        /// <summary>
        /// Voltage of sink current on. When the input voltage exceeds the
        /// set Von value, the load starts to sink the current.
        /// </summary>
        VoltageSink = 16384
    }

    /// <summary>
    /// Modes of operation for the DP800 series power supply output.
    /// </summary>
    public enum OutputMode
    {
        /// <summary>
        /// Output operates in constant current mode.
        /// </summary>
        ConstantCurrent,

        /// <summary>
        /// Output operates in constant voltage mode.
        /// </summary>
        ConstantVoltage,

        /// <summary>
        /// Output is unregulated.
        /// </summary>
        Unregulated
    }

    /// <summary>
    /// Channels available on the DP800 series power supply instrument.
    /// </summary>
    public enum DP800Channel
    {
        /// <summary>
        /// Channel 1 on the instrument.
        /// </summary>
        CH1 = 1,

        /// <summary>
        /// Channel 2 on the instrument.
        /// </summary>
        CH2 = 2,

        /// <summary>
        /// Channel 3 on the instrument.
        /// </summary>
        CH3 = 3
    }


    /// <summary>
    /// Represents a Rigol DP800 series programmable power supply instrument that can be controlled over a network connection.
    /// </summary>
    public class RigolDP800Instrument : NetworkTestInstrument
    {
        /// <summary>
        /// Provides access to the instrument's status register.
        /// </summary>
        public Status Status { get; }

        /// <summary>
        /// Provides access to the instrument's measurement functions.
        /// </summary>
        public Measurement Measure { get; }

        /// <summary>
        /// Provides access to the instrument's output functions.
        /// </summary>
        public Output Output { get; }

        /// <summary>
        /// Provides access to the instrument's current source functions.
        /// </summary>
        public Source Current { get; }

        /// <summary>
        /// Provides access to the instrument's voltage source functions.
        /// </summary>
        public Source Voltage { get; }

        /// <summary>
        /// Represents a Rigol DP800 series programmable power supply instrument that can be controlled over a network connection.
        /// </summary>
        public RigolDP800Instrument() : base(InstrumentType.Supply)
        {
            Measure = new Measurement (this);
            Output = new Output (this);
            Current = new Source(this, "CURR");
            Voltage = new Source(this, "VOLT");
            Status = new Status (this);
        }

        /// <summary>
        /// Query the installation status of the options.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<string>> QueryOptions()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();
            await SendCommand("*OPT?");

            return (await ReadString()).Split(',').ToList();
        }
        /// <summary>
        /// Recalls the state of the instrument from the specified storage location.
        /// </summary>
        /// <param name="storageLocation">The storage location to recall the state from.</param>
        public async Task RecallState(int storageLocation)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"*RCL {storageLocation}");
        }


        /// <summary>
        /// Saves the current state of the instrument to the specified storage location.
        /// </summary>
        /// <param name="storageLocation">The storage location to save the state to.</param>
        public async Task SaveState(int storageLocation)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"*SAV {storageLocation}");
        }

        /// <summary>
        /// Selects the specified DP800 channel for measurement and control.
        /// </summary>
        /// <param name="chan">The channel to select.</param>
        public async Task SelectChannel(DP800Channel chan)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"INST:NSEL {(int)chan}");
        }

        /// <summary>
        /// Queries the currently selected channel and returns it as a DP800Channel enum value.
        /// </summary>
        /// <returns>The currently selected channel.</returns>
        public async Task<DP800Channel> QuerySelectedChannel()
        {
            await ClearBuffer();
            await SendCommand("INST:NSEL?");

            return (DP800Channel)(await ReadInt());
        }

        /// <summary>
        /// Queries the ratings of the currently selected channel and returns them as a ChannelRatings object.
        /// </summary>
        /// <returns>A ChannelRatings object representing the ratings of the currently selected channel, or null if an error occurs.</returns>
        public async Task<ChannelRatings?> QuerySelectedChannelRatings()
        {
            await ClearBuffer();
            await SendCommand("INST?");

            string resp = await ReadString();

            // The response string should contain information about the ratings of the selected channel in the format "CHx:V/A",
            // where x is the channel number, V is the maximum voltage, and A is the maximum current. We'll use a regular expression
            // to extract this information from the response string.
            Regex regex = new Regex("CH(?<ch>\\d):(?<v>-?\\d+)V/(?<a>\\d+)A");
            var match = regex.Match(resp);
            if (!match.Success)
                return null;

            // Create a new ChannelRatings object with the extracted information and return it.
            return new ChannelRatings(
                (DP800Channel)(match.Groups["ch"].Value[0] - '0'),  // convert channel number to enum value
                int.Parse(match.Groups["v"].Value),
                int.Parse(match.Groups["a"].Value));
        }

        /// <summary>
        /// Select the specified channel as the current channel and set the voltage/current of
        /// this channel.
        /// </summary>
        /// <param name="voltage"></param>
        /// <param name="current"></param>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetChannel(double voltage, double current, DP800Channel chan)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"APPL {chan},{voltage:F4},{current:F4}");
        }

        /// <summary>
        /// Queries the specified DP800 channel for its settings and returns a ChannelSettings object representing those settings.
        /// </summary>
        /// <param name="chan">The channel to query.</param>
        /// <returns>A ChannelSettings object representing the settings of the specified channel, or null if an error occurs.</returns>
        public async Task<ChannelSettings?> QueryChannel(DP800Channel chan)
        {
            await ClearBuffer();
            await SendCommand($"APPL? {chan}");

            string resp = await ReadString();

            // The response string should contain information about the channel settings in the format "CHx:Vmax/Amax,V,A",
            // where x is the channel number, Vmax is the maximum voltage, Amax is the maximum current, V is the current voltage,
            // and A is the current current. We'll use a regular expression to extract this information from the response string.
            Regex regex = new Regex("CH(?<ch>\\d):(?<maxv>-?\\d+)V/(?<maxa>\\d+)A,(?<v>-?\\d+(?:\\.\\d+)?),(?<a>-?\\d+(?:\\.\\d+)?)");
            var match = regex.Match(resp);
            if (!match.Success)
                return null;

            // Create a new ChannelSettings object with the extracted information and return it.
            return new ChannelSettings(
                (DP800Channel)(match.Groups["ch"].Value[0] - '0'),  // convert channel number to enum value
                double.Parse(match.Groups["v"].Value),
                double.Parse(match.Groups["a"].Value),
                int.Parse(match.Groups["maxv"].Value),
                int.Parse(match.Groups["maxa"].Value)
            );
        }        

    }
}
