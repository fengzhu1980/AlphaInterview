using CanWeFixItRepository;
using CanWeFixItRepository.Models;
using CanWeFixItService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanWeFixItService
{
    public class InstrumentService
    {
        private readonly IRepository<InstrumentDM> _repository;

        public InstrumentService(IRepository<InstrumentDM> repository)
        {
            _repository = repository;
        }

        public async Task<List<InstrumentVM>> GetAllInstrumentsAsync()
        {
            var resultList = new List<InstrumentVM>();
            var instrumentsInDB = await _repository.GetAll();
            if (instrumentsInDB != null)
            {
                resultList = instrumentsInDB.Select(t => new InstrumentVM(t)).ToList();
            }
            return resultList;
        }

        public async Task<List<InstrumentVM>> GetAllInstrumentsByFilterAsync(GetInstrumentFilter filter)
        {
            var resultList = new List<InstrumentVM>();
            // "SELECT id, active FROM instrument WHERE active = 1 and id IN (2, 4, 6, 8)";
            var activeString = filter.Active == true ? 1 : 0;
            var filterToQuery = $"SELECT id, sedol, name, active FROM instrument WHERE active = { activeString } ";

            if (filter.Ids != null && filter.Ids.Count > 0)
            {
                var idsString = string.Join(",", filter.Ids);
                if (!string.IsNullOrEmpty(idsString)) filterToQuery += $" and id IN ({ idsString })";
            }

            if (filter.Sedols != null && filter.Sedols.Count > 0)
            {
                var sedolString = string.Join("','", filter.Sedols);
                if (!string.IsNullOrEmpty(sedolString)) filterToQuery += $" and sedol IN ('{ sedolString }')";
            }

            var instrumentsInDB = await _repository.GetAllByFilter(filterToQuery);

            if (instrumentsInDB != null)
            {
                resultList = instrumentsInDB.Select(t => new InstrumentVM(t)).ToList();
            }
            return resultList;
        }
    }
}
