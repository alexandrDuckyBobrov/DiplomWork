using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomProject.Models
{
    using System;
    using System.Collections.Generic;
    public class RequestsViewModel
    {
        public int requestid { get; set; }
        public int status { get; set; }
        public virtual requeststatus requeststatus { get; set; }
        public string requestdesc { get; set; }
        public string requestfiles { get; set; }
        public string userlogin { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}