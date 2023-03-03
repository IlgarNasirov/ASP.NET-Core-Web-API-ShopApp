using ShopApp.Data.Models;
using ShopApp.DTOs;

namespace ShopApp.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProducts();
        public Task<Product> GetProduct(int id);
        public Task<Product> AddProduct(ProductDTO productDTO);
        public Task<CustomReturnDTO> UpdateProduct(int id, ProductDTO productDTO);
        public Task<CustomReturnDTO> DeleteProduct(int id);
    }
}
