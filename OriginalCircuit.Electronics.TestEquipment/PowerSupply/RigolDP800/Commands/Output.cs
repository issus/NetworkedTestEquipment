using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands
{
    public class Output
    {
        NetworkTestInstrument equipment;

        public Output(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        private OutputMode? ModeFromString(string input)
        {
            switch (input.Trim())
            {
                case "CV":
                    return OutputMode.ConstantVoltage;
                case "CC":
                    return OutputMode.ConstantCurrent;
                case "UR":
                    return OutputMode.Unregulated;
            }

            return null;
        }

        /// <summary>
        /// Query the current output mode of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<OutputMode?> QueryMode(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"OUTP:MODE? {chan}");

            return ModeFromString(await equipment.ReadString());
        }

        /// <summary>
        /// Query whether OCP occurred on the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool?> QueryOCPAlarm(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"OUTP:OCP:ALAR? {chan}");

            return await equipment.ReadBool();
        }

        /// <summary>
        /// Clear the label of the overcurrent protection occurred on the specified channel. 
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ClearOCP(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"OUTP:OCP:CLEAR {chan}");
        }

        /// <summary>
        /// Enable or disable the overcurrent protection (OCP) function of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetOCPEnabled(bool ocpOn, DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string ocp = ocpOn ? "ON" : "OFF";

            if (ocp == null)
                await equipment.SendCommand($"OUTP:OCP {ocp}");
            else
                await equipment.SendCommand($"OUTP:OCP {chan},{ocp}");
        }

        /// <summary>
        /// Query the status of the overcurrent protection (OCP) function of the specified
        /// channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool?> QueryOCPEnabled(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"OUTP:OCP? {chan}");

            return await equipment.ReadBool();
        }

        /// <summary>
        /// Set the overcurrent protection value of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetOCPValue(double voltage, DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (chan == null)
                await equipment.SendCommand($"OUTP:OCP:VAL {voltage:F5}");
            else
                await equipment.SendCommand($"OUTP:OCP:VAL {chan},{voltage:F5}");
        }

        /// <summary>
        /// Query the overcurrent protection value of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryOCPValue(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"OUTP:OCP:VAL? {chan}");

            return await equipment.ReadDouble();
        }



        /// <summary>
        /// Query whether OVP occurred on the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool?> QueryOVPAlarm(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"OUTP:OVP:ALAR? {chan}");

            return await equipment.ReadBool();
        }

        /// <summary>
        /// Clear the label of the overvoltage protection occurred on the specified channel. 
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ClearOVP(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"OUTP:OVP:CLEAR {chan}");
        }

        /// <summary>
        /// Enable or disable the overvoltage protection (OVP) function of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetOVPEnabled(bool ovpOn, DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string ovp = ovpOn ? "ON" : "OFF";

            if (ovp == null)
                await equipment.SendCommand($"OUTP:OVP {ovp}");
            else
                await equipment.SendCommand($"OUTP:OVP {chan},{ovp}");
        }

        /// <summary>
        /// Query the status of the overvoltage protection (OVP) function of the specified
        /// channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool?> QueryOVPEnabled(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"OUTP:OVP? {chan}");

            return await equipment.ReadBool();
        }

        /// <summary>
        /// Set the overvoltage protection value of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetOVPValue(double voltage, DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            if (chan == null)
                await equipment.SendCommand($"OUTP:OVP:VAL {voltage:F5}");
            else
                await equipment.SendCommand($"OUTP:OVP:VAL {chan},{voltage:F5}");
        }

        /// <summary>
        /// Query the overvoltage protection value of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryOVPValue(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"OUTP:OVP:VAL? {chan}");

            return await equipment.ReadDouble();
        }

        public async Task Enable(DP800Channel? chan = null)
        {
            await SetOutputEnabled(true, chan);
        }

        public async Task Disable(DP800Channel? chan = null)
        {
            await SetOutputEnabled(false, chan);
        }

        /// <summary>
        /// Enable or disable the output of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetOutputEnabled(bool enabled, DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var output = enabled ? "ON" : "OFF";

            if (chan == null)
                await equipment.SendCommand($"OUTP {output}");
            else
                await equipment.SendCommand($"OUTP {chan},{output}");
        }

        /// <summary>
        /// Query the output status of the specified channel.
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryOutputEnabled(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"OUTP:? {chan}");
            return await equipment.ReadBool();
        }

        /// <summary>
        /// Enable or disable the track function of the specified channel. 
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetTracking(bool enabled, DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var output = enabled ? "ON" : "OFF";

            if (chan == null)
                await equipment.SendCommand($"OUTP:TRAC {output}");
            else
                await equipment.SendCommand($"OUTP:TRAC {chan},{output}");
        }

        /// <summary>
        /// Query the status of the track function of the specified channel. 
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryTracking(DP800Channel? chan = null)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"OUTP:TRAC? {chan}");
            return await equipment.ReadBool();
        }
    }
}
