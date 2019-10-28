using System;
using System.Linq;
using NorthwindStore.Data.Models;

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

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					dbContext.Dispose();
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}