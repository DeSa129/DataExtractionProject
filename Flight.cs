using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtractionProject
{
    class Flight
    {
        public string DepartureGeoCode { get; set; }
        public string ArrivalGeoCode { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public int Taxes { get; set; }
    }
}
