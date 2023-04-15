using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands.BurstWaveCommand;
using static OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.SiglentSDGInstrument;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents a command for setting the frequency counter on a Siglent SDG series function generator.
    /// </summary>
    public class FrequencyCounterCommand
    {
        /// <summary>
        /// The instrument that this command will be sent to.
        /// </summary>
        NetworkTestInstrument? equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyCounterCommand"/> class with the specified equipment.
        /// </summary>
        /// <param name="instrument">The instrument that this command will be sent to.</param>
        public FrequencyCounterCommand(NetworkTestInstrument instrument)
        {
            equipment = instrument;
        }

        /// <summary>
        /// Sets the state of the frequency counter.
        /// </summary>
        /// <param name="state">The desired state of the frequency counter.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetState(bool state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");
            await equipment.SendCommand($"FCNT STATE,{(state ? "ON" : "OFF")}");
        }

        /// <summary>
        /// Sets the reference frequency for calculating the frequency deviation.
        /// </summary>
        /// <param name="refFreq">The desired reference frequency in Hertz.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetReferenceFrequency(double refFreq)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FCNT REFQ,{refFreq}");
        }

        /// <summary>
        /// Sets the trigger level for the frequency counter.
        /// </summary>
        /// <param name="trigLevel">The desired trigger level in Volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetTriggerLevel(double trigLevel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");
            await equipment.SendCommand($"FCNT TRG,{trigLevel}");
        }

        /// <summary>
        /// Sets the coupling mode for the frequency counter.
        /// </summary>
        /// <param name="mode">The desired coupling mode.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetCouplingMode(CouplingMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");
            await equipment.SendCommand($"FCNT MODE,{mode}");
        }

        /// <summary>
        /// Sets the state of the High Frequency Rejection for the frequency counter.
        /// </summary>
        /// <param name="state">The desired state of the High Frequency Rejection.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetHighFrequencyRejectionState(bool state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");
            await equipment.SendCommand($"FCNT HFR,{(state ? "ON" : "OFF")}");
        }

        /// <summary>
        /// Queries the frequency counter parameters.
        /// </summary>
        /// <returns>A string containing the frequency counter parameters.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<string> QueryFrequencyCounterParameters()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");
            await equipment.ClearBuffer();

            await equipment.SendCommand($"FCNT?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Enumeration representing the coupling mode for the frequency counter.
        /// </summary>
        public enum CouplingMode
        {
            /// <summary>
            /// AC coupling mode.
            /// </summary>
            AC,
            /// <summary>
            /// DC coupling mode.
            /// </summary>
            DC
        }
    }
}