using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Binning subsystem class used for binning operations on the LCX200 equipment.
    /// </summary>
    public class BinningSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinningSubsystem"/> class.
        /// </summary>
        /// <param name="equipment">The <see cref="NetworkTestInstrument"/> instance to use for binning operations.</param>
        public BinningSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Queries the number of samples counted in the bins.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryNumberOfSamples()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HAND:BIN:STAT?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Queries the total number of samples measured since reset. The query returns the sum of all counts.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryNumberOfSamplesCount()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HAND:BIN:STAT:COUN?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Resets the evaluated binning measurement statistics.
        /// </summary>
        /// <returns></returns>
        public async Task BinningMeasurementStatisticReset()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"HAND:BIN:STAT:RES");
        }

        /// <summary>
        /// Uploads the binning configuration file.
        /// </summary>
        /// <returns></returns>
        public async Task UploadBinningConfigurationFile(string path)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"HAND:CONF:PATH {path}");
        }

        /// <summary>
        /// Queries the binning configuration file path.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryBinningConfigurationFile()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HAND:CONF:PATH?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Activates the binning measurement.
        /// </summary>
        /// <returns></returns>
        public async Task ActivateBinningHandlerMeasurement()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"HAND:STAT");
        }
    }
}