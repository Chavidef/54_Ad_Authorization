using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _54_Authorization_simple_ads.Data
{
    public class Ad
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Details { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
       public string UserName { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
