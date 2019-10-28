using NorthwindStore.Data.Models;
using System.Linq;

namespace NorthwindStore.Data
{
    public interface ISupplierRepository
    {
        IQueryable<Suppliers> GetSuppliers();
    }
}
