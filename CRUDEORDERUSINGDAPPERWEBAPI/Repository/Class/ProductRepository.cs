using CRUDEORDERUSINGDAPPERWEBAPI.Context;
using CRUDEORDERUSINGDAPPERWEBAPI.Model;
using CRUDEORDERUSINGDAPPERWEBAPI.Repository.Interface;
using Dapper;

namespace CRUDEORDERUSINGDAPPERWEBAPI.Repository.Class
{
    public class ProductRepository: IProductRepository
    {
        private readonly DapperContext _context;
        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewProduct(Product product)
        {
            var query = "insert into tblProductt (pName,price) values(@pName,@price)";
            using(var connection=_context.CreateConnection())
            {
                var prod = await connection.ExecuteAsync(query, product);


                return prod;
            }
        }
        public async Task<int> UpdateProduct(Product product)
        {
            var query = "update tblProductt set pName=@pName,price=@price where pId=@pId";
            using(var connection=_context.CreateConnection())
            {
                var result=await connection.ExecuteAsync(query,product);
                return result;
            }
        }
    }
}
