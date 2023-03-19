using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    /// <summary>
    /// Represents a command that sets the constant voltage value of a Rigol DL3000 series load.
    /// </summary>
    public class ConstantVoltage : ConstantWithRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantVoltage"/> class with the specified network test equipment.
        /// </summary>
        /// <param name="equipment">The network test equipment to send the command to.</param>
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
