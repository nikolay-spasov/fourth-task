namespace FourthTask.DomainModels
{
    public class OrderDetails
    {
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public Product Product { get; set; }
    }
}
