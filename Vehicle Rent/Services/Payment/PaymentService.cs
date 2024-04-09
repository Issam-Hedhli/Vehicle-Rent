using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Vehicle_Rent.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }
        public string CreateCheckOutSession(string amount, string currency, string successUrl, string cancelUrl)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData= new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount=Convert.ToInt32(amount) * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name="Product Name",
                                Description="Product Description"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };
            var service = new SessionService();
            var session = service.Create(options);
            // you may need to story session.Id;
            return session.Url;
        }
    }
}
