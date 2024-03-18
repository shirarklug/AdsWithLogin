using AdsWithLogins.Data;

namespace AdsWithLogins.Web.Models
{
    public class GiveawaysViewModel
    {
        public List<GiveawayAd> GiveawayAds { get; set; }
        public int? UserId { get; set; }
    }
}
