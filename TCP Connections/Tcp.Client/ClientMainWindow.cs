using System;
using System.Windows.Forms;
using TCPConnections.Library.Client;
using TCPConnections.Library;

namespace TCPConnections.TcpClient
{
    public partial class ClientMainWindow : Form
    {
        private Client client = new Client();

        public ClientMainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show message on top of the log textbox, the message fades away in 2 seconds
        /// </summary>
        /// <param name="message">The message to show</param>
        private void ShowMessage(string message)
        {
            labelRes.Text = message;
            msgTimer.Start();
        }
        
        private void SendMsgBtnClick(object sender, EventArgs e)
        {
            if (messageTextBox.Text == string.Empty || messageTextBox.Text == "Your message") return;
            if (!client.CanConnectToServer())
            {
                ShowMessage("Server is busy! Try again later.");
                return;
            }

            OperationResult res = client.SendMessageToServerWithLog(messageTextBox.Text);
            if(res.Result == Result.OK)
            {
                messageTextBox.Text = "Your message";
                ShowMessage("Message was sent succefully!");

                logBox.AppendText(res.Message + "\r\n");
            }
            else MessageBox.Show($"Cannot send the message to the server: {res.Message}");
        }

        private void OnMsgTimerTick(object sender, EventArgs e)
        {
            labelRes.Text = string.Empty;
            msgTimer.Stop();
        }

        private void SendFileBtn_Click(object sender, EventArgs e)
        {
            if (!client.CanConnectToServer())
            {
                ShowMessage("Server is busy! Try again later.");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All files | *.*"; 
            dialog.Multiselect = false; 

            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                OperationResult res = client.SendFileToServerWithLog(dialog.FileName);
                if (res.Result == Result.OK) ShowMessage("File was sent succefully!");
                else MessageBox.Show($"Cannot send the file to the server: {res.Message}");


                logBox.AppendText(res.Message + "\r\n");
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (messageTextBox.Text == "Your message") messageTextBox.Text = string.Empty;
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (messageTextBox.Text == string.Empty) messageTextBox.Text = "Your message";
        }
    }
}
