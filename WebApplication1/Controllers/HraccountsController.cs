using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace WebApplication1.Controllers
{
    public class HraccountsController : Controller
    {
        private readonly JobManagementContext _context;

        public HraccountsController(JobManagementContext context)
        {
            _context = context;
        }

        // GET: Hraccounts
        public async Task<IActionResult> Index()
        {
              return View(await _context.Hraccounts.ToListAsync());
        }

        // GET: Hraccounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Hraccounts == null)
            {
                return NotFound();
            }

            var hraccount = await _context.Hraccounts
                .FirstOrDefaultAsync(m => m.Email == id);
            if (hraccount == null)
            {
                return NotFound();
            }

            return View(hraccount);
        }

        // GET: Hraccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,FullName,MemberRole")] Hraccount hraccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hraccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hraccount);
        }

        // GET: Hraccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Hraccounts == null)
            {
                return NotFound();
            }

            var hraccount = await _context.Hraccounts.FindAsync(id);
            if (hraccount == null)
            {
                return NotFound();
            }
            return View(hraccount);
        }

        // POST: Hraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Password,FullName,MemberRole")] Hraccount hraccount)
        {
            if (id != hraccount.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HraccountExists(hraccount.Email))
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
            return View(hraccount);
        }

        // GET: Hraccounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Hraccounts == null)
            {
                return NotFound();
            }

            var hraccount = await _context.Hraccounts
                .FirstOrDefaultAsync(m => m.Email == id);
            if (hraccount == null)
            {
                return NotFound();
            }

            return View(hraccount);
        }

        // POST: Hraccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Hraccounts == null)
            {
                return Problem("Entity set 'JobManagementContext.Hraccounts'  is null.");
            }
            var hraccount = await _context.Hraccounts.FindAsync(id);
            if (hraccount != null)
            {
                _context.Hraccounts.Remove(hraccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HraccountExists(string id)
        {
          return _context.Hraccounts.Any(e => e.Email == id);
        }
    }
}
