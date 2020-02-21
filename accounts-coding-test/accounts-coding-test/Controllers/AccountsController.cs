using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using accounts_coding_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace accounts_coding_test.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            List<AccountsViewModel> mod = new List<AccountsViewModel>();
            return View(mod);
        }
    }
}
