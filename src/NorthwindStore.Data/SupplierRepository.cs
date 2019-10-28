using NorthwindStore.Data.Models;
using System.Linq;

namespace NorthwindStore.Data
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindContext dbContext;

        public SupplierRepository(NorthwindContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Suppliers> GetSuppliers()
        {
            return dbContext.Suppliers;
        }
    }
}
