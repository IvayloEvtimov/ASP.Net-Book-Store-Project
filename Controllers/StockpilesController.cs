using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class StockpilesController : Controller
    {
        private readonly MvcBookContext _context;

        public StockpilesController(MvcBookContext context)
        {
            _context = context;
        }

        // GET: Stockpiles
        public async Task<IActionResult> Index()
        {
            var mvcBookContext = _context.Stockpiles.Include(s => s.Book);
            return View(await mvcBookContext.ToListAsync());
        }

        // GET: Stockpiles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockpile = await _context.Stockpiles
                .Include(s => s.Book)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (stockpile == null)
            {
                return NotFound();
            }

            return View(stockpile);
        }

        // GET: Stockpiles/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Books, "ISBN", "ISBN");
            return View();
        }

        // POST: Stockpiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Volume")] Stockpile stockpile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockpile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Books, "ISBN", "ISBN", stockpile.BookID);
            return View(stockpile);
        }

        // GET: Stockpiles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockpile = await _context.Stockpiles.FindAsync(id);
            if (stockpile == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Books, "ISBN", "ISBN", stockpile.BookID);
            return View(stockpile);
        }

        // POST: Stockpiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("BookID,Volume")] Stockpile stockpile)
        {
            if (id != stockpile.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockpile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockpileExists(stockpile.BookID))
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
            ViewData["BookID"] = new SelectList(_context.Books, "ISBN", "ISBN", stockpile.BookID);
            return View(stockpile);
        }

        // GET: Stockpiles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockpile = await _context.Stockpiles
                .Include(s => s.Book)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (stockpile == null)
            {
                return NotFound();
            }

            return View(stockpile);
        }

        // POST: Stockpiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var stockpile = await _context.Stockpiles.FindAsync(id);
            _context.Stockpiles.Remove(stockpile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockpileExists(long id)
        {
            return _context.Stockpiles.Any(e => e.BookID == id);
        }
    }
}
