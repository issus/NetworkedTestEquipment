using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands.BasicWaveCommand;
using static OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.SiglentSDGInstrument;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents a modulated wave command for a Siglent SDG series function generator.
    /// </summary>
    public class ModulatedWaveCommand
    {
        /// <summary>
        /// The network test instrument used for the command.
        /// </summary>
        /// <remarks>
        /// If this property is null, the command will not be executed.
        /// </remarks>
        public NetworkTestInstrument? Equipment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulatedWaveCommand"/> class with the specified equipment.
        /// </summary>
        /// <param name="equipment">The network test instrument to use for the command.</param>
        public ModulatedWaveCommand(NetworkTestInstrument equipment)
        {
            Equipment = equipment;
        }

        /// <summary>
        /// Sets the modulation type for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to set the modulation type.</param>
        /// <param name="type">The modulation type to set.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetModulationType(SiglentSdgChannel channel, ModulationType type)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe {type}");
        }

        /// <summary>
        /// Sets the modulation state for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the modulation state for.</param>
        /// <param name="state">The desired modulation state (ON or OFF).</param>
        public async Task SetModulationState(SiglentSdgChannel channel, bool state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe STATE,{(state ? "ON" : "OFF")}");
        }

        /// <summary>
        /// Sets the modulation source for the specified channel and modulation type.
        /// </summary>
        /// <param name="channel">The channel to set the modulation source for.</param>
        /// <param name="type">The modulation type to set the source for.</param>
        /// <param name="source">The desired modulation source (INT or EXT).</param>
        public async Task SetModulationSource(SiglentSdgChannel channel, ModulationType type, ModulationSource source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe {type},SRC,{source}");
        }

        /// <summary>
        /// Sets the modulation wave shape for the specified channel and modulation type.
        /// </summary>
        /// <param name="channel">The channel to set the modulation wave shape for.</param>
        /// <param name="type">The modulation type to set the wave shape for.</param>
        /// <param name="waveShape">The desired modulation wave shape.</param>
        public async Task SetModulationWaveShape(SiglentSdgChannel channel, ModulationType type, ModulationWaveShape waveShape)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe {type},MDSP,{waveShape}");
        }

        /// <summary>
        /// Sets the modulation frequency for the specified channel and modulation type.
        /// </summary>
        /// <param name="channel">The channel to set the modulation frequency for.</param>
        /// <param name="type">The modulation type to set the frequency for.</param>
        /// <param name="frequency">The desired modulation frequency in Hertz.</param>
        public async Task SetModulationFrequency(SiglentSdgChannel channel, ModulationType type, double frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe {type},FRQ,{frequency}");
        }

        /// <summary>
        /// Sets the modulation depth for the specified channel (AM modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the modulation depth for.</param>
        /// <param name="depth">The desired modulation depth (0 to 120).</param>
        public async Task SetModulationDepth(SiglentSdgChannel channel, int depth)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe AM,DEPTH,{depth}");
        }

        /// <summary>
        /// Sets the FM frequency deviation for the specified channel (FM modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the FM frequency deviation for.</param>
        /// <param name="deviation">The desired FM frequency deviation.</param>
        public async Task SetFMFrequencyDeviation(SiglentSdgChannel channel, double deviation)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe FM,DEVI,{deviation}");
        }

        /// <summary>
        /// Sets the PM phase deviation for the specified channel (PM modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the PM phase deviation for.</param>
        /// <param name="deviation">The desired PM phase deviation (0 to 360 degrees).</param>
        public async Task SetPMPhaseDeviation(SiglentSdgChannel channel, int deviation)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe PM,DEVI,{deviation}");
        }

        /// <summary>
        /// Sets the PWM frequency for the specified channel (PWM modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the PWM frequency for.</param>
        /// <param name="frequency">The desired PWM frequency in Hertz.</param>
        public async Task SetPWMFrequency(SiglentSdgChannel channel, double frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe PWM,FRQ,{frequency}");
        }

        /// <summary>
        /// Sets the PWM duty cycle deviation for the specified channel (PWM modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the PWM duty cycle deviation for.</param>
        /// <param name="deviation">The desired PWM duty cycle deviation in percentage.</param>
        public async Task SetPWMDutyCycleDeviation(SiglentSdgChannel channel, double deviation)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe PWM,DEVI,{deviation}");
        }

        /// <summary>
        /// Sets the ASK key frequency for the specified channel (ASK modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the ASK key frequency for.</param>
        /// <param name="frequency">The desired ASK key frequency in Hertz.</param>
        public async Task SetASKKeyFrequency(SiglentSdgChannel channel, double frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe ASK,KFRQ,{frequency}");
        }

        /// <summary>
        /// Sets the FSK key frequency for the specified channel (FSK modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the FSK key frequency for.</param>
        /// <param name="frequency">The desired FSK key frequency in Hertz.</param>
        public async Task SetFSKKeyFrequency(SiglentSdgChannel channel, double frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe FSK,KFRQ,{frequency}");
        }

        /// <summary>
        /// Sets the FSK hop frequency for the specified channel (FSK modulation only).
        /// </summary>
        /// <param name="channel">The channel to set the FSK hop frequency for.</param>
        /// <param name="frequency">The desired FSK hop frequency in Hertz.</param>
        public async Task SetFSKHopFrequency(SiglentSdgChannel channel, double frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe FSK,HFRQ,{frequency}");
        }

        /// <summary>
        /// Sets the carrier waveform type for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the carrier waveform type for.</param>
        /// <param name="waveType">The desired carrier waveform type.</param>
        public async Task SetCarrierWaveType(SiglentSdgChannel channel, WaveType waveType)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,WVTP,{waveType}");
        }

        /// <summary>
        /// Sets the carrier frequency for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the carrier frequency for.</param>
        /// <param name="frequency">The desired carrier frequency in Hertz.</param>
        public async Task SetCarrierFrequency(SiglentSdgChannel channel, double frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,FRQ,{frequency}");
        }

        /// <summary>
        /// Sets the carrier phase for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the carrier phase for.</param>
        /// <param name="phase">The desired carrier phase in degrees (0 to 360).</param>
        public async Task SetCarrierPhase(SiglentSdgChannel channel, int phase)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,PHSE,{phase}");
        }

        /// <summary>
        /// Sets the carrier amplitude for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the carrier amplitude for.</param>
        /// <param name="amplitude">The desired carrier amplitude in volts, peak-to-peak (Vpp).</param>
        public async Task SetCarrierAmplitude(SiglentSdgChannel channel, double amplitude)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,AMP,{amplitude}");
        }

        /// <summary>
        /// Sets the carrier offset for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the carrier offset for.</param>
        /// <param name="offset">The desired carrier offset in volts (V).</param>
        public async Task SetCarrierOffset(SiglentSdgChannel channel, double offset)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,OFST,{offset}");
        }

        /// <summary>
        /// Sets the carrier symmetry for the specified channel (RAMP waveform only).
        /// </summary>
        /// <param name="channel">The channel to set the carrier symmetry for.</param>
        /// <param name="symmetry">The desired carrier symmetry in percentage (0 to 100).</param>
        public async Task SetCarrierSymmetry(SiglentSdgChannel channel, int symmetry)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,SYM,{symmetry}");
        }

        /// <summary>
        /// Sets the carrier duty cycle for the specified channel (SQUARE or PULSE waveform only).
        /// </summary>
        /// <param name="channel">The channel to set the carrier duty cycle for.</param>
        /// <param name="duty">The desired carrier duty cycle in percentage (0 to 100).</param>
        public async Task SetCarrierDutyCycle(SiglentSdgChannel channel, int duty)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,DUTY,{duty}");
        }

        /// <summary>
        /// Sets the carrier rise time for the specified channel (PULSE waveform only).
        /// </summary>
        /// <param name="channel">The channel to set the carrier rise time for.</param>
        /// <param name="riseTime">The desired carrier rise time in seconds.</param>
        public async Task SetCarrierRiseTime(SiglentSdgChannel channel, double riseTime)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,RIST,{riseTime}");
        }

        /// <summary>
        /// Sets the carrier fall time for the specified channel (PULSE waveform only).
        /// </summary>
        /// <param name="channel">The channel to set the carrier fall time for.</param>
        /// <param name="fallTime">The desired carrier fall time in seconds.</param>
        public async Task SetCarrierFallTime(SiglentSdgChannel channel, double fallTime)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,FALT,{fallTime}");
        }

        /// <summary>
        /// Sets the carrier pulse width for the specified channel (PULSE waveform only).
        /// </summary>
        /// <param name="channel">The channel to set the carrier pulse width for.</param>
        /// <param name="pulseWidth">The desired carrier pulse width in seconds.</param>
        public async Task SetCarrierPulseWidth(SiglentSdgChannel channel, double pulseWidth)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,PLWD,{pulseWidth}");
        }

        /// <summary>
        /// Sets the carrier edge time for the specified channel (ARB waveform only).
        /// </summary>
        /// <param name="channel">The channel to set the carrier edge time for.</param>
        /// <param name="edgeTime">The desired carrier edge time in seconds.</param>
        public async Task SetCarrierEdgeTime(SiglentSdgChannel channel, double edgeTime)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:MoDulateWaVe CARR,EDGT,{edgeTime}");
        }



        /// <summary>
        /// Queries the modulation parameters for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which to query the modulation parameters.</param>
        /// <returns>A dictionary containing the modulation parameters and their values.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<Dictionary<string, string>> QueryModulationParameters(SiglentSdgChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            //todo: reimplement this to correctly handle the response.
            await equipment.ClearBuffer();

            await equipment.SendCommand($"{channel}:MoDulateWaVe?");

            var response = await equipment.ReadString();
            var parameterStrings = response.Split(',');

            Dictionary<string, string> modulationParameters = new Dictionary<string, string>();

            for (int i = 0; i < parameterStrings.Length; i += 2)
            {
                modulationParameters.Add(parameterStrings[i], parameterStrings[i + 1]);
            }

            return modulationParameters;
        }




        /// <summary>
        /// Represents modulation types.
        /// </summary>
        public enum ModulationType
        {
            /// <summary>
            /// Amplitude modulation
            /// </summary>
            AM,

            /// <summary>
            /// Double-sideband amplitude modulation
            /// </summary>
            DSBAM,

            /// <summary>
            /// Frequency modulation
            /// </summary>
            FM,

            /// <summary>
            /// Phase modulation
            /// </summary>
            PM,

            /// <summary>
            /// Pulse width modulation
            /// </summary>
            PWM,

            /// <summary>
            /// Amplitude shift keying
            /// </summary>
            ASK,

            /// <summary>
            /// Frequency shift keying
            /// </summary>
            FSK,

            /// <summary>
            /// Phase shift keying
            /// </summary>
            PSK
        }

        /// <summary>
        /// Represents modulation signal sources.
        /// </summary>
        public enum ModulationSource
        {
            /// <summary>
            /// Internal modulation source
            /// </summary>
            INT,

            /// <summary>
            /// External modulation source
            /// </summary>
            EXT
        }

        /// <summary>
        /// Represents modulation wave shapes.
        /// </summary>
        public enum ModulationWaveShape
        {
            /// <summary>
            /// Sine wave modulation
            /// </summary>
            SINE,

            /// <summary>
            /// Square wave modulation
            /// </summary>
            SQUARE,

            /// <summary>
            /// Triangle wave modulation
            /// </summary>
            TRIANGLE,

            /// <summary>
            /// Upward ramp modulation
            /// </summary>
            UPRAMP,

            /// <summary>
            /// Downward ramp modulation
            /// </summary>
            DNRAMP,

            /// <summary>
            /// Noise modulation
            /// </summary>
            NOISE,

            /// <summary>
            /// Arbitrary waveform modulation
            /// </summary>
            ARB
        }

        /// <summary>
        /// Modulation state for the Siglent SDG series function generator.
        /// </summary>
        public enum ModulationState
        {
            /// <summary>
            /// Modulation is on.
            /// </summary>
            ON,

            /// <summary>
            /// Modulation is off.
            /// </summary>
            OFF
        }

        /// <summary>
        /// Carrier wave types for the Siglent SDG series function generator.
        /// </summary>
        public enum CarrierWaveType
        {
            /// <summary>
            /// Sine wave.
            /// </summary>
            SINE,

            /// <summary>
            /// Square wave.
            /// </summary>
            SQUARE,

            /// <summary>
            /// Ramp wave.
            /// </summary>
            RAMP,

            /// <summary>
            /// Arbitrary wave.
            /// </summary>
            ARB,

            /// <summary>
            /// Pulse wave.
            /// </summary>
            PULSE
        }

        /// <summary>
        /// Represents modulation parameters for a Siglent SDG series function generator.
        /// </summary>
        public enum ModulationParameter
        {
            /// <summary>
            /// The state of modulation.
            /// </summary>
            STATE,

            /// <summary>
            /// The source of amplitude modulation (AM).
            /// </summary>
            AM_SRC,

            /// <summary>
            /// The modulation depth scaling of amplitude modulation (AM).
            /// </summary>
            AM_MDSP,

            /// <summary>
            /// The modulation frequency of amplitude modulation (AM).
            /// </summary>
            AM_FRQ,

            /// <summary>
            /// The depth of amplitude modulation (AM).
            /// </summary>
            AM_DEPTH,

            /// <summary>
            /// The source of double-sideband amplitude modulation (DSBAM).
            /// </summary>
            DSBAM_SRC,

            /// <summary>
            /// The modulation depth scaling of double-sideband amplitude modulation (DSBAM).
            /// </summary>
            DSBAM_MDSP,

            /// <summary>
            /// The modulation frequency of double-sideband amplitude modulation (DSBAM).
            /// </summary>
            DSBAM_FRQ,

            /// <summary>
            /// The source of frequency modulation (FM).
            /// </summary>
            FM_SRC,

            /// <summary>
            /// The modulation depth scaling of frequency modulation (FM).
            /// </summary>
            FM_MDSP,

            /// <summary>
            /// The modulation frequency of frequency modulation (FM).
            /// </summary>
            FM_FRQ,

            /// <summary>
            /// The frequency deviation of frequency modulation (FM).
            /// </summary>
            FM_DEVI,

            /// <summary>
            /// The source of phase modulation (PM).
            /// </summary>
            PM_SRC,

            /// <summary>
            /// The modulation depth scaling of phase modulation (PM).
            /// </summary>
            PM_MDSP,

            /// <summary>
            /// The modulation frequency of phase modulation (PM).
            /// </summary>
            PM_FRQ,

            /// <summary>
            /// The phase deviation of phase modulation (PM).
            /// </summary>
            PM_DEVI,

            /// <summary>
            /// The source of pulse-width modulation (PWM).
            /// </summary>
            PWM_SRC,

            /// <summary>
            /// The modulation frequency of pulse-width modulation (PWM).
            /// </summary>
            PWM_FRQ,

            /// <summary>
            /// The pulse width deviation of pulse-width modulation (PWM).
            /// </summary>
            PWM_DEVI,

            /// <summary>
            /// The modulation depth scaling of pulse-width modulation (PWM).
            /// </summary>
            PWM_MDSP,

            /// <summary>
            /// The source of amplitude-shift keying (ASK).
            /// </summary>
            ASK_SRC,

            /// <summary>
            /// The frequency of the square wave for amplitude-shift keying (ASK).
            /// </summary>
            ASK_KFRQ,

            /// <summary>
            /// The source of frequency-shift keying (FSK).
            /// </summary>
            FSK_SRC,

            /// <summary>
            /// The frequency of the square wave for frequency-shift keying (FSK).
            /// </summary>
            FSK_KFRQ,

            /// <summary>
            /// The high frequency for frequency-shift keying (FSK).
            /// </summary>
            FSK_HFRQ,

            /// <summary>
            /// The source of phase-shift keying (PSK).
            /// </summary>
            PSK_SRC,

            /// <summary>
            /// The frequency of the square wave for phase-shift keying (PSK).
            /// </summary>
            PSK_KFRQ,

            /// <summary>
            /// The waveform type of the carrier.
            /// </summary>
            CARR_WVTP,

            /// <summary>
            /// The frequency of the carrier.
            /// </summary>
            CARR_FRQ,

            /// <summary>
            /// The initial phase of the carrier.
            /// </summary>
            CARR_PHSE,

            /// <summary>
            /// The amplitude of the carrier.
            /// </summary>
            CARR_AMP,

            /// <summary>
            /// The offset of the carrier.
            /// </summary>
            CARR_OFST,

            /// <summary>
            /// The symmetry of the carrier waveform.
            /// </summary>
            CARR_SYM,

            /// <summary>
            /// The duty cycle of the carrier waveform.
            /// </summary>
            CARR_DUTY,

            /// <summary>
            /// The rise time of the carrier waveform.
            /// </summary>
            CARR_RISE,

            /// <summary>
            /// The fall time of the carrier waveform.
            /// </summary>
            CARR_FALL,

            /// <summary>
            /// The delay of the carrier waveform.
            /// </summary>
            CARR_DLY
        }

    }
}
