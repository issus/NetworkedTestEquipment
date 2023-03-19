using OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{

    /// <summary>
    /// Represents the possible waveform sources for the oscilloscope.
    /// </summary>
    public enum WaveformSource
    {
        /// <summary>
        /// Channel 1 waveform source.
        /// </summary>
        CHAN1,
        /// <summary>
        /// Channel 2 waveform source.
        /// </summary>
        CHAN2,
        /// <summary>
        /// Channel 3 waveform source.
        /// </summary>
        CHAN3,
        /// <summary>
        /// Channel 4 waveform source.
        /// </summary>
        CHAN4,
        /// <summary>
        /// Digital channel 0 waveform source.
        /// </summary>
        D0,
        /// <summary>
        /// Digital channel 1 waveform source.
        /// </summary>
        D1,
        /// <summary>
        /// Digital channel 2 waveform source.
        /// </summary>
        D2,
        /// <summary>
        /// Digital channel 3 waveform source.
        /// </summary>
        D3,
        /// <summary>
        /// Digital channel 4 waveform source.
        /// </summary>
        D4,
        /// <summary>
        /// Digital channel 5 waveform source.
        /// </summary>
        D5,
        /// <summary>
        /// Digital channel 6 waveform source.
        /// </summary>
        D6,
        /// <summary>
        /// Digital channel 7 waveform source.
        /// </summary>
        D7,
        /// <summary>
        /// Digital channel 8 waveform source.
        /// </summary>
        D8,
        /// <summary>
        /// Digital channel 9 waveform source.
        /// </summary>
        D9,
        /// <summary>
        /// Digital channel 10 waveform source.
        /// </summary>
        D10,
        /// <summary>
        /// Digital channel 11 waveform source.
        /// </summary>
        D11,
        /// <summary>
        /// Digital channel 12 waveform source.
        /// </summary>
        D12,
        /// <summary>
        /// Digital channel 13 waveform source.
        /// </summary>
        D13,
        /// <summary>
        /// Digital channel 14 waveform source.
        /// </summary>
        D14,
        /// <summary>
        /// Digital channel 15 waveform source.
        /// </summary>
        D15,
        /// <summary>
        /// Math channel 1 waveform source.
        /// </summary>
        MATH1,
        /// <summary>
        /// Math channel 2 waveform source.
        /// </summary>
        MATH2,
        /// <summary>
        /// Math channel 3 waveform source.
        /// </summary>
        MATH3,
        /// <summary>
        /// Math channel 4 waveform source.
        /// </summary>
        MATH4
    }

    /// <summary>
    /// Represents the possible waveform modes for the oscilloscope.
    /// </summary>
    public enum WaveformMode
    {
        /// <summary>
        /// Reads the waveform data currently displayed on the screen
        /// </summary>
        NORM = 0,
        /// <summary>
        /// Reads the waveform data displayed on the screen when the oscilloscope
        /// is in the Run state; reads the waveform data in the internal memory when the
        /// oscilloscope is in the Stop state.
        /// </summary>
        MAX = 1,
        /// <summary>
        /// Reads the waveform data in the internal memory. Note: The data in the
        /// internal memory can only be read when the oscilloscope is in the Stop state.You are
        /// not allowed to operate the instrument when it is reading data.
        /// </summary>
        RAW = 2
    }

    /// <summary>
    /// Represents the waveform format used in a SCPI query.
    /// </summary>
    public enum WaveformFormat
    {
        /// <summary>
        /// Each waveform point occupies 2 bytes (16 bits). The lower 8 bits are valid
        /// and the higher 8 bits are 0.
        /// </summary>
        WORD = 1,
        /// <summary>
        /// Each waveform point occupies one byte (8 bits).
        /// </summary>
        BYTE = 0,
        /// <summary>
        /// The query returns the actual voltage value of each waveform point in
        /// scientific notation; and the voltage values are separated by commas.
        /// </summary>
        ASC = 2
    }

    /// <summary>
    /// Represents a waveform captured by an MSO5000 Oscilloscope.
    /// </summary>
    public class Mso5000Waveform
    {
        NetworkTestInstrument? equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mso5000Waveform"/> class.
        /// </summary>
        public Mso5000Waveform()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mso5000Waveform"/> class with the specified <see cref="NetworkTestInstrument"/>.
        /// </summary>
        /// <param name="equipment">The test equipment to use.</param>
        public Mso5000Waveform(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the waveform source to be used.
        /// </summary>
        /// <param name="source">The source to be set. (See <see cref="WaveformSource"/> for possible values)</param>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetSource(WaveformSource source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:SOUR {source}");
        }

        /// <summary>
        /// Queries the waveform source that is currently set.
        /// </summary>
        /// <returns>The current waveform source. (See <see cref="WaveformSource"/> for possible values)</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<WaveformSource> QuerySource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:SOUR?");
            return Enum.Parse<WaveformSource>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the waveform mode to be used.
        /// </summary>
        /// <param name="mode">The mode to be set. (See <see cref="WaveformMode"/> for possible values)</param>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetMode(WaveformMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:MODE {mode}");
        }

        /// <summary>
        /// Queries the waveform mode that is currently set.
        /// </summary>
        /// <returns>The current waveform mode. (See <see cref="WaveformMode"/> for possible values)</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<WaveformMode> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:MODE?");
            return Enum.Parse<WaveformMode>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the waveform format.
        /// </summary>
        /// <param name="format">The waveform format to be set.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetFormat(WaveformFormat format)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:FORM {format}");
        }

        /// <summary>
        /// Queries the waveform format.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task<WaveformFormat> QueryFormat()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:FORM?");
            return Enum.Parse<WaveformFormat>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the number of points in the waveform.
        /// </summary>
        /// <param name="points">The number of points to be set.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPoints(int points)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:POIN {points}");
        }

        /// <summary>
        /// Queries the number of available points in the waveform.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task<int> QueryAvailablePoints()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:POIN?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Gets waveform data points from the test equipment.
        /// </summary>
        /// <param name="source">The waveform source.</param>
        /// <param name="mode">The waveform mode.</param>
        /// <returns>A list of waveform data points.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<List<WaveformDataPoint>?> Data(WaveformSource? source = null, WaveformMode? mode = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (source.HasValue)
                await SetSource(source.Value);

            if (mode.HasValue)
                await SetMode(mode.Value);

            await SetFormat(WaveformFormat.BYTE);
            await equipment.WaitForOperationComplete(1);

            List<WaveformDataPoint> points = new List<WaveformDataPoint>();

            var preamble = await Preamble();

            if (preamble == null) return null;

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:DATA?");
            var data = await equipment.ReadData();

            if (data == null) return null;

            var lengthFound = false;

            int i = 0;
            for (; i < data.Length; i++)
            {
                if (data[i] == '#')
                {
                    lengthFound = true;
                    break;
                }
            }

            if (!lengthFound) return null;

            int lengthBytes = data[++i] - '0';
            var pointCount = int.Parse(Encoding.UTF8.GetString(data, ++i, lengthBytes));
            i += lengthBytes + 1;

            int dataLength = data.Length - i;

            int voltageOffset = preamble.YReference + preamble.YOrigin;
            double time = preamble.XOrigin;

            for (--i; i < dataLength; i++)
            {
                double voltage = (data[i] - voltageOffset) * preamble.YIncrement;
                points.Add(new WaveformDataPoint(time, voltage));

                time += preamble.XIncrement;
            }

            return points;
        }

        /// <summary>
        /// Queries the X increment from the test equipment.
        /// </summary>
        /// <returns>The X increment value.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>

        public async Task<double> QueryXIncrement()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XINC?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the X origin from the test equipment.
        /// </summary>
        /// <returns>The X origin value.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<double> QueryXOrigin()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XOR?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the X reference from the test equipment.
        /// </summary>
        /// <returns>The X reference value.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<double> QueryXReference()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XREF?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the Y Increment value of the waveform from the instrument
        /// </summary>
        /// <returns>The Y Increment value</returns>
        public async Task<double> QueryYIncrement()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:YINC?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the Y Origin value of the waveform from the instrument
        /// </summary>
        /// <returns>The Y Origin value</returns>
        public async Task<double> QueryYOrigin()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XOR?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the Y Reference value of the waveform from the instrument
        /// </summary>
        /// <returns>The Y Reference value</returns>
        public async Task<double> QueryYReference()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:YREF?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the start point of the waveform
        /// </summary>
        /// <param name="points">The point at which to start</param>
        public async Task SetStart(int points)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:STAR {points}");
        }
        /// <summary>
        /// Starts acquisition of the waveform.
        /// </summary>
        /// <returns>The integer result of the WAV:STAR? command sent to the equipment.</returns>
        public async Task<int> QueryStart()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:STAR?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Sets the stop point for the waveform acquisition.
        /// </summary>
        /// <param name="points">The number of points to set for the WAV:STOP command.</param>
        public async Task SetStop(int points)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:STOP {points}");
        }

        /// <summary>
        /// Queries the stop point of the waveform acquisition.
        /// </summary>
        /// <returns>The integer result of the WAV:STOP? command sent to the equipment.</returns>
        public async Task<int> QueryStop()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:STOP?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Retrieves the waveform preamble.
        /// </summary>
        /// <returns>A <see cref="WaveformPreamble"/> object created from the result of the WAV:PRE? command sent to the equipment.</returns>
        public async Task<WaveformPreamble> Preamble()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:PRE?");

            return new WaveformPreamble(await equipment.ReadString());
        }
    }
}
