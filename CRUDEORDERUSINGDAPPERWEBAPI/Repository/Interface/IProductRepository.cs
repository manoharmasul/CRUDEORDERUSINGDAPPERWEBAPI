using CRUDEORDERUSINGDAPPERWEBAPI.Model;

namespace CRUDEORDERUSINGDAPPERWEBAPI.Repository.Interface
{
    public interface IProductRepository
    {
        public Task<int> AddNewProduct(Product product);
        public Task<int> UpdateProduct(Product product);
    }
}
