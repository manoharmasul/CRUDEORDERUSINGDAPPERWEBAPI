using CRUDEORDERUSINGDAPPERWEBAPI.Model;

namespace CRUDEORDERUSINGDAPPERWEBAPI.Repository.Interface
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetAllOrders();
        public Task<Order> GetAllOrderById(int id);  
        public Task<int> AddNewOrder(Order order);
        public Task<double> UpdateOrder(Order order);
        public Task<int> DeleteOrder(int id);
        
    }
}
