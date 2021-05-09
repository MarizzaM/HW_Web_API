using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppFirst.Models
{
    public class Flight
    {

        public long Id {get; set;}
        public string OriginCountry { get; set; }
        public string DestCountry { get; set; }
        public long Remaining { get; set; }

    }
}