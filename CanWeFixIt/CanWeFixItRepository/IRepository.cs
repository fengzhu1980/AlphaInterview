using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllByFilter(string filter);
    }
}
