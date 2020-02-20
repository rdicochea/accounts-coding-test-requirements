using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace accounts_coding_test.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public float AmountDue { get; set; }
        public string PaymentDueDate { get; set; }
        public int AccountStatusId { get; set; }
    }
}
