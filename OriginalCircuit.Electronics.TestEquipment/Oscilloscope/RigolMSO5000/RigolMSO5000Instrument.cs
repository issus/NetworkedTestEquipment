using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000.Commands;

namespace OriginalCircuit.Electronics.TestEquipment.Oscilloscope.RigolMSO5000
{

    public enum ScopeChannel
    {
        CH1 = 1,
        CH2 = 2,
        CH3 = 3,
        CH4 = 4,
    }

    public enum Coupling
    {
        AC,
        DC,
        GND
    }

    public enum ChannelUnits
    {
        Voltage,
        Watt,
        Ampere,
        Unknown
    }

    public enum ScopeKey
    {
        CH1, CH2, CH3, CH4, MATH, REF, LA, DECode, MOFF, F1, F2,
        F3, F4, F5, F6, F7, NPRevious, NNEXt, NSTop,
        VOFFset1, VOFFset2, VOFFset3, VOFFset4, VSCale1,
        VSCale2, VSCale3, VSCale4, HSCale, HPOSition, KFUNction,
        TLEVel, TMENu, TMODe, DEFault, CLEar, AUTO, RSTop, SINGle,
        QUICk, MEASure, ACQuire, STORage, CURSor, DISPlay, UTILity,
        FORCe, GENerator1, GENerator2, BACK, TOUCh, ZOOM, SEARch
    }

    public static class ChannelBandwidth
    {
        public static string BW20MHz { get; } = "20M";
        public static string BW100MHz { get; } = "100M";
        public static string BW200MHz { get; } = "200M";
        public static string Off { get; } = "OFF";
    }

    public class RigolMSO5000Instrument : NetworkTestInstrument
    {
        //public AcquireCommands Acquire { get; }
        public Mso5000Channel Channel { get; }
        public Measure Measure { get; }
        //public SaveCommands Save { get; }
        public Trigger Trigger { get; }
        public Mso5000Waveform Waveform { get; }
        public Display Display { get; }
        public Timebase Timebase { get; }

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

        public async Task SendKeypress(ScopeKey key)
        {
            if (!IsConnected)
                throw new Exception("Test equipment not connected");

            await SendCommand($"SYST:KEY:PRES {key}");
        }
    }
}
