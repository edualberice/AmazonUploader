using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Security.Cryptography;
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
        private Thread sendFiles = null;
        private Thread loginProcess = null;
        private string hashKey = "ODT SOLUÇÕES EmpresariAIS";
        private string httpFunc = "";
        private User userData = null;
        private Config configs = null;

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

                System.Threading.Thread.Sleep(this.configs.threadSleep);
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
            string[] filePaths = Directory.GetFiles(this.configs.sourceFolder);

            foreach(string file in filePaths)
            {
                this.files.Add(file);
            }
        }

        private void sendFilesToBucket()
        {
            string bucket = this.userData.contract.bucketName;
            string key = this.configs.targetCloudFolder.Equals(" ") ? "" : this.configs.targetCloudFolder;

            decimal progress = 0;
            decimal filesSent = 0;
            decimal totalFiles = (decimal)files.Count;

            int currentProgress = 0;

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
                        BucketName = bucket,
                        FilePath = file,
                        StorageClass = S3StorageClass.Standard,
                        Key = key,
                        CannedACL = S3CannedACL.Private,
                    };

                    if(this.userData.contract.encryption > 0)
                    {
                        fileTransferUtilityRequest.ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256;
                    }

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
                    this.updateDialogLabel("Erro ao enviar o arquivo para nuvem");
                }

                try
                {
                    string[] filePath = file.Split('\\');
                    int last = filePath.Length - 1;

                    string from = this.configs.sourceFolder + filePath[last];
                    string to = this.configs.targetLocalFolder + filePath[last];
                    
                    File.Move(from, to);
                } 
                catch(Exception e)
                {
                    this.updateDialogLabel("Impossível mover o arquivo para a pasta destino");
                }

                if (!keepAlive)
                    break;
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
            if(button3.InvokeRequired)
            {
                button3.Invoke(new MethodInvoker(delegate { button3.Enabled = false; }));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.loginProcess = new Thread(new ThreadStart(proccessLogin));
            this.loginProcess.IsBackground = true;
            this.loginProcess.Start();
        }

        private void proccessLogin()
        {
            this.configs = new Config();

            this.updateDialogLabel("Carregando configurações");

            string status = this.configs.loadConfigs();

            if (status.Equals("OK"))
            {
                this.httpFunc = "LGN";

                if (loginErrorLabel.InvokeRequired)
                {
                    loginErrorLabel.Invoke(new MethodInvoker(delegate { loginErrorLabel.Text = "Autenticando usuário"; }));
                }

                string user = username.Text;
                string pass = password.Text;

                byte[] keyBytes = Encoding.UTF8.GetBytes(this.hashKey);
                byte[] passBytes = Encoding.UTF8.GetBytes(pass);

                var md5 = new HMACMD5(keyBytes);
                byte[] hashedBytesPass = md5.ComputeHash(passBytes);
                string md5Pass = BitConverter.ToString(hashedBytesPass).Replace("-", "").ToLower();

                Dictionary<string, string> fields = new Dictionary<string, string>();
                fields.Add("FNC", this.httpFunc);
                fields.Add("USR", user);
                fields.Add("PWD", md5Pass);

                string json = this.generateJson(fields);

                string randomKey = Cryptography.randomString(32);
                string randomIV = Cryptography.randomString(32);

                string encryptedData = Cryptography.encryptRJ256(json, randomKey, randomIV);
                encryptedData += randomKey;
                encryptedData += randomIV;

                Dictionary<string, string> sendData = new Dictionary<string, string>();
                sendData.Add("CTT", encryptedData);

                string webResponse = HttpComm.httpPostData(sendData, this.configs.consultGateway);

                if (webResponse.Length <= 6)
                {
                    string message = HttpComm.treatHttpError(webResponse, this.httpFunc);
                    this.updateDialogLabel(message);
                }
                else
                {
                    this.performLogin(webResponse);
                }
            }
            else
            {
                this.updateDialogLabel(status);
            }
        }
        //REMOVER
        private string generateJson(Dictionary<string, string> fields)
        {
            var entries = fields.Select(d =>
                string.Format("\"{0}\":\"{1}\"", d.Key, string.Join(",", d.Value)));
            string json = "{" + string.Join(",", entries) + "}";

            return json;
        }

        private void performLogin(string response)
        {
            string status = "";
            string encryptedData = "";
            string key = response.Substring(0, 32);
            string iv = response.Substring(response.Length - 32);

            int keyIndex = response.IndexOf(key);
            encryptedData = response.Remove(keyIndex, key.Length);

            int ivIndex = encryptedData.IndexOf(iv);
            encryptedData = encryptedData.Remove(ivIndex, iv.Length);

            string data = Cryptography.decryptRJ256(encryptedData, key, iv);
            this.userData = JSON.decodeUser(data);

            if(this.userData.roles["write"])
            {
                this.updateDialogLabel("Buscando configurações");

                status = this.configs.getConfigs(this.userData.token);

                if (status.Equals("OK"))
                {
                    this.updateDialogLabel("Carregando contrato");

                    status = this.userData.contract.loadContract();
                    if (!status.Equals("OK"))
                    {
                        if (status.Equals("NOTFOUND"))
                        {
                            this.updateDialogLabel("Impossível carregar os dados locais");
                        }
                        else if (status.Equals("ERROR"))
                        {
                            this.updateDialogLabel("Um erro ocorreu ao ler os dados do contrato");
                        }
                    }
                    else
                    {
                        this.updateDialogLabel("Buscando atualizações");

                        status = this.userData.contract.getUpdates(this.userData.token, this.configs.consultGateway);

                        if (status.Equals("OK"))
                            this.showMainScreen();
                        else
                            this.updateDialogLabel("Servidor incomunicável");
                    }
                }
                else
                {
                    this.updateDialogLabel(status);
                }
            }
            else
            {
                this.updateDialogLabel("Usuário sem permissão de uso");
            }
        }

        private void showMainScreen()
        {
            if(loginPanel.InvokeRequired)
            {
                loginPanel.Invoke(new MethodInvoker(delegate { loginPanel.TabIndex = 25; loginPanel.Visible = false; }));
            }

            if (helloLabel.InvokeRequired)
            {
                helloLabel.Invoke(new MethodInvoker(delegate { helloLabel.Text = "Olá " + this.userData.firstName + " " + this.userData.lastName+"!"; }));
            }

            if(startSending.InvokeRequired)
            {
                startSending.Invoke(new MethodInvoker(delegate { startSending.TabIndex = 0; startSending.Visible = true; }));
            }
        }

        private void updateDialogLabel(string error)
        {
            if(loginErrorLabel.InvokeRequired)
            {
                loginErrorLabel.Invoke(new MethodInvoker(delegate { loginErrorLabel.Text = error; }));
            }
        }
    }
}
