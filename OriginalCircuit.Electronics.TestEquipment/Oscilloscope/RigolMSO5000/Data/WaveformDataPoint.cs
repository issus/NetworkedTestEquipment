using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Data
{
    /// <summary>
    /// Represents a single point of waveform data, consisting of time and voltage.
    /// </summary>
    public class WaveformDataPoint
    {
        /// <summary>
        /// Gets or sets the time value of the waveform data point.
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Gets or sets the voltage value of the waveform data point.
        /// </summary>
        public double Voltage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveformDataPoint"/> class with the specified time and voltage values.
        /// </summary>
        /// <param name="time">The time value of the waveform data point.</param>
        /// <param name="voltage">The voltage value of the waveform data point.</param>
        public WaveformDataPoint(double time, double voltage)
        {
            Time = time;
            Voltage = voltage;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"[{Time}] {Voltage}";
        }
    }
}
