using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtractionProject
{
    class FareFlight
    {
        public string outbound_departure { get; set; }
        public string outbound_arrival { get; set; }
        public string inbound_departure { get; set; }
        public string inbound_arrival { get; set; }
        public string total_price { get; set; }
        public int total_taxes { get; set; }
    }
}
