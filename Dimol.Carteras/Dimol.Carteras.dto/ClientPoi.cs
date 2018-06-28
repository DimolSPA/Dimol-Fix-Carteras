using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class ClientPoi
    {
        public string user { get; set; }
        public string pass { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public string description { get; set; }
        public int radius { get; set; }
        public string imei { get; set; }
        public string layer_id {get; set;}
        public Double latitude { get; set; }
        public Double longitude { get; set; }
    }
    public class ClientPoiID
    {
        public string id { get; set; }
    }
}
