using System.Linq;
using System.Threading.Tasks;
using DI.Domain.App;
using DI.Domain.Core;
using DI.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Stores
{
    public class AutoNumberStore : DbQueryBase<AutoNumberEntity>, IAutoNumberStore
    {
        public AutoNumberStore(DbSet<AutoNumberEntity> dbSet, IDataStore dataStore) : base(dbSet, dataStore)
        {
        }

        public async Task<int> GetNext(string key)
        {
            var index = 1;
            var entity = await ActiveQuery.Where(x => x.Name == key).FirstOrDefaultAsync();
            if (entity == null)
            {
                entity = new AutoNumberEntity {Name = key, Number = index};
                await Set.AddAsync(entity);
            }
            else
            {
                index = entity.Number + 1;
                entity.Number = index;
                Set.Update(entity);
            }

            await DataStore.SaveAsync();
            return index;
        }
    }
}