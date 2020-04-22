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
    public class CustomerWebhook : ControllerBase
    {
        private readonly string _webhookSecret;

        public CustomerWebhook(IOptions<StripeConfig> options)
        {
            _webhookSecret = options.Value.WebhookSigningKey;
        }

		[HttpPost]
		public async Task<IActionResult> Index()
		{
			string json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], _webhookSecret);
				switch (stripeEvent.Type)
				{
					case Events.CustomerSourceUpdated:
						//make sure payment info is valid
						break;
					case Events.CustomerSourceExpiring:
						//send reminder email to update payment method
						break;
					case Events.ChargeFailed:
						//do something
						break;
				}
				return Ok();
			}
			catch (StripeException e)
			{
				return BadRequest();
			}
		}
	}
}
