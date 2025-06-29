using MarkoKosticIT6922.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkoKosticIT6922.Controllers
{
    public class AdminpanelController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminpanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
