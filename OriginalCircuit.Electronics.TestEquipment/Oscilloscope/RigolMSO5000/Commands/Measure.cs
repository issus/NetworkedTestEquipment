using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{
    /// <summary>
    /// Represents the available measurement channels in the instrument.
    /// </summary>
    public enum MeasureChannel
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
        /// Digital I/O channel 0
        /// </summary>
        D0,

        /// <summary>
        /// Digital I/O channel 1
        /// </summary>
        D1,

        /// <summary>
        /// Digital I/O channel 2
        /// </summary>
        D2,

        /// <summary>
        /// Digital I/O channel 3
        /// </summary>
        D3,

        /// <summary>
        /// Digital I/O channel 4
        /// </summary>
        D4,

        /// <summary>
        /// Digital I/O channel 5
        /// </summary>
        D5,

        /// <summary>
        /// Digital I/O channel 6
        /// </summary>
        D6,

        /// <summary>
        /// Digital I/O channel 7
        /// </summary>
        D7,

        /// <summary>
        /// Digital I/O channel 8
        /// </summary>
        D8,

        /// <summary>
        /// Digital I/O channel 9
        /// </summary>
        D9,

        /// <summary>
        /// Digital I/O channel 10
        /// </summary>
        D10,

        /// <summary>
        /// Digital I/O channel 11
        /// </summary>
        D11,

        /// <summary>
        /// Digital I/O channel 12
        /// </summary>
        D12,

        /// <summary>
        /// Digital I/O channel 13
        /// </summary>
        D13,

        /// <summary>
        /// Digital I/O channel 14
        /// </summary>
        D14,

        /// <summary>
        /// Digital I/O channel 15
        /// </summary>
        D15,

        /// <summary>
        /// Math channel 1
        /// </summary>
        MATH1,

        /// <summary>
        /// Math channel 2
        /// </summary>
        MATH2,

        /// <summary>
        /// Math channel 3
        /// </summary>
        MATH3,

        /// <summary>
        /// Math channel 4
        /// </summary>
        MATH4,

        /// <summary>
        /// Turn off the channel
        /// </summary>
        OFF
    }

    /// <summary>
    /// Represents the available measurement items in the instrument.
    /// </summary>
    public enum MeasureItem
    {
        /// <summary>
        /// Measurement item 1
        /// </summary>
        ITEM1,

        /// <summary>
        /// Measurement item 2
        /// </summary>
        ITEM2,

        /// <summary>
        /// Measurement item 3
        /// </summary>
        ITEM3,

        /// <summary>
        /// Measurement item 4
        /// </summary>
        ITEM4,

        /// <summary>
        /// Measurement item 5
        /// </summary>
        ITEM5,

        /// <summary>
        /// Measurement item 6
        /// </summary>
        ITEM6,

        /// <summary>
        /// Measurement item 7
        /// </summary>
        ITEM7,

        /// <summary>
        /// Measurement item 8
        /// </summary>
        ITEM8,

        /// <summary>
        /// Measurement item 9
        /// </summary>
        ITEM9,

        /// <summary>
        /// Measurement item 10
        /// </summary>
        ITEM10,

        /// <summary>
        /// All available measurement items
        /// </summary>
        ALL
    }

    /// <summary>
    /// Represents the measurement mode.
    /// </summary>
    public enum MeasureMode
    {
        /// <summary>
        /// Normal measurement mode.
        /// </summary>
        Normal,
        /// <summary>
        /// Precision measurement mode.
        /// </summary>
        Precision
    }

    /// <summary>
    /// Represents the measurement function.
    /// </summary>
    public enum MeasureFunction
    {
        /// <summary>
        /// Maximum voltage.
        /// </summary>
        VMAX,
        /// <summary>
        /// Minimum voltage.
        /// </summary>
        VMIN,
        /// <summary>
        /// Peak-to-peak voltage.
        /// </summary>
        VPP,
        /// <summary>
        /// Top voltage.
        /// </summary>
        VTOP,
        /// <summary>
        /// Base voltage.
        /// </summary>
        VBASE,
        /// <summary>
        /// Amplitude voltage.
        /// </summary>
        VAMP,
        /// <summary>
        /// Average voltage.
        /// </summary>
        VAVG,
        /// <summary>
        /// RMS voltage.
        /// </summary>
        VRMS,
        /// <summary>
        /// Overshoot voltage.
        /// </summary>
        OVERSHOOT,
        /// <summary>
        /// Preshoot voltage.
        /// </summary>
        PRESHOOT,
        /// <summary>
        /// Mean area voltage.
        /// </summary>
        MAREA,
        /// <summary>
        /// Mean positive area voltage.
        /// </summary>
        MPAREA,
        /// <summary>
        /// Period.
        /// </summary>
        PERIOD,
        /// <summary>
        /// Frequency.
        /// </summary>
        FREQUENCY,
        /// <summary>
        /// Rise time.
        /// </summary>
        RTIME,
        /// <summary>
        /// Fall time.
        /// </summary>
        FTIME,
        /// <summary>
        /// Positive pulse width.
        /// </summary>
        PWIDTH,
        /// <summary>
        /// Negative pulse width.
        /// </summary>
        NWIDTH,
        /// <summary>
        /// Positive duty cycle.
        /// </summary>
        PDUTY,
        /// <summary>
        /// Negative duty cycle.
        /// </summary>
        NDUTY,
        /// <summary>
        /// Top voltage timing.
        /// </summary>
        TVMAX,
        /// <summary>
        /// Base voltage timing.
        /// </summary>
        TVMIN,
        /// <summary>
        /// Positive slew rate.
        /// </summary>
        PSLEWRATE,
        /// <summary>
        /// Negative slew rate.
        /// </summary>
        NSLEWRATE,
        /// <summary>
        /// Upper voltage limit.
        /// </summary>
        VUPPER,
        /// <summary>
        /// Mid voltage.
        /// </summary>
        VMID,
        /// <summary>
        /// Lower voltage limit.
        /// </summary>
        VLOWER,
        /// <summary>
        /// Variance.
        /// </summary>
        VARIANCE,
        /// <summary>
        /// Positive RMS voltage.
        /// </summary>
        PVRMS,
        /// <summary>
        /// Positive pulses count.
        /// </summary>
        PPULSES,
        /// <summary>
        /// Negative pulses count.
        /// </summary>
        NPULSES,
        /// <summary>
        /// Positive edges count.
        /// </summary>
        PEDGES,
        /// <summary>
        /// Negative edges count.
        /// </summary>
        NEDGES,
        /// <summary>
        /// Rising to rising delay.
        /// </summary>
        RRDELAY,
        /// <summary>
        /// Rising to falling delay.
        /// </summary>
        RFDELAY,
        /// <summary>
        /// Falling to rising delay.
        /// </summary>
        FRDELAY,
        /// <summary>
        /// Falling to falling delay.
        /// </summary>
        FFDELAY,
        /// <summary>
        /// Rising to rising phase.
        /// </summary>
        RRPHASE,
        /// <summary>
        /// Rising to falling phase.
        /// </summary>
        RFPHASE,
        /// <summary>
        /// Falling to rising phase.
        /// </summary>
        FRPHASE,
        /// <summary>
        /// Falling to falling phase.
        /// </summary>
        FFPHASE
    }
    /// <summary>
    /// Enum representing the type of measurement to be taken.
    /// </summary>
    public enum MeasureType
    {
        /// <summary>
        /// The maximum value of the measurement.
        /// </summary>
        Maximum,
        /// <summary>
        /// The minimum value of the measurement.
        /// </summary>
        Minimum,
        /// <summary>
        /// The current value of the measurement.
        /// </summary>
        Current,
        /// <summary>
        /// The average value of the measurement.
        /// </summary>
        Average,
        /// <summary>
        /// The deviation value of the measurement.
        /// </summary>
        Deviation,
        /// <summary>
        /// The count of the measurement.
        /// </summary>
        Count
    }

    /// <summary>
    /// Enum representing the area of the measurement.
    /// </summary>
    public enum MeasureArea
    {
        /// <summary>
        /// The main area of the measurement.
        /// </summary>
        Main,
        /// <summary>
        /// The zoom area of the measurement.
        /// </summary>
        Zoom,
        /// <summary>
        /// The cursor area of the measurement.
        /// </summary>
        Cursor
    }

    /// <summary>
    /// Enum representing the category of the measurement.
    /// </summary>
    public enum MeasureCategory
    {
        /// <summary>
        /// The horizontal category of the measurement.
        /// </summary>
        Horizontal = 0,
        /// <summary>
        /// The vertical category of the measurement.
        /// </summary>
        Vertical = 1,
        /// <summary>
        /// The other category of the measurement.
        /// </summary>
        Other = 2
    }

    /// <summary>
    /// Represents a measurement instance.
    /// </summary>
    public class Measure
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the Measure class.
        /// </summary>
        /// <param name="equipment">The instrument to be used for measurement.</param>
        public Measure(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the channel source of the current measurement parameter
        /// Only the currently enabled channels can be selected
        /// </summary>
        /// <returns></returns>
        public async Task SetSource(MeasureChannel chan)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SOUR {chan}");
        }

        /// <summary>
        /// The query returns channel source of the current measurement parameter
        /// The query returns D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        /// D15, CHAN1, CHAN2, CHAN3, CHAN4, MATH1, MATH2, MATH3, or MATH4.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureChannel> QuerySource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SOUR?");
            return Enum.Parse<MeasureChannel>(await equipment.ReadString());
        }

        /// <summary>
        /// Clears any one or all of the 10 measurement items that have been turned on
        /// </summary>
        /// <returns></returns>
        public async Task Clear(MeasureItem? item = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:CLE {item}");
        }

        /// <summary>
        /// Sets the threshold source.
        /// Modifying the threshold will affect the measurement results of time, delay, and phase
        /// parameters.
        /// </summary>
        /// <returns></returns>
        public async Task SetThresholdSource(MeasureChannel chan)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:THR:SOUR {chan}");
        }

        /// <summary>
        /// Sets the threshold level of the analog channel in auto measurement to a default value.
        /// </summary>
        /// <returns></returns>
        public async Task SetThresholdDefault()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:THR:DEF");
        }

        /// <summary>
        /// Sets the measurement mode.
        /// NORMal: executes measurement of up to 1 Mpts
        /// PRECision: executes measurement of up to 200 Mpts, improving the resolution of
        /// measurement results. Note, in this mode, the refresh rate of the waveforms may be
        /// reduced.
        /// </summary>
        /// <returns></returns>
        public async Task SetMode(MeasureMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:MODE {mode}");
        }

        /// <summary>
        /// Query returns the measurement mode.
        /// NORMal: executes measurement of up to 1 Mpts
        /// PRECision: executes measurement of up to 200 Mpts, improving the resolution of
        /// measurement results. Note, in this mode, the refresh rate of the waveforms may be
        /// reduced.
        /// The query returns NORM or PREC.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureMode> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:MODE?");
            var resp = await equipment.ReadString();

            switch (resp.ToUpper())
            {
                case "PREC":
                case "PRECISION":
                    return MeasureMode.Precision;
                default:
                    return MeasureMode.Normal;
            }
        }

        /// <summary>
        /// Sets the source and displays all measurement values of the set source
        /// </summary>
        /// <returns></returns>
        public async Task SetAllSource(MeasureChannel chan)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:AMS {chan}");
        }

        /// <summary>
        /// Queries the channel source(s) of the all measurement function.
        /// The query returns CHAN1, CHAN2, CHAN3, CHAN4, or OFF. 
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureChannel> QueryAllSource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:AMS?");
            return Enum.Parse<MeasureChannel>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the threshold level upper limit of the analog channel in auto
        /// measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetThresholdMax(int threshold)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SET:MAX {threshold}");
        }

        /// <summary>
        /// Queries the threshold level upper limit of the analog channel in auto measurement.
        /// The query returns an integer.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryThresholdMax()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SET:MAX?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Sets the threshold level middle value of the analog channel in auto 
        /// measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetThresholdMid(int threshold)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SET:MID {threshold}");
        }

        /// <summary>
        /// Queries the threshold level middle value of the analog channel in auto measurement.
        /// The query returns an integer.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryThresholdMid()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SET:MID?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Sets the threshold level lower limit of the analog channel in auto measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetThresholdMin(int threshold)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SET:MIN {threshold}");
        }

        /// <summary>
        /// Queries the threshold level lower limit of the analog channel in auto measurement.
        /// The query returns an integer.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryThresholdMin()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SET:MIN?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Sets Source A in the phase measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetPhaseSourceA(MeasureChannel chan)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SET:PSA {chan}");
        }

        /// <summary>
        /// Queries Source A in the phase measurement.
        /// The query returns D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        /// D15, CHAN1, CHAN2, CHAN3, CHAN4, MATH1, MATH2, MATH3, or MATH4.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureChannel> QueryPhaseSourceA()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SET:PSA?");
            return Enum.Parse<MeasureChannel>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets Source B in the phase measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetPhaseSourceB(MeasureChannel chan)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SET:PSB {chan}");
        }

        /// <summary>
        /// Queries Source B in the phase measurement.
        /// The query returns D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        /// D15, CHAN1, CHAN2, CHAN3, CHAN4, MATH1, MATH2, MATH3, or MATH4.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureChannel> QueryPhaseSourceB()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SET:PSB?");
            return Enum.Parse<MeasureChannel>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets Source A in the delay measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetDelaySourceA(MeasureChannel chan)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SET:DSA {chan}");
        }

        /// <summary>
        /// Queries Source A in the delay measurement.
        /// The query returns D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        /// D15, CHAN1, CHAN2, CHAN3, CHAN4, MATH1, MATH2, MATH3, or MATH4.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureChannel> QueryDelaySourceA()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SET:DSA?");
            return Enum.Parse<MeasureChannel>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets Source B in the delay measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetDelaySourceB(MeasureChannel chan)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:SET:DSB {chan}");
        }

        /// <summary>
        /// Queries Source B in the delay measurement.
        /// The query returns D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        /// D15, CHAN1, CHAN2, CHAN3, CHAN4, MATH1, MATH2, MATH3, or MATH4.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureChannel> QueryDelaySourceB()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:SET:DSB?");
            return Enum.Parse<MeasureChannel>(await equipment.ReadString());
        }

        /// <summary>
        /// Enables or disables the statistical function
        /// </summary>
        /// <returns></returns>
        public async Task SetStatisticDisplay(bool display)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var inp = display ? "ON" : "OFF";

            await equipment.SendCommand($"MEAS:STAT:DISP {inp}");
        }

        /// <summary>
        /// Queries the status of the statistical function.
        /// The query returns 1 or 0.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> QueryStatisticDisplay()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:STAT:DISP?");
            return await equipment.ReadBool();
        }

        /// <summary>
        /// Clears the history statistics data and makes statistics again.
        /// </summary>
        /// <returns></returns>
        public async Task ResetStatistic()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:STAT:RES");
        }

        /// <summary>
        /// Enables the statistical function of any waveform parameter of the specified source
        /// </summary>
        /// <returns></returns>
        public async Task EnableStatistic(MeasureFunction function, MeasureChannel source, MeasureChannel? sourceB = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (!sourceB.HasValue)
                await equipment.SendCommand($"MEAS:STAT:ITEM {FunctionToString(function)},{source}");
            else
                await equipment.SendCommand($"MEAS:STAT:ITEM {FunctionToString(function)},{source},{sourceB}");
        }

        /// <summary>
        /// Queries the statistical results of any waveform parameter of the specified source.
        /// The query returns the statistical results in scientific notation
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryStatistic(MeasureFunction function, MeasureType measurement, MeasureChannel? source = null, MeasureChannel? sourceB = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (source == null)
            {
                await equipment.SendCommand($"MEAS:STAT:ITEM? {MeasTypeToString(measurement)},{FunctionToString(function)}");
            }
            else if (sourceB == null)
            {
                await equipment.SendCommand($"MEAS:STAT:ITEM? {MeasTypeToString(measurement)},{FunctionToString(function)},{source}");
            }
            else
            {
                await equipment.SendCommand($"MEAS:STAT:ITEM? {MeasTypeToString(measurement)},{FunctionToString(function)},{source},{sourceB}");
            }

            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Measures any waveform parameter of the specified source
        /// </summary>
        /// <returns></returns>
        public async Task EnableMeasurement(MeasureFunction function, MeasureChannel source, MeasureChannel? sourceB = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (!sourceB.HasValue)
                await equipment.SendCommand($"MEAS:ITEM {FunctionToString(function)},{source}");
            else
                await equipment.SendCommand($"MEAS:ITEM {FunctionToString(function)},{source},{sourceB}");
        }

        /// <summary>
        /// Queries the statistical results of any waveform parameter of the specified source.
        /// The query returns the current measurement value in scientific notation.
        /// </summary>
        /// <param name="function">The type of measurement function to perform</param>
        /// <param name="source">The measurement source channel</param>
        /// <param name="sourceB">The second measurement source channel, if applicable</param>
        /// <returns>The measurement result in scientific notation</returns>
        public async Task<double> QueryMeasurement(MeasureFunction function, MeasureChannel source, MeasureChannel? sourceB = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (!sourceB.HasValue)
                await equipment.SendCommand($":MEAS:ITEM? {FunctionToString(function)},{source}");
            else
                await equipment.SendCommand($":MEAS:ITEM? {FunctionToString(function)},{source},{sourceB}");

            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the statistical results of any waveform parameter of the specified source.
        /// The query returns the current measurement value in scientific notation.
        /// </summary>
        /// <param name="function">The type of measurement function to perform</param>
        /// <returns>The measurement result in scientific notation</returns>
        public async Task<double> QueryMeasurement(MeasureFunction function)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:ITEM? {FunctionToString(function)}");

            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the type of the measurement range.
        /// </summary>
        /// <returns></returns>
        public async Task SetArea(MeasureArea area)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:AREA {area}");
        }

        /// <summary>
        /// Queries the type of the measurement range.
        /// The query returns MAIN, ZOOM, or CURS.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureArea> QueryArea()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:AREA?");
            return Enum.Parse<MeasureArea>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the position of Cursor A when the measurement range is the "cursor region"
        /// </summary>
        /// <returns></returns>
        public async Task SetCursorRegionAX(int x)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:CREG:CAX {x}");
        }

        /// <summary>
        /// Queries the position of Cursor A when the measurement range is the "cursor region"
        /// The query returns the position of Cursor A in integer
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryCursorRegionAX()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:CREG:CAX?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Sets the position of Cursor B when the measurement range is the "cursor region"
        /// </summary>
        /// <returns></returns>
        public async Task SetCursorRegionBX(int x)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:CREG:CBX {x}");
        }

        /// <summary>
        /// Queries the position of Cursor B when the measurement range is the "cursor region"
        /// The query returns the position of Cursor B in integer.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryCursorRegionBX()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:CREG:CBX?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Queries the measurement type.
        /// 0: horizontal; 1: vertical; 2: other
        /// </summary>
        /// <returns></returns>
        public async Task SetCategory(MeasureCategory cat)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:CAT {(int)cat}");
        }

        /// <summary>
        /// Sets the measurement type.
        /// The query returns an integer ranging from 0 to 2
        /// 0: horizontal; 1: vertical; 2: other
        /// </summary>
        /// <returns></returns>
        public async Task<MeasureCategory> QueryCategory()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:CAT?");
            return Enum.Parse<MeasureCategory>(await equipment.ReadString());
        }

        private string MeasTypeToString(MeasureType meas)
        {
            switch (meas)
            {
                case MeasureType.Maximum:
                    return "MAX";
                case MeasureType.Minimum:
                    return "MIN";
                case MeasureType.Current:
                    return "CURR";
                case MeasureType.Average:
                    return "AVER";
                case MeasureType.Deviation:
                    return "DEV";
                case MeasureType.Count:
                default:
                    return "CNT";
            }
        }

        private string FunctionToString(MeasureFunction func)
        {
            switch (func)
            {
                case MeasureFunction.VMAX:
                case MeasureFunction.VMIN:
                case MeasureFunction.VPP:
                case MeasureFunction.VTOP:
                case MeasureFunction.VAMP:
                case MeasureFunction.VAVG:
                case MeasureFunction.VRMS:
                case MeasureFunction.TVMAX:
                case MeasureFunction.TVMIN:
                case MeasureFunction.VMID:
                    return func.ToString();
                case MeasureFunction.VBASE:
                    return "VBAS";
                case MeasureFunction.OVERSHOOT:
                    return "OVER";
                case MeasureFunction.PRESHOOT:
                    return "PRES";
                case MeasureFunction.MAREA:
                    return "MAR";
                case MeasureFunction.MPAREA:
                    return "MPAR";
                case MeasureFunction.PERIOD:
                    return "PER";
                case MeasureFunction.FREQUENCY:
                    return "FREQ";
                case MeasureFunction.RTIME:
                    return "RTIM";
                case MeasureFunction.FTIME:
                    return "FTIM";
                case MeasureFunction.PWIDTH:
                    return "PWID";
                case MeasureFunction.NWIDTH:
                    return "NWID";
                case MeasureFunction.PDUTY:
                    return "PDUT";
                case MeasureFunction.NDUTY:
                    return "NDUT";
                case MeasureFunction.PSLEWRATE:
                    return "PSL";
                case MeasureFunction.NSLEWRATE:
                    return "NSL";
                case MeasureFunction.VUPPER:
                    return "VUPP";
                case MeasureFunction.VLOWER:
                    return "VLOW";
                case MeasureFunction.VARIANCE:
                    return "VAR";
                case MeasureFunction.PVRMS:
                    return "PVRM";
                case MeasureFunction.PPULSES:
                    return "PPUL";
                case MeasureFunction.NPULSES:
                    return "NPUL";
                case MeasureFunction.PEDGES:
                    return "PEDG";
                case MeasureFunction.NEDGES:
                    return "NEDG";
                case MeasureFunction.RRDELAY:
                    return "RRD";
                case MeasureFunction.RFDELAY:
                    return "RFD";
                case MeasureFunction.FRDELAY:
                    return "FRD";
                case MeasureFunction.FFDELAY:
                    return "FFD";
                case MeasureFunction.RRPHASE:
                    return "RRPH";
                case MeasureFunction.RFPHASE:
                    return "RFPH";
                case MeasureFunction.FRPHASE:
                    return "FRPH";
                case MeasureFunction.FFPHASE:
                    return "FFPH";
                default:
                    return "";
            }
        }
    }
}
