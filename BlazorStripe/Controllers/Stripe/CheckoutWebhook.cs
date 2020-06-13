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
	public class CheckoutWebhook : ControllerBase
	{
		private readonly string _webhookSecret;

		public CheckoutWebhook(IOptions<StripeConfig> options)
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
					case Events.CheckoutSessionCompleted:
						//retrieve customer and subscription info
						//provision access to appropriate content
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
