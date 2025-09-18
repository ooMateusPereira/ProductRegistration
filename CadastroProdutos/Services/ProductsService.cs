using Microsoft.AspNetCore.Http.HttpResults;
using ProductRegistration.Services;
using System;

namespace ProductRegistration.Services
{
    public class ProductsService : IProductsService
    {
        private static List<Product> products = new List<Product>()
        {
        new Product() {Id = 1, Name = "Mouse", Price = 99.99m, Stock = 50,},
        new Product() {Id = 2, Name = "Keyboard", Price = 249.90m, Stock = 30,}
        };


        public List<Product> GetAll()
        {
            return products;
        }


        public Product GetWithId(int id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }

        public void AddNewProduct(Product newProduct)
        {
            products.Add(newProduct);
        }

        public Product Update(int id, Product uptadedProduct)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product is null)
            {
                return null;
            }

            product.Name = uptadedProduct.Name;
            product.Price = uptadedProduct.Price;
            product.Stock = uptadedProduct.Stock;

            return product;
        }

        public bool Delete(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product is null)
            {
                return false;
            }

            products.Remove(product);

            return true;
        }
    }
}
