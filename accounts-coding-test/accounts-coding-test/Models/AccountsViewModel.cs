using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace accounts_coding_test.Models
{
    public class AccountsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        [DisplayFormat(DataFormatString = "{0:(###)-###-####}")]
        public ulong PhoneNumber { get; set; }
        [DisplayName("Amount Due")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double AmountDue { get; set; }
        [DisplayName("Due Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? PaymentDueDate { get; set; }
        public int AccountStatusId { get; set; }
    }
}
