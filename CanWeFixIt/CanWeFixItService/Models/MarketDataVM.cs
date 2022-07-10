using CanWeFixItRepository.Models;

namespace CanWeFixItService.Models
{
    public class MarketDataVM
    {
        public MarketDataVM()
        {
        }
        public int Id { get; set; }
        public long? DataValue { get; set; }
        public string Sedol { get; set; }
        public bool Active { get; set; }
        public MarketDataVM(MarketDataDM marketDataDM)
        {
            Id = marketDataDM.Id;
            DataValue = marketDataDM.DataValue;
            Sedol = marketDataDM.Sedol;
            Active = marketDataDM.Active;
        }
    }
}
