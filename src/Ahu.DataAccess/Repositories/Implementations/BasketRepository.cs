using Ahu.Core.Entities;
using Ahu.DataAccess.Contexts;
using Ahu.DataAccess.Repositories.Interfaces;

namespace Ahu.DataAccess.Repositories.Implementations;

public class BasketRepository : Repository<BasketItem>, IBasketRepository
{
    public BasketRepository(AppDbContext context) : base(context)
    {
    }
}