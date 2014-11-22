using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ODTGed_Uploader
{
    class Config
    {
        public int configId { get; set; }
        public int threadSleep { get; set; }
        public string sourceFolder { get; set; }
        public string targetCloudFolder { get; set; }
        public string targetLocalFolder { get; set; }
        public string consultGateway { get; set; }
        public double lastModified { get; set; }

        public string getConfigs(string token)
        {
            string status = "OK";
            string json = JSON.getConfigUpdate(token);
            string key = Cryptography.randomString(32);
            string iv = Cryptography.randomString(32);

            string encryptedContent = Cryptography.encryptRJ256(json, key, iv);
            string sendData = encryptedContent + key + iv;

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("CTT", sendData);

            string webResponse = HttpComm.httpPostData(data, this.consultGateway);

            if (webResponse.Length <= 6)
            {
                status = HttpComm.treatHttpError(webResponse, "CCF");
            }
            else
            {
                string encryptedData = "";
                string decodeKey = webResponse.Substring(0, 32);
                string decodeIv = webResponse.Substring(webResponse.Length - 32);

                int keyIndex = webResponse.IndexOf(decodeKey);
                encryptedData = webResponse.Remove(keyIndex, decodeKey.Length);

                int ivIndex = encryptedData.IndexOf(decodeIv);
                encryptedData = encryptedData.Remove(ivIndex, decodeIv.Length);

                string configData = Cryptography.decryptRJ256(encryptedData, decodeKey, decodeIv);
                Config c = new Config();
                c = JSON.decodeConfig(configData);

                if(c.lastModified > this.lastModified)
                {
                    this.consultGateway = c.consultGateway;
                    this.threadSleep = c.threadSleep;
                    this.lastModified = c.lastModified;

                    try
                    { 
                        this.saveConfigs(); 
                    }
                    catch(Exception e)
                    {
                        status = "Impossível salvar as configurações";
                    }                    
                }
            }
            return status;
        }

        public string loadConfigs()
        {
            string status = "OK";
            try
            {
                string connString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "odtdrive.db";
                string query = "SELECT * FROM Configs";

                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            if (dr.HasRows)
                            {
                                this.threadSleep = Convert.ToInt32(dr.GetValue(0));
                                this.sourceFolder = dr.GetValue(1).ToString();
                                this.targetCloudFolder = dr.GetValue(2).ToString();
                                this.targetLocalFolder = dr.GetValue(3).ToString();
                                this.consultGateway = dr.GetValue(4).ToString();
                                this.lastModified = Convert.ToDouble(dr.GetValue(5));
                                this.configId = Convert.ToInt32(dr.GetValue(6));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                status = "Impossível carregar configurações";
            }

            return status;
        }

        public void saveConfigs()
        {
            string connString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "odtdrive.db";
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                StringBuilder saveStatement = new StringBuilder();

                conn.Open();
                string query = "SELECT * FROM Configs WHERE id = 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    dr.Read();

                    if (dr.HasRows)
                    {
                        saveStatement.Append("UPDATE Configs SET ");
                        saveStatement.Append("thread_sleep = " + this.threadSleep + ", ");
                        saveStatement.Append("source_folder = '" + this.sourceFolder + "', ");
                        saveStatement.Append("target_cloud_folder = '" + this.targetCloudFolder + "', ");
                        saveStatement.Append("target_local_folder = '" + this.targetLocalFolder + "', ");
                        saveStatement.Append("consult_gateway = '" + this.consultGateway + "', ");
                        saveStatement.Append("last_modified = " + this.lastModified + " ");
                        saveStatement.Append(" WHERE id = 1");
                    }
                    else
                    {
                        saveStatement.Append("INSERT INTO Configs (thread_sleep, source_folder, target_cloud_folder, target_local_folder, consult_gateway, last_modified) VALUES (");
                        saveStatement.Append(this.threadSleep + ",'" + this.sourceFolder + "', '" + this.targetCloudFolder + "', '" + this.targetLocalFolder + "', '" + this.consultGateway + "', " + this.lastModified + ")");
                    }
                }

                using (SQLiteCommand cmd = new SQLiteCommand(saveStatement.ToString(), conn))
                {
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
