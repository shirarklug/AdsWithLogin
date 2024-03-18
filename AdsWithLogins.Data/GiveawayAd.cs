using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsWithLogins.Data
{
    public class GiveawayAd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
    }
}
