using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Models;
using System.Security.Cryptography;

namespace EShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly EshopContext _context;
        private readonly IWebHostEnvironment _environment;

        public CategoriesController(EshopContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'EshopContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image")] Category category, IFormFile? file )
        {

            var ImagePath = "/images/" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using( FileStream dd = new FileStream (_environment.WebRootPath + ImagePath, FileMode.Create))
            {
                file.CopyTo(dd);
            }
            category.Image = ImagePath;

            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'EshopContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        public IActionResult Jackets()
        {
            var jacket = (from d in _context.Products
                         where d.Name == "Jackets"
                         select d).ToList();
            return View(jacket);
        }

        public IActionResult Shoes()
        {
            var shoes = (from d in _context.Products
                         where d.Name == "Shoes"
                         select d).ToList();
            return View(shoes);
        }

        public IActionResult Shorts()
        {
            var shorts = (from d in _context.Products
                         where d.Name == "Shorts"
                         select d).ToList();
            return View(shorts);
        }

        public IActionResult Hoodies()
        {
            var hoodie = (from d in _context.Products
                         where d.Name == "Hoodies"
                         select d).ToList();
            return View(hoodie);
        }


        public async Task<IActionResult> Shirts()
        {
            var shirt = (from d in _context.Products
                       where d.Name == "Shirts"
                       select d).ToList();
            return View(shirt);
        }



        public IActionResult Gloves()
        {
            var gloves = (from d in _context.Products
                         where d.Name == "Gloves"
                         select d).ToList();
            return View(gloves);
        }
        public IActionResult Bands()
        {
            var Bands = (from d in _context.Products
                         where d.Name == "Bands"
                         select d).ToList();
            return View(Bands);
        }

        public async Task<IActionResult> Shop()
        {
            return _context.Products != null ?
                        View(await _context.Products.ToListAsync()) :
                        Problem("Entity set 'EshopContext.Categories'  is null.");
        }

        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}
