using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data
{
    /// <summary>
    /// Represents the output reading of an electronics test equipment
    /// </summary>
    public class OutputReading
    {
        /// <summary>
        /// Gets or sets the voltage of the output reading.
        /// </summary>
        public double Voltage { get; set; }
        /// <summary>
        /// Gets or sets the current of the output reading.
        /// </summary>
        public double Current { get; set; }
        /// <summary>
        /// Gets or sets the power of the output reading.
        /// </summary>
        public double Power { get; set; }

        /// <summary>
        /// Initializes a new instance of the OutputReading class with the specified voltage, current, and power.
        /// </summary>
        /// <param name="voltage">The voltage of the output reading.</param>
        /// <param name="current">The current of the output reading.</param>
        /// <param name="power">The power of the output reading.</param>
        public OutputReading(double voltage, double current, double power)
        {
            Voltage = voltage;
            Current = current;
            Power = power;
        }

        /// <summary>
        /// Returns a string that represents the current output reading.
        /// </summary>
        /// <returns>A string that represents the current output reading.</returns>
        public override string ToString()
        {
            return $"{Voltage}V {Current}A {Power}W";
        }
    }

}
