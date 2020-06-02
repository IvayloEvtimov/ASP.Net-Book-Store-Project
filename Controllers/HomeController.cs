	using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
	public class HomeController : Controller
	{
		private readonly MvcBookContext _context;
		private readonly ILogger<HomeController> _logger;


		public HomeController(MvcBookContext context, ILogger<HomeController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			LoadAuthors();
			var mvcBookContext = _context.Books.Include(model => model.Genre).Include(model => model.Stockpile);
			return View(await mvcBookContext.ToListAsync());
		}

		public IActionResult Filter(String SearchString, String[] selectedAuthors)
		{
			ViewData["CurrentFilter"] = SearchString;

			var WrittenBooks = (from model in _context.BookAuthors select model).Include(model => model.Book);

			List<String> Authors = new List<String>(selectedAuthors);
			IQueryable<Written_By> item= _context.BookAuthors;

			if (!String.IsNullOrEmpty(SearchString) && Authors!=null)
			{
				item = WrittenBooks.Where(model => Authors.Contains(model.Author.Name) || model.Book.Title.Contains(SearchString));
			}else if (!String.IsNullOrEmpty(SearchString) && Authors==null)
			{
				item = WrittenBooks.Where(model => Authors.Contains(model.Author.Name));
			}

			LoadAuthors();
			return View(nameof(Index),item.Select(model => model.Book));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToCart(string BookId)
		{
			try
			{				
				if (ModelState.IsValid)
				{
					if(HttpContext.Session.Get("Email") == null){
						throw new ArgumentException("No logged user");
					}

					long ISBN = long.Parse(BookId);
					string Customer_Email=HttpContext.Session.GetString("Email");
					Task<Customer> Customer = _context.Customers.FirstOrDefaultAsync(model => model.Email == Customer_Email);
					var ExistingCart = _context.Carts.FirstOrDefaultAsync(model => model.Customer_ID == Customer.Result.ID && model.ISBN==ISBN);
					if(ExistingCart.Result != null)
					{
						ExistingCart.Result.Volume+=1;
					}else
					{
						Cart cart = new Cart { Customer_ID = Customer.Result.ID, ISBN = ISBN, Volume=1 };
						_context.Add(cart);
					}
					await _context.SaveChangesAsync();
				}
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("", "Unable to add to Cart. " + "Try again, and if the problem persists " +
										"see your system administrator.");
			}

			// LoadAuthors();
			// var mvcBookContext = _context.Books.Include(b => b.Genre);
			// return View("Index",await mvcBookContext.ToListAsync());
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> BookInfo(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _context.Books.Include(i => i.BookAuthors).ThenInclude(i => i.Author)
			.Include(i => i.Genre).FirstOrDefaultAsync(m => m.ISBN == id);

			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		public async Task<IActionResult> Cart()
		{
			string Customer_Email = HttpContext.Session.GetString("Email");
			var Customer_ID = (from model in _context.Customers where model.Email == Customer_Email select model.ID)
				.ToListAsync().Result[0];
			var MvcBookContext = (from model in _context.Carts where model.Customer_ID== Customer_ID select model)
				.Include(model => model.Book).ToListAsync();
			var MvcBookContext1 = _context.Carts.Include(model => model.Book).AsNoTracking().ToListAsync();
			return View(await MvcBookContext);

		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(long? id)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _context.Customers.FirstOrDefaultAsync(model => model.Email == Request.Form["Email"].ToString());
					var password = Request.Form["Password"].ToString();
			
					if(user != null)
					{

						HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
						// Convert the data to hash to an array of Bytes.
						byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(password);
						// Compute the Hash. This returns an array of Bytes.
						byte[] bytHash = hashAlg.ComputeHash(bytValue);
						// Optionally, represent the hash value as a base64-encoded string, 
						// For example, if you need to display the value or transmit it over a network.
						string base64 = Convert.ToBase64String(bytHash);

						if (user.Password == base64)
						{
							HttpContext.Session.SetString("Email", user.Email);
							HttpContext.Session.SetString("Name", user.Name);

							LoadAuthors();
							var mvcBookContext = _context.Books.Include(b => b.Genre);
							return View("Index",await mvcBookContext.ToListAsync());
						}
					}
				}catch(Exception )
				{
					ModelState.AddModelError("", "Unable to Login. " + "Try again, and if the problem persists " +
						"see your system administrator.");
				}

			}
			return NotFound();
		}

		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			var mvcBookContext = _context.Books.Include(b => b.Genre);
			return View(nameof(Index), await mvcBookContext.ToListAsync());
		}

		private void LoadAuthors()
		{
			var authors = _context.Authors.ToArray();
			ViewBag.Authors = authors;
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
