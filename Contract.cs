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
        public double lastModified { get; set; }

        public void saveContract()
        {
            string connString = "Data Source="+AppDomain.CurrentDomain.BaseDirectory+"odtdrive.db";
            SQLiteConnection conn = new SQLiteConnection(connString);

            string query = "SELECT * FROM Contract WHERE id = " + this.contractId;
            SQLiteCommand cmd = new SQLiteCommand(query, conn);

            conn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader();
            dr.Read();

            StringBuilder saveStatement = new StringBuilder();

            if(dr.HasRows)
            {
                saveStatement.Append("UPDATE Contract SET ");
                saveStatement.Append("contract_name = '"+this.contractName+"', ");
                saveStatement.Append("disk_space = "+this.diskSpace+", ");
                saveStatement.Append("usage = "+this.diskUsed+", ");
                saveStatement.Append("encryption = " + this.encryption + ", ");
                saveStatement.Append("versioning = "+this.versioning+", ");
                saveStatement.Append("bucket = '"+this.bucketName+"', ");
                saveStatement.Append("last_modified = "+this.lastModified);
                saveStatement.Append(" WHERE id = "+this.contractId);               
            }
            else
            {
                saveStatement.Append("INSERT INTO Contract (id, contract_name, disk_space, usage, encryption, versioning, bucket, last_modified) VALUES (");
                saveStatement.Append(this.contractId + ",'" + this.contractName + "', " + this.diskSpace + ", " + this.diskUsed + ", " + this.encryption + ", " + this.versioning + ", '" + this.bucketName + "', " + this.lastModified + ")");
            }

            cmd = new SQLiteCommand(saveStatement.ToString(), conn);
            cmd.ExecuteNonQuery();
        }
    }
}
