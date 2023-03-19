using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands.Triggers
{
    /// <summary>
    /// Represents a Pulse object which provides control over a Network Test Instrument.
    /// </summary>
    public class Pulse
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the Pulse class with the specified Network Test Instrument.
        /// </summary>
        /// <param name="equipment">The Network Test Instrument to be controlled.</param>
        public Pulse(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the trigger source of Pulse trigger.
        /// </summary>
        /// <returns></returns>
        public async Task SetSource(TriggerPulseSource source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:PULS:SOUR {source}");
        }

        /// <summary>
        /// Queries the trigger source of Pulse trigger.
        /// The query returns D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, 
        /// D15, CHAN1, CHAN2, CHAN3, or CHAN4.
        /// </summary>
        /// <returns></returns>
        public async Task<TriggerPulseSource> QuerySource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:PULS:SOUR?");
            return Enum.Parse<TriggerPulseSource>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the trigger condition of Pulse trigger.
        /// GREater: triggers when the positive/negative pulse width of the input signal is
        /// greater than the specified pulse width.
        /// LESS: triggers when the positive/negative pulse width of the input signal is smaller
        /// than the specified pulse width.
        /// GLESs: triggers when the positive/negative pulse width of the input signal is greater
        /// than the pulse lower limit and smaller than the specified pulse upper limit.
        /// </summary>
        /// <returns></returns>
        public async Task SetWhen(TriggerPulseWhen source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:PULS:WHEN {source}");
        }

        /// <summary>
        /// Queries the trigger condition of Pulse trigger.
        /// The query returns GRE, LESS, or GLES.
        /// </summary>
        /// <returns></returns>
        public async Task<TriggerPulseWhen> QueryWhen()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:PULS:WHEN?");
            return Enum.Parse<TriggerPulseWhen>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the pulse upper limit of the Pulse trigger. The default unit is s.
        /// </summary>
        /// <returns></returns>
        public async Task SetUpperWidthLimit(double limit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:PULS:UWID {limit}");
        }

        /// <summary>
        /// Queries the pulse upper limit of the Pulse trigger. The default unit is s.
        /// The query returns the upper limit of the pulse width in scientific notation
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryUpperWidthLimit()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:PULS:UWID?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the pulse lower limit of the Pulse trigger. The default unit is s.
        /// </summary>
        /// <returns></returns>
        public async Task SetLowerWidthLimit(double limit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:PULS:LWID {limit}");
        }

        /// <summary>
        /// Queries the pulse lower limit of the Pulse trigger. The default unit is s.
        /// The query returns the lower limit of the pulse width in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryLowerWidthLimit()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:PULS:LWID?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the trigger level of Pulse trigger. The unit is the same as that of the current amplitude.
        /// </summary>
        /// <returns></returns>
        public async Task SetLevel(double level)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:PULS:LEV {level}");
        }

        /// <summary>
        /// Queries the trigger level of Pulse trigger. The unit is the same as that of the current amplitude.
        /// The query returns the trigger level in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryLevel()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:PULS:LEV?");
            return await equipment.ReadDouble();
        }
    }

    /// <summary>
    /// Slope class used to interact with a NetworkTestInstrument object
    /// </summary>
    public class Slope
    {
        /// <summary>
        /// NetworkTestInstrument object
        /// </summary>
        private NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the Slope class.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument object to use for interaction</param>
        public Slope(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the trigger source of Slope trigger.
        /// </summary>
        /// <returns></returns>
        public async Task SetTriggerSlopeSource(TriggerSlopeSource source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SLOP:SOUR {source}");
        }

        /// <summary>
        /// Queries the trigger source of Slope trigger.
        /// The query returns CHAN1, CHAN2, CHAN3, or CHAN4.
        /// </summary>
        /// <returns></returns>
        public async Task<TriggerSlopeSource> TriggerSlopeSource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SLOP:SOUR?");
            return Enum.Parse<TriggerSlopeSource>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the trigger condition of Slope trigger.
        /// GREater: triggers when the positive/negative pulse width of the input signal is
        /// greater than the specified pulse width.
        /// LESS: triggers when the positive/negative pulse width of the input signal is smaller
        /// than the specified pulse width.
        /// GLESs: triggers when the positive/negative pulse width of the input signal is greater
        /// than the pulse lower limit and smaller than the specified pulse upper limit.
        /// </summary>
        /// <returns></returns>
        public async Task SetTriggerSlopeWhen(TriggerSlopeWhen source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SLOP:WHEN {source}");
        }

        /// <summary>
        /// Queries the trigger condition of Slope trigger.
        /// The query returns GRE, LESS, or GLES.
        /// </summary>
        /// <returns></returns>
        public async Task<TriggerSlopeWhen> QueryTriggerSlopeWhen()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SLOP:WHEN?");
            return Enum.Parse<TriggerSlopeWhen>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the upper time limit value of the Slope trigger. The default unit is s
        /// </summary>
        /// <returns></returns>
        public async Task SetSlopeTupperLimit(double limit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SLOP:TUPP {limit}");
        }

        /// <summary>
        /// Queries the upper time limit value of the Slope trigger. The default unit is s
        /// The query returns the upper time limit in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QuerySlopeTupperLimit()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SLOP:TUPP?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the lower time limit value of the Slope trigger. The default unit is s
        /// </summary>
        /// <returns></returns>
        public async Task SetSlopeTlowerLimit(double limit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SLOP:TLOW {limit}");
        }

        /// <summary>
        /// Queries the lower time limit value of the Slope trigger. The default unit is s
        /// The query returns the lower time limit in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QuerySlopeTlowerLimit()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SLOP:TLOW?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the vertical window type of Slope trigger.
        /// TA: only adjusts the upper limit of the trigger level. 
        /// TB: only adjust the lower limit of the trigger level. 
        /// TAB: adjusts the upper and lower limits of the trigger level at the same time. 
        /// </summary>
        /// <returns></returns>
        public async Task SetTriggerSlopeWindow(TriggerSlopeWindow source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SLOP:WIND {source}");
        }

        /// <summary>
        /// Queries the vertical window type of Slope trigger.
        /// The query returns TA, TB, or TAB.
        /// </summary>
        /// <returns></returns>
        public async Task<TriggerSlopeWindow> QueryTriggerSlopeWindow()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SLOP:WIND?");
            return Enum.Parse<TriggerSlopeWindow>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the upper limit of the trigger level of Slope trigger. The unit is the
        /// same as that of the current amplitude.
        /// </summary>
        /// <returns></returns>
        public async Task SetSlopeALevel(double level)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SLOP:ALEV {level}");
        }

        /// <summary>
        /// Queries the upper limit of the trigger level of Slope trigger. The unit is the
        /// same as that of the current amplitude.
        /// The query returns the upper limit of the trigger level in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QuerySlopeALevel()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SLOP:ALEV?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the lower limit of the trigger level of Slope trigger. The unit is the
        /// same as that of the current amplitude.
        /// </summary>
        /// <returns></returns>
        public async Task SetSlopeBLevel(double level)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:SLOP:BLEV {level}");
        }

        /// <summary>
        /// Queries the lower limit of the trigger level of Slope trigger. The unit is the
        /// same as that of the current amplitude.
        /// The query returns the lower limit of the trigger level in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QuerySlopeBLevel()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:SLOP:BLEV?");
            return await equipment.ReadDouble();
        }
    }
}
