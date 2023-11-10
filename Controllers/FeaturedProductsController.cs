using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Models;
using Microsoft.Extensions.Hosting;

namespace EShop.Controllers
{
    public class FeaturedProductsController : Controller
    {
        private readonly EshopContext _context;
        private readonly IWebHostEnvironment _environment;

        public FeaturedProductsController(EshopContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: FeaturedProducts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(UsersController.Login), "Users");
            }
            ViewBag.Count = _context.Categories.Count();
            return _context.FeaturedProducts != null ? 
                          View(await _context.FeaturedProducts.ToListAsync()) :
                          Problem("Entity set 'EshopContext.FeaturedProducts'  is null.");
        }

        // GET: FeaturedProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(UsersController.Login), "Users");
            }
            if (id == null || _context.FeaturedProducts == null)
            {
                return NotFound();
            }

            var featuredProduct = await _context.FeaturedProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featuredProduct == null)
            {
                return NotFound();
            }

            return View(featuredProduct);
        }

        // GET: FeaturedProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeaturedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image,Price")] FeaturedProduct featuredProduct, IFormFile? file)
        {

            var ImagePath = "/images/" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using (FileStream dd = new FileStream(_environment.WebRootPath + ImagePath, FileMode.Create))
            {
                file.CopyTo(dd);
            }
            featuredProduct.Image = ImagePath;
            if (ModelState.IsValid)
            {
                _context.Add(featuredProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(featuredProduct);
        }

        // GET: FeaturedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(UsersController.Login), "Users");
            }
            if (id == null || _context.FeaturedProducts == null)
            {
                return NotFound();
            }

            var featuredProduct = await _context.FeaturedProducts.FindAsync(id);
            if (featuredProduct == null)
            {
                return NotFound();
            }
            return View(featuredProduct);
        }

        // POST: FeaturedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image,Price")] FeaturedProduct featuredProduct)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(UsersController.Login), "Users");
            }
            if (id != featuredProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featuredProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeaturedProductExists(featuredProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(featuredProduct);
        }

        // GET: FeaturedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(UsersController.Login), "Users");
            }
            if (id == null || _context.FeaturedProducts == null)
            {
                return NotFound();
            }

            var featuredProduct = await _context.FeaturedProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featuredProduct == null)
            {
                return NotFound();
            }

            return View(featuredProduct);
        }

        // POST: FeaturedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(UsersController.Login), "Users");
            }
            if (_context.FeaturedProducts == null)
            {
                return Problem("Entity set 'EshopContext.FeaturedProducts'  is null.");
            }
            var featuredProduct = await _context.FeaturedProducts.FindAsync(id);
            if (featuredProduct != null)
            {
                _context.FeaturedProducts.Remove(featuredProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeaturedProductExists(int id)
        {
          return (_context.FeaturedProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
