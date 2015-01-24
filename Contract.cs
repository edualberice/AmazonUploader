using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ODTGed_Uploader
{
    class Contract
    {
        public int contractId { get; set; }
        public string contractName { get; set; }
        public double diskSpace { get; set; }
        public double diskUsed { get; set; }
        public int encryption { get; set; }
        public int versioning { get; set; }
        public string bucketName { get; set; }
        public string targetFolder { get; set; }
        public double lastModified { get; set; }
        public double lastModifiedWeb { get; set; }

        public void saveContract()
        {
            string connString = "Data Source="+AppDomain.CurrentDomain.BaseDirectory+"odtdrive.db";
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                StringBuilder saveStatement = new StringBuilder();

                conn.Open();
                string query = "SELECT * FROM Contract WHERE id = " + this.contractId;
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    dr.Read();                   

                    if (dr.HasRows)
                    {
                        saveStatement.Append("UPDATE Contract SET ");
                        saveStatement.Append("contract_name = '" + this.contractName + "', ");
                        saveStatement.Append("disk_space = " + this.diskSpace + ", ");
                        saveStatement.Append("usage = " + this.diskUsed + ", ");
                        saveStatement.Append("encryption = " + this.encryption + ", ");
                        saveStatement.Append("versioning = " + this.versioning + ", ");
                        saveStatement.Append("bucket = '" + this.bucketName + "', ");
                        saveStatement.Append("last_modified = " + this.lastModified + ", ");
                        saveStatement.Append("target_folder = '" + this.targetFolder + "' ");
                        saveStatement.Append(" WHERE id = " + this.contractId);
                    }
                    else
                    {
                        saveStatement.Append("INSERT INTO Contract (id, contract_name, disk_space, usage, encryption, versioning, bucket, last_modified, target_folder) VALUES (");
                        saveStatement.Append(this.contractId + ",'" + this.contractName + "', " + this.diskSpace + ", " + this.diskUsed + ", " + this.encryption + ", " + this.versioning + ", '" + this.bucketName + "', " + this.lastModified + "', '" + this.targetFolder + "')");
                    }
                }

                using (SQLiteCommand cmd = new SQLiteCommand(saveStatement.ToString(), conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string getUpdates(string token, string url)
        {
            string status = "";
            string json = JSON.getContractUpdate(token);
            string key = Cryptography.randomString(32);
            string iv = Cryptography.randomString(32);

            string encryptedContent = Cryptography.encryptRJ256(json, key, iv);
            string sendData = encryptedContent + key + iv;

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("CTT", sendData);

            string webResponse = HttpComm.httpPostData(data, url);

            if (webResponse.Length <= 6)
            {
                status = HttpComm.treatHttpError(webResponse, "UCT");
            }
            else
            {
                if (this.lastModified != this.lastModifiedWeb)
                {
                    status = this.updateContractData(webResponse);
                }
                else
                {
                    status = "OK";
                }
            }
            return status;
        }

        private string updateContractData(string response)
        {
            string status = "OK";
            string encryptedData = "";
            string key = response.Substring(0, 32);
            string iv = response.Substring(response.Length - 32);

            try
            {

                int keyIndex = response.IndexOf(key);
                encryptedData = response.Remove(keyIndex, key.Length);

                int ivIndex = encryptedData.IndexOf(iv);
                encryptedData = encryptedData.Remove(ivIndex, iv.Length);

                string data = Cryptography.decryptRJ256(encryptedData, key, iv);
                Contract contract = new Contract();
                contract = JSON.decodeContract(data);

                this.contractName = contract.contractName;
                this.diskSpace = contract.diskSpace;
                this.diskUsed = contract.diskUsed;
                this.encryption = contract.encryption;
                this.versioning = contract.versioning;
                this.bucketName = contract.bucketName;
                this.targetFolder = contract.targetFolder;
                this.lastModified = this.lastModifiedWeb;

                this.saveContract();
            }
            catch(Exception e)
            {
                status = "Erro ao salvar as atualizações do contrato";
            }

            return status;
        }

        public string loadContract()
        {
            string status = "OK";
            try
            {
                string connString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "odtdrive.db";
                string query = "SELECT * FROM Contract";

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
                                this.contractId = Convert.ToInt32(dr.GetValue(0));
                                this.contractName = dr.GetValue(1).ToString();
                                this.diskSpace = Convert.ToDouble(dr.GetValue(2));
                                this.diskUsed = Convert.ToDouble(dr.GetValue(3));
                                this.encryption = Convert.ToInt32(dr.GetValue(4));
                                this.versioning = Convert.ToInt32(dr.GetValue(5));
                                this.bucketName = dr.GetValue(6).ToString();
                                this.targetFolder = dr.GetValue(7).ToString();
                                this.lastModified = Convert.ToDouble(dr.GetValue(8));
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                status = "ERROR";
            }
            
            return status;
        }
    }
}
