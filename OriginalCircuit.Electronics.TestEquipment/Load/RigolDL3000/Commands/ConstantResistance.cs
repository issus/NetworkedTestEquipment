using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    public class ConstantResistance : ConstantWithRange
    {
        public ConstantResistance(NetworkTestInstrument equipment) : base(equipment, "RES") { }

        /// <summary>
        /// Sets the load resistance.
        /// </summary>
        /// <param name="resistance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetResistance(double resistance)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:RES {resistance:F4}");
        }

        /// <summary>
        /// Queries the load resistance setting.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QuerySetResistance()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:RES?");
            return await equipment.ReadDouble();
        }
    }
}
