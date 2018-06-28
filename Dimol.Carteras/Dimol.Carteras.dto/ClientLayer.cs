using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class ClientLayer
    {
        public string user { get; set; }
        public string pass {get; set;}
        public string imei { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    public class PostClientLayersGestor
    {
        public string user { get; set; }
        public string pass { get; set; }
        public string imei { get; set; }
        
    }
    public class ClientLayersGestor
    {
        public string name { get; set; }
        public string description { get; set; }
        public string id { get; set; }

    }
  
}
