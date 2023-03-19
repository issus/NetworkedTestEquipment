using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000
{
    /// <summary>
    /// Represents the available channels on the test equipment.
    /// </summary>
    public enum ScopeChannel
    {
        /// <summary>
        /// Channel 1
        /// </summary>
        CH1 = 1,

        /// <summary>
        /// Channel 2
        /// </summary>
        CH2 = 2,

        /// <summary>
        /// Channel 3
        /// </summary>
        CH3 = 3,

        /// <summary>
        /// Channel 4
        /// </summary>
        CH4 = 4,
    }

    /// <summary>
    /// Represents the available coupling types on the test equipment.
    /// </summary>
    public enum Coupling
    {
        /// <summary>
        /// Alternating Current
        /// </summary>
        AC,

        /// <summary>
        /// Direct Current
        /// </summary>
        DC,

        /// <summary>
        /// Ground
        /// </summary>
        GND
    }

    /// <summary>
    /// Represents the available units for channel measurements on the test equipment.
    /// </summary>
    public enum ChannelUnits
    {
        /// <summary>
        /// Voltage unit
        /// </summary>
        Voltage,

        /// <summary>
        /// Watt unit
        /// </summary>
        Watt,

        /// <summary>
        /// Ampere unit
        /// </summary>
        Ampere,

        /// <summary>
        /// Unknown unit
        /// </summary>
        Unknown
    }



    /// <summary>
    /// The keys available to the scope for remote control.
    /// </summary>
    public enum ScopeKey
    {
        /// <summary>
        /// Channel 1 key.
        /// </summary>
        CH1,
        /// <summary>
        /// Channel 2 key.
        /// </summary>
        CH2,

        /// <summary>
        /// Channel 3 key.
        /// </summary>
        CH3,

        /// <summary>
        /// Channel 4 key.
        /// </summary>
        CH4,

        /// <summary>
        /// Math key.
        /// </summary>
        MATH,

        /// <summary>
        /// Reference key.
        /// </summary>
        REF,

        /// <summary>
        /// Logic analyzer key.
        /// </summary>
        LA,

        /// <summary>
        /// Digital decode key.
        /// </summary>
        DECode,

        /// <summary>
        /// Menu off key.
        /// </summary>
        MOFF,

        /// <summary>
        /// Function 1 key.
        /// </summary>
        F1,

        /// <summary>
        /// Function 2 key.
        /// </summary>
        F2,

        /// <summary>
        /// Function 3 key.
        /// </summary>
        F3,

        /// <summary>
        /// Function 4 key.
        /// </summary>
        F4,

        /// <summary>
        /// Function 5 key.
        /// </summary>
        F5,

        /// <summary>
        /// Function 6 key.
        /// </summary>
        F6,

        /// <summary>
        /// Function 7 key.
        /// </summary>
        F7,

        /// <summary>
        /// Navigation previous key.
        /// </summary>
        NPRevious,

        /// <summary>
        /// Navigation next key.
        /// </summary>
        NNEXt,

        /// <summary>
        /// Navigation stop key.
        /// </summary>
        NSTop,

        /// <summary>
        /// Voltage offset set 1 key.
        /// </summary>
        VOFFset1,

        /// <summary>
        /// Voltage offset set 2 key.
        /// </summary>
        VOFFset2,

        /// <summary>
        /// Voltage offset set 3 key.
        /// </summary>
        VOFFset3,

        /// <summary>
        /// Voltage offset set 4 key.
        /// </summary>
        VOFFset4,

        /// <summary>
        /// Voltage scale 1 key.
        /// </summary>
        VSCale1,

        /// <summary>
        /// Voltage scale 2 key.
        /// </summary>
        VSCale2,

        /// <summary>
        /// Voltage scale 3 key.
        /// </summary>
        VSCale3,

        /// <summary>
        /// Voltage scale 4 key.
        /// </summary>
        VSCale4,

        /// <summary>
        /// Horizontal scale key.
        /// </summary>
        HSCale,

        /// <summary>
        /// Horizontal position key.
        /// </summary>
        HPOSition,

        /// <summary>
        /// Function key.
        /// </summary>
        KFUNction,

        /// <summary>
        /// Time level key.
        /// </summary>
        TLEVel,

        /// <summary>
        /// Time menu key.
        /// </summary>
        TMENu,

        /// <summary>
        /// Time mode key.
        /// </summary>
        TMODe,

        /// <summary>
        /// Default key.
        /// </summary>
        DEFault,

        /// <summary>
        /// Clear key
        /// </summary>
        CLEar,

        /// <summary>
        /// Auto key.
        /// </summary>
        AUTO,

        /// <summary>
        /// Reset stop key.
        /// </summary>
        RSTop,

        /// <summary>
        /// Single key.
        /// </summary>
        SINGle,

        /// <summary>
        /// Quick key.
        /// </summary>
        QUICk,

        /// <summary>
        /// Measure key.
        /// </summary>
        MEASure,

        /// <summary>
        /// Acquire key.
        /// </summary>
        ACQuire,

        /// <summary>
        /// Storage key.
        /// </summary>
        STORage,

        /// <summary>
        /// Cursor key.
        /// </summary>
        CURSor,

        /// <summary>
        /// Display key.
        /// </summary>
        DISPlay,

        /// <summary>
        /// Utility key.
        /// </summary>
        UTILity,

        /// <summary>
        /// Force key.
        /// </summary>
        FORCe,

        /// <summary>
        /// Generator 1 key.
        /// </summary>
        GENerator1,

        /// <summary>
        /// Generator 2 key.
        /// </summary>
        GENerator2,

        /// <summary>
        /// Back key.
        /// </summary>
        BACK,

        /// <summary>
        /// Touch key.
        /// </summary>
        TOUCh,

        /// <summary>
        /// Zoom key.
        /// </summary>
        ZOOM,

        /// <summary>
        /// Search key.
        /// </summary>
        SEARch
    }


    /// <summary>
    /// Represents the available channel bandwidths for the test equipment.
    /// </summary>
    public static class ChannelBandwidth
    {
        /// <summary>
        /// The 20 MHz bandwidth option.
        /// </summary>
        public static string BW20MHz { get; } = "20M";

        /// <summary>
        /// The 100 MHz bandwidth option.
        /// </summary>
        public static string BW100MHz { get; } = "100M";

        /// <summary>
        /// The 200 MHz bandwidth option.
        /// </summary>
        public static string BW200MHz { get; } = "200M";

        /// <summary>
        /// The option to turn off the channel bandwidth.
        /// </summary>
        public static string Off { get; } = "OFF";
    }


    /// <summary>
    /// Represents a Rigol MSO5000 series oscilloscope instrument that communicates over the network using LXI/SCPI protocol.
    /// </summary>
    public class RigolMSO5000Instrument : NetworkTestInstrument
    {
        /// <summary>
        /// The channel settings for the oscilloscope.
        /// </summary>
        public Mso5000Channel Channel { get; }

        /// <summary>
        /// The measurement commands for the oscilloscope.
        /// </summary>
        public Measure Measure { get; }

        /// <summary>
        /// The trigger settings for the oscilloscope.
        /// </summary>
        public Trigger Trigger { get; }

        /// <summary>
        /// The waveform data for the oscilloscope.
        /// </summary>
        public Mso5000Waveform Waveform { get; }

        /// <summary>
        /// The display settings for the oscilloscope.
        /// </summary>
        public Display Display { get; }

        /// <summary>
        /// The timebase settings for the oscilloscope.
        /// </summary>
        public Timebase Timebase { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RigolMSO5000Instrument"/> class.
        /// </summary>
        public RigolMSO5000Instrument() : base(InstrumentType.Oscilloscope)
        {
            //Acquire = new AcquireCommands(this);
            Channel = new Mso5000Channel(this);
            Measure = new Measure(this);
            //Save = new SaveCommands(this);
            Trigger = new Trigger(this);
            Waveform = new Mso5000Waveform(this);
            Display = new Display(this);
            Timebase = new Timebase(this);
        }

        /// <summary>
        /// Enables the waveform auto setting function. The oscilloscope will automatically adjust the
        /// vertical scale, horizontal time base, and trigger mode according to the input signal to
        /// realize optimal waveform display.This command functions the same as the AUTO key on
        /// the front panel.
        /// </summary>
        /// <returns></returns>
        public async Task Auto()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"AUT");
        }

        /// <summary>
        /// Clears all the waveforms on the screen. This command functions the same as the CLEAR
        /// key on the front panel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Clear()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"CLE");
        }

        /// <summary>
        /// This command functions the same as the RUN/STOP key on the front panel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Run()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"RUN");
        }

        /// <summary>
        /// This command functions the same as the RUN/STOP key on the front panel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Stop()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"STOP");
        }

        /// <summary>
        /// Sets the trigger mode of the oscilloscope to "Single". This command functions the same as
        /// pressing SINGLE on the front panel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Single()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"SING");
        }

        /// <summary>
        /// Generates a trigger signal forcefully. This command is only applicable to the normal and
        /// single trigger modes. This command functions
        /// the same as the FORCE key in the trigger control area of the front panel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ForceTrigger()
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"TFOR");
        }

        /// <summary>
        /// Sends a key press command to the test equipment.
        /// </summary>
        /// <param name="key">The key to be pressed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if test equipment is not connected.</exception>
        public async Task SendKeypress(ScopeKey key)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"SYST:KEY:PRES {key}");
        }

    }
}
