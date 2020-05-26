using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisplayCredits.Models
{
    public class Client
    {
        public Client()
        {
            Credit = new HashSet<Credit>();
        }

        public int ClientNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public int authoriyId { get; set; }

        public virtual ICollection<Credit> Credit { get; set; }
    }
}