using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLRMGWEB.Models
{
    public class clsMasterEntry
    {
    }

    public class clsDivision
    {
        public Int64 ID { get; set; }
        public string Division { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
    }





}