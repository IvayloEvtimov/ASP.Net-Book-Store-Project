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
		public class BooksController : Controller
		{
				private readonly MvcBookContext _context;

				public BooksController(MvcBookContext context)
				{
						_context = context;
				}

				// GET: Books
				public async Task<IActionResult> Index()
				{
						var mvcBookContext = _context.Books.Include(b => b.Genre);
						return View(await mvcBookContext.ToListAsync());
				}

				// GET: Books/Details/5
				public async Task<IActionResult> Details(long? id)
				{
						if (id == null)
						{
								return NotFound();
						}

						var book = await _context.Books
								.Include(b => b.Genre)
								.FirstOrDefaultAsync(m => m.ISBN == id);
						if (book == null)
						{
								return NotFound();
						}

						return View(book);
				}

				// GET: Books/Create
				public IActionResult Create()
				{
						ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "ID");
						return View();
				}

				// POST: Books/Create
				// To protect from overposting attacks, enable the specific properties you want to bind to, for 
				// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
				[HttpPost]
				[ValidateAntiForgeryToken]
				public async Task<IActionResult> Create([Bind("ISBN,Title,ReleaseYear,GenreId,Price,Pages,Info,Cover")] Book book)
				{
						if (ModelState.IsValid)
						{
								_context.Add(book);
								await _context.SaveChangesAsync();
								return RedirectToAction(nameof(Index));
						}
						ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "ID", book.GenreId);
						return View(book);
				}

				// GET: Books/Edit/5
				public async Task<IActionResult> Edit(long? id)
				{
						if (id == null)
						{
								return NotFound();
						}

						var book = await _context.Books.FindAsync(id);
						if (book == null)
						{
								return NotFound();
						}
						ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "ID", book.GenreId);
						return View(book);
				}

				// POST: Books/Edit/5
				// To protect from overposting attacks, enable the specific properties you want to bind to, for 
				// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
				[HttpPost]
				[ValidateAntiForgeryToken]
				public async Task<IActionResult> Edit(long id, [Bind("ISBN,Title,ReleaseYear,GenreId,Price,Pages,Info,Cover")] Book book)
				{
						if (id != book.ISBN)
						{
								return NotFound();
						}

						if (ModelState.IsValid)
						{
								try
								{
										_context.Update(book);
										await _context.SaveChangesAsync();
								}
								catch (DbUpdateConcurrencyException)
								{
										if (!BookExists(book.ISBN))
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
						ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "ID", book.GenreId);
						return View(book);
				}

				// GET: Books/Delete/5
				public async Task<IActionResult> Delete(long? id)
				{
						if (id == null)
						{
								return NotFound();
						}

						var book = await _context.Books
								.Include(b => b.Genre)
								.FirstOrDefaultAsync(m => m.ISBN == id);
						if (book == null)
						{
								return NotFound();
						}

						return View(book);
				}

				// POST: Books/Delete/5
				[HttpPost, ActionName("Delete")]
				[ValidateAntiForgeryToken]
				public async Task<IActionResult> DeleteConfirmed(long id)
				{
						var book = await _context.Books.FindAsync(id);
						_context.Books.Remove(book);
						await _context.SaveChangesAsync();
						return RedirectToAction(nameof(Index));
				}

				private bool BookExists(long id)
				{
						return _context.Books.Any(e => e.ISBN == id);
				}
		}
}
