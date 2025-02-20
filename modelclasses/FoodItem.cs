namespace FrothyFrolics_server_.Models
{
    public class FoodItem
    {
        public string title { get;set; }
        public string description { get; set; }
        public double price { get; set; }
        public string imageResId { get; set; }
        public int quantity {  get; set; }

        public string category { get; set; }    
    }
}