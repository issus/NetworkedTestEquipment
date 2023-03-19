using OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands
{
    /// <summary>
    /// Represents a set of measurement commands to be executed on the NetworkTestInstrument.
    /// </summary>
    public class Measurement
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Measurement"/> class with the specified equipment.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument to perform the measurement commands on.</param>
        public Measurement(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Query the voltage, current and power measured on the output terminal of the
        /// specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<OutputReading> ChannelOutput(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:ALL? {chan}");

            var reading = (await equipment.ReadString()).Split(',');

            return new OutputReading(double.Parse(reading[0]), double.Parse(reading[1]), double.Parse(reading[2]));
        }

        /// <summary>
        /// Query the current measured on the output terminal of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> Current(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:CURR? {chan}");

            var reading = (await equipment.ReadString());

            return double.Parse(reading);
        }

        /// <summary>
        /// Query the power measured on the output terminal of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> Power(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS:POWE? {chan}");

            var reading = (await equipment.ReadString());

            return double.Parse(reading);
        }

        /// <summary>
        /// Query the voltage measured on the output terminal of the specified channel. 
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> Voltage(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"MEAS? {chan}");

            var reading = (await equipment.ReadString());

            return double.Parse(reading);
        }
    }
}
