using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsdsShop.Data;
using CsdsShop.Models;

namespace CsdsShop.Controllers
{
    public class SellersController : Controller
    {
        private readonly ConsignmentDbContext _context;

        public SellersController(ConsignmentDbContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index()
        {
            return _context.Sellers != null ?
                        View(await _context.Sellers.ToListAsync()) :
                        Problem("Entity set 'ConsignmentDbContext.Sellers'  is null.");
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,AccountToCredit,Email,CellNum")] SellerCreationDto dto)
        {
            if (ModelState.IsValid)
            {
                Seller seller = new Seller()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    AccountToCredit = dto.AccountToCredit,
                    Email = dto.Email,
                    IsActive = true,
                    CellNum = dto.CellNum,
                    ItemsList = new List<Item>()
                };
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//change to put
        [ValidateAntiForgeryToken]
        //remove sellerId?
        public async Task<IActionResult> Edit(int id, [FromForm] SellerUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var seller = new Seller()
                    {
                        Id = id,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        AccountToCredit = dto.AccountToCredit,
                        CellNum = dto.CellNum,
                        IsActive = dto.IsActive,
                        Notes = dto.Notes,
                      
                    };
                    
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(dto.Id))
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
            return View(dto);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set 'ConsignmentDbContext.Sellers'  is null.");
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
          return (_context.Sellers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
