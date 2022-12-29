using System;
using System.Linq;

namespace OriginalCircuit.Electronics.TestEquipment.Load.RigolDL3000.Commands
{
    public class Transient
    {
        NetworkTestInstrument equipment;

        public Transient(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }


        /// <summary>
        /// Sets the transient operation mode in CC mode.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetMode(TransientMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (mode)
            {
                case TransientMode.Continuous:
                    fn = "CONT";
                    break;
                case TransientMode.Pulse:
                    fn = "PULS";
                    break;
                case TransientMode.Toggle:
                default:
                    fn = "TOGG";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRAN:MODE {fn}");
        }

        /// <summary>
        /// Queries the transient operation mode in CC mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TransientMode?> QueryMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRAN:MODE?");
            var output = await equipment.ReadString();

            return Source.TransientModeFromString(output);
        }


        /// <summary>
        /// Sets Level A in transient operation mode
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetCurrentHigh(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRANS:ALEV {fn}");
        }

        /// <summary>
        /// Sets Level A in transient operation mode
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetCurrentHigh(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:TRANS:ALEV {rate:F4}");
        }

        /// <summary>
        /// Queries Level A set in transient operation mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryCurrentHigh()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRANS:ALEV?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets Level B in transient operation mode
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetCurrentLow(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRANS:BLEV {fn}");
        }

        /// <summary>
        /// Sets Level B in transient operation mode
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetCurrentLow(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:TRANS:BLEV {rate:F4}");
        }

        /// <summary>
        /// Queries Level B set in transient operation mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryCurrentLow()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRANS:BLEV?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the width of Level A in continuous and pulsed transient operation.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetWidthA(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRANS:AWID {fn}");
        }

        /// <summary>
        /// Sets the width of Level A in continuous and pulsed transient operation.
        /// </summary>
        /// <param name="rate">Time in milliseconds</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetWidthA(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:TRANS:AWID {rate:F4}");
        }

        /// <summary>
        /// Queries width of Level A in continuous and pulsed transient operation.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryWidthA()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRANS:AWID?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the width of Level B in continuous and pulsed transient operation.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetWidthB(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRANS:BWID {fn}");
        }

        /// <summary>
        /// Sets the width of Level B in continuous and pulsed transient operation.
        /// </summary>
        /// <param name="rate">Time in milliseconds</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetWidthB(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:TRANS:BWID {rate:F4}");
        }

        /// <summary>
        /// Queries width of Level B in continuous and pulsed transient operation.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryWidthB()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRANS:BWID?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the frequency in continuous mode.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetFrequency(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRANS:FREQ {fn}");
        }

        /// <summary>
        /// Sets the frequency in continuous mode.
        /// </summary>
        /// <param name="rate">Frequency in kHz</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetFrequency(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:TRANS:FREQ {rate:F4}");
        }

        /// <summary>
        /// Queries the frequency set in continuous mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryFrequency()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRANS:FREQ?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the frequency in continuous mode.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetPeriod(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRANS:PER {fn}");
        }

        /// <summary>
        /// Sets the period in continuous mode
        /// </summary>
        /// <param name="rate">Period in ms</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetPeriod(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:TRANS:PER {rate:F4}");
        }

        /// <summary>
        /// Queries the period in continuous mode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryPeriod()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRANS:PER?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the duty cycle in continuous mode.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetDuty(DL3000Range range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string fn;
            switch (range)
            {
                case DL3000Range.Minimum:
                    fn = "MIN";
                    break;
                case DL3000Range.Maximum:
                    fn = "MAX";
                    break;
                case DL3000Range.Default:
                default:
                    fn = "DEF";
                    break;
            }

            await equipment.SendCommand($"SOUR:CURR:TRANS:ADUT {fn}");
        }

        /// <summary>
        /// Sets the duty cycle in continuous mode.
        /// </summary>
        /// <param name="rate">Period in %</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SetDuty(double rate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SOUR:CURR:TRANS:ADUT {rate:F4}");
        }

        /// <summary>
        /// Queries the duty cycle in continuous mode
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<double> QueryDuty()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand("SOUR:CURR:TRANS:ADUT?");
            return await equipment.ReadDouble();
        }
    }
}
