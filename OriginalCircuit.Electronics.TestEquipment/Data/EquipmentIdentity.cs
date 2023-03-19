using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Data
{
    /// <summary>
    /// Represents an electronic instrument's identity information.
    /// </summary>
    public class InstrumentIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentIdentity"/> class.
        /// </summary>
        /// <param name="manufacturer">The name of the instrument's manufacturer.</param>
        /// <param name="model">The model of the instrument.</param>
        /// <param name="serialNumber">The serial number of the instrument.</param>
        /// <param name="version">The version of the instrument's firmware or software.</param>
        public InstrumentIdentity(string manufacturer, string model, string serialNumber, string version)
        {
            Manufacturer = manufacturer;
            Model = model;
            SerialNumber = serialNumber;
            Version = version;
        }

        /// <summary>
        /// Gets or sets the name of the instrument's manufacturer.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the model of the instrument.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the serial number of the instrument.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the version of the instrument's firmware or software.
        /// </summary>
        public string Version { get; set; }
    }
}
