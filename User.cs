using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODTGed_Uploader
{
    class User
    {
        public string token { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int userId { get; set; }
        public Dictionary<string, bool> roles { get; set; }
        public Contract contract { get; set; }
    }
}
