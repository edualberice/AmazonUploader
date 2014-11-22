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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.sendFileProgress = new System.Windows.Forms.ProgressBar();
            this.sendingPanel = new System.Windows.Forms.Panel();
            this.sendingStatus = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.sendFileLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.loginErrorLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startSending = new System.Windows.Forms.Panel();
            this.helloLabel2 = new System.Windows.Forms.Label();
            this.helloLabel = new System.Windows.Forms.Label();
            this.configPanel = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cloudTarget = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.localTarget = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.localFiles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sendingPanel.SuspendLayout();
            this.loginPanel.SuspendLayout();
            this.startSending.SuspendLayout();
            this.configPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(264, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Enviar Arquivos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sendFileProgress
            // 
            this.sendFileProgress.Location = new System.Drawing.Point(11, 26);
            this.sendFileProgress.Name = "sendFileProgress";
            this.sendFileProgress.Size = new System.Drawing.Size(260, 33);
            this.sendFileProgress.TabIndex = 1;
            // 
            // sendingPanel
            // 
            this.sendingPanel.Controls.Add(this.sendingStatus);
            this.sendingPanel.Controls.Add(this.button3);
            this.sendingPanel.Controls.Add(this.sendFileLabel);
            this.sendingPanel.Controls.Add(this.sendFileProgress);
            this.sendingPanel.Location = new System.Drawing.Point(1, 70);
            this.sendingPanel.Name = "sendingPanel";
            this.sendingPanel.Size = new System.Drawing.Size(283, 138);
            this.sendingPanel.TabIndex = 2;
            this.sendingPanel.Visible = false;
            // 
            // sendingStatus
            // 
            this.sendingStatus.AutoSize = true;
            this.sendingStatus.Location = new System.Drawing.Point(11, 71);
            this.sendingStatus.Name = "sendingStatus";
            this.sendingStatus.Size = new System.Drawing.Size(99, 13);
            this.sendingStatus.TabIndex = 7;
            this.sendingStatus.Text = "Enviando arquivo...";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(11, 96);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(260, 33);
            this.button3.TabIndex = 0;
            this.button3.Text = "Interromper Envio";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(264, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Configurações";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.loginPanel.Size = new System.Drawing.Size(283, 156);
            this.loginPanel.TabIndex = 0;
            // 
            // loginErrorLabel
            // 
            this.loginErrorLabel.Location = new System.Drawing.Point(3, 115);
            this.loginErrorLabel.Name = "loginErrorLabel";
            this.loginErrorLabel.Size = new System.Drawing.Size(277, 33);
            this.loginErrorLabel.TabIndex = 5;
            this.loginErrorLabel.Text = "Erro ao logar. Usuário ou senha incorretos.";
            this.loginErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuário:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(100, 80);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 33);
            this.button4.TabIndex = 4;
            this.button4.Text = "Acessar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(68, 44);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(202, 20);
            this.password.TabIndex = 3;
            this.password.UseSystemPasswordChar = true;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(68, 15);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(202, 20);
            this.username.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Senha:";
            // 
            // startSending
            // 
            this.startSending.Controls.Add(this.helloLabel2);
            this.startSending.Controls.Add(this.button2);
            this.startSending.Controls.Add(this.helloLabel);
            this.startSending.Controls.Add(this.button1);
            this.startSending.Location = new System.Drawing.Point(1, 52);
            this.startSending.Name = "startSending";
            this.startSending.Size = new System.Drawing.Size(283, 156);
            this.startSending.TabIndex = 4;
            this.startSending.Visible = false;
            // 
            // helloLabel2
            // 
            this.helloLabel2.AutoSize = true;
            this.helloLabel2.Location = new System.Drawing.Point(16, 38);
            this.helloLabel2.Name = "helloLabel2";
            this.helloLabel2.Size = new System.Drawing.Size(192, 13);
            this.helloLabel2.TabIndex = 5;
            this.helloLabel2.Text = "Seja bem-vindo ao ODTDrive Uploader";
            // 
            // helloLabel
            // 
            this.helloLabel.AutoSize = true;
            this.helloLabel.Location = new System.Drawing.Point(16, 7);
            this.helloLabel.Name = "helloLabel";
            this.helloLabel.Size = new System.Drawing.Size(110, 13);
            this.helloLabel.TabIndex = 5;
            this.helloLabel.Text = "Olá Eduardo Alberice!";
            // 
            // configPanel
            // 
            this.configPanel.Controls.Add(this.button6);
            this.configPanel.Controls.Add(this.button5);
            this.configPanel.Controls.Add(this.cloudTarget);
            this.configPanel.Controls.Add(this.label5);
            this.configPanel.Controls.Add(this.localTarget);
            this.configPanel.Controls.Add(this.label4);
            this.configPanel.Controls.Add(this.localFiles);
            this.configPanel.Controls.Add(this.label3);
            this.configPanel.Location = new System.Drawing.Point(1, 52);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(283, 156);
            this.configPanel.TabIndex = 5;
            this.configPanel.Visible = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(60, 124);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "Voltar";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(141, 124);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "Salvar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cloudTarget
            // 
            this.cloudTarget.Location = new System.Drawing.Point(6, 99);
            this.cloudTarget.Name = "cloudTarget";
            this.cloudTarget.Size = new System.Drawing.Size(274, 20);
            this.cloudTarget.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Destino na nuvem(em branco para a raiz):";
            // 
            // localTarget
            // 
            this.localTarget.Location = new System.Drawing.Point(6, 60);
            this.localTarget.Name = "localTarget";
            this.localTarget.Size = new System.Drawing.Size(274, 20);
            this.localTarget.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pasta de destino:";
            // 
            // localFiles
            // 
            this.localFiles.Location = new System.Drawing.Point(6, 21);
            this.localFiles.Name = "localFiles";
            this.localFiles.Size = new System.Drawing.Size(274, 20);
            this.localFiles.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Local dos arquivos:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(273, 50);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 211);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.startSending);
            this.Controls.Add(this.configPanel);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.sendingPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ODTDrive Uploader";
            this.sendingPanel.ResumeLayout(false);
            this.sendingPanel.PerformLayout();
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.startSending.ResumeLayout(false);
            this.startSending.PerformLayout();
            this.configPanel.ResumeLayout(false);
            this.configPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar sendFileProgress;
        private System.Windows.Forms.Panel sendingPanel;
        private System.Windows.Forms.Label sendFileLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label loginErrorLabel;
        private System.Windows.Forms.Panel startSending;
        private System.Windows.Forms.Label helloLabel2;
        private System.Windows.Forms.Label helloLabel;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox cloudTarget;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox localTarget;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox localFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label sendingStatus;
    }
}

