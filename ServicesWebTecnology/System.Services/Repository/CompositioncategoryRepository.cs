using System.Infrastructure.IRepository;
using SystemQuickzal.Contexts;
using SystemQuickzal.Data.Entity;

namespace System.Infrastructure.Repository
{
    public class CompositioncategoryRepository : GenericRepository<Compositioncategory>, ICompositioncategoryRepository
    {
        private readonly AplicationDataContext _context;
        public CompositioncategoryRepository(AplicationDataContext context) : base(context)
        {
            _context = context;
        }

    }
}
