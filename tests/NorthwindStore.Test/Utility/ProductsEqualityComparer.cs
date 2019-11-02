using System.Collections.Generic;
using NorthwindStore.Data.Models;

namespace NorthwindStore.Test.Utility
{
    public class ProductsEqualityComparer : IEqualityComparer<Products>
    {
        public bool Equals(Products x, Products y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Discontinued == y.Discontinued
                   && x.CategoryId == y.CategoryId
                   && x.ProductId == y.ProductId
                   && x.OrderDetails.Count == y.OrderDetails.Count
                   && x.ProductName == y.ProductName
                   && x.QuantityPerUnit == y.QuantityPerUnit
                   && x.ReorderLevel == y.ReorderLevel
                   && x.UnitPrice == y.UnitPrice
                   && x.UnitsInStock == y.UnitsInStock
                   && x.UnitsOnOrder == y.UnitsOnOrder;
        }

        public int GetHashCode(Products obj)
        {
            return obj.ProductId.GetHashCode();
        }
    }
}
