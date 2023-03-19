using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    /// <summary>
    /// Represents a command for setting constant current on the Rigol DL3000 load.
    /// </summary>
    public class ConstantCurrent : ConstantWithRange
    {
        /// <summary>
        /// Gets or sets the transient settings for the current.
        /// </summary>
        public Transient Transient { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantCurrent"/> class.
        /// </summary>
        /// <param name="equipment">The network test instrument connected to the load.</param>
        public ConstantCurrent(NetworkTestInstrument equipment) : base(equipment, "CURR")
        {
            Transient = new Transient(equipment);
        }
        /// <summary>
        /// Sets the load's regulated current in CC mode.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetCurrent(double current)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR {current:F4}");
        }

        /// <summary>
        /// Queries the load's regulated current set in CC mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QuerySetCurrent()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the rising and falling slew rate in CC mode.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetSlew(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:SLEW {fn}");
        }
        /// <summary>
        /// Sets the rising and falling slew rate in CC mode.
        /// </summary>
        /// <param name="rate">The rising and falling slew rate to set in CC mode.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when test equipment is not connected.</exception>
        public async Task SetSlew(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:SLEW {rate:F4}");
        }

        /// <summary>
        /// Queries the rising and falling rate set in CC mode.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the rate in CC mode.</returns>
        /// <exception cref="Exception">Thrown when test equipment is not connected.</exception>
        public async Task<double> QuerySlew()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:SLEW?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the positive rising rate in transient operation mode.
        /// </summary>
        /// <param name="range">The range of the positive rising rate to set in transient operation mode.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when test equipment is not connected.</exception>
        public async Task SetPositiveSlew(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:SLEW:POS {fn}");
        }

        /// <summary>
        /// Sets the positive rising rate in transient operation mode.
        /// </summary>
        /// <param name="rate">The positive rising rate to set in transient operation mode.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when test equipment is not connected.</exception>
        public async Task SetPositiveSlew(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:SLEW:POS {rate:F4}");
        }


        /// <summary>
        /// Queries the rising rate set in transient operation mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryPositiveSlew()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:SLEW:POS?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the falling rate in transient operation mode.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetNegativeSlew(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:SLEW:NEG {fn}");
        }

        /// <summary>
        /// Sets the negative slew rate in transient operation mode.
        /// </summary>
        /// <param name="rate">The negative slew rate to be set in A/s.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetNegativeSlew(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:SLEW:NEG {rate:F4}");
        }

        /// <summary>
        /// Queries the falling rate set in transient operation mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryNegativeSlew()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:SLEW:NEG?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the starting voltage in CC mode.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetVoltageOn(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:VON {fn}");
        }

        /// <summary>
        /// Sets the starting voltage in CC (Constant Current) mode.
        /// </summary>
        /// <param name="voltage">The voltage value to set, in volts.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetVoltageOn(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:VON {voltage:F4}");
        }

        /// <summary>
        /// Queries the starting voltage set in CC (Constant Current) mode.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the voltage value in volts.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<double> QueryVoltageOn()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:VON?");
            return await equipment.ReadDouble();
        }

    }
}
