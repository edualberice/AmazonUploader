using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace ODTGed_Uploader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loginErrorLabel.Text = "";
        }

        private bool keepAlive = true;
        private List<string> files = new List<string>();
        private int totalProgress = 0;
        private Thread sendFiles = null;
        private Thread loginProcess = null;

        private void button1_Click(object sender, EventArgs e)
        {
            this.sendFiles = new Thread(new ThreadStart(sendFilesToCloud));
            this.sendFiles.IsBackground = true;
            this.sendFiles.Start();
        }

        private void sendFilesToCloud()
        {
            this.keepAlive = true;

            if(sendingPanel.InvokeRequired)
            {
                sendingPanel.Invoke(new MethodInvoker(delegate { sendingPanel.Visible = true; }));
            }

            while (keepAlive)
            {
                this.files = new List<string>();

                if (sendFileLabel.InvokeRequired)
                {
                    sendFileLabel.Invoke(new MethodInvoker(delegate { sendFileLabel.Text = "Aguardando arquivos para enviar para a nuvem"; }));
                }

                if (sendFileProgress.InvokeRequired)
                {
                    sendFileProgress.Invoke(new MethodInvoker(delegate { sendFileProgress.Value = 0; }));
                }

                if(startSending.InvokeRequired)
                {
                    startSending.Invoke(new MethodInvoker(delegate { startSending.Visible = false; }));
                }

                if(stopSending.InvokeRequired)
                {
                    stopSending.Invoke(new MethodInvoker(delegate { stopSending.Visible = true; }));
                }

                this.getAllFilesFromFolder();
                if(this.files.Count > 0)
                    this.sendFilesToBucket();

                //***O tempo de espera deve vir do banco de dados
                System.Threading.Thread.Sleep(5000);
            }

            if(sendingPanel.InvokeRequired)
            {
                sendingPanel.Invoke(new MethodInvoker(delegate { sendingPanel.Visible = false; }));
            }

            if(stopSending.InvokeRequired)
            {
                stopSending.Invoke(new MethodInvoker(delegate { stopSending.Visible = false; }));
            }

            if(startSending.InvokeRequired)
            {
                startSending.Invoke(new MethodInvoker(delegate { startSending.Visible = true; }));
            }

            this.sendFiles.Join();
        }

        private void getAllFilesFromFolder()
        {
            //*** O caminho da pasta deve vir do banco de dados
            string[] filePaths = Directory.GetFiles("C:\\Teste");

            foreach(string file in filePaths)
            {
                this.files.Add(file);
            }
        }

        private void sendFilesToBucket()
        {
            //*** O nome do balde deve vir do banco de dados, a key também, pois a pasta será pré-definida
            string bucket = "odtbucket";
            string key = "";

            decimal progress = 0;
            decimal filesSent = 0;
            decimal totalFiles = (decimal)files.Count;

            int currentProgress = 0;

            if (button3.InvokeRequired)
            {
                button3.Invoke(new MethodInvoker(delegate { button3.Enabled = false; }));
            }

            foreach(String file in this.files)
            {
                if (sendFileLabel.InvokeRequired)
                {
                    sendFileLabel.Invoke(new MethodInvoker(delegate { sendFileLabel.Text = "Enviando arquivo "+(filesSent+1)+" de "+totalFiles; }));
                }

                TransferUtility fileTransferUtility = new TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.SAEast1));

                try
                {
                    TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        //*** O nome do balde vem do banco de dados
                        BucketName = bucket,
                        FilePath = file,
                        StorageClass = S3StorageClass.Standard,
                        //*** A key vem do banco de dados
                        Key = key,
                        CannedACL = S3CannedACL.Private,
                        //*** Deve ser definido se haverá encriptação de acordo com o banco de dados
                        ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
                    };
                    fileTransferUtility.Upload(fileTransferUtilityRequest);

                    filesSent++;
                    progress = filesSent / totalFiles * 100;
                    currentProgress = Convert.ToInt32(progress);

                    if(sendFileProgress.InvokeRequired)
                    {
                        sendFileProgress.Invoke(new MethodInvoker(delegate{sendFileProgress.Value = currentProgress;}));
                    }
                }
                catch (Exception exception)
                {
                    //Something went wrong
                }

                try
                {
                    string[] filePath = file.Split('\\');
                    int last = filePath.Length - 1;

                    //*** FROM deve vir do banco de dados
                    string from = @"C:\Teste\" + filePath[last];
                    //*** TO deve vir do banco de dados
                    string to = @"C:\Teste\Processados\" + filePath[last];
                    
                    File.Move(from, to);
                } 
                catch(Exception e)
                {
                    //Could not move the files
                }
            }

            if(sendFileLabel.InvokeRequired)
            {
                sendFileLabel.Invoke(new MethodInvoker(delegate { sendFileLabel.Text = "Envio realizado com sucesso"; }));
            }

            if(button3.InvokeRequired)
            {
                button3.Invoke(new MethodInvoker(delegate { button3.Enabled = true; }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Configurações
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.keepAlive = false;
        }

        private void proccessLogin()
        {
            string user = username.Text;
            string pass = password.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.loginProcess = new Thread(new ThreadStart(proccessLogin));
        }
    }
}
