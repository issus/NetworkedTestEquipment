using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands
{
    /// <summary>
    /// Represents a source on a test equipment.
    /// </summary>
    public class Source
    {
        NetworkTestInstrument equipment;
        string tree;

        /// <summary>
        /// Initializes a new instance of the <see cref="Source"/> class.
        /// </summary>
        /// <param name="equipment">The test equipment used for communication with the source.</param>
        /// <param name="tree">The source identifier for the test equipment, e.g. "VOLT".</param>
        public Source(NetworkTestInstrument equipment, string tree)
        {
            this.equipment = equipment;
            this.tree = tree;
        }


        /// <summary>
        /// Sets the output current of the specified channel.
        /// </summary>
        /// <param name="value">The current value to set in units defined by the test equipment.</param>
        /// <param name="channel">The channel to set the current for. If null, the current will be set for all channels.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task Set(double value, DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (channel == null)
                await equipment.SendCommand($"{tree} {value:F4}");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree} {value:F4}");
        }

        /// <summary>
        /// Queries the output current of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the current for. If null, the current for all channels will be queried.</param>
        /// <returns>The current value in units defined by the test equipment.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task<double> QuerySet(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (channel == null)
                await equipment.SendCommand("{tree}?");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the step of the current change of the specified channel.
        /// </summary>
        /// <param name="step">The step size to set in units defined by the test equipment.</param>
        /// <param name="channel">The channel to set the step for. If null, the step will be set for all channels.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task SetStep(double step, DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (channel == null)
                await equipment.SendCommand($"{tree}:STEP {step:F4}");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:STEP {step:F4}");
        }

        /// <summary>
        /// Queries the step of the current change of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the step for. If null, the step for all channels will be queried.</param>
        /// <returns>The step size in units defined by the test equipment.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task<double> QueryStep(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (channel == null)
                await equipment.SendCommand("{tree}:STEP?");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:STEP?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Set the trigger current of the specified channel.
        /// </summary>
        /// <param name="trigger">The trigger current to set.</param>
        /// <param name="channel">The channel to set the trigger current for. If null, the global trigger current will be set.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task SetTrigger(double trigger, DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (channel == null)
                await equipment.SendCommand($"{tree}:TRIG {trigger:F4}");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:TRIG {trigger:F4}");
        }

        /// <summary>
        /// Query the trigger current of the specified channel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryTrigger(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (channel == null)
                await equipment.SendCommand("{tree}:TRIG?");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:TRIG?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Clear the overcurrent protection (OCP) condition and corresponding label of the channel and turn on the output of the channel.
        /// </summary>
        /// <param name="channel">The channel to clear the protection for. Null value means to clear protection for all channels.</param>
        /// <returns>A Task object representing the completion of the operation.</returns>
        /// <exception cref="Exception">Thrown when test equipment is not connected.</exception>
        public async Task ClearProtection(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (channel == null)
                await equipment.SendCommand($"{tree}:PROT:CLE");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:PROT:CLE");
        }

        /// <summary>
        /// Query whether OCP occurred on the specified channel
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryProtectionTriggered(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (channel == null)
                await equipment.SendCommand("{tree}:PROT:TRIP?");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:PROT:TRIP?");

            return await equipment.ReadBool();
        }


        /// <summary>
        /// Set the overcurrent protection (OCP) value of the specified channel.
        /// </summary>
        /// <param name="trigger">The overcurrent protection value to set.</param>
        /// <param name="channel">The channel to set the OCP value for. If null, the global OCP value will be set.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task SetProtection(double trigger, DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (channel == null)
                await equipment.SendCommand($"{tree}:PROT {trigger:F4}");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:PROT {trigger:F4}");
        }

        /// <summary>
        /// Query the overcurrent protection (OCP) value of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the OCP value for. If null, the global OCP value will be queried.</param>
        /// <returns>The overcurrent protection value of the specified channel.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task<double> QueryProtection(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (channel == null)
                await equipment.SendCommand($"{tree}:PROT?");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:PROT?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Enable or disable the overcurrent protection (OCP) function of the specified channel.
        /// </summary>
        /// <param name="enabled">True to enable the OCP function, false to disable it.</param>
        /// <param name="channel">The channel to set the OCP function for. If null, the global OCP function will be set.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task SetProtectionEnabled(bool enabled, DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string en = enabled ? "ON" : "OFF";

            if (channel == null)
                await equipment.SendCommand($"{tree}:PROT:STAT {en}");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:PROT:STAT {en}");
        }

        /// <summary>
        /// Query the status of the overcurrent protection (OCP) function of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the OCP function status for. If null, the global OCP function status will be queried.</param>
        /// <returns>True if the OCP function is enabled for the specified channel, false otherwise.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task<bool> QueryProtectionEnabled(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (channel == null)
                await equipment.SendCommand($"{tree}:PROT:STAT?");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:PROT:STAT?");

            return await equipment.ReadBool();
        }

    }
}
