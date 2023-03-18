using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Repository.Models;
using Repository.Repositories.HraccountRepository;
using WebApplication1.Extensions;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHraccountRepository hraccountRepository;

        public HomeController(ILogger<HomeController> logger, IHraccountRepository hraccountRepository)
        {
            _logger = logger;
            this.hraccountRepository = hraccountRepository;
        }

        public IActionResult Index()
        {
            if (ExtensionSession.Get<string>(HttpContext.Session, "user") != null)
            {
                //đã tồn tại session có user
                return RedirectToAction("Index", "CandidateProfiles");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel request)
        {
            if(ExtensionSession.Get<string>(HttpContext.Session, "user") != null)
            {
                //đã tồn tại session có user
                return RedirectToAction("Index", "CandidateProfiles");
            }

            if(ModelState.IsValid)
            {
                //check login
                Hraccount account = hraccountRepository.FirstOrDefault(expression: x => x.Email == request.Email && x.Password == request.Password && x.MemberRole == 1);
                if(account != null)
                {
                    //write into session
                    ExtensionSession.Set(HttpContext.Session, "user", account.Email);

                    //redirect to candidate profile
                    return RedirectToAction("Index", "CandidateProfiles");
                }
                //show error
                ViewBag.Error = "Invalid credential";
            } 
            //error -> return về trang login
            return View(nameof(Index));
        }

        public IActionResult Privacy()
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