namespace ODTGed_Uploader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.sendFileProgress = new System.Windows.Forms.ProgressBar();
            this.sendingPanel = new System.Windows.Forms.Panel();
            this.sendFileLabel = new System.Windows.Forms.Label();
            this.startSending = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.stopSending = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.loginErrorLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sendingPanel.SuspendLayout();
            this.startSending.SuspendLayout();
            this.stopSending.SuspendLayout();
            this.loginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Enviar Arquivos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sendFileProgress
            // 
            this.sendFileProgress.Location = new System.Drawing.Point(11, 20);
            this.sendFileProgress.Name = "sendFileProgress";
            this.sendFileProgress.Size = new System.Drawing.Size(260, 23);
            this.sendFileProgress.TabIndex = 1;
            // 
            // sendingPanel
            // 
            this.sendingPanel.Controls.Add(this.sendFileLabel);
            this.sendingPanel.Controls.Add(this.sendFileProgress);
            this.sendingPanel.Location = new System.Drawing.Point(1, 70);
            this.sendingPanel.Name = "sendingPanel";
            this.sendingPanel.Size = new System.Drawing.Size(283, 46);
            this.sendingPanel.TabIndex = 2;
            this.sendingPanel.Visible = false;
            // 
            // sendFileLabel
            // 
            this.sendFileLabel.AutoSize = true;
            this.sendFileLabel.Location = new System.Drawing.Point(11, 4);
            this.sendFileLabel.Name = "sendFileLabel";
            this.sendFileLabel.Size = new System.Drawing.Size(35, 13);
            this.sendFileLabel.TabIndex = 2;
            this.sendFileLabel.Text = "label1";
            // 
            // startSending
            // 
            this.startSending.Controls.Add(this.button2);
            this.startSending.Controls.Add(this.button1);
            this.startSending.Location = new System.Drawing.Point(0, 0);
            this.startSending.Name = "startSending";
            this.startSending.Size = new System.Drawing.Size(283, 30);
            this.startSending.TabIndex = 3;
            this.startSending.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(143, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Configurações";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // stopSending
            // 
            this.stopSending.Controls.Add(this.button3);
            this.stopSending.Controls.Add(this.startSending);
            this.stopSending.Location = new System.Drawing.Point(1, 119);
            this.stopSending.Name = "stopSending";
            this.stopSending.Size = new System.Drawing.Size(283, 30);
            this.stopSending.TabIndex = 1;
            this.stopSending.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(11, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(260, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Interromper Envio";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // loginPanel
            // 
            this.loginPanel.Controls.Add(this.loginErrorLabel);
            this.loginPanel.Controls.Add(this.label1);
            this.loginPanel.Controls.Add(this.button4);
            this.loginPanel.Controls.Add(this.password);
            this.loginPanel.Controls.Add(this.username);
            this.loginPanel.Controls.Add(this.label2);
            this.loginPanel.Location = new System.Drawing.Point(1, 52);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(283, 97);
            this.loginPanel.TabIndex = 0;
            // 
            // loginErrorLabel
            // 
            this.loginErrorLabel.Location = new System.Drawing.Point(43, 80);
            this.loginErrorLabel.Name = "loginErrorLabel";
            this.loginErrorLabel.Size = new System.Drawing.Size(208, 13);
            this.loginErrorLabel.TabIndex = 5;
            this.loginErrorLabel.Text = "Erro ao logar. Usuário ou senha incorretos.";
            this.loginErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuário:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(105, 53);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Acessar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(68, 30);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(202, 20);
            this.password.TabIndex = 3;
            this.password.UseSystemPasswordChar = true;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(68, 4);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(202, 20);
            this.username.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Senha:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 154);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.sendingPanel);
            this.Controls.Add(this.stopSending);
            this.Name = "Form1";
            this.Text = "ODTDrive Uploader";
            this.sendingPanel.ResumeLayout(false);
            this.sendingPanel.PerformLayout();
            this.startSending.ResumeLayout(false);
            this.stopSending.ResumeLayout(false);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar sendFileProgress;
        private System.Windows.Forms.Panel sendingPanel;
        private System.Windows.Forms.Label sendFileLabel;
        private System.Windows.Forms.Panel startSending;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel stopSending;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label loginErrorLabel;
    }
}

