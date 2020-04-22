using System.ComponentModel.DataAnnotations;

namespace BlazorStripe.Models
{
    public class CreditCard
    {
        [Required, Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Required, CreditCard, Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required, Display(Name = "Expiration Month"), Range(0, 12)]
        public long? CardExpiryMonth { get; set; }

        [Required, Display(Name = "Expiration Year"), Range(2020, 2100)]
        public long? CardExpiryYear { get; set; }

        [Required, Display(Name = "CVC Security Code"),
            RegularExpression("^[0-9]*$", ErrorMessage = "CVC security code can only contain numbers")]
        public string CardCvc { get; set; }
    }
}
