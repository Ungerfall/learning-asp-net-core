﻿using System;
using System.Linq;

namespace NorthwindStore.Data
{
    public interface IProductRepository : IDisposable
    {
        IQueryable<Models.Products> GetProducts();
        Models.Products GetProductById(int? productId);
        void InsertProduct(Models.Products product);
        void UpdateProduct(Models.Products product);
        void Save();
    }
}
