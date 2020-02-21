using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using accounts_coding_test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace accounts_coding_test.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            var accountModel = new AccountsModel();
            var accountsList = accountModel.GetAllAccounts();
            return View(accountsList);
        }
    }
}
