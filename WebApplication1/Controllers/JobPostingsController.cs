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
    public class JobPostingsController : Controller
    {
        private readonly JobManagementContext _context;

        public JobPostingsController(JobManagementContext context)
        {
            _context = context;
        }

        // GET: JobPostings
        public async Task<IActionResult> Index()
        {
              return View(await _context.JobPostings.ToListAsync());
        }

        // GET: JobPostings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.JobPostings == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPostings
                .FirstOrDefaultAsync(m => m.PostingId == id);
            if (jobPosting == null)
            {
                return NotFound();
            }

            return View(jobPosting);
        }

        // GET: JobPostings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostingId,JobPostingTitle,JobDescription,PostedDate")] JobPosting jobPosting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPosting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobPosting);
        }

        // GET: JobPostings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.JobPostings == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting == null)
            {
                return NotFound();
            }
            return View(jobPosting);
        }

        // POST: JobPostings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PostingId,JobPostingTitle,JobDescription,PostedDate")] JobPosting jobPosting)
        {
            if (id != jobPosting.PostingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPosting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostingExists(jobPosting.PostingId))
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
            return View(jobPosting);
        }

        // GET: JobPostings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.JobPostings == null)
            {
                return NotFound();
            }

            var jobPosting = await _context.JobPostings
                .FirstOrDefaultAsync(m => m.PostingId == id);
            if (jobPosting == null)
            {
                return NotFound();
            }

            return View(jobPosting);
        }

        // POST: JobPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.JobPostings == null)
            {
                return Problem("Entity set 'JobManagementContext.JobPostings'  is null.");
            }
            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting != null)
            {
                _context.JobPostings.Remove(jobPosting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostingExists(string id)
        {
          return _context.JobPostings.Any(e => e.PostingId == id);
        }
    }
}
