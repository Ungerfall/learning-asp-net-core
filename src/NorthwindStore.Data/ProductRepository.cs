﻿using Microsoft.EntityFrameworkCore;
using NorthwindStore.Data.Filters;
using NorthwindStore.Data.Models;
using System;
using System.Linq;

namespace NorthwindStore.Data
{
    public class ProductRepository : IProductRepository
    {
        internal const int TAKE_ALL_PRODUCTS_VALUE = 0;

        private readonly NorthwindContext dbContext;
        private readonly ProductFilter cfg;

        public ProductRepository(NorthwindContext dbContext, ProductFilter filter)
        {
            if (filter.MaximumCount < 0) throw new ArgumentException(nameof(filter));

            this.dbContext = dbContext;
            cfg = filter;
        }

        public IQueryable<Models.Products> GetProducts()
        {
            var products = dbContext.Products
                .Include(x => x.Supplier)
                .Include(x => x.Category);

            return cfg.MaximumCount != TAKE_ALL_PRODUCTS_VALUE
                ? products.Take(cfg.MaximumCount)
                : products;
        }

        public Products GetProductById(int? productId)
        {
            return dbContext.Products
                .FirstOrDefault(x => x.ProductId == productId);
        }

        public void InsertProduct(Products product)
        {
            dbContext.Products.Add(product);
        }

        public void UpdateProduct(Products product)
        {
            dbContext.Attach(product);
            dbContext.Entry(product).State = EntityState.Modified;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
