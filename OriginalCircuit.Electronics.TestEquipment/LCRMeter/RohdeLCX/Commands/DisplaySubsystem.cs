using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Represents the display subsystem of the test equipment.
    /// </summary>
    public class DisplaySubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the DisplaySubsystem class.
        /// </summary>
        /// <param name="equipment">The test equipment that the display subsystem is associated with.</param>
        public DisplaySubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the brightness of the display.
        ///  Range: 0.1 to 1, Increment: 0.1
        /// </summary>
        /// <returns></returns>
        public async Task SetDisplayBrightness(double brightness)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DISP:BRIG {brightness}");
        }

        /// <summary>
        /// Queries the brightness of the display.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryDisplayBrightness()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DISP:BRIG?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Enables you to post a text message on the display.
        /// </summary>
        /// <returns></returns>
        public async Task PostTextOnDisplay(string text)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DISP:TEXT {text}");
        }

        /// <summary>
        /// Closes a user defined text message on the display.
        /// </summary>
        /// <returns></returns>
        public async Task CloseDisplayMessage()
        {

            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand("DISP:WIND:TEXT:CLE");
        }
    }
}