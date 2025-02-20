namespace FrothyFrolics_server_.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserEmail { get; set; }

        public string ProductDetails {  get; set; }

        public int couponEarned {  get; set; }
    }
}
