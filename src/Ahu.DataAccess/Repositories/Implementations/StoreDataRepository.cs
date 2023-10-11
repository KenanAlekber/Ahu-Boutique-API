using Ahu.Core.Entities;
using Ahu.DataAccess.Contexts;
using Ahu.DataAccess.Repositories.Interfaces;

namespace Ahu.DataAccess.Repositories.Implementations;

public class StoreDataRepository : Repository<StoreData>, IStoreDataRepository
{
    public StoreDataRepository(AppDbContext context) : base(context)
    {
    }
}