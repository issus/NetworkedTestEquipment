using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    /// <summary>
    /// Represents a source command for the Rigol DL3000 series network test instrument.
    /// </summary>
    public class Source
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Gets the Constant Current command object.
        /// </summary>
        public ConstantCurrent ConstantCurrent { get; }

        /// <summary>
        /// Gets the Constant Voltage command object.
        /// </summary>
        public ConstantVoltage ConstantVoltage { get; }

        /// <summary>
        /// Gets the Constant Resistance command object.
        /// </summary>
        public ConstantResistance ConstantResistance { get; }

        /// <summary>
        /// Gets the Constant Power command object.
        /// </summary>
        public ConstantPower ConstantPower { get; }

        //todo: list
        //todo: wave

        /// <summary>
        /// Initializes a new instance of the Source class.
        /// </summary>
        /// <param name="equipment">The network test instrument.</param>
        public Source(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;

            ConstantCurrent = new ConstantCurrent(equipment);
            ConstantVoltage = new ConstantVoltage(equipment);
            ConstantPower = new ConstantPower(equipment);
            ConstantResistance = new ConstantResistance(equipment);
        }

        /// <summary>
        /// Converts a string input to the corresponding <see cref="SourceFunction"/> value.
        /// </summary>
        /// <param name="input">The string input representing a <see cref="SourceFunction"/> value.</param>
        /// <returns>The corresponding <see cref="SourceFunction"/> value, or null if the input is invalid.</returns>
        static internal SourceFunction? FunctionFromString(string input)
        {
            switch (input)
            {
                case "CC":
                    return SourceFunction.Current;
                case "CV":
                    return SourceFunction.Voltage;
                case "CR":
                    return SourceFunction.Resistance;
                case "CP":
                    return SourceFunction.Power;
            }

            return null;
        }

        /// <summary>
        /// Converts a string input to the corresponding <see cref="FunctionMode"/> value.
        /// </summary>
        /// <param name="input">The string input representing a <see cref="FunctionMode"/> value.</param>
        /// <returns>The corresponding <see cref="FunctionMode"/> value, or null if the input is invalid.</returns>
        static internal FunctionMode? FunctionModeFromString(string input)
        {
            // The query returns LIST in List mode; returns FIX or BATT in waveform display
            // interface; and returns FIX in other modes.
            switch (input)
            {
                case "FIX":
                case "FIXED":
                    return FunctionMode.Fixed;
                case "LIST":
                    return FunctionMode.List;
                case "BATT":
                case "BATTERY":
                    return FunctionMode.Battery;
            }

            return null;
        }

        /// <summary>
        /// Converts a string input to the corresponding <see cref="DL3000Range"/> value.
        /// </summary>
        /// <param name="input">The string input representing a <see cref="DL3000Range"/> value.</param>
        /// <returns>The corresponding <see cref="DL3000Range"/> value, or null if the input is invalid.</returns>
        static internal DL3000Range? RangeFromString(string input)
        {
            switch (input.ToUpper())
            {
                case "MIN":
                case "MINIMUM":
                    return DL3000Range.Minimum;
                case "MAX":
                case "MAXIMUM":
                    return DL3000Range.Maximum;
                case "DEF":
                case "DEFAULT":
                    return DL3000Range.Default;
            }

            return null;
        }

        /// <summary>
        /// Converts a string input to the corresponding <see cref="TransientMode"/> value.
        /// </summary>
        /// <param name="input">The string input representing a <see cref="TransientMode"/> value.</param>
        /// <returns>The corresponding <see cref="TransientMode"/> value, or null if the input is invalid.</returns>
        static internal TransientMode? TransientModeFromString(string input)
        {
            switch (input.ToUpper())
            {
                case "CONT":
                case "CONTINUOUS":
                    return TransientMode.Continuous;
                case "PULS":
                case "PULSE":
                    return TransientMode.Pulse;
                case "TOGG":
                case "TOGGLE":
                    return TransientMode.Toggle;
            }

            return null;
        }

        /// <summary>
        /// Enables the input of the electronic load.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Enable()
        {
            await SetInput(true);
        }

        /// <summary>
        /// Disables the input of the electronic load.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Disable()
        {
            await SetInput(false);
        }

        /// <summary>
        /// Sets the input of the electronic load to be on or off.
        /// </summary>
        /// <param name="enabled">True to turn the input on; false to turn the input off.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetInput(bool enabled)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var output = enabled ? "ON" : "OFF";

            await equipment.SendCommand($"SOUR:INP {output}");
        }

        /// <summary>
        /// Queries whether the input of the electronic load is on or off
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryInputEnabled()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:INP?");
            var output = await equipment.ReadString();

            // todo: check if response is ON/OFF or 1/0. Manual unclear.
            return output[1] == 'N';
        }

        /// <summary>
        /// Sets the static operation mode of the electronic load.
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetFunction(SourceFunction function)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (function)
            {
                case SourceFunction.Current:
                    fn = "CURR";
                    break;
                case SourceFunction.Resistance:
                    fn = "RES";
                    break;
                case SourceFunction.Voltage:
                    fn = "VOLT";
                    break;
                case SourceFunction.Power:
                default:
                    fn = "POW";
                    break;
            }

            await equipment.SendCommand($"SOUR:FUNC {fn}");
        }

        /// <summary>
        /// Queries the static operation mode of the electronic load.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SourceFunction?> QueryFunction()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:FUNC?");
            var output = await equipment.ReadString();

            return FunctionFromString(output);
        }

        /// <summary>
        /// The input regulation mode setting is controlled by the FUNCtion command, the list value,
        /// the waveform display command, or the battery discharge command.
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetMode(FunctionMode function)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (function)
            {
                case FunctionMode.Fixed:
                    fn = "FIX";
                    break;
                case FunctionMode.List:
                    fn = "LIST";
                    break;
                case FunctionMode.Waveform:
                    fn = "WAV";
                    break;
                case FunctionMode.Battery:
                default:
                    fn = "BATT";
                    break;
            }

            await equipment.SendCommand($"SOUR:FUNC:MODE {fn}");
        }

        /// <summary>
        /// Queries what controls the input regulation mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<FunctionMode?> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:FUNC:MODE?");
            var output = await equipment.ReadString();

            return FunctionModeFromString(output);
        }

        /// <summary>
        /// Sets the trigger function to be on or off. Running this command produces the same effect as pressing the TRAN key.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetTransient(bool enabled)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var output = enabled ? "1" : "0";

            await equipment.SendCommand($"SOUR:TRAN:STAT {output}");
        }

        /// <summary>
        /// Queries the state of the transient generator
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryTransientEnabled()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:TRAN:STAT?");
            var output = await equipment.ReadInt();

            return output == 1;
        }

    }
}
