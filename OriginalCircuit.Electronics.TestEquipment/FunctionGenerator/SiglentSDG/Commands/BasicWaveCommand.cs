using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.SiglentSDGInstrument;

namespace OriginalCircuit.Electronics.TestEquipment.FunctionGenerator.SiglentSDG.Commands
{
    /// <summary>
    /// Represents a basic wave command for a Siglent SDG series function generator.
    /// </summary>
    public class BasicWaveCommand
    {
        /// <summary>
        /// The network test instrument used to send the command.
        /// </summary>
        private readonly NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicWaveCommand"/> class with the specified equipment.
        /// </summary>
        /// <param name="equipment">The network test instrument used to send the command.</param>
        public BasicWaveCommand(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the waveform type for the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the waveform type for.</param>
        /// <param name="waveType">The waveform type to set.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetWaveType(SiglentSdgChannel channel, WaveType waveType)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BSWV WVTP,{waveType}");
        }

        /// <summary>
        /// Queries the basic wave parameters for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the basic wave parameters are being queried.</param>
        /// <returns>A dictionary containing the basic wave parameters and their values.</returns>
        public async Task<Dictionary<WaveParameter, string>> QueryParameters(SiglentSdgChannel channel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();
            await equipment.SendCommand($"{channel}:BaSic_WaVe?");
            var response = await equipment.ReadString();
            var parameterPairs = response.Split(',');

            var result = new Dictionary<WaveParameter, string>();
            for (int i = 0; i < parameterPairs.Length; i += 2)
            {
                WaveParameter parameter = Enum.Parse<WaveParameter>(parameterPairs[i]);
                result.Add(parameter, parameterPairs[i + 1]);
            }

            return result;
        }

        /// <summary>
        /// Sets the frequency for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the frequency is being set.</param>
        /// <param name="frequency">The desired frequency in hertz.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetFrequency(SiglentSdgChannel channel, double frequency)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe FRQ,{frequency}");
        }

        /// <summary>
        /// Sets the period for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the period is being set.</param>
        /// <param name="period">The desired period in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPeriod(SiglentSdgChannel channel, double period)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe PERI,{period}");
        }

        /// <summary>
        /// Sets the amplitude for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the amplitude is being set.</param>
        /// <param name="amplitude">The desired amplitude in volts peak-to-peak.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetAmplitude(SiglentSdgChannel channel, double amplitude)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe AMP,{amplitude}");
        }

        /// <summary>
        /// Sets the offset for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the offset is being set.</param>
        /// <param name="offset">The desired offset in volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetOffset(SiglentSdgChannel channel, double offset)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe OFST,{offset}");
        }

        /// <summary>
        /// Sets the symmetry for the specified channel when the wave type is RAMP.
        /// </summary>
        /// <param name="channel">The channel for which the symmetry is being set.</param>
        /// <param name="symmetry">The desired symmetry in percentage (0 to 100).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetSymmetry(SiglentSdgChannel channel, double symmetry)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe SYM,{symmetry}");
        }

        /// <summary>
        /// Sets the duty cycle for the specified channel when the wave type is SQUARE or PULSE.
        /// </summary>
        /// <param name="channel">The channel for which the duty cycle is being set.</param>
        /// <param name="duty">The desired duty cycle in percentage (0 to 100).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetDutyCycle(SiglentSdgChannel channel, double duty)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe DUTY,{duty}");
        }

        /// <summary>
        /// Sets the phase for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the phase is being set.</param>
        /// <param name="phase">The desired phase in degrees (0 to 360).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPhase(SiglentSdgChannel channel, double phase)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe PHSE,{phase}");
        }

        /// <summary>
        /// Sets the standard deviation for the specified channel when the wave type is NOISE.
        /// </summary>
        /// <param name="channel">The channel for which the standard deviation is being set.</param>
        /// <param name="stdev">The desired standard deviation in volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetStandardDeviation(SiglentSdgChannel channel, double stdev)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe STDEV,{stdev}");
        }

        /// <summary>
        /// Sets the mean for the specified channel when the wave type is NOISE.
        /// </summary>
        /// <param name="channel">The channel for which the mean is being set.</param>
        /// <param name="mean">The desired mean in volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetMean(SiglentSdgChannel channel, double mean)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe MEAN,{mean}");
        }

        /// <summary>
        /// Sets the positive pulse width for the specified channel when the wave type is PULSE.
        /// </summary>
        /// <param name="channel">The channel for which the pulse width is being set.</param>
        /// <param name="width">The desired positive pulse width in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPulseWidth(SiglentSdgChannel channel, double width)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe WIDTH,{width}");
        }

        /// <summary>
        /// Sets the rise time (10% to 90%) for the specified channel when the wave type is PULSE.
        /// </summary>
        /// <param name="channel">The channel for which the rise time is being set.</param>
        /// <param name="rise">The desired rise time in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetRiseTime(SiglentSdgChannel channel, double rise)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe RISE,{rise}");
        }

        /// <summary>
        /// Sets the fall time (90% to 10%) for the specified channel when the wave type is PULSE.
        /// </summary>
        /// <param name="channel">The channel for which the fall time is being set.</param>
        /// <param name="fall">The desired fall time in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetFallTime(SiglentSdgChannel channel, double fall)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe FALL,{fall}");
        }

        /// <summary>
        /// Sets the pulse delay for the specified channel when the wave type is PULSE.
        /// </summary>
        /// <param name="channel">The channel for which the pulse delay is being set.</param>
        /// <param name="delay">The desired pulse delay in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPulseDelay(SiglentSdgChannel channel, double delay)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe DLY,{delay}");
        }

        /// <summary>
        /// Sets the high level for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the high level is being set.</param>
        /// <param name="highLevel">The desired high level in volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetHighLevel(SiglentSdgChannel channel, double highLevel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe HLEV,{highLevel}");
        }

        /// <summary>
        /// Sets the low level for the specified channel.
        /// </summary>
        /// <param name="channel">The channel for which the low level is being set.</param>
        /// <param name="lowLevel">The desired low level in volts.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetLowLevel(SiglentSdgChannel channel, double lowLevel)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe LLEV,{lowLevel}");
        }

        /// <summary>
        /// Sets the bandwidth switch for the specified channel when the wave type is NOISE.
        /// </summary>
        /// <param name="channel">The channel for which the bandwidth switch is being set.</param>
        /// <param name="state">The desired bandwidth switch state (ON or OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetBandwidthSwitch(SiglentSdgChannel channel, bool state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string onOff = state ? "ON" : "OFF";
            await equipment.SendCommand($"{channel}:BaSic_WaVe BANDSTATE,{onOff}");
        }

        /// <summary>
        /// Sets the noise bandwidth for the specified channel when the wave type is NOISE.
        /// </summary>
        /// <param name="channel">The channel for which the noise bandwidth is being set.</param>
        /// <param name="bandwidth">The desired noise bandwidth in Hertz.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetNoiseBandwidth(SiglentSdgChannel channel, double bandwidth)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe BANDWIDTH,{bandwidth}");
        }

        /// <summary>
        /// Sets the PRBS length for the specified channel when the wave type is PRBS.
        /// </summary>
        /// <param name="channel">The channel for which the PRBS length is being set.</param>
        /// <param name="length">The desired PRBS length (3 to 32).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPrbsLength(SiglentSdgChannel channel, int length)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe LENGTH,{length}");
        }

        /// <summary>
        /// Sets the PRBS rise/fall time for the specified channel when the wave type is PRBS.
        /// </summary>
        /// <param name="channel">The channel for which the PRBS rise/fall time is being set.</param>
        /// <param name="time">The desired PRBS rise/fall time in seconds.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPrbsRiseFallTime(SiglentSdgChannel channel, double time)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe EDGE,{time}");
        }

        /// <summary>
        /// Sets the PRBS differential switch for the specified channel when the wave type is PRBS.
        /// </summary>
        /// <param name="channel">The channel for which the PRBS differential switch is being set.</param>
        /// <param name="state">The desired PRBS differential switch state (ON or OFF).</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPrbsDifferentialSwitch(SiglentSdgChannel channel, bool state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            string onOff = state ? "ON" : "OFF";
            await equipment.SendCommand($"{channel}:BaSic_WaVe DIFFSTATE,{onOff}");
        }

        /// <summary>
        /// Sets the PRBS bit rate for the specified channel when the wave type is PRBS.
        /// </summary>
        /// <param name="channel">The channel for which the PRBS bit rate is being set.</param>
        /// <param name="bitRate">The desired PRBS bit rate in bits-per-second.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetPrbsBitRate(SiglentSdgChannel channel, double bitRate)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"{channel}:BaSic_WaVe BITRATE,{bitRate}");
        }

        /// <summary>
        /// Represents the waveform types supported by the Siglent SDG series function generator.
        /// </summary>
        public enum WaveType
        {
            /// <summary>
            /// Represents a sine waveform.
            /// </summary>
            SINE,

            /// <summary>
            /// Represents a square waveform.
            /// </summary>
            SQUARE,

            /// <summary>
            /// Represents a ramp waveform.
            /// </summary>
            RAMP,

            /// <summary>
            /// Represents a pulse waveform.
            /// </summary>
            PULSE,

            /// <summary>
            /// Represents a noise waveform.
            /// </summary>
            NOISE,

            /// <summary>
            /// Represents an arbitrary waveform.
            /// </summary>
            ARB,

            /// <summary>
            /// Represents a DC (constant) waveform.
            /// </summary>
            DC,

            /// <summary>
            /// Represents a pseudo-random binary sequence (PRBS) waveform.
            /// </summary>
            PRBS
        }

        /// <summary>
        /// Repreesnts the Basic wave parameters.
        /// </summary>
        public enum WaveParameter
        {
            /// <summary>
            /// Waveform type parameter.
            /// </summary>
            WVTP,

            /// <summary>
            /// Frequency parameter.
            /// </summary>
            FRQ,

            /// <summary>
            /// Period parameter.
            /// </summary>
            PERI,

            /// <summary>
            /// Amplitude parameter.
            /// </summary>
            AMP,

            /// <summary>
            /// Offset parameter.
            /// </summary>
            OFST,

            /// <summary>
            /// Symmetry parameter.
            /// </summary>
            SYM,

            /// <summary>
            /// Duty cycle parameter.
            /// </summary>
            DUTY,

            /// <summary>
            /// Phase parameter.
            /// </summary>
            PHSE,

            /// <summary>
            /// Standard deviation parameter.
            /// </summary>
            STDEV,

            /// <summary>
            /// Mean parameter.
            /// </summary>
            MEAN,

            /// <summary>
            /// Width parameter.
            /// </summary>
            WIDTH,

            /// <summary>
            /// Rise time parameter.
            /// </summary>
            RISE,

            /// <summary>
            /// Fall time parameter.
            /// </summary>
            FALL,

            /// <summary>
            /// Delay time parameter.
            /// </summary>
            DLY,

            /// <summary>
            /// High level parameter.
            /// </summary>
            HLEV,

            /// <summary>
            /// Low level parameter.
            /// </summary>
            LLEV,

            /// <summary>
            /// Bandwidth state parameter.
            /// </summary>
            BANDSTATE,

            /// <summary>
            /// Bandwidth parameter.
            /// </summary>
            BANDWIDTH,

            /// <summary>
            /// Length parameter.
            /// </summary>
            LENGTH,

            /// <summary>
            /// Edge parameter.
            /// </summary>
            EDGE,

            /// <summary>
            /// Differential state parameter.
            /// </summary>
            DIFFSTATE,

            /// <summary>
            /// Bit rate parameter.
            /// </summary>
            BITRATE
        }

    }
}
