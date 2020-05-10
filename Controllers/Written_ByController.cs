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
    public class Written_ByController : Controller
    {
        private readonly MvcBookContext _context;

        public Written_ByController(MvcBookContext context)
        {
            _context = context;
        }

        // GET: Written_By
        public async Task<IActionResult> Index()
        {
            var mvcBookContext = _context.BookAuthors.Include(w => w.Author);
            return View(await mvcBookContext.ToListAsync());
        }

        // GET: Written_By/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var written_By = await _context.BookAuthors
                .Include(w => w.Author)
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (written_By == null)
            {
                return NotFound();
            }

            return View(written_By);
        }

        // GET: Written_By/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "ID");
            return View();
        }

        // POST: Written_By/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ISBN,AuthorID")] Written_By written_By)
        {
            if (ModelState.IsValid)
            {
                _context.Add(written_By);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "ID", written_By.AuthorID);
            return View(written_By);
        }

        // GET: Written_By/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var written_By = await _context.BookAuthors.FindAsync(id);
            if (written_By == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "ID", written_By.AuthorID);
            return View(written_By);
        }

        // POST: Written_By/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ISBN,AuthorID")] Written_By written_By)
        {
            if (id != written_By.AuthorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(written_By);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Written_ByExists(written_By.AuthorID))
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
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "ID", written_By.AuthorID);
            return View(written_By);
        }

        // GET: Written_By/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var written_By = await _context.BookAuthors
                .Include(w => w.Author)
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (written_By == null)
            {
                return NotFound();
            }

            return View(written_By);
        }

        // POST: Written_By/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var written_By = await _context.BookAuthors.FindAsync(id);
            _context.BookAuthors.Remove(written_By);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Written_ByExists(int id)
        {
            return _context.BookAuthors.Any(e => e.AuthorID == id);
        }
    }
}
