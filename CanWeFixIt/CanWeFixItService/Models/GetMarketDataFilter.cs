using System.Collections.Generic;

namespace CanWeFixItService.Models
{
    public class GetMarketDataFilter
    {
        public bool? Active { get; set; }
        public List<int> Ids { get; set; }
    }
}
