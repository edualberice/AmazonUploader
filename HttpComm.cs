using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;

namespace ODTGed_Uploader
{
    class HttpComm
    {
        public static string httpPostData(Dictionary<string, string> data, string url)
        {
            var httpData = new NameValueCollection();
            foreach (KeyValuePair<string, string> field in data)
            {
                httpData[field.Key] = field.Value;
            }

            using (var wb = new WebClient())
            {
                try
                {
                    var response = wb.UploadValues(url, "POST", httpData);
                    string webResponse = System.Text.Encoding.UTF8.GetString(response);

                    return webResponse;
                }
                catch (Exception e)
                {
                    return "ERR6";
                }
            }
        }

        public static string treatHttpError(string webResponse, string func)
        {
            string message = "";

            //Login usando a base da web
            if (func.Equals("LGN"))
            {
                if (webResponse.Equals("INV"))
                {
                    message = "Usuário ou senha inválidos";
                }
                else
                {
                    message = "Erro na comunicação com o servidor(ERRO 2)";
                }
            }

            //Buscando dados de configurações na web
            if(func.Equals("CCF"))
            {
                if(webResponse.Equals(""))
                {
                    message = "Configurações não encontradas";
                }
            }

            if(func.Equals("NUF"))
            {
                if(webResponse.Equals("ERR7"))
                {
                    message = "ERRO 7 - Token de login inválido";
                }
                else if(webResponse.Equals("ERR8"))
                {
                    message = "ERRO 8 - Erro ao gerar log";
                }
            }

            return message;
        }

        public static string newFileUploaded(string token, string key, string file, string url)
        {
            string status = "OK";

            file = file.Replace("\\", "#");
            string[] filePath = file.Split('#');

            string json = JSON.encodeFileUploadLog("NUF", token, key, filePath[filePath.Length - 1]);
            string encryptKey = Cryptography.randomString(32);
            string encryptIv = Cryptography.randomString(32);

            string encryptedContent = Cryptography.encryptRJ256(json, encryptKey, encryptIv);
            string sendData = encryptedContent + encryptKey + encryptIv;

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("CTT", sendData);

            string webResponse = HttpComm.httpPostData(data, url);

            if (webResponse.Length <= 6)
            {
                status = HttpComm.treatHttpError(webResponse, "NUF");
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

                string logData = Cryptography.decryptRJ256(encryptedData, decodeKey, decodeIv);
                status = JSON.decodeUploadLog(logData);
            }

            return status;
        }
    }
}
