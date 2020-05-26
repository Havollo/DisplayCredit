using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisplayCredits.Models
{
    public class Diary
    {
        public int Id { get; set; }
        public int? ContractNo { get; set; }
        public int? InstallmentSequence { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? PrincipalAmount { get; set; }
        public decimal? RemainingPrincipalAmount { get; set; }
        public DateTime? InstallmentDate { get; set; }

        public virtual Credit ContractNoNavigation { get; set; }
        public virtual Parameter StatusNavigation { get; set; }
        public virtual Parameter TransactionTypeNavigation { get; set; }
    }
}