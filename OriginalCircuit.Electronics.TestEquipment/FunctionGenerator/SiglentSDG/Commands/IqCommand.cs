using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents a command for the Siglent SDG series function generator.
    /// </summary>
    public class IqCommand
    {
        /// <summary>
        /// Gets or sets the equipment used to send the command.
        /// </summary>
        NetworkTestInstrument? equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="IqCommand"/> class
        /// with the specified equipment.
        /// </summary>
        /// <param name="equipment">The equipment used to send the command.</param>
        public IqCommand(NetworkTestInstrument instrument)
        {
            equipment = instrument;
        }

        /// <summary>
        /// Sets the center frequency of the I/Q modulator.
        /// </summary>
        /// <param name="centerFrequency">The desired center frequency.</param>
        /// <param name="unit">The unit of the center frequency (Hz, kHz, MHz, GHz).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetCenterFrequency(double centerFrequency, string unit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($":SOUR:IQ:CENT {centerFrequency}{unit}");
        }

        /// <summary>
        /// Queries the center frequency of the I/Q modulator.
        /// </summary>
        /// <returns>The center frequency value in Hz.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<double> QueryCenterFrequency()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($":SOUR:IQ:CENT?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the I/Q sampling rate.
        /// </summary>
        /// <param name="sampleRate">The desired sample rate.</param>
        /// <param name="unit">The unit of the sample rate (Hz, kHz, MHz, GHz).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetIQSamplingRate(double sampleRate, string unit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($":IQ:SAMP {sampleRate}{unit}");
        }

        /// <summary>
        /// Queries the I/Q sampling rate.
        /// </summary>
        /// <returns>The sampling rate value in Hz.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<double> QueryIQSamplingRate()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($":IQ:SAMP?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the I/Q symbol rate.
        /// </summary>
        /// <param name="symbolRate">The desired symbol rate.</param>
        /// <param name="unit">The unit of the symbol rate (S/s, kS/s, MS/s).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetIQSymbolRate(double symbolRate, string unit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($":IQ:SYMB {symbolRate}{unit}");
        }

        /// <summary>
        /// Queries the I/Q symbol rate.
        /// </summary>
        /// <returns>The symbol rate value in S/s.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<double> QueryIQSymbolRate()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($":IQ:SYMB?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the I/Q amplitude.
        /// </summary>
        /// <param name="amplitude">
        /// The desired amplitude.</param>
        /// <param name="unit">The unit of the amplitude (Vrms, mVrms, dBm).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetIQAmplitude(double amplitude, string unit)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($":IQ:AMPL {amplitude}{unit}");
        }

        /// <summary>
        /// Queries the I/Q amplitude.
        /// </summary>
        /// <returns>The amplitude value in Vrms.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<double> QueryIQAmplitude()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($":IQ:AMPL?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the gain ratio of I to Q.
        /// </summary>
        /// <param name="gainRatio">The desired gain ratio.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetIQAdjustmentGain(double gainRatio)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($":IQ:IQADjustment:GAIN {gainRatio}");
        }

        /// <summary>
        /// Queries the gain ratio of I to Q.
        /// </summary>
        /// <returns>The gain ratio value in dB.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<double> QueryIQAdjustmentGain()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($":IQ:IQADjustment:GAIN?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the I channel offset value.
        /// </summary>
        /// <param name="offset">The desired I offset value in volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetIChannelOffset(double offset)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string unit = "V";
            await equipment.SendCommand($":IQ:IQADjustment:IOFFset {offset}{unit}");
        }

        /// <summary>
        /// Queries the I channel offset value.
        /// </summary>
        /// <returns>The I offset value in volts.</returns>
        public async Task<double> QueryIChannelOffset()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand(":IQ:IQADjustment:IOFFset?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the Q channel offset value.
        /// </summary>
        /// <param name="offset">The desired Q offset value in volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetQChannelOffset(double offset)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string unit = "V";
            await equipment.SendCommand($":IQ:IQADjustment:QOFFset {offset}{unit}");
        }

        /// <summary>
        /// Queries the Q channel offset value.
        /// </summary>
        /// <returns>The Q offset value in volts.</returns>
        public async Task<double> QueryQChannelOffset()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand(":IQ:IQADjustment:QOFFset?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the phase angle (quadrature skew) between the I and Q vectors.
        /// </summary>
        /// <param name="angle">The desired phase angle in degrees.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetQSkew(double angle)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($":IQ:IQADjustment:QSKew {angle}");
        }

        /// <summary>
        /// Queries the phase angle (quadrature skew) between the I and Q vectors.
        /// </summary>
        /// <returns>The phase angle in degrees.</returns>
        public async Task<double> QueryQSkew()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand(":IQ:IQADjustment:QSKew?");
            return await equipment.ReadDouble();
        }

        /// <summary>
        /// Sets the I/Q trigger source.
        /// </summary>
        /// <param name="source">The desired trigger source.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetIQTriggerSourceAsync(IQTriggerSource source)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string sourceString = source.ToString().ToUpper();
            await equipment.SendCommand($":IQ:TRIGger:SOURce {sourceString}");
        }

        /// <summary>
        /// Queries the I/Q trigger source.
        /// </summary>
        /// <returns>The current I/Q trigger source.</returns>
        public async Task<IQTriggerSource> QueryIQTriggerSource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand(":IQ:TRIGger:SOURce?");
            return Enum.Parse<IQTriggerSource>(await equipment.ReadString(), true);
        }

        /// <summary>
        /// Translate between enum and the value the SDG expects.
        /// </summary>
        private static readonly Dictionary<BuiltInWaveforms, string> WaveformNames = new()
        {
            { BuiltInWaveforms.ASK2, "2ASK" },
            { BuiltInWaveforms.ASK4, "4ASK" },
            { BuiltInWaveforms.ASK8, "8ASK" },
            { BuiltInWaveforms.BPSK, "BPSK" },
            { BuiltInWaveforms.PSK4, "4PSK" },
            { BuiltInWaveforms.PSK8, "8PSK" },
            { BuiltInWaveforms.DBPSK, "DBPSK" },
            { BuiltInWaveforms.DPSK4, "4DPSK" },
            { BuiltInWaveforms.DPSK8, "8DPSK" },
            { BuiltInWaveforms.QAM8, "8QAM" },
            { BuiltInWaveforms.QAM16, "16QAM" },
            { BuiltInWaveforms.QAM32, "32QAM" },
            { BuiltInWaveforms.QAM64, "64QAM" },
            { BuiltInWaveforms.QAM128, "128QAM" },
            { BuiltInWaveforms.QAM256, "256QAM" }
        };

        /// <summary>
        /// Sets the I/Q waveform from the built-in waveform list.
        /// </summary>
        /// <param name="waveform">The desired waveform from the built-in list.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetBuiltinWaveform(BuiltInWaveforms waveform)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string waveName = WaveformNames[waveform];

            await equipment.SendCommand($":IQ:WAVE:BUIL {waveName}");
        }

        /// <summary>
        /// Sets the I/Q waveform to a user-stored waveform.
        /// </summary>
        /// <param name="waveName">The name of the user-stored waveform to load.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task SetUserStoredWaveform(string waveName)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($":IQ:WAVEload:USER{waveName}");
        }

        /// <summary>
        /// Queries the I/Q waveform loaded on the test equipment.
        /// </summary>
        /// <returns>The I/Q waveform loaded on the test equipment.</returns>
        /// <exception cref="Exception">Thrown when the equipment is not connected.</exception>
        public async Task<(WaveformType Type, object WaveName)> QueryIQWaveLoad()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($":IQ:WAVE?");
            var response = await equipment.ReadString();
            var parts = response.Split(' ');
            var waveformType = Enum.Parse<WaveformType>(parts[0].ToUpper());

            object waveName;
            if (waveformType == WaveformType.BUILTIN)
            {
                response = response.Replace("BUILtin", "").Trim();
                var foundEntry = WaveformNames.FirstOrDefault(entry => entry.Value == response);

                if (foundEntry.Key == default && foundEntry.Value != response)
                    throw new Exception($"Invalid waveform name: {response}");

                waveName = foundEntry.Key;
            }
            else
            {
                waveName = parts.Length > 1 ? parts[1] : string.Empty;
            }

            return (Type: waveformType, WaveName: waveName);
        }

        /// <summary>
        /// The built-in waveforms for the I/Q test equipment.
        /// </summary>
        public enum BuiltInWaveforms
        {
            /// <summary>2ASK waveform</summary>
            ASK2,
            /// <summary>4ASK waveform</summary>
            ASK4,
            /// <summary>8ASK waveform</summary>
            ASK8,
            /// <summary>BPSK waveform</summary>
            BPSK,
            /// <summary>4PSK waveform</summary>
            PSK4,
            /// <summary>8PSK waveform</summary>
            PSK8,
            /// <summary>DBPSK waveform</summary>
            DBPSK,
            /// <summary>4DPSK waveform</summary>
            DPSK4,
            /// <summary>8DPSK waveform</summary>
            DPSK8,
            /// <summary>8QAM waveform</summary>
            QAM8,
            /// <summary>16QAM waveform</summary>
            QAM16,
            /// <summary>32QAM waveform</summary>
            QAM32,
            /// <summary>64QAM waveform</summary>
            QAM64,
            /// <summary>128QAM waveform</summary>
            QAM128,
            /// <summary>256QAM waveform</summary>
            QAM256
        }

        /// <summary>
        /// The trigger sources for the I/Q trigger.
        /// </summary>
        public enum IQTriggerSource
        {
            /// <summary>
            /// The trigger source is set to internal.
            /// </summary>
            Internal,

            /// <summary>
            /// The trigger source is set to external.
            /// </summary>
            External,

            /// <summary>
            /// The trigger source is set to manual.
            /// </summary>
            Manual
        }

        /// <summary>
        /// Represents the type of I/Q waveform loaded on the test equipment.
        /// </summary>
        public enum WaveformType
        {
            /// <summary>
            /// Indicates a built-in waveform.
            /// </summary>
            BUILTIN,

            /// <summary>
            /// Indicates a user-stored waveform.
            /// </summary>
            USERSTORED
        }
    }
}
