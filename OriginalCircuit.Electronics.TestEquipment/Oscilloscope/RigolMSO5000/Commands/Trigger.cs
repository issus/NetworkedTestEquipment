using OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{
    /// <summary>
    /// Enum for trigger modes
    /// </summary>
    public enum TriggerMode
    {
        /// <summary>
        /// Trigger mode for edge triggering
        /// </summary>
        EDGE,

        /// <summary>
        /// Trigger mode for pulse triggering
        /// </summary>
        PULS,

        /// <summary>
        /// Trigger mode for slope triggering
        /// </summary>
        SLOP,

        /// <summary>
        /// Trigger mode for video triggering
        /// </summary>
        VID,

        /// <summary>
        /// Trigger mode for pattern triggering
        /// </summary>
        PATT,

        /// <summary>
        /// Trigger mode for duration triggering
        /// </summary>
        DUR,

        /// <summary>
        /// Trigger mode for timeout triggering
        /// </summary>
        TIM,

        /// <summary>
        /// Trigger mode for runt triggering
        /// </summary>
        RUNT,

        /// <summary>
        /// Trigger mode for window triggering
        /// </summary>
        WIND,

        /// <summary>
        /// Trigger mode for delay triggering
        /// </summary>
        DEL,

        /// <summary>
        /// Trigger mode for setup triggering
        /// </summary>
        SET,

        /// <summary>
        /// Trigger mode for n-edge triggering
        /// </summary>
        NEDG,

        /// <summary>
        /// Trigger mode for RS232 communication
        /// </summary>
        RS232,

        /// <summary>
        /// Trigger mode for IIC communication
        /// </summary>
        IIC,

        /// <summary>
        /// Trigger mode for SPI communication
        /// </summary>
        SPI,

        /// <summary>
        /// Trigger mode for CAN communication
        /// </summary>
        CAN,

        /// <summary>
        /// Trigger mode for FlexRay communication
        /// </summary>
        FLEX,

        /// <summary>
        /// Trigger mode for LIN communication
        /// </summary>
        LIN,

        /// <summary>
        /// Trigger mode for IIS communication
        /// </summary>
        IIS,

        /// <summary>
        /// Trigger mode for M1553 communication
        /// </summary>
        M1553
    }

    /// <summary>
    /// The coupling options available for instrument trigger.
    /// </summary>
    public enum TriggerCoupling
    {
        /// <summary>
        /// Blocks any DC components.
        /// </summary>
        AC,
        /// <summary>
        /// Allows DC and AC components to pass the trigger path.
        /// </summary>
        DC,
        /// <summary>
        /// Blocks the DC components and rejects the low frequency components.
        /// </summary>
        LFR,
        /// <summary>
        /// Rejects the high frequency components.
        /// </summary>
        HFR
    }

    /// <summary>
    /// Represents the trigger status of the instrument.
    /// </summary>
    public enum TriggerStatus
    {
        /// <summary>
        /// Trigger delay.
        /// </summary>
        TD,
        /// <summary>
        /// Waiting for trigger.
        /// </summary>
        WAIT,
        /// <summary>
        /// Running.
        /// </summary>
        RUN,
        /// <summary>
        /// Automatic trigger.
        /// </summary>
        AUTO,
        /// <summary>
        /// Stopped.
        /// </summary>
        STOP
    }

    /// <summary>
    /// Specifies the type of trigger sweep for an oscilloscope.
    /// </summary>
    public enum TriggerSweep
    {
        /// <summary>
        /// Auto trigger. The waveforms are displayed no matter whether the trigger
        /// conditions are met.
        /// </summary>
        AUTO,

        /// <summary>
        /// Normal trigger. The waveforms are displayed when trigger conditions
        /// are met. If the trigger conditions are not met, the oscilloscope displays the
        /// original waveforms and waits for another trigger.
        /// </summary>
        NORM,

        /// <summary>
        /// Single trigger. The oscilloscope waits for a trigger, displays the
        /// waveforms when the trigger conditions are met, and then stops.
        /// </summary>
        SIGN
    }

    /// <summary>
    /// Enum for defining the trigger edge source
    /// </summary>
    public enum TriggerEdgeSource
    {
        /// <summary>
        /// Channel 1
        /// </summary>
        CHAN1,

        /// <summary>
        /// Channel 2
        /// </summary>
        CHAN2,

        /// <summary>
        /// Channel 3
        /// </summary>
        CHAN3,

        /// <summary>
        /// Channel 4
        /// </summary>
        CHAN4,

        /// <summary>
        /// Digital I/O line 0
        /// </summary>
        D0,

        /// <summary>
        /// Digital I/O line 1
        /// </summary>
        D1,

        /// <summary>
        /// Digital I/O line 2
        /// </summary>
        D2,

        /// <summary>
        /// Digital I/O line 3
        /// </summary>
        D3,

        /// <summary>
        /// Digital I/O line 4
        /// </summary>
        D4,

        /// <summary>
        /// Digital I/O line 5
        /// </summary>
        D5,

        /// <summary>
        /// Digital I/O line 6
        /// </summary>
        D6,

        /// <summary>
        /// Digital I/O line 7
        /// </summary>
        D7,

        /// <summary>
        /// Digital I/O line 8
        /// </summary>
        D8,

        /// <summary>
        /// Digital I/O line 9
        /// </summary>
        D9,

        /// <summary>
        /// Digital I/O line 10
        /// </summary>
        D10,

        /// <summary>
        /// Digital I/O line 11
        /// </summary>
        D11,

        /// <summary>
        /// Digital I/O line 12
        /// </summary>
        D12,

        /// <summary>
        /// Digital I/O line 13
        /// </summary>
        D13,

        /// <summary>
        /// Digital I/O line 14
        /// </summary>
        D14,

        /// <summary>
        /// Digital I/O line 15
        /// </summary>
        D15,

        /// <summary>
        /// AC Line
        /// </summary>
        ACL
    }

    /// <summary>
    /// Specifies the slope of the trigger.
    /// </summary>
    public enum TriggerSlope
    {
        /// <summary>
        /// Indicates the rising edge.
        /// </summary>
        POS,
        /// <summary>
        /// Indicates the falling edge.
        /// </summary>
        NEG,
        /// <summary>
        /// Indicates the rising or falling edge.
        /// </summary>
        RFAL
    }

    /// <summary>
    /// Specifies the source of the trigger slope.
    /// </summary>
    public enum TriggerSlopeSource
    {
        /// <summary>
        /// Specifies channel 1 as the source of the trigger slope.
        /// </summary>
        CHAN1,
        /// <summary>
        /// Specifies channel 2 as the source of the trigger slope.
        /// </summary>
        CHAN2,
        /// <summary>
        /// Specifies channel 3 as the source of the trigger slope.
        /// </summary>
        CHAN3,
        /// <summary>
        /// Specifies channel 4 as the source of the trigger slope.
        /// </summary>
        CHAN4
    }

    /// <summary>
    /// Specifies the window of the trigger slope.
    /// </summary>
    public enum TriggerSlopeWindow
    {
        /// <summary>
        /// Only adjusts the upper limit of the trigger level.
        /// </summary>
        TA,
        /// <summary>
        /// Only adjusts the lower limit of the trigger level.
        /// </summary>
        TB,
        /// <summary>
        /// Adjusts the upper and lower limits of the trigger level at the same time.
        /// </summary>
        TAB
    }

    /// <summary>
    /// Specifies the available trigger pulse sources.
    /// </summary>
    public enum TriggerPulseSource
    {
        /// <summary>
        /// Digital output 0.
        /// </summary>
        D0,
        /// <summary>
        /// Digital output 1.
        /// </summary>
        D1,
        /// <summary>
        /// Digital output 2.
        /// </summary>
        D2,
        /// <summary>
        /// Digital output 3.
        /// </summary>
        D3,
        /// <summary>
        /// Digital output 4.
        /// </summary>
        D4,
        /// <summary>
        /// Digital output 5.
        /// </summary>
        D5,
        /// <summary>
        /// Digital output 6.
        /// </summary>
        D6,
        /// <summary>
        /// Digital output 7.
        /// </summary>
        D7,
        /// <summary>
        /// Digital output 8.
        /// </summary>
        D8,
        /// <summary>
        /// Digital output 9.
        /// </summary>
        D9,
        /// <summary>
        /// Digital output 10.
        /// </summary>
        D10,
        /// <summary>
        /// Digital output 11.
        /// </summary>
        D11,
        /// <summary>
        /// Digital output 12.
        /// </summary>
        D12,
        /// <summary>
        /// Digital output 13.
        /// </summary>
        D13,
        /// <summary>
        /// Digital output 14.
        /// </summary>
        D14,
        /// <summary>
        /// Digital output 15.
        /// </summary>
        D15,
        /// <summary>
        /// Channel 1.
        /// </summary>
        CHAN1,
        /// <summary>
        /// Channel 2.
        /// </summary>
        CHAN2,
        /// <summary>
        /// Channel 3.
        /// </summary>
        CHAN3,
        /// <summary>
        /// Channel 4.
        /// </summary>
        CHAN4
    }
    /// <summary>
    /// Defines when the trigger pulse should occur based on the input signal's pulse width.
    /// </summary>
    public enum TriggerPulseWhen
    {
        /// <summary>
        /// Triggers when the positive/negative pulse width of the input signal is greater than the specified pulse width.
        /// </summary>
        GRE,
        /// <summary>
        /// Triggers when the positive/negative pulse width of the input signal is smaller than the specified pulse width.
        /// </summary>
        LESS,
        /// <summary>
        /// Triggers when the positive/negative pulse width of the input signal is greater than the pulse lower limit and smaller than the specified pulse upper limit.
        /// </summary>
        GLES
    }

    /// <summary>
    /// Defines when the trigger slope should occur based on the input signal's slope.
    /// </summary>
    public enum TriggerSlopeWhen
    {
        /// <summary>
        /// Triggers when the input signal slope is positive/negative and the absolute slope is greater than the specified slope.
        /// </summary>
        GRE,
        /// <summary>
        /// Triggers when the input signal slope is positive/negative and the absolute slope is smaller than the specified slope.
        /// </summary>
        LESS,
        /// <summary>
        /// Triggers when the input signal slope is positive/negative and the absolute slope is greater than the pulse lower limit and smaller than the specified pulse upper limit.
        /// </summary>
        GLES
    }

    /// <summary>
    /// Defines the polarity of the trigger.
    /// </summary>
    public enum TriggerPolarity
    {
        /// <summary>
        /// Positive polarity trigger.
        /// </summary>
        POS,
        /// <summary>
        /// Negative polarity trigger.
        /// </summary>
        NEG
    }

    /// <summary>
    /// Defines the video mode for the trigger.
    /// </summary>
    public enum TriggerVideoMode
    {
        /// <summary>
        /// Odd field trigger.
        /// </summary>
        ODDF,
        /// <summary>
        /// Even field trigger.
        /// </summary>
        EVEN,
        /// <summary>
        /// Line trigger.
        /// </summary>
        LINE,
        /// <summary>
        /// Alternate line trigger.
        /// </summary>
        ALIN
    }

    /// <summary>
    /// Represents the video standard for triggering.
    /// </summary>
    public enum TriggerVideoStandard
    {
        /// <summary>
        /// PAL Standard.
        /// </summary>
        VIDPALS,
        /// <summary>
        /// NTSC Standard.
        /// </summary>
        VIDNTSC,
        /// <summary>
        /// 480p Resolution.
        /// </summary>
        VID480P,
        /// <summary>
        /// 576p Resolution.
        /// </summary>
        VID576P,
        /// <summary>
        /// 720p60 Resolution.
        /// </summary>
        VID720P60,
        /// <summary>
        /// 720p50 Resolution.
        /// </summary>
        VID720P50,
        /// <summary>
        /// 720p30 Resolution.
        /// </summary>
        VID720P30,
        /// <summary>
        /// 720p25 Resolution.
        /// </summary>
        VID720P25,
        /// <summary>
        /// 720p24 Resolution.
        /// </summary>
        VID720P24,
        /// <summary>
        /// 1080p60 Resolution.
        /// </summary>
        VID1080P60,
        /// <summary>
        /// 1080p50 Resolution.
        /// </summary>
        VID1080P50,
        /// <summary>
        /// 1080p30 Resolution.
        /// </summary>
        VID1080P30,
        /// <summary>
        /// 1080p25 Resolution.
        /// </summary>
        VID1080P25,
        /// <summary>
        /// 1080p24 Resolution.
        /// </summary>
        VID1080P24,
        /// <summary>
        /// 1080i60 Resolution.
        /// </summary>
        VID1080I60,
        /// <summary>
        /// 1080i50 Resolution.
        /// </summary>
        VID1080I50
    }

    /// <summary>
    /// Represents the range for triggering.
    /// </summary>
    public enum TriggerPatternRange
    {
        /// <summary>
        /// High range for triggering.
        /// </summary>
        H,
        /// <summary>
        /// Low range for triggering.
        /// </summary>
        L,
        /// <summary>
        /// Exact value for triggering.
        /// </summary>
        X,
        /// <summary>
        /// Rising edge for triggering.
        /// </summary>
        R,
        /// <summary>
        /// Falling edge for triggering.
        /// </summary>
        F
    }

    /// <summary>
    /// Represents the duration for triggering.
    /// </summary>
    public enum TriggerDurationWhen
    {
        /// <summary>
        /// Triggers when the set duration time of the pattern is greater than the preset time.
        /// </summary>
        GRE,
        /// <summary>
        /// Triggers when the set duration time of the pattern is smaller than the preset time.
        /// </summary>
        LESS,
        /// <summary>
        /// Triggers when the set duration time of the pattern is smaller than the preset upper
        /// time limit and greater than the preset lower time limit.
        /// </summary>
        GLES,
        /// <summary>
        /// Triggers when the set duration time of the pattern is greater than the preset upper
        /// time limit and smaller than the preset lower time limit.
        /// </summary>
        UNGL
    }

    /// <summary>
    /// Specifies the trigger window position for a trigger source.
    /// </summary>
    public enum TriggerWindowPosition
    {
        /// <summary>
        /// Triggers when the input signal exits the specified trigger level range.
        /// </summary>
        EXIT,
        /// <summary>
        /// Triggers when the input signal enters the specified trigger level range
        /// </summary>
        ENT,
        /// <summary>
        /// Triggers when the accumulated hold time after the trigger signal enters the
        /// specified trigger level range is equal to the window time.
        /// </summary>
        TIME
    }

    /// <summary>
    /// Represents a trigger that can be set on a Network Test Instrument.
    /// </summary>
    public class Trigger
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Gets the Edge trigger settings.
        /// </summary>
        public Edge Edge { get; }

        /// <summary>
        /// Gets the Pulse trigger settings.
        /// </summary>
        public Pulse Pulse { get; }

        /// <summary>
        /// Gets the Slope trigger settings.
        /// </summary>
        public Slope Slope { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trigger"/> class.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument to set the trigger on.</param>
        public Trigger(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
            Edge = new Edge(equipment);
            Pulse = new Pulse(equipment);
            Slope = new Slope(equipment);
        }

        /// <summary>
        /// Sets the trigger mode on the Network Test Instrument.
        /// </summary>
        /// <param name="mode">The trigger mode to set.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetMode(TriggerMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:MODE {mode}");
        }

        /// <summary>
        /// Query the trigger mode from the test equipment.
        /// </summary>
        /// <returns>The current trigger mode.</returns>
        public async Task<TriggerMode> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:MODE?");

            return Enum.Parse<TriggerMode>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the trigger coupling for the test equipment.
        /// </summary>
        /// <param name="coupling">The coupling to set.</param>
        public async Task SetCoupling(TriggerCoupling coupling)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:COUP {coupling}");
        }

        /// <summary>
        /// Query the trigger coupling from the test equipment.
        /// </summary>
        /// <returns>The current trigger coupling.</returns>
        public async Task<TriggerCoupling> QueryCoupling()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:COUP?");

            return Enum.Parse<TriggerCoupling>(await equipment.ReadString());
        }

        /// <summary>
        /// Query the trigger status from the test equipment.
        /// </summary>
        /// <returns>The current trigger status.</returns>
        public async Task<TriggerStatus> QueryStatus()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:STAT?");

            return Enum.Parse<TriggerStatus>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the trigger sweep of the test equipment.
        /// </summary>
        /// <param name="sweep">The desired TriggerSweep enum value for the equipment.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetSweep(TriggerSweep sweep)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SWE {sweep}");
        }

        /// <summary>
        /// Queries the trigger sweep of the test equipment.
        /// </summary>
        /// <returns>The TriggerSweep enum value of the equipment.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<TriggerSweep> QuerySweep()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SWE?");
            return Enum.Parse<TriggerSweep>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the trigger holdoff of the test equipment.
        /// </summary>
        /// <param name="holdoff">The desired holdoff value in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetHoldoff(double holdoff)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:HOLD {holdoff}");
        }

        /// <summary>
        /// Queries the trigger holdoff of the test equipment.
        /// </summary>
        /// <returns>The trigger holdoff value in seconds.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<double> QueryHoldoff()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:HOLD?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Enables noise rejection in the test equipment.
        /// </summary>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task EnableNoiseRejection()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:NREJ ON");
        }

        /// <summary>
        /// Disables noise rejection in the test equipment.
        /// </summary>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task DisableNoiseRejection()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:NREJ OFF");
        }

        /// <summary>
        /// Queries the status of noise rejection in the test equipment.
        /// </summary>
        /// <returns>True if noise rejection is enabled, false if it is disabled.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<bool> QueryNoiseRejection()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:NREJ?");
            return await equipment.ReadBool();
        }
    }
}
