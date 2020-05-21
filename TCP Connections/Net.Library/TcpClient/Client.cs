using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace TCPConnections.Library.Client
{
    public class Client
    {
        private TcpClient _tcpClient;

        /// <summary>
        /// Attempts to recieves a message from the server
        /// </summary>
        /// <param name="stream">Stream with the message</param>
        /// <returns>Result of an attempt</returns>
        public  OperationResult ReceiveMessageFromServer(StreamReader stream)
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
        /// Attempts to send a message to the server
        /// </summary>
        /// <param name="stream">Stream to send the message through</param>
        /// <param name="message">The message to send</param>
        /// <returns>Result of an attempt</returns>
        public OperationResult SendMessageToServer(StreamWriter stream, string message)
        {
            try
            {
                stream.WriteLine(message);
                stream.Flush();
                return new OperationResult(Result.OK, string.Empty) ;
            }
            catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.Message);
            }
        }

        /// <summary>
        /// Attempts to send a message to the server and then recieve the answer with the log confirming the result 
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <returns>Result of an attempt with answer from the server</returns>
        public OperationResult SendMessageToServerWithLog(string message)
        {
            _tcpClient = new TcpClient("127.0.0.1", 8080);
            OperationResult res;

            using (StreamWriter writer = new StreamWriter(_tcpClient.GetStream()))
            using (StreamReader reader = new StreamReader(_tcpClient.GetStream()))
            {
                res = SendMessageToServer(writer, message);
                res = ReceiveMessageFromServer(reader);
            }

            _tcpClient.Close();

            return res;

        }

        /// <summary>
        /// Attempts to send file to the server and then recieve the answer with the log confirming the result 
        /// </summary>
        /// <param name="filePath">Path to the file to send</param>
        /// <returns>Result of an attempt with answer from the server</returns>
        public OperationResult SendFileToServerWithLog(string filePath)
        {
            _tcpClient = new TcpClient("127.0.0.1", 8081);
            OperationResult res;

            using (NetworkStream writer = _tcpClient.GetStream())
            using (StreamReader reader = new StreamReader(_tcpClient.GetStream()))
            {
                res = SendFileToServer(writer, filePath);
                res = ReceiveMessageFromServer(reader);
            }

            _tcpClient.Close();

            return res;
        }

        /// <summary>
        /// Attempts to send file to the server
        /// </summary>
        /// <param name="stream">Stream to send file through</param>
        /// <param name="filePath">Path to the file to send</param>
        /// <returns>Result of an attempt</returns>
        public OperationResult SendFileToServer(NetworkStream stream, string filePath)
        {
            try
            {
                byte[] header = new byte[15];
                byte[] extention = Encoding.UTF8.GetBytes(Path.GetExtension(filePath));
                for (int i = 0; i < extention.Length; ++i) header[i] = extention[i];
                stream.Write(header, 0, header.Length);

                byte[] data = File.ReadAllBytes(filePath);
                stream.Write(data, 0, data.Length);
                                
                return new OperationResult(Result.OK, string.Empty);
            }
            catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.Message);
            }
        }
    }
}
