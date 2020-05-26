using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisplayCredits.Models
{
    public class Parameter
    {
        public Parameter()
        {
            Credit = new HashSet<Credit>();
            DiaryStatusNavigation = new HashSet<Diary>();
            DiaryTransactionTypeNavigation = new HashSet<Diary>();
        }

        public string Value { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Credit> Credit { get; set; }
        public virtual ICollection<Diary> DiaryStatusNavigation { get; set; }
        public virtual ICollection<Diary> DiaryTransactionTypeNavigation { get; set; }
    }
}