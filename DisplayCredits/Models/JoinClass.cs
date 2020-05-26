using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisplayCredits.Models
{
    public class JoinClass
    {
        public branch GetBranch { get; set; }
        public client GetClient { get; set; }
        public credit GetCredit { get; set; }
        public diary GetDiary { get; set; }
        public parameter GetParameter { get; set; }
    }
}