using OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG
{
    /// <summary>
    /// Represents a Siglent SDG series function generator instrument.
    /// </summary>
    public class SiglentSDGInstrument : NetworkTestInstrument
    {
        /// <summary>
        /// Gets the arbitrary waveform command for the instrument.
        /// </summary>
        public ArbCommand Arb { get; }

        /// <summary>
        /// Gets the basic waveform command for the instrument.
        /// </summary>
        public BasicWaveCommand BasicWave { get; }

        /// <summary>
        /// Gets the burst waveform command for the instrument.
        /// </summary>
        public BurstWaveCommand BurstWave { get; }

        /// <summary>
        /// Gets the coupling command for the instrument.
        /// </summary>
        public CouplingCommand Coupling { get; }

        /// <summary>
        /// Gets the frequency counter command for the instrument.
        /// </summary>
        public FrequencyCounterCommand FrequencyCounter { get; }

        /// <summary>
        /// Gets the IQ command for the instrument.
        /// </summary>
        public IqCommand IQ { get; }

        /// <summary>
        /// Gets the modulated waveform command for the instrument.
        /// </summary>
        public ModulatedWaveCommand ModulatedWave { get; }

        /// <summary>
        /// Gets the sweep waveform command for the instrument.
        /// </summary>
        public SweepWaveCommand SweepWave { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiglentSDGInstrument"/> class.
        /// </summary>
        public SiglentSDGInstrument() : base(InstrumentType.FunctionGenerator)
        {
            Arb = new ArbCommand(this);
            BasicWave = new BasicWaveCommand(this);
            BurstWave = new BurstWaveCommand(this);
            Coupling = new CouplingCommand(this);
            FrequencyCounter = new FrequencyCounterCommand(this);
            IQ = new IqCommand(this);
            ModulatedWave = new ModulatedWaveCommand(this);
            SweepWave = new SweepWaveCommand(this);
        }


        /// <summary>
        /// Enables or disables the output for the specified channel, and sets the load and polarity.
        /// </summary>
        /// <param name="channel">The channel to configure.</param>
        /// <param name="enabled">Whether the output should be enabled or disabled.</param>
        /// <param name="load">The desired load value in ohms. Use "HZ" for High Z.</param>
        /// <param name="polarity">The desired output polarity.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetOutput(SiglentSdgChannel channel, bool enabled, string load, Polarity polarity)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            string command = $"{channel}:OUTP {(enabled ? "ON" : "OFF")},LOAD,{load},PLRT,{polarity}";
            await SendCommand(command);
        }

        /// <summary>
        /// Queries the output state for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query.</param>
        /// <returns>A tuple containing the output state (enabled/disabled), load value, and polarity.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<(bool Enabled, string Load, Polarity Polarity)> QueryOutput(SiglentSdgChannel channel)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();
            await SendCommand($"{channel}:OUTP?");

            string response = await ReadString();
            var parts = response.Split(',');

            bool enabled = parts[0].Trim().ToUpper() == "ON";
            string load = parts[1].Trim().Substring(5);
            Polarity polarity = Enum.Parse<Polarity>(parts[2].Trim().Substring(5));

            return (enabled, load, polarity);
        }
        
        /// <summary>
        /// Sets the buzzer state of the test equipment.
        /// </summary>
        /// <param name="isOn">The desired buzzer state. True for on, false for off.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetBuzzerState(bool isOn)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            string state = isOn ? "ON" : "OFF";
            await SendCommand($"BUZZ {state}");
        }

        /// <summary>
        /// Queries the buzzer state of the test equipment.
        /// </summary>
        /// <returns>True if the buzzer is on, false otherwise.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<bool> QueryBuzzerState()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();

            await SendCommand($"BUZZ?");
            var response = await ReadString();
            return response.Equals("ON", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sets the clock source of the test equipment.
        /// </summary>
        /// <param name="source">The desired clock source.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetClockSource(ClockSource source)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"ROSCillator {source}");
        }

        /// <summary>
        /// Queries the clock source of the test equipment.
        /// </summary>
        /// <returns>The current clock source.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<ClockSource> QueryClockSource()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();

            await SendCommand($"ROSCillator?");
            return Enum.Parse<ClockSource>(await ReadString());
        }

        /// <summary>
        /// Sets the inverted state of the specified channel on the Siglent SDG series function generator.
        /// </summary>
        /// <param name="channel">The channel to set the inverted state for.</param>
        /// <param name="isInverted">The new inverted state.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SetInverted(SiglentSdgChannel channel, bool isInverted)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            string state = isInverted ? "ON" : "OFF";
            await SendCommand($"{channel}:INVT {state}");
        }

        /// <summary>
        /// Queries the inverted state of the specified channel on the Siglent SDG series function generator.
        /// </summary>
        /// <param name="channel">The channel to query the inverted state for.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains <see langword="true"/> if the channel is inverted, <see langword="false"/> otherwise.</returns>
        public async Task<bool> QueryInverted(SiglentSdgChannel channel)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();

            await SendCommand($"{channel}:INVT?");
            var response = await ReadString();
            return response.Equals("ON", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sets the waveform combining state for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the waveform combining state for.</param>
        /// <param name="isInverted">Whether or not to enable waveform combining inversion.</param>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task SetWaveformCombining(SiglentSdgChannel channel, bool isInverted)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            string state = isInverted ? "ON" : "OFF";
            await SendCommand($"{channel}:CMBN {state}");
        }

        /// <summary>
        /// Queries the waveform combining state for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the waveform combining state for.</param>
        /// <returns>True if waveform combining is enabled, false otherwise.</returns>
        /// <exception cref="Exception">Thrown when the test equipment is not connected.</exception>
        public async Task<bool> QueryWaveformCombining(SiglentSdgChannel channel)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();

            await SendCommand($"{channel}:CMBN?");
            var response = await ReadString();
            return response.Equals("ON", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sets the over voltage protection state of the Siglent SDG series function generator.
        /// </summary>
        /// <param name="isOn">True to turn on over voltage protection, false to turn it off.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task SetOverVoltageProtection(bool isOn)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            string state = isOn ? "ON" : "OFF";
            await SendCommand($"VOLTPRT {state}");
        }

        /// <summary>
        /// Queries the over voltage protection state of the Siglent SDG series function generator.
        /// </summary>
        /// <returns>True if over voltage protection is on, false if it is off.</returns>
        /// <exception cref="Exception">Thrown if the test equipment is not connected.</exception>
        public async Task<bool> QueryOverVoltageProtection()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();

            await SendCommand($"VOLTPRT?");
            var response = await ReadString();
            return response.Equals("ON", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Simulates pressing a key on the front panel of the test equipment.
        /// </summary>
        /// <param name="key">The virtual key to press.</param>
        /// <param name="state">True if the key press is effective, false if it is not.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task PressVirtualKey(SdgVirtualKey key, bool state)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            int stateValue = state ? 1 : 0;
            await SendCommand($"VKEY VALUE,{(int)key},STATE,{stateValue}");
        }

        /// <summary>
        /// Sets the harmonic parameters for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the harmonic parameters for.</param>
        /// <param name="parameters">The harmonic parameters to set.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetHarmonicParameters(SiglentSdgChannel channel, HarmonicParameters parameters)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            string command = $"{channel}:HARM HARMSTATE,{(parameters.State ? "ON" : "OFF")},HARMTYPE,{parameters.Type},HARMORDER,{parameters.Order},{parameters.Unit},{parameters.Value},HARMPHASE,{parameters.Phase}";
            await SendCommand(command);
        }

        /// <summary>
        /// Queries the harmonic parameters for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to query the harmonic parameters for.</param>
        /// <returns>The harmonic parameters for the specified channel.</returns>
        public async Task<HarmonicParameters> QueryHarmonicParameters(SiglentSdgChannel channel)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();
            await SendCommand($"{channel}:HARM?");
            var response = await ReadString();
            var parts = response.Split(',');

            return new HarmonicParameters
            {
                State = parts[1].ToUpper() == "ON",
                Type = Enum.Parse<HarmonicType>(parts[3]),
                Order = int.Parse(parts[5]),
                Unit = Enum.Parse<HarmonicUnit>(parts[6]),
                Value = double.Parse(parts[7].TrimEnd('V', 'd', 'B', 'c')),
                Phase = double.Parse(parts[9].TrimEnd('d', 'e', 'g', 'r', 'e', 'e'))
            };
        }

        /// <summary>
        /// Sets the phase mode of the test equipment.
        /// </summary>
        /// <param name="mode">The desired phase mode.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetPhaseMode(PhaseMode mode)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"MODE {mode}");
        }

        /// <summary>
        /// Queries the phase mode of the test equipment.
        /// </summary>
        /// <returns>The current phase mode.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<PhaseMode> QueryPhaseMode()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await ClearBuffer();

            await SendCommand($"MODE?");
            return Enum.Parse<PhaseMode>(await ReadString());
        }

        /// <summary>
        /// Represents the harmonic parameters for a channel.
        /// </summary>
        public class HarmonicParameters
        {
            /// <summary>
            /// Gets or sets a value indicating whether the harmonic is enabled or not.
            /// </summary>
            public bool State { get; set; }

            /// <summary>
            /// Gets or sets the type of the harmonic.
            /// </summary>
            public HarmonicType Type { get; set; }

            /// <summary>
            /// Gets or sets the order of the harmonic.
            /// </summary>
            public int Order { get; set; }

            /// <summary>
            /// Gets or sets the unit of the harmonic.
            /// </summary>
            public HarmonicUnit Unit { get; set; }

            /// <summary>
            /// Gets or sets the value of the harmonic.
            /// </summary>
            public double Value { get; set; }

            /// <summary>
            /// Gets or sets the phase of the harmonic.
            /// </summary>
            public double Phase { get; set; }
        }

        /// <summary>
        /// Represents the phase mode options.
        /// </summary>
        public enum PhaseMode
        {
            /// <summary>
            /// The phase-locked mode.
            /// </summary>
            PhaseLocked,

            /// <summary>
            /// The independent mode.
            /// </summary>
            Independent
        }

        /// <summary>
        /// Specifies the type of the harmonic.
        /// </summary>
        public enum HarmonicType
        {
            /// <summary>
            /// Specifies an even harmonic.
            /// </summary>
            Even,

            /// <summary>
            /// Specifies an odd harmonic.
            /// </summary>
            Odd,

            /// <summary>
            /// Specifies all harmonics.
            /// </summary>
            All
        }

        /// <summary>
        /// Specifies the unit of the harmonic.
        /// </summary>
        public enum HarmonicUnit
        {
            /// <summary>
            /// Specifies the amplitude of the harmonic.
            /// </summary>
            HARMAMP,

            /// <summary>
            /// Specifies the amplitude in dBc of the harmonic.
            /// </summary>
            HARMDBC
        }

        /// <summary>
        /// Represents the virtual keys for the Siglent SDG series function generator.
        /// </summary>
        public enum SdgVirtualKey
        {
            /// <summary>
            /// Function key 1.
            /// </summary>
            FUNC1 = 28,

            /// <summary>
            /// Number 4 key.
            /// </summary>
            NUMBER_4 = 52,

            /// <summary>
            /// Function key 2.
            /// </summary>
            FUNC2 = 23,

            /// <summary>
            /// Number 5 key.
            /// </summary>
            NUMBER_5 = 53,

            /// <summary>
            /// Function key 3.
            /// </summary>
            FUNC3 = 18,

            /// <summary>
            /// Number 6 key.
            /// </summary>
            NUMBER_6 = 54,

            /// <summary>
            /// Function key 4.
            /// </summary>
            FUNC4 = 13,

            /// <summary>
            /// Number 7 key.
            /// </summary>
            NUMBER_7 = 55,

            /// <summary>
            /// Function key 5.
            /// </summary>
            FUNC5 = 8,

            /// <summary>
            /// Number 8 key.
            /// </summary>
            NUMBER_8 = 56,

            /// <summary>
            /// Function key 6.
            /// </summary>
            FUNC6 = 3,

            /// <summary>
            /// Number 9 key.
            /// </summary>
            NUMBER_9 = 57,

            /// <summary>
            /// Sine wave key.
            /// </summary>
            SINE = 34,

            /// <summary>
            /// Decimal point key.
            /// </summary>
            POINT = 46,

            /// <summary>
            /// Square wave key.
            /// </summary>
            SQUARE = 29,

            /// <summary>
            /// Negative key.
            /// </summary>
            NEGATIVE = 43,

            /// <summary>
            /// Ramp wave key.
            /// </summary>
            RAMP = 24,

            /// <summary>
            /// Left arrow key.
            /// </summary>
            LEFT = 44,

            /// <summary>
            /// Pulse wave key.
            /// </summary>
            PULSE = 19,

            /// <summary>
            /// Right arrow key.
            /// </summary>
            RIGHT = 40,

            /// <summary>
            /// Noise key.
            /// </summary>
            NOISE = 14,

            /// <summary>
            /// Up arrow key.
            /// </summary>
            UP = 45,

            /// <summary>
            /// Arbitrary waveform key.
            /// </summary>
            ARB = 9,

            /// <summary>
            /// Down arrow key.
            /// </summary>
            DOWN = 39,

            /// <summary>
            /// Modulation key.
            /// </summary>
            MOD = 15,

            /// <summary>
            /// Output 1 key.
            /// </summary>
            OUTPUT1 = 153,

            /// <summary>
            /// Sweep key.
            /// </summary>
            SWEEP = 16,

            /// <summary>
            /// Output 2 key.
            /// </summary>
            OUTPUT2 = 152,

            /// <summary>
            /// Burst key.
            /// </summary>
            BURST = 17,

            /// <summary>
            /// Knob right key.
            /// </summary>
            KNOB_RIGHT = 175,

            /// <summary>
            /// Waveform key.
            /// </summary>
            WAVES = 4,

            /// <summary>
            /// Knob left key.
            /// </summary>
            KNOB_LEFT = 177,

            /// <summary>
            /// Utility key.
            /// </summary>
            UTILITY = 11,

            /// <summary>
            /// Knob down key.
            /// </summary>
            KNOB_DOWN = 176,

            /// <summary>
            /// Parameter key.
            /// </summary>
            PARAMETER = 5,

            /// <summary>
            /// Help key.
            /// </summary>
            HELP = 12,

            /// <summary>
            /// Store/Recall key.
            /// </summary>
            STORE_RECALL = 70,

            /// <summary>
            /// Channel key.
            /// </summary>
            CHANNEL = 72,

            /// <summary>
            /// Number 0 key.
            /// </summary>
            NUMBER_0 = 48,

            /// <summary>
            /// Number 1 key.
            /// </summary>
            NUMBER_1 = 49,

            /// <summary>
            /// Number 2 key.
            /// </summary>
            NUMBER_2 = 50,

            /// <summary>
            /// Number 3 key.
            /// </summary>
            NUMBER_3 = 51
        }

        /// <summary>
        /// Clock source options.
        /// </summary>
        public enum ClockSource
        {
            /// <summary>
            /// Internal clock source.
            /// </summary>
            INT,

            /// <summary>
            /// External clock source.
            /// </summary>
            EXT
        }

        /// <summary>
        /// Represents the available channels for the output command.
        /// </summary>
        public enum SiglentSdgChannel
        {
            /// <summary>
            /// Channel 1
            /// </summary>
            C1,
            /// <summary>
            /// Channel 2
            /// </summary>
            C2
        }

        /// <summary>
        /// Represents the polarity options for the output command.
        /// </summary>
        public enum Polarity
        {
            /// <summary>
            /// Normal Polarity
            /// </summary>
            Normal,
            /// <summary>
            /// Inverted Polarity
            /// </summary>
            Inverted
        }
    }
}
