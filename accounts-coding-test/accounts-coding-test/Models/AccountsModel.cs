using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace accounts_coding_test.Models
{
    public class AccountsModel
    {
        public List<AccountsViewModel> GetAllAccounts()
        {
            var url = "https://frontiercodingtests.azurewebsites.net/api/accounts/getall";

            //List<AccountsViewModel> acctsList = new List<AccountsViewModel>();
            using (var webClient = new WebClient())
            {
                var data = webClient.DownloadString(url);
                var accountsList = JsonConvert.DeserializeObject<List<AccountsViewModel>>(data);
                return accountsList;
            }
        }
    }
}
