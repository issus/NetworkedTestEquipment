using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{
    public enum MeasureChannel
    {
        CHAN1,
        CHAN2,
        CHAN3,
        CHAN4,
        D0, D1, D2, D3,
        D4, D5, D6, D7,
        D8, D9, D10, D11,
        D12, D13, D14, D15,
        MATH1, MATH2,
        MATH3, MATH4,
        OFF
    }

    public enum MeasureItem
    {
        ITEM1, ITEM2, ITEM3, ITEM4, ITEM5,
        ITEM6, ITEM7, ITEM8, ITEM9, ITEM10,
        ALL
    }

    public enum MeasureMode
    {
        Normal,
        Precision
    }

    public enum MeasureFunction
    {
        VMAX, VMIN, VPP, VTOP, VBASE, VAMP, VAVG,
        VRMS, OVERSHOOT, PRESHOOT, MAREA,
        MPAREA, PERIOD, FREQUENCY, RTIME,
        FTIME, PWIDTH, NWIDTH, PDUTY, NDUTY,
        TVMAX, TVMIN, PSLEWRATE, NSLEWRATE,
        VUPPER, VMID, VLOWER, VARIANCE, PVRMS,
        PPULSES, NPULSES, PEDGES, NEDGES, RRDELAY,
        RFDELAY, FRDELAY, FFDELAY, RRPHASE, RFPHASE,
        FRPHASE, FFPHASE
    }

    public enum MeasureType
    {
        Maximum,
        Minimum,
        Current,
        Average,
        Deviation,
        Count
    }

    public enum MeasureArea
    {
        Main,
        Zoom,
        Cursor
    }

    public enum MeasureCategory
    {
        Horizontal = 0,
        Vertical = 1,
        Other = 2
    }

    public class Measure
    {
        NetworkTestInstrument equipment;

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
        /// Queries the statistical results of any waveform parameter of the specified source
        /// The query returns the current measurement value in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryMeasurement(MeasureFunction function)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:ITEM? {FunctionToString(function)}");
            return await equipment.ReadDouble();
        }

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



        public string MeasTypeToString(MeasureType meas)
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

        public string FunctionToString(MeasureFunction func)
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
