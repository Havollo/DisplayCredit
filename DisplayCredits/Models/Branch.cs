using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisplayCredits.Models
{
    public class Branch
    {
        public Branch()
        {
            Credit = new HashSet<Credit>();
        }

        public decimal BranchCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Credit> Credit { get; set; }
    }
}