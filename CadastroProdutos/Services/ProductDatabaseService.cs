
using Microsoft.EntityFrameworkCore;
using ProductRegistration.Database;

namespace ProductRegistration.Services
{
    public class ProductDatabaseService : IProductsService
    {
        private ApplicationDbContext context; 

        public ProductDatabaseService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddNewProduct(Product newProduct)
        {
            ProductValidation(newProduct);
            context.Products.Add(newProduct);
            context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);

            if (product is null)
            {
                return false;
            }

            context.Products.Remove(product);
            context.SaveChangesAsync();

            return true;
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetWithId(int id)
        {
            return context.Products.FirstOrDefault(x => x.Id == id);
            
        }

        public Product Update(int id, Product uptadedProduct)
        {
            ProductValidation(uptadedProduct);

            var product = context.Products.FirstOrDefault(x => x.Id == id);

            if (product is null)
            {
                return null;
            }

            product.Name = uptadedProduct.Name;
            product.Price = uptadedProduct.Price;
            product.Stock = uptadedProduct.Stock;

            context.SaveChangesAsync();

            return product;
        }

        private void ProductValidation(Product product)
        {
            if (product.Name == " ")
            {
                throw new Exception("The name cannot be empty.");
            }

            if (product.Stock > 10)
            {
                throw new Exception("The stock cannot be greater than 10.");
            }
        }
    }
}
