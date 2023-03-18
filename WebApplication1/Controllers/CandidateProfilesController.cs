
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories.CandidateProfileRepository;
using Repository.Repositories.JobPostingRepository;

namespace WebApplication1.Controllers
{
    public class CandidateProfilesController : Controller
    {
        private readonly JobManagementContext _context;
        private readonly ICandidateProfileRepository candidateProfileRepository;
        private readonly IJobPostingRepository jobPostingRepository;

        public CandidateProfilesController(JobManagementContext context, ICandidateProfileRepository candidateProfileRepository, IJobPostingRepository jobPostingRepository)
        {
            _context = context;
            this.candidateProfileRepository = candidateProfileRepository;
            this.jobPostingRepository = jobPostingRepository;
        }

        // GET: CandidateProfiles
        public async Task<IActionResult> Index()
        {
            var candidateProfiles = candidateProfileRepository.GetAll(includeFunc: (x) => x.Include(a => a.Posting));
            return View(candidateProfiles);
        }

        // GET: CandidateProfiles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateProfile = candidateProfileRepository.FirstOrDefault(
                    expression: m => m.CandidateId == id, 
                    includeFunc: (x) => x.Include(a => a.Posting)
                );
            if (candidateProfile == null)
            {
                return NotFound();
            }

            return View(candidateProfile);
        }

        // GET: CandidateProfiles/Create
        public IActionResult Create()
        {
            
            ViewData["PostingId"] = new SelectList(jobPostingRepository.GetAll(), nameof(JobPosting.PostingId), nameof(JobPosting.JobPostingTitle));
            return View();
        }

        // POST: CandidateProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidateId,Fullname,Birthday,ProfileShortDescription,ProfileUrl,PostingId")] CandidateProfile candidateProfile)
        {
            if (ModelState.IsValid)
            {
                candidateProfileRepository.Add(candidateProfile);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostingId"] = new SelectList(jobPostingRepository.GetAll(), nameof(JobPosting.PostingId), nameof(JobPosting.JobPostingTitle), candidateProfile.PostingId);
            return View(candidateProfile);
        }

        // GET: CandidateProfiles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateProfile = candidateProfileRepository.FirstOrDefault(expression: x => x.CandidateId == id, includeFunc: q => q.Include(x => x.Posting));
            if (candidateProfile == null)
            {
                return NotFound();
            }
            ViewBag.PostingId = new SelectList(jobPostingRepository.GetAll(), nameof(JobPosting.PostingId), nameof(JobPosting.JobPostingTitle), candidateProfile.PostingId);
            return View(candidateProfile);
        }

        // POST: CandidateProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CandidateId,Fullname,Birthday,ProfileShortDescription,ProfileUrl,PostingId")] CandidateProfile candidateProfile)
        {
            if (id != candidateProfile.CandidateId)
            {
                return NotFound();
            }

            CandidateProfile data = candidateProfileRepository.FirstOrDefault(expression: x => x.CandidateId == id);

            if(data == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                data.CandidateId= candidateProfile.CandidateId;
                data.Fullname= candidateProfile.Fullname;
                data.Birthday= candidateProfile.Birthday;
                data.ProfileShortDescription= candidateProfile.ProfileShortDescription;
                data.ProfileUrl= candidateProfile.ProfileUrl;
                data.PostingId= candidateProfile.PostingId;

                candidateProfileRepository.Update(data);

                return RedirectToAction(nameof(Index));
            }
            ViewData["PostingId"] = new SelectList(jobPostingRepository.GetAll(), nameof(JobPosting.PostingId), nameof(JobPosting.JobPostingTitle), candidateProfile.PostingId);
            return View(candidateProfile);
        }

        // GET: CandidateProfiles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateProfile = candidateProfileRepository.FirstOrDefault(
                    expression: m => m.CandidateId == id,
                    includeFunc: (x) => x.Include(a => a.Posting)
                );
            if (candidateProfile == null)
            {
                return NotFound();
            }

            return View(candidateProfile);
        }

        // POST: CandidateProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var candidateProfile = candidateProfileRepository.FirstOrDefault(
                    expression: m => m.CandidateId == id,
                    includeFunc: (x) => x.Include(a => a.Posting)
                );
            if (candidateProfile != null)
            {
                candidateProfileRepository.Remove(candidateProfile);
            }
         
            return RedirectToAction(nameof(Index));
        }
    }
}
