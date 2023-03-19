using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Specifies the external voltage state.
    /// </summary>
    public enum ExternalVoltage
    {
        /// <summary>
        /// The external voltage is ON.
        /// </summary>
        ON = 1,

        /// <summary>
        /// The external voltage is OFF.
        /// </summary>
        OFF = 0
    }

    /// <summary>
    /// Specifies the bias state.
    /// </summary>
    public enum BiasState
    {
        /// <summary>
        /// The bias is ON.
        /// </summary>
        ON = 1,

        /// <summary>
        /// The bias is OFF.
        /// </summary>
        OFF = 0
    }

    /// <summary>
    /// Represents the bias subsystem of the network test instrument.
    /// </summary>
    public class BiasSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the BiasSubsystem class.
        /// </summary>
        /// <param name="equipment">An instance of NetworkTestInstrument to communicate with the test equipment.</param>
        public BiasSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the internal bias current value.
        /// To activate the bias, use the command BIAS:STATe.
        /// </summary>
        /// <param name="current">The current value to set.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task SetBiasCurrentLevel(double current)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"BIAS:CURR:LEV {current}");
        }

        /// <summary>
        /// Queries the internal bias current value.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryBiasCurrentLevel()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"BIAS:CURR:LEV?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the value of the externally applied voltage.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryExternalAppliedVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"BIAS:EXT:MEAS:VOLT?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Activates the externally supplied bias voltage.
        /// </summary>
        /// <returns></returns>
        public async Task SetBiasExternalVoltageState(ExternalVoltage status)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"BIAS:EXT:VOLT:STAT {status}");
        }

        /// <summary>
        /// The query the externally supplied bias voltage state.
        /// </summary>
        /// <returns></returns>
        public async Task<ExternalVoltage> QueryBiasExternalVoltageState()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"BIAS:EXT:VOLT:STAT?");
            return Enum.Parse<ExternalVoltage>(await equipment.ReadString());
        }

        /// <summary>
        /// Activates the internal DC bias.
        /// </summary>
        /// <returns></returns>
        public async Task SetBiasInternalState(BiasState status)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"BIAS:STAT {status}");
        }

        /// <summary>
        /// The query the Activates the internal DC bias state.
        /// </summary>
        /// <returns></returns>
        public async Task<BiasState> QueryBiasInternalState()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"BIAS:STAT?");
            return Enum.Parse<BiasState>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the internal DC bias voltage value.
        /// </summary>
        /// <returns></returns>
        public async Task SetInternalBiasVoltage(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"BIAS:VOLT:LEV {voltage}");
        }

        /// <summary>
        /// Queries the internal DC bias voltage value.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryInternalBiasVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"BIAS:VOLT:LEV?");
            return await equipment.ReadDouble();
        }
    }
}