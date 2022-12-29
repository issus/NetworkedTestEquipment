using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Data
{
    public class InstrumentIdentity
    {
        public InstrumentIdentity(string manufacturer, string model, string serialNumber, string version)
        {
            Manufacturer = manufacturer;
            Model = model;
            SerialNumber = serialNumber;
            Version = version;
        }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Version { get; set; }
    }
}
