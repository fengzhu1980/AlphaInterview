using System.Collections.Generic;

namespace CanWeFixItService.Models
{
    public class GetInstrumentFilter
    {
        public bool? Active { get; set; }
        public List<int> Ids { get; set; }
        public List<string> Sedols { get; set; }
    }
}
