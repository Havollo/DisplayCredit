using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DisplayCredits.Models
{
    public class CreditViewModel
    {
        public credit Credit { get; set; }
        public client Client { get; set; }
        public IList<branch> Branches { get; set; }
        public IList<parameter> Parameters { get; set; }
        public IList<SelectListItem> Clients{ get; set; }
        public IList<string> Currencies { get; set; }
    }
}