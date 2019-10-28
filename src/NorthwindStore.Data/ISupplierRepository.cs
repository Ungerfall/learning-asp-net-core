using System;
using System.Linq;
using NorthwindStore.Data.Models;

namespace NorthwindStore.Data
{
	public interface ISupplierRepository : IDisposable
	{
		IQueryable<Suppliers> GetSuppliers();
	}
}