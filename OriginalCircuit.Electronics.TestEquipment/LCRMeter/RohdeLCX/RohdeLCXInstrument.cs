using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands;


namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX
{
    /// <summary>
    /// Rohde &amp; Schwarz LCX Instrument
    /// </summary>
    public class RohdeLCXInstrument : NetworkTestInstrument
    {
        /// <summary>
        /// The BIAS subsystem contains the commands for bias configuration.
        /// </summary>
        public BiasSubsystem BiasSubsystem { get; }
        /// <summary>
        /// The CORRection subsystem contains the commands for controlling the Open/Short and Load correction operations.
        /// </summary>
        public CorrectionSubsystem Correction { get; }
        /// <summary>
        /// The DATA subsystem contains the commands for managing files and data.
        /// </summary>
        public DataSubsystem Data { get; }
        /// <summary>
        /// The DIMeasure subsystem contains the commands for configuring and executing dynamic impedance measurements.
        /// </summary>
        public DynamicImpedanceSubsystem DynamicImpedance { get; }
        /// <summary>
        /// The DISPlay subsystem contains the commands for customizing the display appearance, and enables you to close messages.
        /// </summary>
        public DisplaySubsystem Display { get; }
        /// <summary>
        /// The FUNCtion subsystem enables you to configure the measurement type and ranges.
        /// </summary>
        public FunctionSubsystem Function { get; }
        /// <summary>
        /// The HANDler subsystem contains the commands for configuring the binning function.
        /// </summary>
        public BinningSubsystem Binning { get; }
        /// <summary>
        /// The HCOPy subsystem contains the commands for requesting information and configuring the format of hardcopies.
        /// </summary>
        public HardcopySubsystem Hardcopy { get; }
        /// <summary>
        /// The LOG subsystem contains the commands for setting the parameters of the measurement logging function.
        /// </summary>
        public LogSubsystem Log { get; }
        /// <summary>
        /// Commands to set the measurement mode, query the main parameters and accuracy, and to get the results of a measurement.
        /// </summary>
        public MeasurementCommands Measurement { get; }
        /// <summary>
        /// This system contains the commands for the status reporting system
        /// </summary>
        public StatusSubsystem Status { get; }
        /// <summary>
        /// The SYSTem subsystem contains the commands for general functions which do not directly affect instrument operation.
        /// </summary>
        public SystemSubsystem System { get; }
        /// <summary>
        /// Commands for setting the test signal and general measurement parameters.
        /// </summary>
        public TestSignalCommands TestSignal { get; }

        /// <summary>
        /// Initialize a new Rohde &amp; Schwarz LCX Instrument
        /// </summary>
        public RohdeLCXInstrument() : base(InstrumentType.LCRMeter)
        {
            BiasSubsystem = new BiasSubsystem(this);
            Correction = new CorrectionSubsystem(this);
            Data = new DataSubsystem(this);
            DynamicImpedance = new DynamicImpedanceSubsystem(this);
            Display = new DisplaySubsystem(this);
            Function = new FunctionSubsystem(this);
            Binning = new BinningSubsystem(this);
            Hardcopy = new HardcopySubsystem(this);
            Log = new LogSubsystem(this);
            Measurement = new MeasurementCommands(this);
            Measurement = new MeasurementCommands(this);
            Status = new StatusSubsystem(this);
            System = new SystemSubsystem(this);
            TestSignal = new TestSignalCommands(this);
}
    }
}
























