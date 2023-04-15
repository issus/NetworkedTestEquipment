using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.SiglentSDGInstrument;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents a sweep wave command for a Siglent SDG series function generator.
    /// </summary>
    public class SweepWaveCommand
    {
        /// <summary>
        /// The network test instrument used to send the command.
        /// </summary>
        public NetworkTestInstrument? equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="SweepWaveCommand"/> class with the specified <paramref name="equipment"/>.
        /// </summary>
        /// <param name="equipment">The network test instrument used to send the command.</param>
        public SweepWaveCommand(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets a sweep wave parameter for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep wave parameter for.</param>
        /// <param name="parameter">The sweep wave parameter to set.</param>
        /// <param name="value">The value of the sweep wave parameter.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetParameter(SiglentSdgChannel channel, string parameter, string value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:SweepWaVe {parameter},{value}");
        }

        /// <summary>
        /// Queries the sweep wave parameters for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the sweep wave parameters for.</param>
        /// <returns>A string containing the sweep wave parameters for the specified channel.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<string> QuerySweepWaveParameters(SiglentSdgChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"{channel}:SWeepWaVe?");

            return await equipment.ReadString();
        }

        // Sweep wave state
        /// <summary>
        /// Sets the sweep state for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep state for.</param>
        /// <param name="state">The desired sweep state (ON or OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetState(SiglentSdgChannel channel, bool state)
        {
            await SetParameter(channel, "STATE", state ? "ON" : "OFF");
        }

        // Sweep wave time
        /// <summary>
        /// Sets the sweep time for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep time for.</param>
        /// <param name="time">The desired sweep time in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetTime(SiglentSdgChannel channel, double time)
        {
            await SetParameter(channel, "TIME", $"{time}");
        }

        // Sweep wave start frequency
        /// <summary>
        /// Sets the sweep start frequency for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep start frequency for.</param>
        /// <param name="startFrequency">The desired start frequency in Hertz.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetStartFrequency(SiglentSdgChannel channel, double startFrequency)
        {
            await SetParameter(channel, "START", $"{startFrequency}");
        }

        // Sweep wave stop frequency
        /// <summary>
        /// Sets the sweep stop frequency for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep stop frequency for.</param>
        /// <param name="stopFrequency">The desired stop frequency in Hertz.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetStopFrequency(SiglentSdgChannel channel, double stopFrequency)
        {
            await SetParameter(channel, "STOP", $"{stopFrequency}");
        }

        // Sweep wave mode
        /// <summary>
        /// Sets the sweep mode for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep mode for.</param>
        /// <param name="sweepMode">The desired sweep mode (LINE or LOG).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetMode(SiglentSdgChannel channel, SweepMode sweepMode)
        {
            await SetParameter(channel, "SWMD", sweepMode.ToString());
        }

        // Sweep wave direction
        /// <summary>
        /// Sets the sweep direction for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep direction for.</param>
        /// <param name="sweepDirection">The desired sweep direction (UP or DOWN).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetDirection(SiglentSdgChannel channel, SweepDirection sweepDirection)
        {
            await SetParameter(channel, "DIR", sweepDirection.ToString());
        }

        // Sweep wave trigger source
        /// <summary>
        /// Sets the sweep trigger source for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep trigger source for.</param>
        /// <param name="triggerSource">The desired trigger source (EXT, INT, or MAN).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetTriggerSource(SiglentSdgChannel channel, TriggerSource triggerSource)
        {
            await SetParameter(channel, "TRSR", triggerSource.ToString());
        }

        // Sweep wave manual trigger
        /// <summary>
        /// Sends a manual trigger for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to send a manual trigger for.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SendSweepWaveManualTrigger(SiglentSdgChannel channel)
        {
            await SetParameter(channel, "MTRIG", string.Empty);
        }

        // Sweep wave trigger mode
        /// <summary>
        /// Sets the sweep trigger mode for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep trigger mode for.</param>
        /// <param name="triggerMode">The desired trigger mode (ON or OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetTriggerMode(SiglentSdgChannel channel, bool triggerMode)
        {
            await SetParameter(channel, "TRMD", triggerMode ? "ON" : "OFF");
        }

        // Sweep wave trigger edge
        /// <summary>
        /// Sets the sweep trigger edge for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep trigger edge for.</param>
        /// <param name="triggerEdge">The desired trigger edge (RISE or FALL).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetTriggerEdge(SiglentSdgChannel channel, TriggerEdge triggerEdge)
        {
            await SetParameter(channel, "EDGE", triggerEdge.ToString());
        }

        // Sweep wave carrier waveform type
        /// <summary>
        /// Sets the sweep carrier waveform type for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep carrier waveform type for.</param>
        /// <param name="waveType">The desired carrier waveform type (SINE, SQUARE, RAMP, or ARB).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCarrierWaveType(SiglentSdgChannel channel, CarrierWaveformType waveType)
        {
            await SetParameter(channel, "CARR,WVTP", waveType.ToString());
        }

        // Sweep wave carrier frequency
        /// <summary>
        /// Sets the sweep carrier frequency for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep carrier frequency for.</param>
        /// <param name="frequency">The desired carrier frequency in Hertz.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCarrierFrequency(SiglentSdgChannel channel, double frequency)
        {
            await SetParameter(channel, "CARR,FRQ", $"{frequency}");
        }

        // Sweep wave carrier phase
        /// <summary>
        /// Sets the sweep carrier phase for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep carrier phase for.</param>
        /// <param name="phase">The desired carrier phase in degrees.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCarrierPhase(SiglentSdgChannel channel, double phase)
        {
            await SetParameter(channel, "CARR,PHSE", $"{phase}");
        }

        // Sweep wave carrier amplitude
        /// <summary>
        /// Sets the sweep carrier amplitude for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep carrier amplitude for.</param>
        /// <param name="amplitude">The desired carrier amplitude in volts peak-to-peak (Vpp).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCarrierAmplitude(SiglentSdgChannel channel, double amplitude)
        {
            await SetParameter(channel, "CARR,AMP", $"{amplitude}");
        }

        // Sweep wave carrier offset
        /// <summary>
        /// Sets the sweep carrier offset for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the sweep carrier offset for.</param>
        /// <param name="offset">The desired carrier offset in volts (V).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCarrierOffset(SiglentSdgChannel channel, double offset)
        {
            await SetParameter(channel, "CARR,OFST", $"{offset}");
        }

        // Sweep wave carrier symmetry
        /// <summary>
        /// Sets the sweep carrier symmetry for the specified channel when the carrier is RAMP.
        /// </summary>
        /// <param name="channel">The channel to set the sweep carrier symmetry for.</param>
        /// <param name="symmetry">The desired carrier symmetry in percentage (%).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCarrierSymmetry(SiglentSdgChannel channel, double symmetry)
        {
            await SetParameter(channel, "CARR,SYM", $"{symmetry}");
        }

        // Sweep wave carrier duty cycle
        /// <summary>
        /// Sets the sweep carrier duty cycle for the specified channel when the carrier is SQUARE.
        /// </summary>
        /// <param name="channel">The channel to set the sweep carrier duty cycle for.</param>
        /// <param name="duty">The desired carrier duty cycle in percentage (%).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetCarrierDutyCycle(SiglentSdgChannel channel, double duty)
        {
            await SetParameter(channel, "CARR,DUTY", $"{duty}");
        }


        /// <summary>
        /// Represents the modulation type.
        /// </summary>
        public enum ModulationType
        {
            /// <summary>
            /// Amplitude Modulation.
            /// </summary>
            AM,

            /// <summary>
            /// Double-Sideband Amplitude Modulation.
            /// </summary>
            DSBAM,

            /// <summary>
            /// Frequency Modulation.
            /// </summary>
            FM,

            /// <summary>
            /// Phase Modulation.
            /// </summary>
            PM,

            /// <summary>
            /// Pulse Width Modulation.
            /// </summary>
            PWM,

            /// <summary>
            /// Amplitude-Shift Keying.
            /// </summary>
            ASK,

            /// <summary>
            /// Frequency-Shift Keying.
            /// </summary>
            FSK,

            /// <summary>
            /// Phase-Shift Keying.
            /// </summary>
            PSK
        }

        /// <summary>
        /// Represents the signal source.
        /// </summary>
        public enum SignalSource
        {
            /// <summary>
            /// Internal signal source.
            /// </summary>
            INT,

            /// <summary>
            /// External signal source.
            /// </summary>
            EXT
        }

        /// <summary>
        /// Represents the sweep mode of the Siglent SDG series function generator.
        /// </summary>
        public enum SweepMode
        {
            /// <summary>
            /// Linear sweep mode.
            /// </summary>
            LINE,
            /// <summary>
            /// Logarithmic sweep mode.
            /// </summary>
            LOG
        }

        /// <summary>
        /// Represents the sweep direction of the Siglent SDG series function generator.
        /// </summary>
        public enum SweepDirection
        {
            /// <summary>
            /// Sweep upward.
            /// </summary>
            UP,
            /// <summary>
            /// Sweep downward.
            /// </summary>
            DOWN
        }

        /// <summary>
        /// Represents the trigger source of the Siglent SDG series function generator.
        /// </summary>
        public enum TriggerSource
        {
            /// <summary>
            /// External trigger source.
            /// </summary>
            EXT,
            /// <summary>
            /// Internal trigger source.
            /// </summary>
            INT,
            /// <summary>
            /// Manual trigger source.
            /// </summary>
            MAN
        }

        /// <summary>
        /// Represents the trigger edge of the Siglent SDG series function generator.
        /// </summary>
        public enum TriggerEdge
        {
            /// <summary>
            /// Rising edge trigger.
            /// </summary>
            RISE,
            /// <summary>
            /// Falling edge trigger.
            /// </summary>
            FALL
        }


        /// <summary>
        /// Represents the modulation wave shape.
        /// </summary>
        public enum ModulationWaveShape
        {
            /// <summary>
            /// Sine wave modulation.
            /// </summary>
            SINE,

            /// <summary>
            /// Square wave modulation.
            /// </summary>
            SQUARE,

            /// <summary>
            /// Triangle wave modulation.
            /// </summary>
            TRIANGLE,

            /// <summary>
            /// Upward ramp wave modulation.
            /// </summary>
            UPRAMP,

            /// <summary>
            /// Downward ramp wave modulation.
            /// </summary>
            DNRAMP,

            /// <summary>
            /// Noise wave modulation.
            /// </summary>
            NOISE,

            /// <summary>
            /// Arbitrary wave modulation.
            /// </summary>
            ARB
        }

        /// <summary>
        /// Represents the carrier waveform type.
        /// </summary>
        public enum CarrierWaveformType
        {
            /// <summary>
            /// Sine wave carrier.
            /// </summary>
            SINE,

            /// <summary>
            /// Square wave carrier.
            /// </summary>
            SQUARE,

            /// <summary>
            /// Ramp wave carrier.
            /// </summary>
            RAMP,

            /// <summary>
            /// Arbitrary wave carrier.
            /// </summary>
            ARB,

            /// <summary>
            /// Pulse wave carrier.
            /// </summary>
            PULSE
        }

    }
}
