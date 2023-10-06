using Ahu.Core.Entities;
using Ahu.DataAccess.Contexts;
using Ahu.DataAccess.Repositories.Interfaces;

namespace Ahu.DataAccess.Repositories.Implementations;

public class BrandRepository : Repository<Brand>, IBrandRepository
{
	public BrandRepository(AppDbContext context) : base(context)
	{
	}
}