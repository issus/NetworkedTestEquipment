using OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000
{
    /// <summary>
    /// Represents the various questionable status of the equipment.
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
    /// Represents the range options for the DL3000 instrument.
    /// </summary>
    public enum DL3000Range
    {
        /// <summary>
        /// Represents the minimum range.
        /// </summary>
        Minimum,
        /// <summary>
        /// Represents the maximum range.
        /// </summary>
        Maximum,
        /// <summary>
        /// Represents the default range.
        /// </summary>
        Default
    }

    /// <summary>
    /// Represents the available source functions for the DL3000 instrument.
    /// </summary>
    public enum SourceFunction
    {
        /// <summary>
        /// Represents the current source function.
        /// </summary>
        Current,
        /// <summary>
        /// Represents the resistance source function.
        /// </summary>
        Resistance,
        /// <summary>
        /// Represents the voltage source function.
        /// </summary>
        Voltage,
        /// <summary>
        /// Represents the power source function.
        /// </summary>
        Power
    }

    /// <summary>
    /// Represents the available function modes for the DL3000 instrument.
    /// </summary>
    public enum FunctionMode
    {
        /// <summary>
        /// Represents the fixed function mode.
        /// </summary>
        Fixed,
        /// <summary>
        /// Represents the list function mode.
        /// </summary>
        List,
        /// <summary>
        /// Represents the waveform function mode.
        /// </summary>
        Waveform,
        /// <summary>
        /// Represents the battery function mode.
        /// </summary>
        Battery
    }

    /// <summary>
    /// Represents the available transient modes for the DL3000 instrument.
    /// </summary>
    public enum TransientMode
    {
        /// <summary>
        /// Represents the continuous transient mode.
        /// </summary>
        Continuous,
        /// <summary>
        /// Represents the pulse transient mode.
        /// </summary>
        Pulse,
        /// <summary>
        /// Represents the toggle transient mode.
        /// </summary>
        Toggle
    }

    /// <summary>
    /// Represents a Rigol DL3000 instrument that communicates with the equipment via LXI/SCPI.
    /// </summary>
    public class RigolDL3000Instrument : NetworkTestInstrument
    {
        /// <summary>
        /// Gets the status information for the instrument.
        /// </summary>
        public Status Status { get; }
        /// <summary>
        /// Gets the measurement information for the instrument.
        /// </summary>
        public Measurement Measure { get; }
        /// <summary>
        /// Gets or sets the source information for the instrument.
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RigolDL3000Instrument"/> class with a type of <see cref="InstrumentType.Load"/>.
        /// </summary>
        public RigolDL3000Instrument() : base(InstrumentType.Load)
        {
            Status = new Status(this);
            Measure = new Measurement(this);
            Source = new Source(this);
        }
    }

}
