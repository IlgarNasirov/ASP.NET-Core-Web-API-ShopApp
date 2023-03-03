using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Data.Models;
using ShopApp.DTOs;
using ShopApp.Services;

namespace ShopApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopAppDbContext _shopAppDbContext;
        private readonly IUserService _userService;

        public ProductRepository(IUserService userService, ShopAppDbContext shopAppDbContext)
        {
            _shopAppDbContext = shopAppDbContext;
            _userService = userService;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await _shopAppDbContext.Products.ToListAsync();
        }
        public async Task<Product> GetProduct(int id)
        {
            return await _shopAppDbContext.Products.FindAsync(id);
        }
        public async Task<Product> AddProduct(ProductDTO productDTO)
        {
            Product product = new Product();
            if (productDTO.File != null)
            {
                string extension = Path.GetExtension(productDTO.File.FileName);
                if(extension==".png"||extension==".jpg"||extension == ".jpeg"||extension == ".jfif")
                {
                    string file = Guid.NewGuid() + extension;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDTO.File.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = file;
                }
            }
            product.Title=productDTO.Title;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;
            product.UserId = _userService.GetUserId();
            await _shopAppDbContext.Products.AddAsync(product);
            await _shopAppDbContext.SaveChangesAsync();
            return product;
        }
        public async Task<CustomReturnDTO> UpdateProduct(int id, ProductDTO productDTO)
        {
            var product=await _shopAppDbContext.Products.Where(u=>u.UserId==_userService.GetUserId() && u.Id==id).FirstOrDefaultAsync();
            if (product == null)
            {
                return new CustomReturnDTO { Type="BAD", Message= "Product with this id could not found!" };
            }
            if (productDTO.File != null)
            {
                if (product.ImageUrl != null)
                {
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", product.ImageUrl));
                }
                string extension = Path.GetExtension(productDTO.File.FileName);
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".jfif")
                {
                    string file = Guid.NewGuid() + extension;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file);
                    using(var fileStream=new FileStream(filePath, FileMode.Create))
                    {
                        await productDTO.File.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = file;
                }
            }
            product.Title = productDTO.Title;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;
            await _shopAppDbContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = "OK", Message = "Product successfully edited!" };
        }
        public async Task<CustomReturnDTO>DeleteProduct(int id)
        {
            var product = await _shopAppDbContext.Products.Where(u => u.UserId == _userService.GetUserId() && u.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return new CustomReturnDTO { Type = "BAD", Message = "Product with this id could not found!" };
            }
            if (product.ImageUrl != null)
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", product.ImageUrl));
            }
            _shopAppDbContext.Products.Remove(product);
            await _shopAppDbContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = "OK", Message = "Product successfully deleted!" };
        }
    }
}
