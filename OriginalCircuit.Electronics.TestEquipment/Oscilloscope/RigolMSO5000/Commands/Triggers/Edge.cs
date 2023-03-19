using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands.Triggers
{
    /// <summary>
    /// Represents the edge used as trigger for oscilloscope.
    /// </summary>
    public class Edge
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class with specified network test equipment.
        /// </summary>
        /// <param name="equipment">The network test equipment to use for the edge.</param>
        public Edge(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the trigger source of Edge trigger
        /// </summary>
        /// <returns></returns>
        public async Task SetSource(TriggerEdgeSource source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:EDGE:SOUR {source}");
        }

        /// <summary>
        /// Queries the trigger source of Edge trigger
        /// The query returns D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14,  
        /// D15, CHAN1, CHAN2, CHAN3, CHAN4, or ACL.
        /// </summary>
        /// <returns></returns>
        public async Task<TriggerEdgeSource> QuerySource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:EDGE:SOUR?");
            return Enum.Parse<TriggerEdgeSource>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the edge type of Edge trigger.
        /// POSitive: indicates the rising edge.
        /// NEGative: indicates the falling edge
        /// RFALl: indicates the rising or falling edge
        /// </summary>
        /// <returns></returns>
        public async Task SetSlope(TriggerSlope slope)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:EDGE:SLOP {slope}");
        }

        /// <summary>
        /// Queries the edge type of Edge trigger.
        /// The query returns POS, NEG, or RFAL
        /// </summary>
        /// <returns></returns>
        public async Task<TriggerSlope> QuerySlope()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:EDGE:SLOP?");
            return Enum.Parse<TriggerSlope>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the trigger level of Edge trigger. The unit is the same as that of current
        /// amplitude of the selected source.
        /// </summary>
        /// <returns></returns>
        public async Task SetLevel(double level)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"TRIG:EDGE:LEV {level}");
        }

        /// <summary>
        /// Queries the trigger level of Edge trigger. The unit is the same as that of current
        /// amplitude of the selected source.
        /// The query returns the trigger level in scientific notation.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryLevel()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"TRIG:EDGE:LEV?");
            return await equipment.ReadDouble();
        }
    }
}
