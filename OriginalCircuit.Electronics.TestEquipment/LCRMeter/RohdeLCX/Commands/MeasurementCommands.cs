using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Represents a response from the LCX with a pair of values.
    /// </summary>
    public class ValuePairResponse
    {
        /// <summary>
        /// Gets or sets the first value in the pair.
        /// </summary>
        /// <value>The first value in the pair.</value>
        public double? FirstValue { get; set; }

        /// <summary>
        /// Gets or sets the second value in the pair.
        /// </summary>
        /// <value>The second value in the pair.</value>
        public double? SecondValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuePairResponse"/> class.
        /// </summary>
        public ValuePairResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuePairResponse"/> class with the specified response string.
        /// </summary>
        /// <param name="response">The response string.</param>
        public ValuePairResponse(string response)
        {
            if (!string.IsNullOrWhiteSpace(response))
            {
                var parts = response.Split(',');

                if (parts.Length == 2)
                {
                    double output = 0;
                    if (double.TryParse(parts[0], out output))
                        FirstValue = output;

                    if (double.TryParse(parts[1], out output))
                        SecondValue = output;
                }
            }
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="ValuePairResponse"/> object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{nameof(FirstValue)}: {FirstValue}, {nameof(SecondValue)}: {SecondValue}";
        }
    }


    /// <summary>
    /// Selects whether the LCX starts and continues a measurement, or starts on initiated trigger events.
    /// </summary>
    public enum MeasurementMode
    {
        /// <summary>
        /// Restarts the measurement automatically after a measurement cycle has been completed
        /// </summary>
        CONTinuous,
        /// <summary>
        /// Starts a measurement cycle initiated by a trigger signal.
        /// </summary>
        TRIGgered
    }

    /// <summary>
    /// Represents a collection of commands for performing measurements with the test equipment.
    /// </summary>
    public class MeasurementCommands
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the MeasurementCommands class with the specified equipment.
        /// </summary>
        /// <param name="equipment">The network test instrument to communicate with.</param>
        public MeasurementCommands(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Queries the most recent valid values for measurement pair 2.
        /// If no valid measurement values are available, the response reports error code -230.
        /// </summary>
        /// <returns></returns>
        public async Task<ValuePairResponse> LastValid()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FETC?");
            return new ValuePairResponse(await equipment.ReadString());
        }

        /// <summary>
        /// Queries the most recent valid values of the measured impedance.
        /// </summary>
        /// /// <remarks>Measurement pair 2 MUST be set via SCPI not front panel.</remarks>
        /// <returns>If no valid measurement values are available, the response reports error code -230.</returns>
        public async Task<ValuePairResponse> LastValidImpedance()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FETC:IMP?");
            return new ValuePairResponse(await equipment.ReadString());
        }

        /// <summary>
        /// Starts a new measurement.
        /// </summary>
        /// <returns></returns>
        public async Task StartNew()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"INIT:IMM");
        }


        /// <summary>
        /// Queries the accuracy of the last measurement.
        /// </summary>
        /// <returns></returns>
        public async Task<ValuePairResponse> Accuracy()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:ACC?");
            return new ValuePairResponse(await equipment.ReadString());
        }


        /// <summary>
        /// Queries the current value following next in the measurement.
        /// </summary>
        /// <returns></returns>
        public async Task<double> MeasureCurrent()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:CURR?");

            var resp = await equipment.ReadDouble();
            return resp != double.NaN ? resp : 0;
        }


        /// <summary>
        /// Selects whether the RnS LCX starts and continues a measurement, or starts on initiated trigger events.
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementMode(MeasurementMode mode)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:MODE {mode}");
        }

        /// <summary>
        /// Queries whether the RnS LCX starts and continues a measurement, or starts on initiated trigger events.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasurementMode> QueryMeasurementMode()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:MODE?");
            return Enum.Parse<MeasurementMode>(await equipment.ReadString());
        }


        /// <summary>
        /// Sets a delay time that elapses after a trigger event before the measurement starts.
        /// </summary>
        /// <returns></returns>
        public async Task SetTriggerDelay(double delay)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"MEAS:TRIG:DEL {delay}");
        }


        /// <summary>
        /// Queries the voltage value following next in the measurement.
        /// </summary>
        /// <returns></returns>
        public async Task<double> QueryMeasurementVoltage()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"MEAS:VOLT?");

            var resp = await equipment.ReadDouble();
            return resp != double.NaN ? resp : 0;
        }


        /// <summary>
        /// Queries the measurement results for measurement pair 2.
        /// </summary>
        /// <remarks>Measurement pair 2 MUST be set via SCPI not front panel.</remarks>
        /// <returns></returns>
        public async Task<ValuePairResponse> Read()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"READ?");
            return new ValuePairResponse(await equipment.ReadString());
        }

        /// <summary>
        /// Queries the impedance measurement results.
        /// </summary>
        /// <returns></returns>
        public async Task<ValuePairResponse> Impedance()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"READ:IMP?");
            return new ValuePairResponse(await equipment.ReadString());
        }
    }
}