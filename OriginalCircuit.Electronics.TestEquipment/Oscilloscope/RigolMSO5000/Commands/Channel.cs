using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands
{
    public class Mso5000Channel
    {
        NetworkTestInstrument equipment;

        public Mso5000Channel(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the bandwidth limit of the specified channel.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetBandwidthLimit(string value = "OFF", ScopeChannel channel = ScopeChannel.CH1)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CHAN{(int)channel}:BWL {value}");
        }

        /// <summary>
        /// Queries the bandwidth limit of the specified channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> QueryBandwidthLimit(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:BWL?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Sets the coupling mode of the specified channel.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetCoupling(Coupling value, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CHAN{(int)channel}:COUP {value}");
        }

        /// <summary>
        /// Queries the coupling mode of the specified channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Coupling> QueryCoupling(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:COUP?");
            return Enum.Parse<Coupling>(await equipment.ReadString());
        }

        /// <summary>
        /// Turns on or off the specified channel
        /// </summary>
        /// <param name="input"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetDisplay(bool input, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var value = input ? "ON" : "OFF";

            await equipment.SendCommand($"CHAN{(int)channel}:DISP {value}");
        }

        /// <summary>
        /// Queries the on/off status of the specified channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryDisplay(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:DISP?");
            return await equipment.ReadBool();
        }

        /// <summary>
        /// Turns on or off the waveform invert for the specified channel
        /// </summary>
        /// <param name="input"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetInvert(bool input, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var value = input ? "ON" : "OFF";

            await equipment.SendCommand($"CHAN{(int)channel}:INV {value}");
        }

        /// <summary>
        /// Queries the on/off status of the waveform invert for the specified channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryInvert(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:INV?");
            return await equipment.ReadBool();
        }

        /// <summary>
        /// Sets the vertical offset of the specified channel. The default unit is V.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetOffset(double value, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CHAN{(int)channel}:OFFS {value}");
        }

        /// <summary>
        /// Queries the vertical offset of the specified channel. The default unit is V.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryOffset(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:OFFS?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the delay calibration time (used to calibrate the zero offset of the corresponding channel) of the specified channel. The default unit is s.
        /// </summary>
        /// <param name="value">Delay between -100ns and 100ns - expressed as seconds</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetDelayCalibration(double value, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CHAN{(int)channel}:TCAL {value}");
        }

        /// <summary>
        /// Queries the delay calibration time (used to calibrate the zero offset of the
        /// corresponding channel) of the specified channel. The default unit is s.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns>Delay of channel in seconds</returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryDelayCalibration(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:TCAL?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the vertical scale of the specified channel. The default unit is V.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetScale(double value, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CHAN{(int)channel}:SCALE {value}");
        }

        /// <summary>
        /// Sets the vertical scale of the specified channel. The default unit is V.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryScale(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:SCALE?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the probe ratio of the specified channel.
        /// </summary>
        /// <param name="value">Probe ratio can be:  0.0001, 0.0002, 0.0005, 0.001, 0.002, 0.005, 0.01, 0.02, 0.05, 0.1, 0.2,
        /// 0.5, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, or 50000</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetProbeAttenuation(double value, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CHAN{(int)channel}:PROB {value}");
        }

        /// <summary>
        /// Queries the probe ratio of the specified channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns>One of:  0.0001, 0.0002, 0.0005, 0.001, 0.002, 0.005, 0.01, 0.02, 0.05, 0.1, 0.2,
        /// 0.5, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, or 50000</returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryProbeAttenuation(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:PROB?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the amplitude display unit of the specified analog channel.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetUnits(ChannelUnits input, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string value;
            switch (input)
            {
                case ChannelUnits.Voltage:
                    value = "VOLT";
                    break;
                case ChannelUnits.Watt:
                    value = "WATT";
                    break;
                case ChannelUnits.Ampere:
                    value = "AMP";
                    break;
                case ChannelUnits.Unknown:
                default:
                    value = "UNKN";
                    break;
            }

            await equipment.SendCommand($"CHAN{(int)channel}:UNIT {value}");
        }

        /// <summary>
        /// Queries the amplitude display unit of the specified analog channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ChannelUnits?> QueryUnits(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:UNIT?");
            return ChannelUnitsFromString(await equipment.ReadString());
        }

        /// <summary>
        /// Enables or disables the fine adjustment of the vertical scale of the specified analog channel
        /// </summary>
        /// <param name="input"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetFineAdjustment(bool input, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            var value = input ? "ON" : "OFF";

            await equipment.SendCommand($"CHAN{(int)channel}:VERN {value}");
        }

        /// <summary>
        /// Queries the on/off status of the fine adjustment function of the vertical scale
        /// of the specified analog channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> QueryFineAdjustment(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:VERN?");
            return await equipment.ReadBool();
        }

        /// <summary>
        /// Sets the offset calibration voltage for calibrating the zero point of the specified analog channel.
        /// </summary>
        /// <param name="value">-100 V to 100 V</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetPosition(double value, ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CHAN{(int)channel}:POS {value}");
        }

        /// <summary>
        /// Queries the offset calibration voltage for calibrating the zero point of the specified analog channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns>-100 V to 100 V</returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryPosition(ScopeChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CHAN{(int)channel}:POS?");
            return await equipment.ReadDouble();
        }


        public ChannelUnits ChannelUnitsFromString(string input)
        {
            switch (input.ToUpper())
            {
                case "VOLT":
                case "VOLTAGE":
                    return ChannelUnits.Voltage;
                case "WATT":
                    return ChannelUnits.Watt;
                case "AMP":
                case "AMPERE":
                    return ChannelUnits.Ampere;
                case "UNKN":
                case "UNKNOWN":
                default:
                    return ChannelUnits.Unknown;
            }
        }
    }
}
