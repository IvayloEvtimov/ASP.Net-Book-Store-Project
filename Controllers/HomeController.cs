using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
						_logger=logger;
        }

				public async Task<IActionResult> Index()
				{
						var mvcBookContext = _context.Books.Include(b => b.Genre);
						return View(await mvcBookContext.ToListAsync());
				}

				public async Task<IActionResult> BookInfo(long? id)
				{
					if(id == null)
					{
						return NotFound();
					}

					var book=await _context.Books.Include(b => b.Genre).FirstOrDefaultAsync(m => m.ISBN==id);
					if(book ==null)
					{
						return NotFound();
					}
					
					return View(book);
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
