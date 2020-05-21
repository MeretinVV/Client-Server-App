using System;
using TCPConnections.Library.Server;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TCPConnections.TcpServer
{
    class EnteringPointServer
    {
        static void Main(string[] args)
        {
           try
            {
                Server server = new Server();
                List<Task> listeners = new List<Task>();

                listeners.Add(Task.Run(() => server.TurnOnFileListener()));
                listeners.Add(Task.Run(() => server.TurnOnMessageListener()));

                Task.WaitAll(listeners.ToArray());

                //turn off by command: server.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
