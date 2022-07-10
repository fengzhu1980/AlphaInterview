using CanWeFixItRepository;
using CanWeFixItRepository.Models;
using CanWeFixItService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanWeFixItService
{
    public class MarketDataService
    {
        private readonly IRepository<MarketDataDM> _repository;
        private readonly InstrumentService _instrumentService;

        public MarketDataService(IRepository<MarketDataDM> repository, InstrumentService instrumentService)
        {
            _repository = repository;
            _instrumentService = instrumentService;
        }

        public async Task<IEnumerable<MarketDataDto>> GetAllMarketdataWithInstrumentIdByActiveAsync(bool active)
        {
            var result = new List<MarketDataDto>();

            // 1. Get marketdata by filter
            var marketDataFilter = new GetMarketDataFilter()
            {
                Active = active
            };
            var marketDataListInDB = await GetAllMarketDataByFilterAsync(marketDataFilter);

            // 2. Get instruments ids by same sedol from marketdata
            if (marketDataListInDB != null)
            {
                var instrumentFilter = new GetInstrumentFilter()
                {
                    Active = marketDataFilter.Active,
                    Sedols = marketDataListInDB.Select(m => m.Sedol).ToList()
                };

                var instrumentInDB = await _instrumentService.GetAllInstrumentsByFilterAsync(instrumentFilter);
                if (instrumentInDB != null)
                {
                    foreach (var instrument in instrumentInDB)
                    {
                        var marketDataInDB = marketDataListInDB.Find(m => m.Sedol == instrument.Sedol);
                        if (marketDataInDB != null)
                        {
                            var tempDto = new MarketDataDto
                            {
                                Id = marketDataInDB.Id,
                                DataValue = marketDataInDB.DataValue,
                                InstrumentId = instrument.Id,
                                Active = marketDataInDB.Active
                            };
                            result.Add(tempDto);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<IEnumerable<MarketValuationVM>> GetAllValuationByActiveAsync(bool active)
        {
            var result = new List<MarketValuationVM>();

            // 1. Get marketdata where active is true
            var marketDataFilter = new GetMarketDataFilter()
            {
                Active = active
            };
            var marketDataListInDB = await GetAllMarketDataByFilterAsync(marketDataFilter);

            // 2. Get marketdata's value total
            if (marketDataListInDB != null && marketDataListInDB.Count > 0)
            {
                var tempMarketValuationVM = new MarketValuationVM()
                {
                    Name = "DataValueTotal",
                    Total = marketDataListInDB.Sum(m => m.DataValue)
                };
                result.Add(tempMarketValuationVM);
            }

            return result;
        }

        public async Task<List<MarketDataVM>> GetAllMarketDataByFilterAsync(GetMarketDataFilter filter)
        {
            var resultList = new List<MarketDataVM>();
            // "SELECT m.Id, m.Sedol, m.DataValue, i.Id as InstrumentId FROM MarketData m INNER JOIN Instrument i ON m.Sedol = i.Sedol WHERE m.Active = 1 and m.Id IN (2, 4)"
            var activeString = filter.Active == true ? 1 : 0;
            var filterToQuery = $"SELECT id, sedol, dataValue, active FROM MarketData WHERE active = { activeString } ";

            if (filter.Ids != null && filter.Ids.Count > 0)
            {
                var idsString = string.Join(",", filter.Ids);
                if (!string.IsNullOrEmpty(idsString)) filterToQuery += $" and id IN ({ idsString })";
            }

            var marketDataInDB = await _repository.GetAllByFilter(filterToQuery);

            if (marketDataInDB != null)
            {
                resultList = marketDataInDB.Select(t => new MarketDataVM(t)).ToList();
            }

            return resultList;
        }
    }
}
