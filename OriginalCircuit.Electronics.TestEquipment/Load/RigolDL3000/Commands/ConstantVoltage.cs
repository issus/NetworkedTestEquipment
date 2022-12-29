using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    public class ConstantVoltage : ConstantWithRange
    {
        public ConstantVoltage(NetworkTestInstrument equipment) : base(equipment, "VOLT") { }

        /// <summary>
        /// Sets the load voltage in CV mode.
        /// </summary>
        /// <param name="voltage"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetVoltage(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:VOLT {voltage:F4}");
        }

        /// <summary>
        /// Queries the load voltage set in CV mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QuerySetVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:VOLT?");
            return await equipment.ReadDouble();
        }
    }
}
