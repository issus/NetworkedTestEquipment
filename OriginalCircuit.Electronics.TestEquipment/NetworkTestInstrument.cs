using System.Net.Sockets;
using System.Net;
using System.Text;
using OriginalCircuit.Electronics.TestEquipment.Data;
using SixLabors.ImageSharp;
using System.Diagnostics;

namespace OriginalCircuit.Electronics.TestEquipment
{
    [Flags]
    public enum EventRegister
    {
        OperationComplete = 0,
        QueryError = 4,
        DeviceError = 8,
        ExecutionError = 16,
        CommandError = 32,
        PowerOn = 128
    }

    [Flags]
    public enum StatusRegister
    {
        ErrorQueue = 4,
        QuestionableSummary = 8,
        MessageAvailable = 16,
        StandardEventSummary = 32,
        MasterSummary = 64,
        OperationSummary = 128
    }

    public enum InstrumentType
    {
        Oscilloscope,
        Supply,
        Load,
        Multimeter,
        WaveformGenerator
    }

    public abstract class NetworkTestInstrument
    {
        TcpClient? tcpClient;
        bool lastConnectionState = false;

        public int ReadTimeout { get; set; } = 1000;
        public int SendTimeout { get; set; } = 500;

        public InstrumentType InstrumentType { get; set; }

        public InstrumentIdentity? Identity { get; private set; }

        public IPEndPoint? RemoteHost { get; private set; }

        public event EventHandler<bool>? ConnectionStateChanged;

        public NetworkTestInstrument(InstrumentType instrumentType, IPEndPoint? remoteHost = null)
        {
            InstrumentType = instrumentType;
            RemoteHost = remoteHost;
        }

        public async Task<bool> Connect(string ipAddress, int port = 5555)
        {
            if (String.IsNullOrEmpty(ipAddress))
                throw new ArgumentNullException(nameof(ipAddress));

            return await Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
        }

        public async Task<bool> Connect()
        {
            return await Connect(RemoteHost);
        }

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

        public async Task<bool> ReadBool()
        {
            string? data = (await ReadString())?.ToUpper();

            if (data == null)
                return false;

            if (data == "1" || data == "ON" || data == "YES")
                return true;

            return false;
        }

        public async Task<string> ReadString()
        {
            var data = await ReadData();
            if (data == null)
                return String.Empty;

            return Encoding.UTF8.GetString(data).Trim();
        }

        public async Task<int> ReadInt()
        {
            string response = (await ReadString()).Trim();

            if (int.TryParse(response, out int result))
                return result;
            else
                return int.MinValue;
        }

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

        public async Task ClearBuffer()
        {
            if (tcpClient is null || !IsConnected)
                return;

            if (tcpClient.Available > 0)
                await ReadData();
        }

        /// <summary>
        /// This command clears all status data structures in a device.
        /// </summary>
        /// <returns></returns>
        public async Task ClearStatus()
        {
            await SendCommand("*CLS");
        }

        public async Task SetStandardEventStatusEnable(EventRegister mask)
        {
            await SendCommand($"*ESE {(byte)mask}");
        }

        public async Task<EventRegister> QueryStandardEventStatusEnable()
        {
            await ClearBuffer();

            await SendCommand("*ESE?");

            int response = await ReadInt();
            return (EventRegister)response;
        }

        public async Task<EventRegister> QueryEventRegister()
        {
            await ClearBuffer();

            await SendCommand("*ESR?");

            int response = await ReadInt();
            return (EventRegister)response;
        }

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

        public async Task RegisterOperationComplete()
        {
            await SendCommand("*OPC");
        }

        public async Task<bool> QueryOperationComplete()
        {
            await ClearBuffer();

            await SendCommand("*OPC?");

            return await ReadBool();
        }

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

        public async Task ResetDevice()
        {
            await SendCommand("*RST");
        }

        public async Task SetServiceRequestEnable(StatusRegister mask)
        {
            await SendCommand($"*SRE {(byte)mask}");
        }

        public async Task<EventRegister> QueryServiceRequestEnable()
        {
            await ClearBuffer();

            await SendCommand("*SRE?");

            int response = await ReadInt();
            return (EventRegister)response;
        }

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
        /// <returns></returns>
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
        /// <returns></returns>
        public async Task WaitToContinue()
        {
            await SendCommand("*WAI");
        }

        /// <summary>
        /// Generates a trigger action.
        /// </summary>
        /// <returns></returns>
        public async Task TriggerInstrument()
        {
            await SendCommand("*TRG");
        }

        public void Dispose()
        {
            if (tcpClient != null)
                tcpClient.Dispose();
        }
    }
}