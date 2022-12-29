using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands.Data
{
    public class OutputReading
    {
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double Power { get; set; }

        public OutputReading(double voltage, double current, double power)
        {
            Voltage = voltage;
            Current = current;
            Power = power;
        }

        public override string ToString()
        {
            return $"{Voltage}V {Current}A {Power}W";
        }
    }
}
