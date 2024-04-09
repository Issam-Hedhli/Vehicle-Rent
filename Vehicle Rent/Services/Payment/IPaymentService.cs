namespace Vehicle_Rent.Services.Payment
{
    public interface IPaymentService
    {
        public string CreateCheckOutSession(string amount, string currency, string successUrl, string cancelUrl);
    }
}
