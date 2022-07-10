namespace CanWeFixItRepository.Models
{
    public class MarketDataDM
    {
        public int Id { get; set; }
        public long? DataValue { get; set; }
        public string Sedol { get; set; }
        public bool Active { get; set; }
    }
}
