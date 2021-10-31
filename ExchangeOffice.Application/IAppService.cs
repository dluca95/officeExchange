using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeOffice.Application
{
    public interface IAppService<T, in TQuery>
    {
        public Task<T> Add(T model);
        public Task Update(T model);
        public Task<IEnumerable<T>> GetManyByQuery(TQuery queryModel);
        public Task<T> GetByQuery(TQuery queryModel);
    }
}