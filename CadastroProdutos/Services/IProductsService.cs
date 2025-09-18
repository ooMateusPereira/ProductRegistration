namespace ProductRegistration.Services
{
    public interface IProductsService
    {
        public List<Product> GetAll();

        public Product GetWithId(int id);

        public void AddNewProduct(Product newProduct);

        public Product Update(int id, Product uptadedProduct);

        public bool Delete(int id);
    }
}
