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

            return message;
        }
    }
}
