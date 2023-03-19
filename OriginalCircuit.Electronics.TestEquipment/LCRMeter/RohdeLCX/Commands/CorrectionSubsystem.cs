using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Represents the different states for a load.
    /// </summary>
    public enum LoadState
    {
        /// <summary>
        /// The load is on.
        /// </summary>
        ON = 1,
        /// <summary>
        /// The load is off.
        /// </summary>
        OFF = 0
    }

    /// <summary>
    /// Represents the different states for an open.
    /// </summary>
    public enum OpenState
    {
        /// <summary>
        /// The open is on.
        /// </summary>
        ON = 1,
        /// <summary>
        /// The open is off.
        /// </summary>
        OFF = 0
    }

    /// <summary>
    /// Represents the different states for a short.
    /// </summary>
    public enum ShortState
    {
        /// <summary>
        /// The short is on.
        /// </summary>
        ON = 1,
        /// <summary>
        /// The short is off.
        /// </summary>
        OFF = 0
    }

    /// <summary>
    /// Represents the correction subsystem for a network test instrument.
    /// </summary>
    public class CorrectionSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the CorrectionSubsystem class.
        /// </summary>
        /// <param name="equipment">The network test instrument to use for the correction subsystem.</param>
        public CorrectionSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the length of the leads to the connected test fixture i.e. the DUT.
        /// </summary>
        /// <returns></returns>
        public async Task SetCableLengthCorrection(double length)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:LENG {length}");
        }

        /// <summary>
        /// Queries the current state and mode of load correction, if enabled.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryLoadMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CORR:LOAD:MODE?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Activates the load correction function.
        /// </summary>
        /// <returns></returns>
        public async Task SetCorrectionLoadState(LoadState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:LOAD:STAT {state}");
        }

        /// <summary>
        /// Queries the current state and mode of open correction, if enabled.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryCorrectionOpenMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CORR:OPEN:MODE?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Activates the open correction function.
        /// </summary>
        /// <returns></returns>
        public async Task SetCorrectionOpenState(OpenState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:OPEN:STAT {state}");
        }

        /// <summary>
        /// Executes an open correction on all frequencies.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteOpenCorrection()
        {

            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand("CORR:OPEN:EXEC");
        }

        /// <summary>
        /// Queries the current state and mode of short correction, if enabled.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryCorrectionShortMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"CORR:SHOR:MODE?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Activates the short correction function.
        /// </summary>
        /// <returns></returns>
        public async Task SetCorrectionShortState(ShortState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:SHOR:STAT {state}");
        }

        /// <summary>
        /// Executes a short correction on all frequencies.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteShortCorrection()
        {

            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand("CORR:SHOR:EXEC");
        }

        /// <summary>
        /// Defines a working point for load correction.
        /// </summary>
        /// <returns></returns>
        public async Task SetWorkingPointLoadCorrection(int spot, double primary, double secondary)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:SPOT{spot}:LOAD:STAN {primary}, {secondary}");
        }

        /// <summary>
        /// Executes a load correction at a dedicated working point.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteLoadCorrectionWorkingPoint(int spot)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:SPOT{spot}:LOAD:EXEC");
        }

        /// <summary>
        /// Executes an open correction at a dedicated working point.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteOpenCorrectionWorkingPoint(int spot)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:SPOT{spot}:OPEN:EXEC");
        }

        /// <summary>
        /// Executes a short correction at a dedicated working point.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteShortCorrectionWorkingPoint(int spot)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"CORR:SPOT{spot}:SHOR:EXEC");
        }
    }
}