using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;
using Riode.ViewModels;

namespace Riode.Controllers
{
    public class ProductController : Controller
    {
        private readonly RiodeDbContext _context;
        public ProductController(RiodeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            var products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (products == null)
            {
				return NotFound();
			}
            var productDetailViewModel = new ProductDetailViewModel
            {
				Name = products.Name,
				Description = products.Description,
				Price = products.Price,
				OldPrice = products.OldPrice,
				Rating = products.Rating,
				Discount = products.Discount,
				Features = products.Features,
				Material = products.Material,
				CategoryId = products.CategoryId,
				CategoryName = products.Category.Name,
				BrandId = products.BrandId,
				BrandName = products.Brand.Name,
				RecommendedUse = products.RecommendedUse,
				Images = products.Images
			};
            return View(productDetailViewModel);
        }
    }
}
