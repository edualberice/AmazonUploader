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

        private void button4_Click(object sender, EventArgs e)
        {
            this.loginProcess = new Thread(new ThreadStart(proccessLogin));
            this.loginProcess.IsBackground = true;
            this.loginProcess.Start();
        }

        private void proccessLogin()
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

            string webResponse = this.httpPostData(sendData);

            if (webResponse.Length <= 6)
            {
                this.treatHttpError(webResponse);
            }
            else
            {
                this.performLogin(webResponse);
            }
        }

        private string generateJson(Dictionary<string, string> fields)
        {
            var entries = fields.Select(d =>
                string.Format("\"{0}\":\"{1}\"", d.Key, string.Join(",", d.Value)));
            string json = "{" + string.Join(",", entries) + "}";

            return json;
        }

        private string httpPostData(Dictionary<string, string> data)
        {
            var httpData = new NameValueCollection();
            foreach(KeyValuePair<string, string> field in data)
            {
                httpData[field.Key] = field.Value;
            }

            using(var wb = new WebClient())
            {
                //*** O gateway de envio dos dados deve vir do banco local
                try
                {
                    var response = wb.UploadValues("http://www.odtsolucoes.com/odtged/api/consult/", "POST", httpData);
                    string webResponse = System.Text.Encoding.UTF8.GetString(response);

                    return webResponse;
                }
                catch (Exception e)
                {
                    //return "ERR6";
                    return e.Message;
                }

            }
        }

        private void treatHttpError(string webResponse)
        {
            string message = "";

            if(this.httpFunc.Equals("LGN"))
            {
                if(webResponse.Equals("INV"))
                {
                    message = "Usuário ou senha inválidos";
                }
                else
                {
                    message = "Erro na comunicação com o servidor(ERRO 2)";
                }

                if(loginErrorLabel.InvokeRequired)
                {
                    loginErrorLabel.Invoke(new MethodInvoker(delegate { loginErrorLabel.Text = message; }));
                }
            }
        }

        private void performLogin(string response)
        {
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
                this.checkUpdates();
            }
            else
            {
                if(loginErrorLabel.InvokeRequired)
                {
                    loginErrorLabel.Invoke(new MethodInvoker(delegate { loginErrorLabel.Text = "Usuário sem permissão de uso"; }));
                }
            }
        }

        private void checkUpdates()
        {
            bool error = false;
            if (loginErrorLabel.InvokeRequired)
            {
                loginErrorLabel.Invoke(new MethodInvoker(delegate { loginErrorLabel.Text = "Buscando atualizações"; }));
            }

            //*** Última data de atualização deve vir do banco local
            if (20140105105932145 != this.userData.contract.lastModified)
            {
                string json = JSON.getContractUpdate(this.userData.token);
                string key = Cryptography.randomString(32);
                string iv = Cryptography.randomString(32);

                string encryptedContent = Cryptography.encryptRJ256(json, key, iv);
                string sendData = encryptedContent + key + iv;

                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("CTT", sendData);

                string webResponse = this.httpPostData(data);

                if (webResponse.Length <= 6)
                {
                    error = true;
                    this.treatHttpError(webResponse);
                }
                else
                {
                    this.updateContractData(webResponse);
                }
            }
            else
            {
                //Carregar dados do contrato do banco
            }

            if(!error)
                this.showMainScreen();
        }

        private void updateContractData(string response)
        {
            if (loginErrorLabel.InvokeRequired)
            {
                loginErrorLabel.Invoke(new MethodInvoker(delegate { loginErrorLabel.Text = "Atualizando dados do contrato"; }));
            }

            string encryptedData = "";
            string key = response.Substring(0, 32);
            string iv = response.Substring(response.Length - 32);

            int keyIndex = response.IndexOf(key);
            encryptedData = response.Remove(keyIndex, key.Length);

            int ivIndex = encryptedData.IndexOf(iv);
            encryptedData = encryptedData.Remove(ivIndex, iv.Length);

            string data = Cryptography.decryptRJ256(encryptedData, key, iv);
            Contract contract = new Contract();
            contract = JSON.decodeContract(data);

            contract.contractId = this.userData.contract.contractId;
            contract.lastModified = this.userData.contract.lastModified;

            this.userData.contract = contract;
            this.userData.contract.saveContract();
        }

        private void showMainScreen()
        {
            if(loginPanel.InvokeRequired)
            {
                loginPanel.Invoke(new MethodInvoker(delegate { loginPanel.TabIndex = 25; loginPanel.Visible = false; }));
            }

            if(startSending.InvokeRequired)
            {
                startSending.Invoke(new MethodInvoker(delegate { startSending.TabIndex = 0; startSending.Visible = true; }));
            }
        }
    }
}
