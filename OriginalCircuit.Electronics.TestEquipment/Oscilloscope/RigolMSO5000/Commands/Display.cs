using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{
    /// <summary>
    /// The display type for the waveform data.
    /// </summary>
    public enum DisplayType
    {
        /// <summary>
        /// The sample points are connected by lines and displayed. In most cases,
        /// this mode can provide the most vivid waveform for you to view the steep edge of
        /// the waveform (such as square waveform)
        /// </summary>
        VECT,
        /// <summary>
        /// displays the sample points directly. You can directly view each sample point
        /// and use the cursor to measure the X and Y values of the sample point.
        /// </summary>
        DOTS
    }

    /// <summary>
    /// The Display class provides access to functions that control the display of the instrument.
    /// </summary>
    public class Display
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the Display class.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument object to use for communication with the instrument.</param>
        public Display(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Clears all the waveforms on the screen
        /// If the oscilloscope is in the "RUN" state, new waveforms will continue being
        /// displayed after being cleared.
        /// </summary>
        /// <returns></returns>
        public async Task Clear()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DISP:CLE");
        }

        /// <summary>
        /// Sets the display type of the waveforms on the screen
        /// </summary>
        /// <returns></returns>
        public async Task SetType(DisplayType mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DISP:TYPE {mode}");
        }

        /// <summary>
        /// Queries the display type of the waveforms on the screen
        /// The query returns VECT or DOTS.
        /// </summary>
        /// <returns></returns>
        public async Task<DisplayType> QueryType()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DISP:TYPE?");

            return Enum.Parse<DisplayType>(await equipment.ReadString());
        }

        /// <summary>
        /// Queries the bitmap data stream of the currently displayed image.
        /// The query returns the binary data stream of the screenshot in ".bmp" format
        /// </summary>
        /// <returns></returns>
        public async Task<Image?> Capture()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("DISP:DATA?");
            return await equipment.ReadBitmap();
        }

        /// <summary>
        /// Enables or disables the color grade display
        /// </summary>
        /// <returns></returns>
        public async Task SetColorGrade(bool display)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var inp = display ? "ON" : "OFF";

            await equipment.SendCommand($"DISP:COL {inp}");
        }

        /// <summary>
        /// queries the on/off status of the color grade display.
        /// The query returns 1 or 0
        /// </summary>
        /// <returns></returns>
        public async Task<bool> QueryColorGrade()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DISP:COL?");
            return await equipment.ReadBool();
        }

        /// <summary>
        /// Enables or disables the ruler display
        /// </summary>
        /// <returns></returns>
        public async Task SetDisplayRulers(bool ruler)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var inp = ruler ? "ON" : "OFF";

            await equipment.SendCommand($"DISP:RUL {inp}");
        }

        /// <summary>
        /// queries the on/off status of the ruler
        /// The query returns 1 or 0
        /// </summary>
        /// <returns></returns>
        public async Task<bool> QueryDisplayRulers()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DISP:RUL?");
            return await equipment.ReadBool();
        }
    }
}
