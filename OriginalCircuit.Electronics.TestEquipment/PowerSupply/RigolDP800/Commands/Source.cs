using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands
{

    public class Source
    {
        NetworkTestInstrument equipment;
        string tree;

        public Source(NetworkTestInstrument equipment, string tree)
        {
            this.equipment = equipment;
            this.tree = tree;
        }


        /// <summary>
        /// Set the current of the specified channel. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Query the current of the specified channel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Set the step of the current change of the specified channel.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Query the step of the current change of the specified channel
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <param name="trigger"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Clear the circuit and label of the OCP occurred on the specified channel and turn on
        /// the output of the corresponding channel.
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <param name="trigger"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryProtection(DP800Channel? channel = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            if (channel == null)
                await equipment.SendCommand("{tree}:PROT?");
            else
                await equipment.SendCommand($"SOUR{(int)channel}:{tree}:PROT?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Enable or disable the overcurrent protection (OCP) function of the specified
        /// channel.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Query the status of the overcurrent protection (OCP) function of the specified
        /// channel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
