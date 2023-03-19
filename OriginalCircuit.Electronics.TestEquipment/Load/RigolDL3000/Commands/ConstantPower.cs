using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    /// <summary>
    /// Represents a command that sets the electronic load to operate in constant power mode.
    /// </summary>
    public class ConstantPower : Constant
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantPower"/> class with the specified network test instrument.
        /// </summary>
        /// <param name="equipment">The network test instrument used for the command.</param>
        public ConstantPower(NetworkTestInstrument equipment) : base(equipment, "POW") { }

        /// <summary>
        /// Sets the load power
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetPower(double power)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:POW {power:F4}");
        }

        /// <summary>
        /// Queries the load power
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QuerySetPower()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:POW?");
            return await equipment.ReadDouble();
        }
    }
}
