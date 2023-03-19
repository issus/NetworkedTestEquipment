using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    /// <summary>
    /// Abstract class that defines a constant with a range to be set on a Rigol DL3000 series instrument
    /// </summary>
    public abstract class ConstantWithRange : Constant
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantWithRange"/> class.
        /// </summary>
        /// <param name="equipment">The <see cref="NetworkTestInstrument"/> equipment to be used.</param>
        /// <param name="tree">The tree location of the constant.</param>
        protected ConstantWithRange(NetworkTestInstrument equipment, string tree) : base(equipment, tree)
        {
        }

        /// <summary>
        /// Sets the voltage range to be a high range or a low one.
        /// </summary>
        /// <param name="range">The range to be set</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected</exception>
        public async Task SetRange(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:{tree}:RANG {fn}");
        }

        /// <summary>
        /// Queries the voltage range that is currently set
        /// </summary>
        /// <returns>A task that represents the asynchronous operation and returns the voltage range</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected</exception>
        public async Task<DL3000Range?> QueryRange()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SOUR:{tree}:RANG?");
            var output = await equipment.ReadString();

            return Source.RangeFromString(output);
        }
    }

    /// <summary>
    /// Abstract class that defines a constant on a Rigol DL3000 series instrument
    /// </summary>
    public abstract class Constant
    {
        /// <summary>
        /// NetworkTestInstrument for this instrument
        /// </summary>
        protected internal NetworkTestInstrument equipment;
        /// <summary>
        /// The string representation of the instrument's command tree where the constant is located.
        /// </summary>
        protected internal string tree;

        /// <summary>
        /// Constructor that initializes the instance of the Constant class with the specified NetworkTestInstrument instance and command tree.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument instance.</param>
        /// <param name="tree">The command tree where the constant is located.</param>

        protected Constant(NetworkTestInstrument equipment, string tree)
        {
            this.equipment = equipment;
            this.tree = tree;
        }

        /// <summary>
        /// Sets the voltage limit of the DL3000 Range
        /// </summary>
        /// <param name="range">The DL3000 range which the voltage limit will be set to. Minimum, Maximum or Default.</param>
        /// <returns>Awaitable Task</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetVoltageLimit(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:{tree}:VLIM {fn}");
        }

        /// <summary>
        /// Sets the voltage limit of the DL3000 Range to a specific voltage.
        /// </summary>
        /// <param name="voltage">The voltage limit which will be set to the range.</param>
        /// <returns>Awaitable Task</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetVoltageLimit(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:{tree}:VLIM {voltage:F4}");
        }

        /// <summary>
        /// Queries the voltage limit set on the DL3000 Range
        /// </summary>
        /// <returns>The voltage limit set on the DL3000 Range</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<double> QueryVoltageLimit()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SOUR:{tree}:VLIM?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the current limit of the DL3000 Range
        /// </summary>
        /// <param name="range">The DL3000 range which the current limit will be set to. Minimum, Maximum or Default.</param>
        /// <returns>Awaitable Task</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetCurrentLimit(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:{tree}:ILIM {fn}");
        }

        /// <summary>
        /// Sets the current limit of the DL3000 Range to a specific current.
        /// </summary>
        /// <param name="voltage">The current limit which will be set to the range.</param>
        /// <returns>Awaitable Task</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetCurrentLimit(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:{tree}:ILIM {voltage:F4}");
        }

        /// <summary>
        /// Queries the current limit set on the DL3000 Range
        /// </summary>
        /// <returns>The current limit set on the DL3000 Range</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<double> QueryCurrentLimit()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SOUR:{tree}:ILIM?");
            return await equipment.ReadDouble();
        }
    }
}
