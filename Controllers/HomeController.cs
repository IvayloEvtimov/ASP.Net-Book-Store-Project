using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
	public class HomeController : Controller
	{
			private readonly MvcBookContext _context;
				private readonly ILogger<HomeController> _logger;


				public HomeController(MvcBookContext context,ILogger<HomeController> logger)
				{
					_context = context;
					_logger = logger;
				}

				public async Task<IActionResult> Index()
				{
						var mvcBookContext = _context.Books.Include(b => b.Genre);
						return View(await mvcBookContext.ToListAsync());
				}

				[HttpPost]
				[ValidateAntiForgeryToken]
				public async Task<IActionResult> AddToCart(string BookId)
				{
					try
					{
						if(ModelState.IsValid){
							// TODO: Add dynamic user id
							long ISBN = long.Parse(BookId);
							Cart cart = new Cart{Customer_ID=1,ISBN=ISBN};
							_context.Add(cart);
							await _context.SaveChangesAsync();
						}
					}catch(DbUpdateException )
					{
						ModelState.AddModelError("", "Unable to add to Cart. " + "Try again, and if the problem persists " + 
												"see your system administrator.");
					}

					return RedirectToAction(nameof(Index));
				}

				public async Task<IActionResult> BookInfo(long? id)
				{
					if(id == null)
					{
						return NotFound();
					}

					var book = await _context.Books.Include(i => i.BookAuthors).ThenInclude(i => i.Author)
					.Include(i => i.Genre).FirstOrDefaultAsync(m => m.ISBN == id);

					if(book ==null)
					{
						return NotFound();
					}
					
					return View(book);
				}

				public async Task<IActionResult> Cart()
				{
					var MvcBookContext = _context.Carts.Include(c => c.Book).AsNoTracking();
					return View(await MvcBookContext.ToListAsync());

				}

				public IActionResult Privacy()
				{
						return View();
				}

				public IActionResult About()
				{
						return View();
				}

				[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
				public IActionResult Error()
				{
						return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
				}
	}
}
