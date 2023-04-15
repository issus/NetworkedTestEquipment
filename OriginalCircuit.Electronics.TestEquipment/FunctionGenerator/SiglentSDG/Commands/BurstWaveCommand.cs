using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.SiglentSDGInstrument;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents a burst wave command for a Siglent SDG series function generator.
    /// </summary>
    public class BurstWaveCommand
    {
        /// <summary>
        /// The network test instrument that the burst wave command is associated with.
        /// </summary>
        private NetworkTestInstrument? equipment;

        /// <summary>
        /// Creates a new instance of the <see cref="BurstWaveCommand"/> class and sets the equipment field.
        /// </summary>
        /// <param name="equipment">The network test instrument to associate the burst wave command with.</param>
        public BurstWaveCommand(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the burst wave parameters for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the burst wave parameters.</param>
        /// <param name="parameter">The burst wave parameter to set.</param>
        /// <param name="value">The value to set for the specified parameter.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetParameter(SiglentSdgChannel channel, BurstWaveParameter parameter, string value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BursTWaVe {parameter.ToString().Replace('_', ',')},{value}");
        }

        /// <summary>
        /// Queries the burst wave parameters for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to query the burst wave parameters.</param>
        /// <returns>A string containing the burst wave parameters for the specified channel.</returns>
        public async Task<string> QueryBurstWaveParameters(SiglentSdgChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"{channel}:BTWV?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Sets the burst state for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the burst state.</param>
        /// <param name="state">The state to set for the burst.</param>
        public async Task SetBurstState(SiglentSdgChannel channel, BurstState state)
        {
            await SetParameter(channel, BurstWaveParameter.STATE, state.ToString());
        }

        /// <summary>
        /// Sets the burst period for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the burst period.</param>
        /// <param name="period">The period to set for the burst.</param>
        public async Task SetBurstPeriod(SiglentSdgChannel channel, double period)
        {
            await SetParameter(channel, BurstWaveParameter.PRD, $"{period}S");
        }

        /// <summary>
        /// Sets the start phase for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the start phase.</param>
        /// <param name="startPhase">The start phase to set for the burst.</param>
        public async Task SetBurstStartPhase(SiglentSdgChannel channel, double startPhase)
        {
            await SetParameter(channel, BurstWaveParameter.STPS, $"{startPhase}");
        }

        /// <summary>
        /// Sets the burst mode for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the burst mode.</param>
        /// <param name="mode">The burst mode to set.</param>
        public async Task SetBurstMode(SiglentSdgChannel channel, BurstMode mode)
        {
            await SetParameter(channel, BurstWaveParameter.GATE_NCYC, mode.ToString());
        }

        /// <summary>
        /// Sets the trigger source for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the trigger source.</param>
        /// <param name="source">The trigger source to set.</param>
        public async Task SetTriggerSource(SiglentSdgChannel channel, TriggerSource source)
        {
            await SetParameter(channel, BurstWaveParameter.TRSR, source.ToString());
        }

        /// <summary>
        /// Sends a manual trigger for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to send a manual trigger.</param>
        public async Task SendManualTrigger(SiglentSdgChannel channel)
        {
            await SetParameter(channel, BurstWaveParameter.MTRIG, "");
        }

        /// <summary>
        /// Sets the trigger delay for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the trigger delay.</param>
        /// <param name="delay">The trigger delay to set.</param>
        public async Task SetTriggerDelay(SiglentSdgChannel channel, double delay)
        {
            await SetParameter(channel, BurstWaveParameter.DLAY, $"{delay}S");
        }

        /// <summary>
        /// Sets the gate polarity for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the gate polarity.</param>
        /// <param name="polarity">The gate polarity to set.</param>
        public async Task SetGatePolarity(SiglentSdgChannel channel, GatePolarity polarity)
        {
            await SetParameter(channel, BurstWaveParameter.PLRT, polarity.ToString());
        }

        /// <summary>
        /// Sets the trigger output mode for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the trigger output mode.</param>
        /// <param name="mode">The trigger output mode to set.</param>
        public async Task SetTriggerOutMode(SiglentSdgChannel channel, TriggerOutMode mode)
        {
            await SetParameter(channel, BurstWaveParameter.TRMD, mode.ToString());
        }

        /// <summary>
        /// Sets the trigger edge for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the trigger edge.</param>
        /// <param name="edge">The trigger edge to set.</param>
        public async Task SetTriggerEdge(SiglentSdgChannel channel, TriggerEdge edge)
        {
            await SetParameter(channel, BurstWaveParameter.EDGE, edge.ToString());
        }

        /// <summary>
        /// Sets the burst time for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the burst time.</param>
        /// <param name="time">The burst time to set.</param>
        public async Task SetBurstTime(SiglentSdgChannel channel, string time)
        {
            await SetParameter(channel, BurstWaveParameter.TIME, time);
        }

        /// <summary>
        /// Sets the carrier waveform type for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the carrier waveform type.</param>
        /// <param name="type">The carrier waveform type to set.</param>
        public async Task SetCarrierWaveformType(SiglentSdgChannel channel, CarrierWaveformType type)
        {
            await SetParameter(channel, BurstWaveParameter.CARR_WVTP, type.ToString());
        }

        /// <summary>
        /// Sets the carrier frequency for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the carrier frequency.</param>
        /// <param name="frequency">The carrier frequency to set.</param>
        public async Task SetCarrierFrequency(SiglentSdgChannel channel, double frequency)
        {
            await SetParameter(channel, BurstWaveParameter.CARR_FRQ, $"{frequency}HZ");
        }

        /// <summary>
        /// Sets the carrier phase for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the carrier phase.</param>
        /// <param name="phase">The carrier phase to set.</param>
        public async Task SetCarrierPhase(SiglentSdgChannel channel, double phase)
        {
            await SetParameter(channel, BurstWaveParameter.CARR_PHSE, $"{phase}");
        }

        /// <summary>
        /// Sets the carrier amplitude for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the carrier amplitude.</param>
        /// <param name="amplitude">The carrier amplitude to set.</param>
        public async Task SetCarrierAmplitude(SiglentSdgChannel channel, double amplitude)
        {
            await SetParameter(channel, BurstWaveParameter.CARR_AMP, $"{amplitude}V");
        }

        /// <summary>
        /// Sets the carrier offset for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the carrier offset.</param>
        /// <param name="offset">The carrier offset to set.</param>
        public async Task SetCarrierOffset(SiglentSdgChannel channel, double offset)
        {
            await SetParameter(channel, BurstWaveParameter.CARR_OFST, $"{offset}V");
        }


        /// <summary>
        /// Enumeration representing burst wave parameters for Siglent SDG series function generator.
        /// </summary>
        public enum BurstWaveParameter
        {
            /// <summary>
            /// State of burst wave output. 0=OFF, 1=ON.
            /// </summary>
            STATE,
            /// <summary>
            /// Period of the burst wave. Unit: s
            /// </summary>
            PRD,
            /// <summary>
            /// Step of burst wave period setting. Unit: s
            /// </summary>
            STPS,
            /// <summary>
            /// Number of cycles for each burst. Range: 1-50000
            /// </summary>
            GATE_NCYC,
            /// <summary>
            /// The trigger source of burst output.
            /// </summary>
            TRSR,
            /// <summary>
            /// The multi-trigger function of burst wave output. 0=OFF, 1=ON.
            /// </summary>
            MTRIG,
            /// <summary>
            /// Delay time of burst wave output after the trigger signal. Unit: s
            /// </summary>
            DLAY,
            /// <summary>
            /// The polarity of burst wave output.
            /// </summary>
            PLRT,
            /// <summary>
            /// The trigger mode of burst output.
            /// </summary>
            TRMD,
            /// <summary>
            /// The edge of the trigger signal of burst output.
            /// </summary>
            EDGE,
            /// <summary>
            /// The duration of the trigger signal of burst output. Unit: s
            /// </summary>
            TIME,
            /// <summary>
            /// The waveform type of carrier signal in burst wave output.
            /// </summary>
            CARR_WVTP,
            /// <summary>
            /// The frequency of carrier signal in burst wave output. Unit: Hz
            /// </summary>
            CARR_FRQ,
            /// <summary>
            /// The initial phase of carrier signal in burst wave output. Unit: degree
            /// </summary>
            CARR_PHSE,
            /// <summary>
            /// The amplitude of carrier signal in burst wave output. Unit: Vpp
            /// </summary>
            CARR_AMP,
            /// <summary>
            /// The offset voltage of carrier signal in burst wave output. Unit: V
            /// </summary>
            CARR_OFST,
            /// <summary>
            /// The symmetry of carrier signal in burst wave output. Range: 0-100%
            /// </summary>
            CARR_SYM,
            /// <summary>
            /// The duty cycle of carrier signal in burst wave output. Range: 0-100%
            /// </summary>
            CARR_DUTY,
            /// <summary>
            /// The rise time of carrier signal in burst wave output. Unit: s
            /// </summary>
            CARR_RISE,
            /// <summary>
            /// The fall time of carrier signal in burst wave output. Unit: s
            /// </summary>
            CARR_FALL,
            /// <summary>
            /// The delay time of carrier signal in burst wave output after trigger signal. Unit: s
            /// </summary>
            CARR_DLY,
            /// <summary>
            /// The standard deviation of the noise of carrier signal in burst wave output. Unit: Vpp
            /// </summary>
            CARR_STDEV,
            /// <summary>
            /// The mean value of the noise of carrier signal in burst wave output. Unit: Vpp
            /// </summary>
            CARR_MEAN
        }

        /// <summary>
        /// Represents the burst state of a Siglent SDG series function generator.
        /// </summary>
        public enum BurstState
        {
            /// <summary>
            /// The burst function is enabled.
            /// </summary>
            ON,

            /// <summary>
            /// The burst function is disabled.
            /// </summary>
            OFF
        }

        /// <summary>
        /// Represents the burst mode of a Siglent SDG series function generator.
        /// </summary>
        public enum BurstMode
        {
            /// <summary>
            /// The burst function is controlled by a gate signal.
            /// </summary>
            GATE,

            /// <summary>
            /// The burst function is controlled by a number of cycles.
            /// </summary>
            NCYC
        }

        /// <summary>
        /// Represents the trigger source of a Siglent SDG series function generator.
        /// </summary>
        public enum TriggerSource
        {
            /// <summary>
            /// The trigger source is external.
            /// </summary>
            EXT,

            /// <summary>
            /// The trigger source is internal.
            /// </summary>
            INT,

            /// <summary>
            /// The trigger source is manual.
            /// </summary>
            MAN
        }

        /// <summary>
        /// Represents the polarity of the gate signal of a Siglent SDG series function generator.
        /// </summary>
        public enum GatePolarity
        {
            /// <summary>
            /// The gate signal is active low.
            /// </summary>
            NEG,

            /// <summary>
            /// The gate signal is active high.
            /// </summary>
            POS
        }

        /// <summary>
        /// Represents the trigger output mode of a Siglent SDG series function generator.
        /// </summary>
        public enum TriggerOutMode
        {
            /// <summary>
            /// The trigger output is a rising edge.
            /// </summary>
            RISE,

            /// <summary>
            /// The trigger output is a falling edge.
            /// </summary>
            FALL,

            /// <summary>
            /// The trigger output is off.
            /// </summary>
            OFF
        }

        /// <summary>
        /// Represents the edge of the trigger signal of a Siglent SDG series function generator.
        /// </summary>
        public enum TriggerEdge
        {
            /// <summary>
            /// The trigger signal is triggered on the rising edge.
            /// </summary>
            RISE,

            /// <summary>
            /// The trigger signal is triggered on the falling edge.
            /// </summary>
            FALL
        }

        /// <summary>
        /// Represents the carrier waveform type of a Siglent SDG series function generator.
        /// </summary>
        public enum CarrierWaveformType
        {
            /// <summary>
            /// The carrier waveform is a sine wave.
            /// </summary>
            SINE,

            /// <summary>
            /// The carrier waveform is a square wave.
            /// </summary>
            SQUARE,

            /// <summary>
            /// The carrier waveform is a ramp wave.
            /// </summary>
            RAMP,

            /// <summary>
            /// The carrier waveform is an arbitrary waveform.
            /// </summary>
            ARB,

            /// <summary>
            /// The carrier waveform is a pulse wave.
            /// </summary>
            PULSE,

            /// <summary>
            /// The carrier waveform is a noise waveform.
            /// </summary>
            NOISE
        }

    }
}
