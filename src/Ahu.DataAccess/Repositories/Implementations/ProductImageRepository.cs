using Ahu.Core.Entities;
using Ahu.DataAccess.Contexts;
using Ahu.DataAccess.Repositories.Interfaces;

namespace Ahu.DataAccess.Repositories.Implementations;

public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(AppDbContext context) : base(context)
    {
    }
}