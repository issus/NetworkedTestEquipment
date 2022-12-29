using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{

    public abstract class ConstantWithRange : Constant
    {
        protected ConstantWithRange(NetworkTestInstrument equipment, string tree) : base(equipment, tree)
        {
        }

        /// <summary>
        /// Sets the voltage range in to be a high range or a low one.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Queries the voltage range set
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

    public abstract class Constant
    {
        protected internal NetworkTestInstrument equipment;
        protected internal string tree;

        protected Constant(NetworkTestInstrument equipment, string tree)
        {
            this.equipment = equipment;
            this.tree = tree;
        }

        /// <summary>
        /// Sets the voltage limit
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Sets the voltage limit
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetVoltageLimit(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:{tree}:VLIM {voltage:F4}");
        }

        /// <summary>
        /// Queries the voltage limit set
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryVoltageLimit()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SOUR:{tree}:VLIM?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the current limit
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Sets the current limit
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetCurrentLimit(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:{tree}:ILIM {voltage:F4}");
        }

        /// <summary>
        /// Queries the current limit set
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
