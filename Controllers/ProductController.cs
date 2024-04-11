using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp2.Models;

namespace TestApp2.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext dataContext;

        public ProductController(DataContext context)
        {
            this.dataContext = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel viewModel) 
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Price = viewModel.Price
            };
            await dataContext.TblProducts.AddAsync(product);
            await dataContext.SaveChangesAsync();   
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        { 
            var products = await dataContext.TblProducts.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        { 
            var product = await dataContext.TblProducts.FindAsync(id);
            if (product == null) { }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product viewModel)
        {
            var product = await dataContext.TblProducts.FindAsync(viewModel.Id);

            if (product is not null)
            {
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;

                await dataContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Product");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Product viewModel)
        {
            var product = await dataContext.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (product is not null)
            {
                dataContext.TblProducts.Remove(product);
                await dataContext.SaveChangesAsync();   
            }

            return RedirectToAction("List", "Product");
        }
    }
}
