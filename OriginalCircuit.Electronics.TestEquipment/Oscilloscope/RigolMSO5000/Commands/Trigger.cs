using OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{
    public enum TriggerMode
    {
        EDGE,
        PULS,
        /// <summary>
        /// Slope
        /// </summary>
        SLOP,
        /// <summary>
        /// Video
        /// </summary>
        VID,
        /// <summary>
        /// Pattern
        /// </summary>
        PATT,
        /// <summary>
        /// Duration
        /// </summary>
        DUR,
        /// <summary>
        /// Timeout
        /// </summary>
        TIM,
        /// <summary>
        /// 
        /// </summary>
        RUNT,
        /// <summary>
        /// Window
        /// </summary>
        WIND,
        /// <summary>
        /// Delay
        /// </summary>
        DEL,
        /// <summary>
        /// Setup
        /// </summary>
        SET,
        /// <summary>
        /// N-Edge
        /// </summary>
        NEDG,
        RS232, IIC, SPI, CAN,
        /// <summary>
        /// FlexRay
        /// </summary>
        FLEX, LIN, IIS, M1553
    }

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

    public enum TriggerStatus
    {
        TD, WAIT, RUN, AUTO, STOP
    }


    public enum TriggerSweep
    {
        /// <summary>
        /// Auto trigger. The waveforms are displayed no matter whether the trigger
        /// conditions are met.
        /// </summary>
        AUTO,
        /// <summary>
        /// normal trigger. The waveforms are displayed when trigger conditions
        /// are met. If the trigger conditions are not met, the oscilloscope displays the
        /// original waveforms and waits for another trigger
        /// </summary>
        NORM,
        /// <summary>
        ///  single trigger. The oscilloscope waits for a trigger, displays the
        /// waveforms when the trigger conditions are met, and then stops.
        /// </summary>
        SIGN
    }

    public enum TriggerEdgeSource
    {
        CHAN1, CHAN2, CHAN3, CHAN4,
        D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        D15, 
        /// <summary>
        /// AC Line
        /// </summary>
        ACL
    }


    public enum TriggerSlope
    {
        /// <summary>
        /// Indicates the rising edge.
        /// </summary>
        POS,
        /// <summary>
        /// Indicates the falling edge
        /// </summary>
        NEG,
        /// <summary>
        /// ndicates the rising or falling edge.
        /// </summary>
        RFAL
    }

    public enum TriggerSlopeSource
    {
        CHAN1, CHAN2, CHAN3, CHAN4
    }

    public enum TriggerSlopeWindow
    {
        /// <summary>
        /// Only adjusts the upper limit of the trigger level.
        /// </summary>
        TA,
        /// <summary>
        /// Only adjust the lower limit of the trigger level
        /// </summary>
        TB,
        /// <summary>
        /// Adjusts the upper and lower limits of the trigger level at the same time. 
        /// </summary>
        TAB
    }

    public enum TriggerPulseSource
    {
        D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        D15, CHAN1, CHAN2, CHAN3, CHAN4
    }

    public enum TriggerPulseWhen
    {
        /// <summary>
        /// Triggers when the positive/negative pulse width of the input signal is
        /// greater than the specified pulse width.
        /// </summary>
        GRE,
        /// <summary>
        /// Triggers when the positive/negative pulse width of the input signal is smaller
        /// than the specified pulse width.
        /// </summary>
        LESS,
        /// <summary>
        /// Rriggers when the positive/negative pulse width of the input signal is greater
        /// than the pulse lower limit and smaller than the specified pulse upper limit.
        /// </summary>
        GLES
    }

    public enum TriggerSlopeWhen
    {
        /// <summary>
        /// Triggers when the positive/negative pulse width of the input signal is
        /// greater than the specified pulse width.
        /// </summary>
        GRE,
        /// <summary>
        /// Triggers when the positive/negative pulse width of the input signal is smaller
        /// than the specified pulse width.
        /// </summary>
        LESS,
        /// <summary>
        /// Rriggers when the positive/negative pulse width of the input signal is greater
        /// than the pulse lower limit and smaller than the specified pulse upper limit.
        /// </summary>
        GLES
    }

    public enum TriggerPolarity
    {
        POS, NEG
    }

    public enum TriggerVideoMode
    {
        ODDF, EVEN, LINE, ALIN
    }

    public enum TriggerVideoStandard
    {
        VIDPALS, VIDNTSC, VID480P, VID576P, VID720P60, VID720P50, VID720P30, VID720P25, VID720P24,
        VID1080P60, VID1080P50, VID1080P30, VID1080P25, VID1080P24, VID1080I60, VID1080I50
    }

    public enum TriggerPatternRange
    {
        H, L, X, R, F
    }

    public enum TriggerDurationWhen
    {
        /// <summary>
        /// triggers when the set duration time of the pattern is greater than the preset
        /// time.
        /// </summary>
        GRE,
        /// <summary>
        /// Triggers when the set duration time of the pattern is smaller than the preset
        /// time.
        /// </summary>
        LESS,
        /// <summary>
        /// Triggers when the set duration time of the pattern is smaller than the preset
        /// upper time limit and greater than the preset lower time limit.
        /// </summary>
        GLES,
        /// <summary>
        /// Triggers when the set duration time of the pattern is greater than the
        /// preset upper time limit and smaller than the preset lower time limit.
        /// </summary>
        UNGL
    }

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

    public class Trigger
    {
        NetworkTestInstrument equipment;
        public Edge Edge { get; }
        public Pulse Pulse { get; }
        public Slope Slope { get; }

        public Trigger(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
            Edge = new Edge(equipment);
            Pulse = new Pulse(equipment);
            Slope = new Slope(equipment);
        }

        public async Task SetMode(TriggerMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:MODE {mode}");
        }

        public async Task<TriggerMode> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:MODE?");
            return Enum.Parse<TriggerMode>(await equipment.ReadString());
        }

        public async Task SetCoupling(TriggerCoupling coupling)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:COUP {coupling}");
        }

        public async Task<TriggerCoupling> QueryCoupling()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:COUP?");
            return Enum.Parse<TriggerCoupling>(await equipment.ReadString());
        }

        public async Task<TriggerStatus> QueryStatus()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:COUP?");
            return Enum.Parse<TriggerStatus>(await equipment.ReadString());
        }

        public async Task SetSweep(TriggerSweep sweep)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SWE {sweep}");
        }

        public async Task<TriggerSweep> QuerySweep()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SWE?");
            return Enum.Parse<TriggerSweep>(await equipment.ReadString());
        }

        public async Task SetHoldoff(double holdoff)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:HOLD {holdoff}");
        }

        public async Task<double> QueryHoldoff()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:HOLD?");
            return await equipment.ReadDouble();
        }

        public async Task EnableNoiseRejection()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:NREJ ON");
        }

        public async Task DisableNoiseRejection()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:NREJ OFF");
        }

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
