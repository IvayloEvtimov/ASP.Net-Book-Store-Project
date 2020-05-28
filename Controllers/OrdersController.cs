using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
	public class OrdersController : Controller
	{
		private readonly MvcBookContext _context;

		public OrdersController(MvcBookContext context)
		{
			_context = context;
		}

		// GET: Orders
		public async Task<IActionResult> Index()
		{
			var mvcBookContext = _context.Orders.Include(o => o.Customer);
			return View(await mvcBookContext.ToListAsync());
		}

		// GET: Orders/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _context.Orders
				.Include(o => o.Customer)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (order == null)
			{
				return NotFound();
			}

			return View(order);
		}

		// GET: Orders/Create
		public IActionResult Create()
		{
			ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID");
			return View();
		}

		// POST: Orders/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create([Bind("ID,BookId,CustomerId,Address,Date")] Order order)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        _context.Add(order);
		//        await _context.SaveChangesAsync();
		//        return RedirectToAction(nameof(Index));
		//    }
		//    ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
		//    return View(order);
		//}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int? id, string Address)
		{
			if(id == null)
				return NotFound();

			var Customer = await _context.Customers.FirstOrDefaultAsync(model => model.Email == HttpContext.Session.GetString("Email"));
			if (Customer == null)
				return NotFound();

			var CustomerCart = await (from model in _context.Carts where model.Customer_ID == Customer.ID select model).ToListAsync();
			if (CustomerCart == null)
				return NotFound();

			foreach(var cart in CustomerCart)
			{
				Order order = new Order
				{
					BookId = cart.ISBN,
					CustomerId = Customer.ID,
					Address = Address,
					Date = Convert.ToDateTime(Request.Form["Date"].ToString())
				};

				_context.Add(order);
			}
			_context.Remove(CustomerCart);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Home");
		}

		// GET: Orders/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _context.Orders.FindAsync(id);
			if (order == null)
			{
				return NotFound();
			}
			ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
			return View(order);
		}

		// POST: Orders/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,BookId,CustomerId,Address,Date")] Order order)
		{
			if (id != order.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(order);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!OrderExists(order.ID))
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
			ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
			return View(order);
		}

		// GET: Orders/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _context.Orders
				.Include(o => o.Customer)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (order == null)
			{
				return NotFound();
			}

			return View(order);
		}

		// POST: Orders/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var order = await _context.Orders.FindAsync(id);
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool OrderExists(int id)
		{
			return _context.Orders.Any(e => e.ID == id);
		}
	}
}
