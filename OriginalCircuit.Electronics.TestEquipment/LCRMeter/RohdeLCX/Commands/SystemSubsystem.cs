using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Defines the USB class options that the device can use to connect to the computer.
    /// </summary>
    public enum USBClass
    {
        /// <summary>
        /// Uses the virtual communication port protocol, that enables you to emulate serial ports over USB.
        /// </summary>
        CDC,
        /// <summary>
        /// Uses the protocol for communication with USB devices.
        /// </summary>
        TMC,
    }

    /// <summary>
    /// Defines the available warning beep states.
    /// </summary>
    public enum WarningBeepState
    {
        /// <summary>
        /// Turns the warning beep on.
        /// </summary>
        ON = 1,
        /// <summary>
        /// Turns the warning beep off.
        /// </summary>
        OFF = 0,
    }

    /// <summary>
    /// Defines the available gateway states.
    /// </summary>
    public enum GatewayState
    {
        /// <summary>
        /// Turns the gateway on.
        /// </summary>
        ON = 1,
        /// <summary>
        /// Turns the gateway off.
        /// </summary>
        OFF = 0,
    }

    /// <summary>
    /// Defines the available VCN (Virtual Connect Network) status states.
    /// </summary>
    public enum VCNStatus
    {
        /// <summary>
        /// Turns the VCN status on.
        /// </summary>
        ON = 1,
        /// <summary>
        /// Turns the VCN status off.
        /// </summary>
        OFF = 0,
    }

    /// <summary>
    /// Represents the system subsystem of the test equipment.
    /// </summary>
    public class SystemSubsystem
    {
        private readonly NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the SystemSubsystem class with the specified NetworkTestInstrument instance.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument instance to use for communication with the test equipment.</param>
        public SystemSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Sets the measurement time mode and the acquisition time interval.
        /// </summary>
        /// <returns></returns>
        public async Task SelectUSBClass(USBClass USBclass)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"INT:USB:CLAS {USBclass}");
        }

        /// <summary>
        /// Activates the Rohde LCX to create an acoustic signal on errors and warnings.
        /// </summary>
        /// <returns></returns>
        public async Task SetWarningBeepState(WarningBeepState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:BEEP:WARN:STAT {state}");
        }

        /// <summary>
        /// Queries the state of acoustic signal on errors and warnings.
        /// </summary>
        /// <returns></returns>
        public async Task<WarningBeepState> QueryWarningBeepState()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:BEEP:WARN:STAT?");
            return Enum.Parse<WarningBeepState>(await equipment.ReadString());
        }

        /// <summary>
        /// Activates that the Rohde LCX issues a beep on error or warning immediately.
        /// </summary>
        /// <returns></returns>
        public async Task ActivateWarningBeepImmediate(WarningBeepState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:BEEP:WARN:IMM {state}");
        }

        /// <summary>
        /// Activates the Rohde LCX to create an acoustic signal on a completed operation.
        /// </summary>
        /// <returns></returns>
        public async Task ActivateWarningBeepAfterOperation(WarningBeepState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:BEEP:COMP:STAT {state}");
        }

        /// <summary>
        /// Queries the state an acoustic signal on a completed operation.
        /// </summary>
        /// <returns></returns>
        public async Task<WarningBeepState> QueryWarningBeepAfterOperation()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:BEEP:COMP:STAT?");
            return Enum.Parse<WarningBeepState>(await equipment.ReadString());
        }

        /// <summary>
        /// Activates that the Rohde LCX issues a beep after operation complete immediately.
        /// </summary>
        /// <returns></returns>
        public async Task ActivateWarningBeepAfterOperationImmediate(WarningBeepState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:BEEP:COMP:IMM {state}");
        }

        /// <summary>
        /// Queries the state of a beep after operation complete immediately.
        /// </summary>
        /// <returns></returns>
        public async Task<WarningBeepState> QueryWarningBeepAfterOperationImmediate()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:BEEP:COMP:IMM?");
            return Enum.Parse<WarningBeepState>(await equipment.ReadString());
        }

        /// <summary>
        /// Sets the IP address.
        ///  Range:0.0.0.0. to ff.ff.ff.ff
        /// </summary>
        /// <returns></returns>
        public async Task SetIPAddress(string address)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:ADDR {address}");
        }

        /// <summary>
        /// Assigns and confirms the settings.
        /// </summary>
        /// <returns></returns>
        public async Task ConfirmSettings()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:APPL");
        }

        /// <summary>
        /// Sets the IP address of the default gateway.
        ///  Range:0.0.0.0. to ff.ff.ff.ff
        /// </summary>
        /// <returns></returns>
        public async Task SetDefaultGatewayIPAddress(string address)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:DGAT {address}");
        }

        /// <summary>
        /// Enables or disables the IP address of the default gateway.
        /// </summary>
        /// <returns></returns>
        public async Task SetStateOfDefaultGatewayIPAddress(GatewayState state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:DHCP {state}");
        }

        /// <summary>
        /// Removes the LAN configuration.
        /// </summary>
        /// <returns></returns>
        public async Task RemoveLANConfig()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:DISC");
        }

        /// <summary>
        /// Sets an individual hostname for the Rohde LCX.
        /// </summary>
        /// <returns></returns>
        public async Task SetHostName(string name)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:HOST {name}");
        }

        /// <summary>
        /// Queries the MAC address of the network.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryMACAddress()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:COMM:LAN:MAC?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Terminates the network configuration and restarts the network.
        /// </summary>
        /// <returns></returns>
        public async Task RestartNetwork()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:RES");
        }

        /// <summary>
        /// Sets the subnet mask.
        /// </summary>
        /// <returns></returns>
        public async Task SetSubnetMask(int mask)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:LAN:SMAS {mask}");
        }

        /// <summary>
        /// Sets the VNC port address.
        /// Range: 100 to 65535
        /// </summary>
        /// <returns></returns>
        public async Task SetVCNPowerAddress(int port)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:NETW:VCN:PORT {port}");
        }

        /// <summary>
        /// Activates the VNC interface for remote access.
        /// </summary>
        /// <returns></returns>
        public async Task ActivateWarningBeepAfterOperation(VCNStatus status)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:COMM:NETW:VCN:STAT {status}");
        }

        /// <summary>
        /// Sets the date for the instrument-internal calendar.
        /// </summary>
        /// <returns></returns>
        public async Task SetInstrumentDate(int year, int month, int day)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:DATE {year}, {month}, {day}");
        }

        /// <summary>
        /// Queries the date for the instrument-internal calendar.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryInstrumentDate()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:DATE?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Queries the hardware version of the instrument.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryHWVersion()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:HW:VERS?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Sets the brightness of the front panel keys.
        ///  Range: 0.1 to 1, Increment: 0.1
        /// </summary>
        /// <returns></returns>
        public async Task SetFrontKeyBrightness(double brightness)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:KEY:BRIG {brightness}");
        }

        /// <summary>
        /// Enables manual operation, i.e. unlocks front panel control.
        /// </summary>
        /// <returns></returns>
        public async Task UnlockFrontPanel()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:LOC");
        }

        /// <summary>
        /// Activates remote control.
        /// </summary>
        /// <returns></returns>
        public async Task ActivateRemoteControl()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:REM");
        }

        /// <summary>
        /// Restarts the instrument without restarting the operating system.
        /// </summary>
        /// <returns></returns>
        public async Task IntrumentRestart()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:REST");
        }

        /// <summary>
        /// Locks all front panel controls, i.e. manual operation.
        /// </summary>
        /// <returns></returns>
        public async Task LockFrontPanel()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:RWL");
        }

        /// <summary>
        /// Saves the current instrument settings in a file with defined filename.
        /// </summary>
        /// <returns></returns>
        public async Task SaveSettingsOnFile(string filename)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:SETT:DEF:SAVE {filename}");
        }

        /// <summary>
        /// Sets the time for the instrument-internal clock.
        /// </summary>
        /// <returns></returns>
        public async Task SetInstrumentTime(int hour, int minute, int second)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"SYST:TIME {hour}, {minute}, {second}");
        }

        /// <summary>
        /// Queries the time for the instrument-internal clock.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryInstrumentTime()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:TIME?");
            return await equipment.ReadString();
        }

        /// <summary>
        /// Queries the up time of the operating system.
        /// </summary>
        /// <returns></returns>
        public async Task<string> QueryUpTime()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"SYST:UPT?");
            return await equipment.ReadString();
        }
    }
}