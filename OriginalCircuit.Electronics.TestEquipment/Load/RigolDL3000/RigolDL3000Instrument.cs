using OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000
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

    public enum DL3000Range
    {
        Minimum,
        Maximum,
        Default
    }

    public enum SourceFunction
    {
        Current,
        Resistance,
        Voltage,
        Power
    }

    public enum FunctionMode
    {
        Fixed,
        List,
        Waveform,
        Battery
    }

    public enum TransientMode
    {
        Continuous,
        Pulse,
        Toggle
    }

    public class RigolDL3000Instrument : NetworkTestInstrument
    {
        public Status Status { get; }
        public Measurement Measure { get; }
        public Source Source { get; set; }

        public RigolDL3000Instrument() : base(InstrumentType.Load)
        {
            Status = new Status(this);
            Measure = new Measurement(this);
            Source = new Source(this);
        }
    }
}
