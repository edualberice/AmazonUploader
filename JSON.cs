﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ODTGed_Uploader
{
    class JSON
    {
        public static User decodeUser(string data)
        {
            User user = new User();
            data = data.Replace("\"", "");
            Contract contract = new Contract();

            string untreatedRoles = Regex.Match(data, @"\[([^\]]*)\]").Groups[1].Value;
            string treatedRoles = untreatedRoles.Replace(",", "#");
            data = data.Replace(untreatedRoles, treatedRoles);
            data = data.Replace("{", "");
            data = data.Replace("}", "");

            string[] info = data.Split(',');

            foreach(string userInfo in info)
            {
                string[] uInfo = userInfo.Split(':');

                if(uInfo[0].Equals("TKN"))
                {
                    user.token = uInfo[1];
                }
                else if(uInfo[0].Equals("FNM"))
                {
                    user.firstName = uInfo[1];
                }
                else if (uInfo[0].Equals("LNM"))
                {
                    user.lastName = uInfo[1];
                }
                else if (uInfo[0].Equals("UID"))
                {
                    user.userId = Convert.ToInt32(uInfo[1]);
                }
                else if (uInfo[0].Equals("RLS"))
                {
                    Dictionary<string, bool> roleDictionary = new Dictionary<string, bool>();
                    roleDictionary.Add("login", false);
                    roleDictionary.Add("admin", false);
                    roleDictionary.Add("manager", false);
                    roleDictionary.Add("read", false);
                    roleDictionary.Add("write", false);
                    roleDictionary.Add("remove", false);

                    uInfo[1] = uInfo[1].Replace("[", "");
                    uInfo[1] = uInfo[1].Replace("]", "");

                    string[] roles = uInfo[1].Split('#');

                    foreach(string role in roles)
                    {
                        if (role.Equals("1")) roleDictionary["login"] = true;
                        else if (role.Equals("2")) roleDictionary["admin"] = true;
                        else if (role.Equals("3")) roleDictionary["manager"] = true;
                        else if (role.Equals("4")) roleDictionary["read"] = true;
                        else if (role.Equals("5")) roleDictionary["write"] = true;
                        else if (role.Equals("6")) roleDictionary["remove"] = true;
                    }

                    user.roles = roleDictionary;
                }
                else if (uInfo[0].Equals("CID"))
                {
                    contract.contractId = Convert.ToInt32(uInfo[1]);
                }
                else if (uInfo[0].Equals("CLM"))
                {
                    contract.lastModified = Convert.ToDouble(uInfo[1]);
                }
            }
            user.contract = contract;

            return user;
        }

        public static string getContractUpdate(string token)
        {
            string json = "{\"FNC\":\"UCT\",\"TKN\":\"" + token + "\"}";
            return json;
        }

        public static Contract decodeContract(string data)
        {
            Contract c = new Contract();

            data = data.Replace("\"", "");
            data = data.Replace("{", "");
            data = data.Replace("}", "");

            string[] fields = data.Split(',');
            foreach(string field in fields)
            {
                string[] info = field.Split(':');
                if(info[0].Equals("CNM"))
                {
                    c.contractName = info[1];
                }
                else if (info[0].Equals("DSP"))
                {
                    c.diskSpace = Convert.ToDouble(info[1]);
                }
                else if (info[0].Equals("DUS"))
                {
                    c.diskUsed = Convert.ToDouble(info[1]);
                }
                else if (info[0].Equals("ENC"))
                {
                    if(info[1].Equals("T"))
                        c.encryption = 1;
                    else
                        c.encryption = 0;
                }
                else if (info[0].Equals("VER"))
                {
                    if (info[1].Equals("T"))
                        c.versioning = 1;
                    else
                        c.versioning = 0;
                }
                else if (info[0].Equals("BKT"))
                {
                    c.bucketName = info[1];
                }
            }

            return c;
        }
    }
}