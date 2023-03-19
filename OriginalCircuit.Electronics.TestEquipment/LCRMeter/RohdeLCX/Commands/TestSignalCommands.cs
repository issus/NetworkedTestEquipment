using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Sets the measurement time mode and the acquisition time interval.
    /// </summary>
    public enum MeasurementTime
    {
        /// <summary>
        /// Sets the measurement time ≤.015 s.
        /// </summary>
        SHORt,
        /// <summary>
        /// Sets the measurement time ≤0.100 s.
        /// </summary>
        MEDium,
        /// <summary>
        /// Sets the measurement time ≤0.500 s.
        /// </summary>
        LONG,
        /// <summary>
        /// Uses the default setting "SHORt"
        /// </summary>
        DEFault,

    }

    /// <summary>
    /// Represents the commands for controlling the test signal of an instrument.
    /// </summary>
    public class TestSignalCommands
    {
        /// <summary>
        /// The network test instrument used to send commands to the equipment.
        /// </summary>
        private NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the TestSignalCommands class with the specified network test instrument.
        /// </summary>
        /// <param name="equipment">The network test instrument to use for sending commands.</param>
        public TestSignalCommands(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }


        /// <summary>
        /// Sets the measurement time mode and the acquisition time interval.
        /// </summary>
        /// <returns></returns>
        public async Task SetAperture(MeasurementTime time)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"APER {time}");
        }

        /// <summary>
        /// The query returns the measurement time mode and the acquisition time interval.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasurementTime> QueryAperture()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"APER?");
            return Enum.Parse<MeasurementTime>(await equipment.ReadString());
        }


        /// <summary>
        /// Sets the test signal current in RMS (root mean square).
        /// Note: does need to write CURR:LEV or CURR:[LEV] ???
        /// </summary>
        /// <returns></returns>
        public async Task SetTestCurrent(double current)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CURR:LEV {current}");
        }


        /// <summary>
        /// Queries the test signal current in RMS (root mean square).
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryTestCurrent()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CURR:LEV?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Sets the frequency of the test signal.
        /// </summary>
        /// <returns></returns>
        public async Task SetTestFrequency(int frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FREQ:CW {frequency}");
        }


        /// <summary>
        /// Queries the test signal frequency.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryTestFrequency()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FREQ:CW?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Queries the max test signal frequency.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryMaxFrequency()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FREQ:CW? MAX");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Queries the min test signal frequency.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryMinFrequency()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FREQ:CW? MIN");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the test signal voltage in RMS (root mean square).
        /// </summary>
        /// <returns></returns>
        public async Task SetTestVoltage(double voltage)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"VOLT:LEV {voltage}");
        }

        /// <summary>
        /// Queries the test signal voltage in RMS (root mean square).
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryTestVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"VOLT:LEV?");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Queries the test signal MINimum voltage in RMS (root mean square).
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryMinVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"VOLT:LEV? MIN");
            return await equipment.ReadDouble();
        }



        /// <summary>
        /// Queries the test signal MAXimum voltage in RMS (root mean square).
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryMaxVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"VOLT:LEV? MAX");
            return await equipment.ReadDouble();
        }


        /// <summary>
        /// Queries the test signal DEFault voltage in RMS (root mean square).
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryDefaultTestVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"VOLT:LEV? DEF");
            return await equipment.ReadDouble();
        }

    }
}