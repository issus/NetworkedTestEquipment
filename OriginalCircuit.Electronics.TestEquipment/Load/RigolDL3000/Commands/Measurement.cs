using OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800;
using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    /// <summary>
    /// Class representing a measurement command for Rigol DL3000 series oscilloscope.
    /// </summary>
    public class Measurement
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Constructs a new instance of Measurement with the specified NetworkTestInstrument.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument to use for communication.</param>
        public Measurement(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Reads the input voltage of the instrument.
        /// </summary>
        /// <returns>The input voltage in volts.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<double> Voltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:VOLT?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the maximum input voltage of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> VoltageMax()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:VOLT:MAX?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the minimum input voltage of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> VoltageMin()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:VOLT:MIN?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the input current of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> Current()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:CURR?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the maximum input current of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> CurrentMax()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:CURR:MAX?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the minimum input current of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> CurrentMin()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:CURR:MIN?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the resistance of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> Resistance()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:RES?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the input power of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> Power()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:POW?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the battery capacity
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> BatteryCapacity()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:CAP?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the battery energy
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> WattHours()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:WATT?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the discharge time of the battery.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> DischargeTime()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:DISC?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the integration time of the instrument.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> IntegrationTime()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:TIME?");

            var reading = await equipment.ReadString();

            return double.Parse(reading);
        }

        /// <summary>
        /// Reads the data points (400 data points) in the data cache area in the waveform display
        /// interface.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> WaveData()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:WAV?");

            var reading = await equipment.ReadString();

            // todo: Split into a list
            return reading;
        }
    }
}
