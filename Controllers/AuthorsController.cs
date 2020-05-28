using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
	public class AuthorsController : Controller
	{
		private readonly MvcBookContext _context;

		public AuthorsController(MvcBookContext context)
		{
			_context = context;
		}

		// GET: Authors
		public async Task<IActionResult> Index()
		{
			var authors = await _context.Authors.Include(model => model.Written_Books).ThenInclude(model => model.Book)
			.AsNoTracking().OrderBy(model => model.Name).ToListAsync();
			return View(authors);
		}

		// GET: Authors/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var author = await _context.Authors.Include(model => model.Written_Books).ThenInclude(model => model.Book)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (author == null)
			{
				return NotFound();
			}

			return View(author);
		}

		// GET: Authors/Create
		public IActionResult Create()
		{
			var author = new Author();
			author.Written_Books = new List<Written_By>();
			PopulateWrittenBookData(author);
			return View();
		}

		// POST: Authors/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Name")] Author author)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Add(author);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}

			}
			catch (DbUpdateException /*ex*/)
			{
				ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
			}

			return View(author);
		}

		// GET: Authors/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var author = await _context.Authors.Include(model => model.Written_Books).ThenInclude(model => model.Book)
			.FirstOrDefaultAsync(model => model.ID == id);

			if (author == null)
			{
				return NotFound();
			}

			//ViewBag.Books = (from model in _context.Books orderby model.Title select model).ToList();
			PopulateWrittenBookData(author);
			return View(author);
		}

		// POST: Authors/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, string[] selectedBooks)
		{
			if (id == null)
			{
				return NotFound();
			}

			var authorToUpdate = await _context.Authors.Include(model => model.Written_Books).ThenInclude(model => model.Book)
				.FirstOrDefaultAsync(model => model.ID == id);

			if (await TryUpdateModelAsync<Author>(authorToUpdate, "", model => model.Name))
			{
				UpdateAuthorBooks(selectedBooks, authorToUpdate);
				try
				{
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateException ex )
				{
					//Log the error (uncomment ex variable name and write a log.)
					ModelState.AddModelError("", "Unable to save changes. " +
						"Try again, and if the problem persists, " +
						"see your system administrator.");
				}
				return RedirectToAction(nameof(Index));
			}
			UpdateAuthorBooks(selectedBooks, authorToUpdate);
			PopulateWrittenBookData(authorToUpdate);
			return View(authorToUpdate);
		}

		private void UpdateAuthorBooks(string[] selectedBooks, Author authorToUpdate)
		{
			if (selectedBooks is null)
			{
				authorToUpdate.Written_Books = new List<Written_By>();
				return;
			}

			var selectedBooksHS = new HashSet<string>(selectedBooks);
			var writtenBooks = new HashSet<long>(authorToUpdate.Written_Books.Select(model => model.ISBN));

			foreach(var book in _context.Books)
			{
				if(selectedBooksHS.Contains(book.ISBN.ToString()))
				{
					if (!writtenBooks.Contains(book.ISBN))
					{
						authorToUpdate.Written_Books.Add(new Written_By { ISBN = book.ISBN, AuthorID = authorToUpdate.ID });
					}
				}
				else
				{
					if (writtenBooks.Contains(book.ISBN))
					{
						Written_By bookToRemove = authorToUpdate.Written_Books.FirstOrDefault(model => model.ISBN == book.ISBN);
						_context.Remove(bookToRemove);
					}
				}
			}


		}

		// GET: Authors/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var author = await _context.Authors
				.FirstOrDefaultAsync(m => m.ID == id);
			if (author == null)
			{
				return NotFound();
			}

			return View(author);
		}

		// POST: Authors/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			Author author = await _context.Authors.Include(model => model.Written_Books).SingleAsync(model => model.ID == id);
			var writtenBooks = await _context.BookAuthors.Where(model => model.AuthorID == id).ToListAsync();
			writtenBooks.ForEach(model => model.AuthorID = null);

			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AuthorExists(int id)
		{
			return _context.Authors.Any(e => e.ID == id);
		}

		private void PopulateWrittenBookData(Author author)
		{
			var allBooks = _context.Books;
			var WrittenBooks = new HashSet<long>(author.Written_Books.Select(model => model.ISBN));
			var viewModel = new List<WrittenBooksData>();
			foreach(var book in allBooks)
			{
				viewModel.Add(new WrittenBooksData
				{
					ISBN = book.ISBN,
					Title = book.Title,
					IsWritten = WrittenBooks.Contains(book.ISBN)

				});
			}
			ViewData["Books"] = viewModel;
		}
	}
}
