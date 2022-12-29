using OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands;
using OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800
{

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

    public enum OutputMode
    {
        ConstantCurrent,
        ConstantVoltage,
        Unregulated
    }

    public enum DP800Channel
    {
        CH1 = 1,
        CH2 = 2,
        CH3 = 3
    }

    public class RigolDP800Instrument : NetworkTestInstrument
    {
        public Status Status { get; }
        public Measurement Measure { get; }
        public Output Output { get; }
        public Source Current { get; }
        public Source Voltage { get; }

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

        public async Task RecallState(int storageLocation)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"*RCL {storageLocation}");
        }

        public async Task SaveState(int storageLocation)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"*SAV {storageLocation}");
        }

        public async Task SelectChannel(DP800Channel chan)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"INST:NSEL {(int)chan}");
        }

        public async Task<DP800Channel> QuerySelectedChannel()
        {
            await ClearBuffer();
            await SendCommand("INST:NSEL?");

            return (DP800Channel)(await ReadInt());
        }

        public async Task<ChannelRatings?> QuerySelectedChannelRatings()
        {
            await ClearBuffer();
            await SendCommand("INST?");

            string resp = await ReadString();

            Regex regex = new Regex("CH(?<ch>\\d):(?<v>-?\\d+)V/(?<a>\\d+)A");
            var match = regex.Match(resp);
            if (!match.Success)
                return null;

            return new ChannelRatings(
                (DP800Channel)(match.Groups["ch"].Value[0] - '0'),
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

        public async Task<ChannelSettings?> QueryChannel(DP800Channel chan)
        {
            await ClearBuffer();
            await SendCommand($"APPL? {chan}");

            string resp = await ReadString();

            Regex regex = new Regex("CH(?<ch>\\d):(?<maxv>-?\\d+)V/(?<maxa>\\d+)A,(?<v>-?\\d+(?:\\.\\d+)?),(?<a>-?\\d+(?:\\.\\d+)?)");
            var match = regex.Match(resp);
            if (!match.Success)
                return null;

            return new ChannelSettings(
                (DP800Channel)(match.Groups["ch"].Value[0] - '0'),
                double.Parse(match.Groups["v"].Value),
                double.Parse(match.Groups["a"].Value),
                int.Parse(match.Groups["maxv"].Value),
                int.Parse(match.Groups["maxa"].Value)
                );
        }
    }
}
