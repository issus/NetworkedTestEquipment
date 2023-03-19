using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Enumeration of available hardcopy image formats
    /// </summary>
    public enum HCopyFormat
    {
        /// <summary>
        /// Portable Network Graphics
        /// </summary>
        PNG
    }

    /// <summary>
    /// Represents the hardcopy subsystem of the instrument, which allows for the generation and retrieval of images.
    /// </summary>
    public class HardcopySubsystem
    {
        private NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="HardcopySubsystem"/> class with the specified network test instrument.
        /// </summary>
        /// <param name="equipment">The network test instrument to use for hardcopy operations.</param>
        public HardcopySubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Queries the current display content (screenshot) in binary (raw data) format.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryDisplayContent()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HCOP:DATA?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Queries the file format used for saving a screenshot.
        /// </summary>
        /// <returns></returns>
        public async Task<HCopyFormat> QueryScreenshootFileFormat()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HCOP:FORM?");
            return Enum.Parse<HCopyFormat>(await equipment.ReadString());
        }

        /// <summary>
        /// Queries the file format used for saving a screenshot.
        /// </summary>
        /// <returns></returns>
        public async Task<HCopyFormat> QueryScreenshootFileFormat2()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HCOP:FORM??");
            return Enum.Parse<HCopyFormat>(await equipment.ReadString());
        }

        /// <summary>
        /// Queries the width (horizontal dimension) of the screenshot.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryScreenshotWidthDimension()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HCOP:SIZE:X?");
            return await equipment.ReadInt();
        }

        /// <summary>
        /// Queries the height (vertical dimension) of the screenshot.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryScreenshotHeightDimension()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"HCOP:SIZE:Y?");
            return await equipment.ReadInt();
        }
    }
}