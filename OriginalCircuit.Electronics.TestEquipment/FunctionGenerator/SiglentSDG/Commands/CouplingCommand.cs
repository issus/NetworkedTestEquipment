using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents a coupling command for a Siglent SDG series function generator.
    /// </summary>
    public class CouplingCommand
    {
        /// <summary>
        /// The network test instrument that this coupling command is for.
        /// </summary>
        NetworkTestInstrument? equipment;

        /// <summary>
        /// Creates a new coupling command for the specified network test instrument.
        /// </summary>
        /// <param name="instrument">The network test instrument that this coupling command is for.</param>
        public CouplingCommand(NetworkTestInstrument instrument)
        {
            equipment = instrument;
        }

        /// <summary>
        /// Sets the coupling parameter value.
        /// </summary>
        /// <param name="parameter">The coupling parameter to set.</param>
        /// <param name="value">The value to set for the coupling parameter.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetCouplingParameter(CouplingParameter parameter, object value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"COUP {parameter},{value}");
        }

        /// <summary>
        /// Queries the coupling parameters of the test equipment.
        /// </summary>
        /// <returns>A dictionary of coupling parameters and their respective values.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<Dictionary<CouplingParameter, object?>> QueryCouplingParameters()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand("COUP?");

            var response = await equipment.ReadString();
            var parameterValues = response.Split(',');

            Dictionary<CouplingParameter, object?> couplingParameters = new Dictionary<CouplingParameter, object>();

            for (int i = 0; i < parameterValues.Length; i += 2)
            {
                var parameter = (CouplingParameter)Enum.Parse(typeof(CouplingParameter), parameterValues[i]);
                object? value = null;

                switch (parameter)
                {
                    case CouplingParameter.TRACE:
                    case CouplingParameter.STATE:
                    case CouplingParameter.FCOUP:
                    case CouplingParameter.PCOUP:
                    case CouplingParameter.ACOUP:
                        value = bool.Parse(parameterValues[i + 1]);
                        break;

                    case CouplingParameter.BSCH:
                        value = Enum.Parse<BaseChannel>(parameterValues[i + 1]);
                        break;

                    case CouplingParameter.FDEV:
                    case CouplingParameter.PDEV:
                    case CouplingParameter.ADEV:
                        value = double.Parse(parameterValues[i + 1]);
                        break;

                    case CouplingParameter.FRAT:
                    case CouplingParameter.PRAT:
                    case CouplingParameter.ARAT:
                        value = int.Parse(parameterValues[i + 1]);
                        break;
                }

                couplingParameters.Add(parameter, value);
            }

            return couplingParameters;
        }

        /// <summary>
        /// Sets the trace state of the coupling.
        /// </summary>
        /// <param name="state">The desired trace state (ON/OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetTraceState(bool state)
        {
            await SetCouplingParameter(CouplingParameter.TRACE, state ? "ON" : "OFF");
        }

        /// <summary>
        /// Sets the coupling state.
        /// </summary>
        /// <param name="state">The desired coupling state (ON/OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCouplingState(bool state)
        {
            await SetCouplingParameter(CouplingParameter.STATE, state ? "ON" : "OFF");
        }

        /// <summary>
        /// Sets the base channel for coupling.
        /// </summary>
        /// <param name="channel">The desired base channel (CH1/CH2).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetBaseChannel(BaseChannel channel)
        {
            await SetCouplingParameter(CouplingParameter.BSCH, channel);
        }

        /// <summary>
        /// Sets the frequency coupling state.
        /// </summary>
        /// <param name="state">The desired frequency coupling state (ON/OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetFrequencyCouplingState(bool state)
        {
            await SetCouplingParameter(CouplingParameter.FCOUP, state ? "ON" : "OFF");
        }

        /// <summary>
        /// Sets the frequency deviation for coupling.
        /// </summary>
        /// <param name="frequencyDeviation">The desired frequency deviation in Hz.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetFrequencyDeviation(double frequencyDeviation)
        {
            await SetCouplingParameter(CouplingParameter.FDEV, frequencyDeviation);
        }

        /// <summary>
        /// Sets the frequency ratio for coupling.
        /// </summary>
        /// <param name="frequencyRatio">The desired frequency ratio.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetFrequencyRatio(int frequencyRatio)
        {
            await SetCouplingParameter(CouplingParameter.FRAT, frequencyRatio);
        }

        /// <summary>
        /// Sets the phase coupling state.
        /// </summary>
        /// <param name="state">The desired phase coupling state (ON/OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPhaseCouplingState(bool state)
        {
            await SetCouplingParameter(CouplingParameter.PCOUP, state ? "ON" : "OFF");
        }

        /// <summary>
        /// Sets the phase deviation for coupling.
        /// </summary>
        /// <param name="phaseDeviation">The desired phase deviation in degrees.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPhaseDeviation(double phaseDeviation)
        {
            await SetCouplingParameter(CouplingParameter.PDEV, phaseDeviation);
        }

        /// <summary>
        /// Sets the phase ratio for coupling.
        /// </summary>
        /// <param name="phaseRatio">The desired phase ratio.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPhaseRatio(int phaseRatio)
        {
            await SetCouplingParameter(CouplingParameter.PRAT, phaseRatio);
        }

        /// <summary>
        /// Sets the amplitude coupling state.
        /// </summary>
        /// <param name="state">The desired amplitude coupling state (ON/OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetAmplitudeCouplingState(bool state)
        {
            await SetCouplingParameter(CouplingParameter.ACOUP, state ? "ON" : "OFF");
        }

        /// <summary>
        /// Sets the amplitude ratio for coupling.
        /// </summary>
        /// <param name="amplitudeRatio">The desired amplitude ratio.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetAmplitudeRatio(int amplitudeRatio)
        {
            await SetCouplingParameter(CouplingParameter.ARAT, amplitudeRatio);
        }

        /// <summary>
        /// Sets the amplitude deviation for coupling.
        /// </summary>
        /// <param name="amplitudeDeviation">The desired amplitude deviation in Vpp (Volts peak-to-peak).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetAmplitudeDeviation(double amplitudeDeviation)
        {
            await SetCouplingParameter(CouplingParameter.ADEV, amplitudeDeviation);
        }

        /// <summary>
        /// Enumerates the possible coupling parameters.
        /// </summary>
        public enum CouplingParameter
        {
            /// <summary>
            /// Specifies the trace coupling parameter.
            /// </summary>
            TRACE,

            /// <summary>
            /// Specifies the state coupling parameter.
            /// </summary>
            STATE,

            /// <summary>
            /// Specifies the BSCH coupling parameter.
            /// </summary>
            BSCH,

            /// <summary>
            /// Specifies the frequency coupling parameter.
            /// </summary>
            FCOUP,

            /// <summary>
            /// Specifies the frequency deviation coupling parameter.
            /// </summary>
            FDEV,

            /// <summary>
            /// Specifies the frequency ratio coupling parameter.
            /// </summary>
            FRAT,

            /// <summary>
            /// Specifies the phase coupling parameter.
            /// </summary>
            PCOUP,

            /// <summary>
            /// Specifies the phase deviation coupling parameter.
            /// </summary>
            PDEV,

            /// <summary>
            /// Specifies the phase ratio coupling parameter.
            /// </summary>
            PRAT,

            /// <summary>
            /// Specifies the amplitude coupling parameter.
            /// </summary>
            ACOUP,

            /// <summary>
            /// Specifies the amplitude ratio coupling parameter.
            /// </summary>
            ARAT,

            /// <summary>
            /// Specifies the amplitude deviation coupling parameter.
            /// </summary>
            ADEV
        }

        /// <summary>
        /// Enumerates the possible base channels.
        /// </summary>
        public enum BaseChannel
        {
            /// <summary>
            /// Specifies base channel 1.
            /// </summary>
            CH1,

            /// <summary>
            /// Specifies base channel 2.
            /// </summary>
            CH2
        }

    }
}
