using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace TCPConnections.Library.Server
{
    public class Server
    {
        private TcpListener _messageServerListener = new TcpListener(IPAddress.Loopback, 8080);
        private TcpListener _fileServerListener = new TcpListener(IPAddress.Loopback, 8081);

        private int _fileCount = 0;

        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;

        public Server()
        {
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
        }

        /// <summary>
        /// Stops all listeners and shuts down the server
        /// </summary>
        public void Stop() => _cancelTokenSource.Cancel();
        
        /// <summary>
        /// Turn off a specific listener
        /// </summary>
        /// <param name="listener">Listener to turn off</param>
        /// <returns>Whether operation was successful or not</returns>
        private bool TurnOffListener(TcpListener listener)
        {
            try
            {
                if (listener != null) listener.Stop();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot turn off listener : {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Turns on message listener
        /// </summary>
        public async Task TurnOnMessageListener()
        {
            try
            {
                if (_messageServerListener != null) _messageServerListener.Start();

                while (!_token.IsCancellationRequested)
                {
                    Console.WriteLine("Waiting for messages...");
                    OperationResult res;
                    TcpClient client = _messageServerListener.AcceptTcpClient();
                    
                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    using (StreamReader reader = new StreamReader(client.GetStream()))
                    {
                        res = await ReceiveMessageFromClient(reader);

                        if (res.Result == Result.Fail)
                        {
                            Console.WriteLine($"Unexpected error: {res.Message}");
                            SendMessageToClient(writer, $"Unexpected error: { res.Message}");
                        }
                        else
                        {
                            Console.WriteLine($"New message from client: {res.Message}");
                            SendMessageToClient(writer, $"New message from client: {res.Message}");
                        }
                    }
                    client.Close();
                }

                TurnOffListener(_messageServerListener);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot turn on message listener: {e.Message}");
            }
        }

        /// <summary>
        /// Turns on file listener
        /// </summary>
        public async Task TurnOnFileListener()
        {
            try
            {
                if (_fileServerListener != null) _fileServerListener.Start();

                while (!_token.IsCancellationRequested)
                {
                    Console.WriteLine("Waiting for files...");
                    OperationResult res;
                    TcpClient client = _fileServerListener.AcceptTcpClient();

                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    using (NetworkStream reader = client.GetStream())
                    {
                        res = await ReceiveFileFromClient(reader);

                        if (res.Result == Result.Fail)
                        {
                            Console.WriteLine($"Unexpected error: {res.Message}");
                            SendMessageToClient(writer, $"Unexpected error: { res.Message}");
                        }
                        else
                        {
                            Console.WriteLine(res.Message);
                            SendMessageToClient(writer, res.Message);
                        }
                    }
                    client.Close();
                }
                TurnOffListener(_fileServerListener);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot turn on file listener: {e.Message}");
            }
        }
        
        /// <summary>
        /// Attempts to recieve a message from a client
        /// </summary>
        /// <param name="stream">Stream to recieve message through</param>
        public async Task<OperationResult> ReceiveMessageFromClient(StreamReader stream)
        {
            try
            {
                string recievedMessage = stream.ReadLine();
                return new OperationResult(Result.OK, recievedMessage.ToString());
            }
            catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.Message);
            }
        }

        /// <summary>
        /// Attempts to send a message to a client
        /// </summary>
        /// <param name="stream">Stream to send message through</param>
        /// <param name="message">Message to send</param>
        /// <returns>Result of an attempt</returns>
        public OperationResult SendMessageToClient(StreamWriter stream, string message)
        {
            try
            {
                stream.WriteLine(message);
                stream.Flush();
                return new OperationResult(Result.OK, string.Empty);
            }
            catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.Message);
            }
        }

        /// <summary>
        /// Attempts to recieve a file from a client
        /// </summary>
        /// <param name="stream">Stream to recieve file through</param>
        /// <returns>Result of an attempt</returns>
        public async Task<OperationResult> ReceiveFileFromClient(NetworkStream stream)
        {
            try
            {
                byte[] data = new byte[256];
                string date = DateTime.Now.ToShortDateString();

                if (!Directory.Exists(date)) Directory.CreateDirectory(date);
                byte[] header = new byte[15];
                stream.Read(header, 0, header.Length);

                // Метод Replace принимает такой странный аргумент потому что нулевые байты преобразуются 
                // в какой-то странный символ, который выглядит как пробел, но не удаляется при замене пробела

                string extention = Encoding.UTF8.GetString(header, 0, header.Length)
                    .Replace(Encoding.UTF8.GetString(new byte[1], 0 ,1), string.Empty);

                Interlocked.Increment(ref _fileCount);
                using (FileStream fs = File.Create($"{date}/file{_fileCount}{extention}"))
                {
                    do
                    {
                        int bytes = stream.Read(data, 0, data.Length);
                        fs.Write(data, 0, bytes);
                    }
                    while (stream.DataAvailable);
                }

                return new OperationResult(Result.OK, "File recieved!");
            }
            catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.Message);
            }
        }
    }
}