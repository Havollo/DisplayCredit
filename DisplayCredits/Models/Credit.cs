using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisplayCredits.Models
{
    public class Credit
    {
        public Credit()
        {
            Diary = new HashSet<Diary>();
        }

        public int ContractNo { get; set; }
        public int? ClientNo { get; set; }
        public decimal? BranchCode { get; set; }
        public decimal? OpeningAmount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? MaturityDate { get; set; }

        public virtual Branch BranchCodeNavigation { get; set; }
        public virtual Client ClientNoNavigation { get; set; }
        public virtual Parameter StatusNavigation { get; set; }
        public virtual ICollection<Diary> Diary { get; set; }
    }
}