using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStripe.Controllers.Stripe
{
    [Route("stripe/[controller]")]
    public class ConnectWebhook : ControllerBase
    {
        private readonly string _webhookSecret;

        public ConnectWebhook(IOptions<StripeConfig> options)
        {
            _webhookSecret = options.Value.WebhookSigningKey;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _webhookSecret);
                switch (stripeEvent.Type)
                {
                    case Events.AccountExternalAccountUpdated:
                        //make sure account info is still valid
                        break;
                }
                //All response codes besides 2xx indicate to Stripe that you did not receive the event.
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
