using OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{

    public enum WaveformSource
    {
        CHAN1, CHAN2, CHAN3, CHAN4,
        D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,
        D15,  MATH1, MATH2, MATH3, MATH4
    }

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

    public class Mso5000Waveform
    {
        NetworkTestInstrument? equipment;

        public Mso5000Waveform()
        {
        }

        public Mso5000Waveform(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        public async Task SetSource(WaveformSource source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:SOUR {source}");
        }

        public async Task<WaveformSource> QuerySource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:SOUR?");
            return Enum.Parse<WaveformSource>(await equipment.ReadString());
        }

        public async Task SetMode(WaveformMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:MODE {mode}");
        }

        public async Task<WaveformMode> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:MODE?");
            return Enum.Parse<WaveformMode>(await equipment.ReadString());
        }

        public async Task SetFormat(WaveformFormat format)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:FORM {format}");
        }

        public async Task<WaveformFormat> QueryFormat()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:FORM?");
            return Enum.Parse<WaveformFormat>(await equipment.ReadString());
        }

        public async Task SetPoints(int points)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:POIN {points}");
        }

        public async Task<int> QueryAvailablePoints()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:POIN?");
            return await equipment.ReadInt();
        }

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

        public async Task<double> QueryXIncrement()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XINC?");
            return await equipment.ReadDouble();
        }

        public async Task<double> QueryXOrigin()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XOR?");
            return await equipment.ReadDouble();
        }

        public async Task<double> QueryXReference()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XREF?");
            return await equipment.ReadDouble();
        }

        public async Task<double> QueryYIncrement()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:YINC?");
            return await equipment.ReadDouble();
        }

        public async Task<double> QueryYOrigin()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:XOR?");
            return await equipment.ReadDouble();
        }

        public async Task<double> QueryYReference()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:YREF?");
            return await equipment.ReadDouble();
        }

        public async Task SetStart(int points)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:STAR {points}");
        }

        public async Task<int> QueryStart()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:STAR?");
            return await equipment.ReadInt();
        }

        public async Task SetStop(int points)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"WAV:STOP {points}");
        }

        public async Task<int> QueryStop()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"WAV:STOP?");
            return await equipment.ReadInt();
        }

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
