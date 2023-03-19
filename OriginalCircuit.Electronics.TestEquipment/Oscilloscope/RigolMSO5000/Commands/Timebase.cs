using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{
    /// <summary>
    /// Enum for timebase mode of the instrument.
    /// </summary>
    public enum TimebaseMode
    {
        /// <summary>
        /// Indicates the YT mode.
        /// </summary>
        MAIN,

        /// <summary>
        /// Indicates the XY mode.
        /// </summary>
        XY,

        /// <summary>
        /// Indicates the Roll mode.
        /// </summary>
        ROLL
    }

    /// <summary>
    /// Enum for horizontal reference mode of the instrument.
    /// </summary>
    public enum HRefMode
    {
        /// <summary>
        /// When the horizontal time base is modified, the waveform displayed will be expanded or compressed horizontally relative to the screen center.
        /// </summary>
        CENT,

        /// <summary>
        /// When the horizontal time base is modified, the waveform displayed will be expanded or compressed relative to the left border of the screen.
        /// </summary>
        LB,

        /// <summary>
        /// When the horizontal time base is modified, the waveform displayed will be expanded or compressed relative to the right border of the screen.
        /// </summary>
        RB,

        /// <summary>
        /// When the horizontal time base is modified, the waveform displayed will be expanded or compressed horizontally relative to the trigger position.
        /// </summary>
        TRIG,

        /// <summary>
        /// When the horizontal time base is modified, the waveform displayed will be expanded or compressed horizontally relative to the user-defined reference position.
        /// </summary>
        USER
    }

    /// <summary>
    /// Represents the timebase of a network test instrument and provides methods to control the delayed sweep functionality.
    /// </summary>
    public class Timebase
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the Timebase class.
        /// </summary>
        /// <param name="equipment">The network test instrument to which this timebase is attached.</param>
        public Timebase(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Turns on the delayed sweep functionality.
        /// </summary>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task EnableDelay()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:DEL:ENAB ON");
        }

        /// <summary>
        /// Turns off the delayed sweep functionality.
        /// </summary>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DisableDelay()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:DEL:ENAB OFF");
        }

        /// <summary>
        /// Queries the on/off status of the delayed sweep.
        /// </summary>
        /// <returns>A boolean indicating whether the delayed sweep is enabled (true) or disabled (false).</returns>
        public async Task<bool> QueryDelayEnabled()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("TIM:DEL:ENAB?");
            return await equipment.ReadBool();
        }

        /// <summary>
        /// Sets the offset of the delayed time base.
        /// </summary>
        /// <param name="offset">The offset value to set.</param>
        public async Task SetDelayOffset(double offset)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:DEL:OFFS {offset:F8}");
        }

        /// <summary>
        /// Queries the offset of the delayed time base.
        /// </summary>
        /// <returns>The offset value of the delayed time base in seconds.</returns>
        public async Task<double> QueryDelayOffset()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("TIM:DEL:OFFS?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the scale of the delayed time base.
        /// </summary>
        /// <param name="scale">The scale to set the delayed time base to, in seconds per division (s/div).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetDelayScale(double scale)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:DEL:SCAL {scale:F4}");
        }

        /// <summary>
        /// Queries the scale of the delayed time base.
        /// </summary>
        /// <returns>The delayed time base scale in seconds per division (s/div), as a double in scientific notation.</returns>
        public async Task<double> QueryDelayScale()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("TIM:DEL:SCAL?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the offset of the main time base.
        /// </summary>
        /// <param name="offset">The offset to set the main time base to, in seconds (s).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetOffset(double offset)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:MAIN:OFFS {offset:F8}");
        }

        /// <summary>
        /// Queries the offset of the main time base.
        /// </summary>
        /// <returns>The offset of the main time base in seconds (s), as a double in scientific notation.</returns>
        public async Task<double> QueryOffset()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("TIM:MAIN:OFFS?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the scale of the main time base.
        /// </summary>
        /// <remarks>
        /// The range of scale is related to the current horizontal time base mode of the
        /// oscilloscope and its model.
        /// ➢ YT mode
        /// MSO5354: 1 ns to 1,000 s
        /// MSO5204: 2 ns to 1,000 s
        /// MSO5102/MSO5104: 5 ns to 1,000 s
        /// MSO5072/MSO5074: 5 ns to 1,000 s
        /// ➢ Roll mode
        /// 200 ms to 1,000 s
        /// </remarks>
        /// <param name="scale"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetScale(double scale)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:MAIN:SCAL {scale:F10}");
        }

        /// <summary>
        /// Queries the scale of the main time base.
        /// The query returns the main time base scale in scientific notation.
        /// </summary>
        /// <returns>Returns the main time base scale in scientific notation as a double value.</returns>
        public async Task<double> QueryScale()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("TIM:MAIN:SCAL?");

            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the horizontal time base mode.
        /// MAIN indicates the YT mode.
        /// XY indicates the XY mode.
        /// ROLL indicates the Roll mode.
        /// </summary>
        /// <param name="mode">Specifies the horizontal time base mode. Valid values are MAIN, XY, and ROLL.</param>
        public async Task SetMode(TimebaseMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:MODE {mode}");
        }

        /// <summary>
        /// Queries the horizontal time base mode.
        /// The query returns MAIN, XY, or ROLL.
        /// </summary>
        /// <returns>Returns the horizontal time base mode as a TimebaseMode enum value.</returns>
        public async Task<TimebaseMode> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TIM:MODE?");

            return Enum.Parse<TimebaseMode>(await equipment.ReadString());
        }

        /// <summary>
        /// Specifies the horizontal time base mode for the test equipment.
        /// </summary>
        public enum TimebaseMode
        {
            /// <summary>
            /// Specifies YT mode.
            /// </summary>
            MAIN,

            /// <summary>
            /// Specifies XY mode.
            /// </summary>
            XY,

            /// <summary>
            /// Specifies Roll mode.
            /// </summary>
            ROLL
        }

        /// <summary>
        /// Sets the horizontal reference mode
        /// CENTer: when the horizontal time base is modified, the waveform displayed
        /// will be expanded or compressed horizontally relative to the screen center.
        /// LB: when the horizontal time base is modified, the waveform displayed will be 
        /// expanded or compressed relative to the left border of the screen.
        /// RB: when the horizontal time base is modified, the waveform displayed will be
        /// expanded or compressed relative to the right border of the screen.
        /// TRIG: when the horizontal time base is modified, the waveform displayed will
        /// be expanded or compressed horizontally relative to the trigger position.
        /// USER: when the horizontal time base is modified, the waveform displayed will 
        /// be expanded or compressed horizontally relative to the user-defined reference 
        /// position.
        /// </summary>
        /// <returns></returns>
        public async Task SetHrefMode(HRefMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:HREF:MODE {mode}");
        }

        /// <summary>
        /// Queries the horizontal reference mode
        /// The query returns CENT, LB, RB, TRIG, or USER.
        /// </summary>
        /// <returns></returns>
        public async Task<HRefMode> QueryHrefMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TIM:HREF:MODE?");
            return Enum.Parse<HRefMode>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the user-defined reference position when the waveforms are expanded or compressed horizontally.
        /// </summary>
        /// <returns></returns>
        public async Task SetHrefPosition(double position)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:HREF:POS {position:F4}");
        }

        /// <summary>
        /// Queries the user-defined reference position when the waveforms are expanded or compressed horizontally.
        /// The query returns an integer ranging from -500 to 500.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryHrefPosition()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("TIM:HREF:POS?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        ///  Enables the fine adjustment function of the horizontal scale
        /// </summary>
        /// <returns></returns>
        public async Task EnableFineAdjust()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:VERN ON");
        }

        /// <summary>
        /// Disables the fine adjustment function of the horizontal scale
        /// </summary>
        /// <returns></returns>
        public async Task DisableFineAdjust()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TIM:VERN OFF");
        }

        /// <summary>
        /// Queries the on/off status of the fine adjustment function of the horizontal scale.
        /// The query returns 1 (ON) or 0 (OFF).
        /// </summary>
        /// <returns></returns>
        public async Task<bool> QueryFineAdjustEnabled()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("TIM:VERN?");
            return await equipment.ReadBool();
        }
    }
}
