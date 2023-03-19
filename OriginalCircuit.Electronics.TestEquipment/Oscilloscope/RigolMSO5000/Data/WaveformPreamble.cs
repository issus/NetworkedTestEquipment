using OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Data
{
    /// <summary>
    /// Represents the preamble of a waveform.
    /// </summary>
    public class WaveformPreamble
    {
        /// <summary>
        /// Gets or sets the waveform format.
        /// </summary>
        public WaveformFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the waveform mode.
        /// </summary>
        public WaveformMode Mode { get; set; }


        /// <summary>
        /// Gets or sets the number of data points in the waveform.
        /// After the memory depth option is installed, points is an integer ranging from 1 to 200,000,000.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets the number of averages in the average sample mode. The value of this parameter is 1 in other modes.
        /// </summary>
        public int Averages { get; set; }

        /// <summary>
        /// Gets or sets the time difference between two neighboring points in the X direction.
        /// </summary>
        public double XIncrement { get; set; }

        /// <summary>
        /// Gets or sets the start time of the waveform data in the X direction. 
        /// </summary>
        public double XOrigin { get; set; }

        /// <summary>
        /// Gets or sets the reference time of the waveform data in the X direction.
        /// </summary>
        public double XReference { get; set; }

        /// <summary>
        /// Gets or sets the step value of the waveforms in the Y direction. 
        /// </summary>
        public double YIncrement { get; set; }

        /// <summary>
        /// Gets or sets the vertical offset relative to the "Vertical Reference Position" in the Y direction.
        /// </summary>
        public int YOrigin { get; set; }

        /// <summary>
        /// Gets or sets the vertical reference position in the Y direction.
        /// </summary>
        public int YReference { get; set; }

        /// <summary>
        /// Initializes a new instance of the WaveformPreamble class with the specified query return string.
        /// </summary>
        /// <param name="queryReturn">The query return string.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the query return string is null or empty.</exception>
        public WaveformPreamble(string queryReturn)
        {
            if (String.IsNullOrEmpty(queryReturn)) throw new ArgumentNullException("No query return.");

            var parts = queryReturn.Split(',');

            Format = (WaveformFormat)int.Parse(parts[0]);
            Mode = (WaveformMode)int.Parse((parts[1]));
            Points = int.Parse((parts[2]), NumberStyles.Float);
            Averages = int.Parse((parts[3]), NumberStyles.Float);
            XIncrement = double.Parse((parts[4]), NumberStyles.Float);
            XOrigin = double.Parse((parts[5]), NumberStyles.Float);
            XReference = double.Parse((parts[6]), NumberStyles.Float);
            YIncrement = double.Parse((parts[7]), NumberStyles.Float);
            YOrigin = int.Parse((parts[8]), NumberStyles.Float);
            YReference = int.Parse((parts[9]), NumberStyles.Float);
        }

        /// <summary>
        /// Returns a string representation of the WaveformPreamble.
        /// </summary>
        /// <returns>A string representation of the WaveformPreamble.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Format:\t{Format}");
            sb.AppendLine($"Mode:\t{Mode}");
            sb.AppendLine($"");
            sb.AppendLine($"Points:\t{Points}");
            sb.AppendLine($"Averages:\t{Averages}");
            sb.AppendLine($"");
            sb.AppendLine($"XIncrement:\t{XIncrement}");
            sb.AppendLine($"XOrigin:\t{XOrigin}");
            sb.AppendLine($"XReference:\t{XReference}");
            sb.AppendLine($"");
            sb.AppendLine($"YIncrement:\t{YIncrement}");
            sb.AppendLine($"YOrigin:\t{YOrigin}");
            sb.AppendLine($"YReference:\t{YReference}");

            return sb.ToString();
        }
    }
}
