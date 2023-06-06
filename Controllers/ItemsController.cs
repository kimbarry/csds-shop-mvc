using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsdsShop.Data;
using CsdsShop.Models;
using CsdsShop.Services;

namespace CsdsShop.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ConsignmentDbContext _context;

        public ItemsController(ConsignmentDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public IActionResult Index()
        {
            var items = _context.Items
              .OrderByDescending(i => i.ListDate)
              .Take(10)
              .Select(i => new ItemListViewModel()
              {
                  Id = i.Id,
                  SellerId = i.SellerId,
                  ImgUrl = "http://localhost:9000/images/" + i.SellerId + "-" + i.Id + ".jpeg",
                  Name = i.Name,
                  Price = i.Price,
                  ThumbnailUrl = "",
                  Size = i.Size ?? ""
              }).ToList();
            return View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return base.View(new ItemDetailViewModel()
            {
                Id = item.Id,
                SellerId = item.SellerId,
                Name = item.Name,
                Description = item.Description,
                ListDate = item.ListDate.ToShortDateString(),
                Size = item.Size,
                Price = item.Price,
                ImgUrl = GetImgUrl(item),
                FeePercentage = item.FeePercentage,
                Category = item.Category,
                Active = item.Active,
                IsSold = item.IsSold,
                SoldDate = item.SaleDate == null ? "Not Sold" : item.SaleDate.Value.ToShortDateString()
            });
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ItemCreationDto dto)
        {
            if (ModelState.IsValid)
            {
                var nextId = (_context.Items.Max(i => i.Id) + 1);
                // TODO: Manipulate image -> thumb/cropped&resized main image
                // TODO: Upload image to Minio
                var minio = new MinioService();
                minio.PutObj(dto.Image, dto.SellerId, nextId);
                Item item = new Item()
                {
                    SellerId = dto.SellerId,
                    Name = dto.Name,
                    Description = dto.Description,
                    Size = dto.Size,
                    Price = dto.Price,
                    ListDate = DateTime.Now,
                    SaleDate = null,
                    Active = true,
                    FeePercentage = dto.FeePercentage,
                    Category = (ItemCategory)Enum.Parse(typeof(ItemCategory), dto.Category),
                };
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            // create seller list for dropdown
            // add current seller to top of list
            var sellers = new List<Seller>();
            sellers.Add(await _context.Sellers.FindAsync(item.SellerId));
            sellers.AddRange(_context.Sellers.Where(s => s.Id != item.SellerId));
            ViewBag.Sellers = sellers.Select(s => new SelectListItem()
            { 
                Text = s.Name, 
                Value = s.Id.ToString(),
                Selected = s.Id == item.SellerId
            });
            var itemEditVm = new ItemEditViewModel()
            {
                Id = item.Id,
                SellerId = item.SellerId,
                Name = item.Name,
                Description = item.Description,
                Size = item.Size,
                IsSold = item.IsSold,
                Price = item.Price,
                ImgUrl = GetImgUrl(item),
                FeePercentage = item.FeePercentage,
                Category = item.Category,
                Active = item.Active,
            };
            return View(itemEditVm);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ItemEditViewModel itemVm)
        {
            if (id != itemVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Upload image to Minio
                    if (itemVm.Image != null)
                    {
                        var minio = new MinioService();
                        minio.PutObj(itemVm.Image, itemVm.SellerId, itemVm.Id);
                    }
                    Item item = new Item()
                    {
                        Id = itemVm.Id,
                        SellerId = itemVm.SellerId,
                        Name = itemVm.Name,
                        Description = itemVm.Description,
                        Size = itemVm.Size,
                        Price = itemVm.Price,
                        Active = itemVm.Active,
                        FeePercentage = itemVm.FeePercentage,
                        Category =  itemVm.Category
                    };
                    if (itemVm.IsSold)
                    {
                        item.SaleDate = DateTime.Now;
                    }
                    else
                    {
                        item.SaleDate = null;
                    }
                    
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(id))
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
            return View(itemVm);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ConsignmentDbContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private static string GetImgUrl(Item item)
        {
            return "http://localhost:9000/images/" + item.SellerId + "-" + item.Id + ".jpeg";
        }
    }
}
