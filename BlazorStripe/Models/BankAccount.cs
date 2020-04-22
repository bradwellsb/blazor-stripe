using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStripe.Models
{
    public class BankAccount
    {
        [Required]
        public string AccountHolderName { get; set; }
        [Required,
            MinLength(9, ErrorMessage = "Routing number should be 9 digits"),
            MaxLength(9, ErrorMessage = "Routing Number should be 9 digits"),
            RegularExpression("^[0-9]*$", ErrorMessage = "Routing Number can only contain numbers")]
        public string RoutingNumber { get; set; }

        [Required,
            RegularExpression("^[0-9]*$", ErrorMessage = "Account Number can only contain numbers")]
        public string AccountNumber { get; set; }
    }
}
