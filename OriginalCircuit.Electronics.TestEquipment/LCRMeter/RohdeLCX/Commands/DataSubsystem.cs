using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Represents the data subsystem of a Network Test Instrument.
    /// </summary>
    public class DataSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSubsystem"/> class.
        /// </summary>
        /// <param name="equipment">The Network Test Instrument to use for communication.</param>
        public DataSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Queries the contents of a file, e.g. the data of a logging file path.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryLogFile()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DATA:DATA?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Removes a file from the specified directory.
        /// </summary>
        /// <returns></returns>
        public async Task DeleteLogFile(string path)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DATA:DEL {path}");
        }

        /// <summary>
        /// Queries all files in a specified directory.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryDataList()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DATA:LIST?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Queries the number of measurement readings saved in a file.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryDataPoints()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DATA:POIN?");
            return await equipment.ReadString();
        }
    }
}