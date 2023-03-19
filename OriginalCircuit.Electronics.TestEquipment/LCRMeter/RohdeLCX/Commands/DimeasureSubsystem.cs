using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Enum for setting the interval type of measurement.
    /// </summary>
    public enum IntervalType
    {
        /// <summary>
        /// Measures in defined step sizes within the sweep range, set with DIMeasure:INTerval:STEPsize.
        /// </summary>
        STEP,
        /// <summary>
        /// Measures in increments calculated by the number of sweep points (DIMeasure:INTerval:POINts on page 191) within the sweep range.
        /// </summary>
        POIN,
    }
    /// <summary>
    /// Enum for setting the sweep parameter.
    /// </summary>
    public enum SweepParameter
    {
        /// <summary>
        /// Sweeps the test signal voltage.
        /// </summary>
        VOLT,
        /// <summary>
        /// Sweeps the test signal frequency.
        /// </summary>
        FREQ,
        /// <summary>
        /// Sweeps the voltage bias.
        /// </summary>
        VBI,
        /// <summary>
        /// Sweeps the current bias.
        /// </summary>
        IBI,
    }

    /// <summary>
    /// Class for performing dynamic impedance measurement using the LXI/SCPI instrument.
    /// </summary>
    public class DynamicImpedanceSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicImpedanceSubsystem"/> class with the specified instrument.
        /// </summary>
        /// <param name="equipment">The LXI/SCPI instrument for performing dynamic impedance measurement.</param>
        public DynamicImpedanceSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Stops a running dynamic impedance measurement.
        /// </summary>
        /// <returns></returns>
        public async Task StopDynamicImpedanceMeasurement()
        {

            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand("DIM:ABOR");
        }

        /// <summary>
        /// Starts the dynamic impedance measurement with the selected parameters.
        /// </summary>
        /// <returns></returns>
        public async Task StartDynamicImpedanceMeasurement()
        {

            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand("DIM:EXEC");
        }

        /// <summary>
        /// Sets the number of measurement points within the measurement range for interval type.
        /// DIMeasure:INTerval:TYPE > Number of Points.
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementPoints(double points)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DIM:INT:POIN {points}");
        }

        /// <summary>
        /// Sets the step size within the measurement range for interval type
        ///  DIMeasure:INTerval:TYPE > Step Size.
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementStepSize(double stepsize)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DIM:INT:STEP {stepsize}");
        }

        /// <summary>
        /// Selects the mode to determine the measurement steps within the sweep range
        /// (DIMeasure:SWEep:MINimum to DIMeasure:SWEep:MAXimum).
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementMode(IntervalType type)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DIM:INT:TYPE {type}");
        }

        /// <summary>
        /// Queries the mode to determine the measurement steps within the sweep range
        /// </summary>
        /// <returns></returns>
        public async Task<IntervalType> QueryMeasurementMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DIM:INT:TYPE?");
            return Enum.Parse<IntervalType>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the stop value for the selected sweep parameter.
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementStopValue(double value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DIM:SWE:MAX {value}");
        }

        /// <summary>
        /// Sets the start value for the selected sweep parameter.
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementStartValue(double value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DIM:SWE:MIN {value}");
        }

        /// <summary>
        /// Selects the measurement parameter that varies in defined steps within the sweep range
        /// (DIMeasure:SWEep:MINimum to DIMeasure:SWEep:MAXimum).
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementSweepParameter(SweepParameter parameter)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"DIM:SWE:PAR {parameter}");
        }

        /// <summary>
        /// Queries the measurement parameter that varies in defined steps within the sweep range
        /// (DIMeasure:SWEep:MINimum to DIMeasure:SWEep:MAXimum).
        /// </summary>
        /// <returns></returns>
        public async Task<SweepParameter> QueryMeasurementSweepParameter()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"DIM:SWE:PAR?");
            return Enum.Parse<SweepParameter>(await equipment.ReadString());
        }
    }
}