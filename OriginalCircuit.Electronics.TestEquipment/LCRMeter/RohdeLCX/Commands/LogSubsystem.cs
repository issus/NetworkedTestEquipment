using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Enumeration for log count mode.
    /// </summary>
    public enum LogCountMode
    {
        /// <summary>
        /// Minimum count mode.
        /// </summary>
        MIN,

        /// <summary>
        /// Minimum count mode.
        /// </summary>
        MINimum,

        /// <summary>
        /// Maximum count mode.
        /// </summary>
        MAX,

        /// <summary>
        /// Maximum count mode.
        /// </summary>
        MAXimum
    }

    /// <summary>
    /// Enumeration for log duration mode.
    /// </summary>
    public enum LogDurationMode
    {
        /// <summary>
        /// Minimum duration mode.
        /// </summary>
        MIN,

        /// <summary>
        /// Minimum duration mode.
        /// </summary>
        MINimum,

        /// <summary>
        /// Maximum duration mode.
        /// </summary>
        MAX,

        /// <summary>
        /// Maximum duration mode.
        /// </summary>
        MAXimum
    }

    /// <summary>
    /// Enumeration for log interval.
    /// </summary>
    public enum LogInterval
    {
        /// <summary>
        /// Minimum log interval.
        /// </summary>
        MIN,

        /// <summary>
        /// Minimum log interval.
        /// </summary>
        MINimum,

        /// <summary>
        /// Maximum log interval.
        /// </summary>
        MAX,

        /// <summary>
        /// Maximum log interval.
        /// </summary>
        MAXimum
    }

    /// <summary>
    /// Enumeration for log mode.
    /// </summary>
    public enum LogMode
    {
        /// <summary>
        /// No specified limit of measurement readings.
        /// </summary>
        UNL,

        /// <summary>
        /// Determines the number of measurement readings.
        /// </summary>
        COUN,

        /// <summary>
        /// Sets a time interval between the measurement readings.
        /// </summary>
        DUR,

        /// <summary>
        /// Defines start time and time span for the measurement readings.
        /// </summary>
        SPAN
    }

    /// <summary>
    /// Enumeration for logging state.
    /// </summary>
    public enum LoggingState
    {
        /// <summary>
        /// Logging is on.
        /// </summary>
        ON = 1,

        /// <summary>
        /// Logging is off.
        /// </summary>
        OFF = 0
    }

    /// <summary>
    /// Class representing the log subsystem of the Rohde LCX200 test equipment.
    /// </summary>
    public class LogSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Constructor for the log subsystem.
        /// </summary>
        /// <param name="equipment">The network test instrument used by the log subsystem.</param>
        public LogSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the number of measurement readings in count mode
        /// </summary>
        /// <returns></returns>
        public async Task SetCountNumberReadings(double sample)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"LOG:COUNT {sample}");
        }


        /// <summary>
        /// Queries the mode of measurement readings in count mode
        /// </summary>
        /// <returns></returns>
        public async Task<LogCountMode> QueryCountModeReadings()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"LOG:COUNT?");
            return Enum.Parse<LogCountMode>(await equipment.ReadString());
        }


        /// <summary>
        /// Defines the duration of logging for the measurement in span and duration mode.
        /// </summary>
        /// <returns></returns>
        public async Task SetDurationOfLogging(int time)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"LOG:DUR {time}");
        }


        /// <summary>
        /// Queries the duration of logging for the measurement in span and duration mode.
        /// </summary>
        /// <returns></returns>
        public async Task<LogDurationMode> QueryDurationOfLogging()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"LOG:DUR?");
            return Enum.Parse<LogDurationMode>(await equipment.ReadString());
        }


        /// <summary>
        /// Sets the file name and path for the storing the data recorded during data logging.
        /// </summary>
        /// <returns></returns>
        public async Task SetLoggingFileName(string name)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"LOG:FNAM {name}");
        }


        /// <summary>
        /// The query returns the file name and path.
        /// You can query the information also when data logging is running.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryLoggingFileName()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"LOG:FNAM?");
            return await equipment.ReadString();
        }


        /// <summary>
        /// Selects the logging measurement interval (in seconds).
        /// The measurement interval describes the time between the recorded measurements.
        /// </summary>
        /// <returns></returns>
        public async Task SetLoggingInterval(int time)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"LOG:INT {time}");
        }


        /// <summary>
        /// Queries the logging measurement interval (in seconds).
        /// </summary>
        /// <returns></returns>
        public async Task<LogInterval> QueryLoggingInterval()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"LOG:INT?");
            return Enum.Parse<LogInterval>(await equipment.ReadString());
        }


        /// <summary>
        /// Selects the data logging mode.
        /// </summary>
        /// <returns></returns>
        public async Task SetLogMode(LogMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"LOG:MODE {mode}");
        }

        /// <summary>
        /// Queries the data logging mode.
        /// </summary>
        /// <returns></returns>
        public async Task<LogMode> QueryLogMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"LOG:MODE?");
            return Enum.Parse<LogMode>(await equipment.ReadString());
        }


        /// <summary>
        /// Sets the logging start time.
        /// </summary>
        /// <returns></returns>
        public async Task SetLoggingTime(int year, int month, int day, int hour, int minute, int second)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"LOG:STIM {year}, {month}, {day}, {hour}, {minute}, {second}");
        }


        /// <summary>
        /// Activates the data logging function.
        /// </summary>
        /// <returns></returns>
        public async Task SetLoggingState(LogMode state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"LOG:STAT {state}");
        }



    }
}