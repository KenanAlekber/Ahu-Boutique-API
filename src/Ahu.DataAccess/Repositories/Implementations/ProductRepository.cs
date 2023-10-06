using Ahu.Core.Entities;
using Ahu.DataAccess.Contexts;
using Ahu.DataAccess.Repositories.Interfaces;

namespace Ahu.DataAccess.Repositories.Implementations;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}