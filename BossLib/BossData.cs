using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossLib
{
    public class BossData
    {
        public bool success { get; set; }
        public int count { get; set; }
        public int total { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string region { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string[] drops { get; set; }
        public string healthPoints { get; set; }
    }

}
