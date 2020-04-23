using System;
using System.Windows.Forms;

namespace TCPConnections.TcpClient
{
    static class EnteringPointClient
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClientMainWindow());
        }
    }
}
