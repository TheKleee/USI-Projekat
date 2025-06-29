using MarkoKosticIT6922.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkoKosticIT6922.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminpanelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
