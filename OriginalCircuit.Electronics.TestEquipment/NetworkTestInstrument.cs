using System.Net.Sockets;
using System.Net;
using System.Text;
using OriginalCircuit.Electronics.TestEquipment.Data;
using SixLabors.ImageSharp;
using System.Diagnostics;

namespace OriginalCircuit.Electronics.TestEquipment
{
    /// <summary>
    /// Flags used to indicate events that can be registered for by an instrument
    /// </summary>
    [Flags]
    public enum EventRegister
    {
        /// <summary>
        /// Indicates that an operation has completed successfully
        /// </summary>
        OperationComplete = 0,

        /// <summary>
        /// Indicates that an error has occurred while processing a query
        /// </summary>
        QueryError = 4,

        /// <summary>
        /// Indicates that a device error has occurred
        /// </summary>
        DeviceError = 8,

        /// <summary>
        /// Indicates that an execution error has occurred
        /// </summary>
        ExecutionError = 16,

        /// <summary>
        /// Indicates that a command error has occurred
        /// </summary>
        CommandError = 32,

        /// <summary>
        /// Indicates that the instrument has been powered on
        /// </summary>
        PowerOn = 128
    }

    /// <summary>
    /// Flags used to indicate the status of an instrument
    /// </summary>
    [Flags]
    public enum StatusRegister
    {
        /// <summary>
        /// Indicates that an error is in the instrument's error queue
        /// </summary>
        ErrorQueue = 4,

        /// <summary>
        /// Indicates that a questionable condition exists
        /// </summary>
        QuestionableSummary = 8,

        /// <summary>
        /// Indicates that a message is available for retrieval
        /// </summary>
        MessageAvailable = 16,

        /// <summary>
        /// Indicates that a standard event has occurred
        /// </summary>
        StandardEventSummary = 32,

        /// <summary>
        /// Indicates that a master summary has occurred
        /// </summary>
        MasterSummary = 64,

        /// <summary>
        /// Indicates that an operation summary has occurred
        /// </summary>
        OperationSummary = 128
    }

    /// <summary>
    /// Types of instruments available in the library
    /// </summary>
    public enum InstrumentType
    {
        /// <summary>
        /// An oscilloscope instrument
        /// </summary>
        Oscilloscope,

        /// <summary>
        /// A power supply instrument
        /// </summary>
        Supply,

        /// <summary>
        /// A load instrument
        /// </summary>
        Load,

        /// <summary>
        /// A multimeter instrument
        /// </summary>
        Multimeter,

        /// <summary>
        /// A waveform generator instrument
        /// </summary>
        WaveformGenerator,

        /// <summary>
        /// An LCR meter instrument
        /// </summary>
        LCRMeter
    }

    /// <summary>
    /// Represents an abstract class for network test instrument.
    /// </summary>
    public abstract class NetworkTestInstrument
    {
        TcpClient? tcpClient;
        bool lastConnectionState = false;

        /// <summary>
        /// Gets or sets the read timeout.
        /// </summary>
        public int ReadTimeout { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the send timeout.
        /// </summary>
        public int SendTimeout { get; set; } = 500;

        /// <summary>
        /// Gets or sets the type of the instrument.
        /// </summary>
        public InstrumentType InstrumentType { get; set; }

        /// <summary>
        /// Gets the identity of the instrument.
        /// </summary>
        public InstrumentIdentity? Identity { get; private set; }

        /// <summary>
        /// Gets the remote host.
        /// </summary>
        public IPEndPoint? RemoteHost { get; private set; }

        /// <summary>
        /// Occurs when connection state changes.
        /// </summary>
        public event EventHandler<bool>? ConnectionStateChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkTestInstrument"/> class.
        /// </summary>
        /// <param name="instrumentType">The type of instrument.</param>
        /// <param name="remoteHost">The remote host to connect to.</param>
        public NetworkTestInstrument(InstrumentType instrumentType, IPEndPoint? remoteHost = null)
        {
            InstrumentType = instrumentType;
            RemoteHost = remoteHost;
        }

        /// <summary>
        /// Connects to a test equipment at a given IP address and port.
        /// </summary>
        /// <param name="ipAddress">IP address of the test equipment.</param>
        /// <param name="port">Port number to use for the connection. Default is 5555.</param>
        /// <returns>True if connection was successful, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when ipAddress is null or empty.</exception>
        public async Task<bool> Connect(string ipAddress, int port = 5555)
        {
            if (String.IsNullOrEmpty(ipAddress))
                throw new ArgumentNullException(nameof(ipAddress));

            return await Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
        }

        /// <summary>
        /// Connects to a test equipment using the default RemoteHost.
        /// </summary>
        /// <returns>True if connection was successful, false otherwise.</returns>
        public async Task<bool> Connect()
        {
            return await Connect(RemoteHost);
        }

        /// <summary>
        /// Connects to a test equipment using the provided IPEndPoint.
        /// </summary>
        /// <param name="remoteHost">IPEndPoint of the test equipment.</param>
        /// <returns>True if connection was successful, false otherwise.</returns>
        public async Task<bool> Connect(IPEndPoint? remoteHost)
        {
            if (remoteHost == null)
                return false;

            RemoteHost = remoteHost;

            var timeOut = TimeSpan.FromSeconds(1);
            var cancellationCompletionSource = new TaskCompletionSource<bool>();

            tcpClient = new TcpClient();
            
            try
            {
                using (var cts = new CancellationTokenSource(timeOut))
                {
                    var task = tcpClient.ConnectAsync(remoteHost);

                    using (cts.Token.Register(() => cancellationCompletionSource.TrySetResult(true)))
                    {
                        if (task != await Task.WhenAny(task, cancellationCompletionSource.Task))
                        {
                            throw new OperationCanceledException(cts.Token);
                        }
                    }
                }
                    
            }
            catch { return false; }

            tcpClient.ReceiveTimeout = ReadTimeout;
            tcpClient.SendTimeout = SendTimeout;

            Identity = await Identification();

            IsConnected = tcpClient.Connected;
            // manually create event again, because otherwise the event is fired when
            // the identity is pulled, but wont fire again afterwards. So something
            // checking the identiy after a connection wont be able to see the cached value.
            ConnectionStateChanged?.Invoke(this, tcpClient.Connected);

            return tcpClient.Connected;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the test equipment is connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                var connected = tcpClient != null && tcpClient.Connected;
                if (lastConnectionState != connected)
                {
                    lastConnectionState = connected;
                    ConnectionStateChanged?.Invoke(this, connected);
                }

                return connected;
            }
            private set
            {
                if (lastConnectionState != value)
                {
                    lastConnectionState = value;
                    ConnectionStateChanged?.Invoke(this, value);
                }

            }
        }

        /// <summary>
        /// Disconnects from the test equipment.
        /// </summary>
        public void Disconnect()
        {
            if (tcpClient != null)
            {
                try
                {
                    tcpClient.Close();
                }
                catch { }
            }
        }

        /// <summary>
        /// Sends a command to the test equipment.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <exception cref="ArgumentNullException">Thrown when command is null or empty.</exception>
        public async Task SendCommand(string command)
        {
            if (tcpClient is null || !IsConnected)
                throw new Exception("Test equipment not connected");

            if (String.IsNullOrEmpty(command))
                throw new ArgumentNullException(nameof(command));

            //todo: Move console writes to debug write before pushing live
#if DEBUG
            //Debug.WriteLine($"\t[SEND]\t{command}");
#endif

            var stream = tcpClient.GetStream();
            stream.WriteTimeout = SendTimeout;

            var bytes = Encoding.UTF8.GetBytes($"{command.Trim()}\n");
            await stream.WriteAsync(bytes);
        }

        /// <summary>
        /// Reads data from the connected test equipment.
        /// </summary>
        /// <returns>Byte array containing the received data.</returns>
        /// <exception cref="Exception">Thrown when test equipment is not connected.</exception>
        public async Task<byte[]?> ReadData()
        {
            if (!IsConnected || tcpClient == null)
                throw new Exception("Test equipment not connected");

            // Create a list to store the received data
            List<byte> receivedData = new List<byte>();

            try
            {
                // Get the network stream associated with the TcpClient
                NetworkStream stream = tcpClient.GetStream();

                var stopwatch = Stopwatch.StartNew();

                // 300ms for first byte
                long wait = 400;

                while (stopwatch.ElapsedMilliseconds < wait)
                {
                    if (tcpClient.Available <= 0)
                    {
                        await Task.Delay(3);
                        continue;
                    }

                    // if some data has been received, allow longer wait
                    if (wait == 400 && receivedData.Count > 0)
                        wait = 750;

                    // Read the incoming data into a buffer
                    byte[] buffer = new byte[tcpClient.Available];
                    int bytesReceived = await stream.ReadAsync(buffer, 0, buffer.Length);

                    // Add the received data to the list
                    receivedData.AddRange(buffer.Take(bytesReceived));

                    if (receivedData.Contains((byte)'\n'))
                        break;
                }

                //Debug.WriteLine($"\t[RX] {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Stop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            if (receivedData.Count == 0) 
            {
                return null;
            }

            // Convert the received data to a byte array
            byte[] data = receivedData.ToArray();

#if DEBUG
            if (data != null)
            {
                //Debug.WriteLine($"\t[RECEIVE]\t{Encoding.UTF8.GetString(data)}");
            }
#endif

            return data;
        }

        /// <summary>
        /// Reads bitmap data from the connected test equipment.
        /// </summary>
        /// <returns>An Image object containing the bitmap data.</returns>
        /// <exception cref="Exception">Thrown when test equipment is not connected.</exception>
        public async Task<Image?> ReadBitmap()
        {
            if (tcpClient is null || !IsConnected)
                throw new Exception("Test equipment not connected");

            var data = new byte[tcpClient.ReceiveBufferSize];

            using var readMs = new MemoryStream();
            var stream = tcpClient.GetStream();
            stream.ReadTimeout = ReadTimeout;

            var reader = new BinaryReader(stream);

            try
            {
                // wait for first byte of data to be ready
                readMs.WriteByte(reader.ReadByte());

                while (tcpClient.Available > 0)
                {
                    data = reader.ReadBytes(tcpClient.Available);
                    readMs.Write(data, 0, data.Length);

                    // some test equipment is slow, lets give it a little time to send some more data
                    await Task.Delay(50);
                }
            }
            catch { }

            bool BM6Found = false;
            int pos = 0;
            readMs.Position = 0;
            byte read;

            while (pos < readMs.Length && !BM6Found)
            {
                read = (byte)readMs.ReadByte();

                if (read == 'B')
                {
                    if ((byte)readMs.ReadByte() == 'M')
                    {
                        BM6Found = true;
                        break;
                    }
                }

                pos++;
            }

            if (!BM6Found)
                return null;

            readMs.Position = pos;

            try
            {
                Image image = await Image.LoadAsync(readMs);

                return image;
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Reads boolean data from the connected test equipment.
        /// </summary>
        /// <returns>Boolean value of the received data.</returns>
        public async Task<bool> ReadBool()
        {
            string? data = (await ReadString())?.ToUpper();

            if (data == null)
                return false;

            if (data == "1" || data == "ON" || data == "YES")
                return true;

            return false;
        }

        /// <summary>
        /// Reads string data from the connected test equipment.
        /// </summary>
        /// <returns>String containing the received data.</returns>
        public async Task<string> ReadString()
        {
            var data = await ReadData();
            if (data == null)
                return String.Empty;

            return Encoding.UTF8.GetString(data).Trim();
        }

        /// <summary>
        /// Reads integer data from the connected test equipment.
        /// </summary>
        /// <returns>Integer value of the received data.</returns>
        public async Task<int> ReadInt()
        {
            string response = (await ReadString()).Trim();

            if (int.TryParse(response, out int result))
                return result;
            else
                return int.MinValue;
        }

        /// <summary>
        /// Reads double data from the connected test equipment.
        /// </summary>
        /// <returns>Double value of the received data.</returns>
        public async Task<double> ReadDouble()
        {
            string response = (await ReadString()).Trim();

            if (string.IsNullOrEmpty(response)) 
                return double.NaN;

            if (double.TryParse(response, out double result))
                return result;
            else
                return double.NaN;
        }
        /// <summary>
        /// Clears the data buffer if the client is connected and there is data available to be read.
        /// </summary>
        /// <returns></returns>
        public async Task ClearBuffer()
        {
            if (tcpClient is null || !IsConnected)
                return;

            if (tcpClient.Available > 0)
                await ReadData();
        }

        /// <summary>
        /// Sends the clear status command to the device which clears all status data structures.
        /// </summary>
        /// <returns></returns>
        public async Task ClearStatus()
        {
            await SendCommand("*CLS");
        }

        /// <summary>
        /// Sets the Standard Event Status Enable mask for the device.
        /// </summary>
        /// <param name="mask">The mask to be set for the device</param>
        /// <returns></returns>
        public async Task SetStandardEventStatusEnable(EventRegister mask)
        {
            await SendCommand($"*ESE {(byte)mask}");
        }

        /// <summary>
        /// Queries the Standard Event Status Enable mask for the device.
        /// </summary>
        /// <returns>The EventRegister mask for the device</returns>
        public async Task<EventRegister> QueryStandardEventStatusEnable()
        {
            await ClearBuffer();

            await SendCommand("*ESE?");

            int response = await ReadInt();
            return (EventRegister)response;
        }

        /// <summary>
        /// Queries the Event Register for the device.
        /// </summary>
        /// <returns>The EventRegister for the device</returns>
        public async Task<EventRegister> QueryEventRegister()
        {
            await ClearBuffer();

            await SendCommand("*ESR?");

            int response = await ReadInt();
            return (EventRegister)response;
        }

        /// <summary>
        /// Queries the device for its identification information.
        /// </summary>
        /// <returns>An InstrumentIdentity object containing the identification information of the device or null if no response was received</returns>
        public async Task<InstrumentIdentity?> Identification()
        {
            await ClearBuffer();

            await SendCommand("*IDN?");

            var response = (await ReadString()).Trim();

            if (string.IsNullOrEmpty(response))
            {
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
                IsConnected = false;
                return null;
            }

            var idents = response.Split(',');

            return new InstrumentIdentity(idents[0], idents[1], idents[2], idents[3]);
        }

        /// <summary>
        /// Sends a command to register an operation complete event.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RegisterOperationComplete()
        {
            await SendCommand("*OPC");
        }

        /// <summary>
        /// Sends a command to query if an operation is complete.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The result is true if the operation is complete; otherwise, false.</returns>
        public async Task<bool> QueryOperationComplete()
        {
            await ClearBuffer();

            await SendCommand("*OPC?");

            return await ReadBool();
        }

        /// <summary>
        /// Waits for an operation to complete.
        /// </summary>
        /// <param name="seconds">The number of seconds to wait for the operation to complete. Default is 1 second.</param>
        /// <returns>A task representing the asynchronous operation. The result is true if the operation is complete; otherwise, false.</returns>
        public async Task<bool> WaitForOperationComplete(double seconds = 1)
        {
            int maxWaits = (int)Math.Ceiling(seconds / 0.1);
            int waits = 0;

            while (!await QueryOperationComplete() && waits < maxWaits)
            {
                await Task.Delay(100);
                waits++;
            }

            return await QueryOperationComplete();
        }

        /// <summary>
        /// Sends a command to reset the device.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ResetDevice()
        {
            await SendCommand("*RST");
        }

        /// <summary>
        /// Sets the specified bits in the Service Request Enable (SRE) register to enable the corresponding service request events
        /// </summary>
        /// <param name="mask">The mask indicating which service request events to enable</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task SetServiceRequestEnable(StatusRegister mask)
        {
            await SendCommand($"*SRE {(byte)mask}");
        }

        /// <summary>
        /// Queries the Service Request Enable (SRE) register to determine which service request events are enabled
        /// </summary>
        /// <returns>The EventRegister value indicating which service request events are enabled</returns>
        public async Task<EventRegister> QueryServiceRequestEnable()
        {
            await ClearBuffer();

            await SendCommand("*SRE?");

            int response = await ReadInt();
            return (EventRegister)response;
        }

        /// <summary>
        /// Queries the Status Byte (STB) register to determine the instrument status
        /// </summary>
        /// <returns>The StatusRegister value indicating the instrument status</returns>
        public async Task<StatusRegister> QueryStatus()
        {
            await ClearBuffer();

            await SendCommand("*STB?");

            int response = await ReadInt();
            return (StatusRegister)response;
        }

        /// <summary>
        /// Queries the self-test results of the instrument.
        /// </summary>
        /// <returns>The self-test results of the instrument as a string.</returns>
        public async Task<string> QuerySelfTest()
        {
            await ClearBuffer();

            await SendCommand("*TST?");
            return await ReadString();
        }

        /// <summary>
        /// Configures the instrument to wait for all pending operations to complete before executing 
        /// any additional commands.
        /// </summary>
        public async Task WaitToContinue()
        {
            await SendCommand("*WAI");
        }

        /// <summary>
        /// Generates a trigger action.
        /// </summary>
        public async Task TriggerInstrument()
        {
            await SendCommand("*TRG");
        }

        /// <summary>
        /// Releases all resources used by the LXI instrument object.
        /// </summary>
        public void Dispose()
        {
            if (tcpClient != null)
                tcpClient.Dispose();
        }


        /// <summary>
        /// Convert a number to a string representing the number in SI units
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ConvertToSIUnits(double? number)
        {
            if (!number.HasValue)
                return string.Empty;

            string[] units = {
                "f",
                "p",
                "n",
                "μ",
                "m",
                "",
                "k",
                "M",
                "G"
            };
            double[] thresholds = {
                1e-15,
                1e-12,
                1e-9,
                1e-6,
                1e-3,
                1e0,
                1e3,
                1e6,
                1e9
            };

            int unitIndex = 0;

            for (int i = 0; i < thresholds.Length; i++)
            {
                if ((double)Math.Abs((decimal)number) < thresholds[i])
                {
                    break;
                }
                unitIndex = i;
            }

            double? baseValue = number / thresholds[unitIndex];
            string result = $"{baseValue:G4}{units[unitIndex]}";

            return result;
        }

    }
}