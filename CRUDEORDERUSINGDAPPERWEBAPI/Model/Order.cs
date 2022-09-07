namespace CRUDEORDERUSINGDAPPERWEBAPI.Model
{
    public class Order
    {
        

        public int oId { get; set; }
        public string custName { get; set; }
        public string billintAddress { get; set; }
        public string shippingAddress { get; set; }
        public List<OrderDetails> orderdetails { get; set; }
        public double totalorderAmmount { get; set; }
       
    }
}
