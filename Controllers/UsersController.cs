﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Models;
using EShop.DTOs;
using SQLitePCL;
using Microsoft.AspNetCore.Http;

namespace EShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly EshopContext _context;

        public UsersController(EshopContext context)
        {
            _context = context;
        }
      

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("Username")==null)
            {
                return RedirectToAction(nameof(Login));
            }
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'EshopContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                if(id == null || _context.Users == null)
                {
                    return NotFound();
                }


                var user = await _context.Users
               .FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
            
           
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loginss login)
        {
            SystemUser system = new SystemUser();
            system.Username = login.Username;
            system.Password = login.Password;
            _context.SystemUsers.Add(system);
            _context.SaveChanges();
            
            User user = new User();
            user.Email = login.Email;   
            user.Address = login.Address;   
            user.PhoneNumber = login.PhoneNumber;
            user.SystemUserId = system.Id;
            user.Role = login.Role;
            user.Cnic = login.Cnic; 
            user.Name = login.Name;

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else return RedirectToAction(nameof(Login));
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cnic,Email,PhoneNumber,SystemUserId,Address,Role")] User user)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(Login));
            }
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(Login));
            }
            
                if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'EshopContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Dashboard()
        {
            ViewBag.Categories = _context.Categories.Count();
            ViewBag.Users = _context.Users.Count();
            ViewBag.Vendors = _context.Vendors.Count();
            ViewBag.Products = _context.Products.Count();

            return View();
        }

        public IActionResult Login(Loginss logins)
        {
            var login = _context.SystemUsers.Where(m=> m.Username == logins.Username && m.Password==logins.Password).FirstOrDefault();
           if (login == null)
            {
                return View();
            }
           
            HttpContext.Session.SetString("Username", login.Username);
            
            return RedirectToAction(nameof(Dashboard));
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            
            return RedirectToAction(nameof(Login));
        }
    }
}
