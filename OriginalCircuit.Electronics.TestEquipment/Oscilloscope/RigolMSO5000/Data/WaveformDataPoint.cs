using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Data
{
    public class WaveformDataPoint
    {
        public double Time { get; set; }
        public double Voltage { get; set; }

        public WaveformDataPoint(double time, double voltage)
        {
            Time = time;
            Voltage = voltage;
        }

        public override string ToString()
        {
            return $"[{Time}] {Voltage}";
        }
    }
}
