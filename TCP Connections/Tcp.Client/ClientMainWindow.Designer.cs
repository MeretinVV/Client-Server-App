namespace TCPConnections.TcpClient
{
    partial class ClientMainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sendMsgBtn = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.labelRes = new System.Windows.Forms.Label();
            this.msgTimer = new System.Windows.Forms.Timer(this.components);
            this.sendFileBtn = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sendMsgBtn
            // 
            this.sendMsgBtn.Location = new System.Drawing.Point(11, 11);
            this.sendMsgBtn.Margin = new System.Windows.Forms.Padding(2);
            this.sendMsgBtn.Name = "sendMsgBtn";
            this.sendMsgBtn.Size = new System.Drawing.Size(107, 19);
            this.sendMsgBtn.TabIndex = 0;
            this.sendMsgBtn.Text = "Send Message";
            this.sendMsgBtn.UseVisualStyleBackColor = true;
            this.sendMsgBtn.Click += new System.EventHandler(this.SendMsgBtnClick);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(122, 11);
            this.messageTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageTextBox.Size = new System.Drawing.Size(351, 134);
            this.messageTextBox.TabIndex = 1;
            this.messageTextBox.Text = "Your message";
            this.messageTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.messageTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // labelRes
            // 
            this.labelRes.AutoSize = true;
            this.labelRes.Location = new System.Drawing.Point(121, 147);
            this.labelRes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRes.MaximumSize = new System.Drawing.Size(350, 30);
            this.labelRes.Name = "labelRes";
            this.labelRes.Size = new System.Drawing.Size(0, 13);
            this.labelRes.TabIndex = 2;
            // 
            // msgTimer
            // 
            this.msgTimer.Interval = 2000;
            this.msgTimer.Tick += new System.EventHandler(this.OnMsgTimerTick);
            // 
            // sendFileBtn
            // 
            this.sendFileBtn.Location = new System.Drawing.Point(11, 34);
            this.sendFileBtn.Margin = new System.Windows.Forms.Padding(2);
            this.sendFileBtn.Name = "sendFileBtn";
            this.sendFileBtn.Size = new System.Drawing.Size(107, 19);
            this.sendFileBtn.TabIndex = 3;
            this.sendFileBtn.Text = "Send File";
            this.sendFileBtn.UseVisualStyleBackColor = true;
            this.sendFileBtn.Click += new System.EventHandler(this.SendFileBtn_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(122, 196);
            this.logBox.Margin = new System.Windows.Forms.Padding(2);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(351, 102);
            this.logBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server log";
            // 
            // ClientMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.sendFileBtn);
            this.Controls.Add(this.labelRes);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.sendMsgBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientMainWindow";
            this.Text = "Client application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendMsgBtn;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label labelRes;
        private System.Windows.Forms.Timer msgTimer;
        private System.Windows.Forms.Button sendFileBtn;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Label label1;
    }
}

