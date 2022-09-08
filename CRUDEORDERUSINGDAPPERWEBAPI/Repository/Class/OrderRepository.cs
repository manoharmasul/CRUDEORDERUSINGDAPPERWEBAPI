using CRUDEORDERUSINGDAPPERWEBAPI.Context;
using CRUDEORDERUSINGDAPPERWEBAPI.Model;
using CRUDEORDERUSINGDAPPERWEBAPI.Repository.Interface;
using Dapper;

namespace CRUDEORDERUSINGDAPPERWEBAPI.Repository.Class
{
    public class OrderRepository: IOrderRepository
    {
        private readonly DapperContext _context;
        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewOrder(Order order)
        {
            double rest = 0;
            var query = "insert into tblOrderr(custName,billintAddress,shippingAddress,totalorderAmmount) values(@custName,@billintAddress,@shippingAddress,@totalorderAmmount);SELECT CAST(SCOPE_IDENTITY() as int)";

            List<OrderDetails> orderdetailslist = new List<OrderDetails>();
            orderdetailslist=order.orderdetails.ToList();
            using (var connection = _context.CreateConnection())
            {
                // int result = await connection.QueryAsync<int>(query, order);
                int result = await connection.QuerySingleAsync<int>(query, order);

                if(result !=null)
                {
                    rest = await insertupDate(orderdetailslist, result);                    
                    var qry = "update tblOrderr set totalorderAmmount=@totalorderAmmount where oId=@oId";
                    var retss = await connection.ExecuteAsync(qry, new { totalorderAmmount =rest,oId=result});

                }
                return Convert.ToInt32(rest); 

            }
        }
        public async Task<double> insertupDate(List<OrderDetails> odlist,int result)
        {
            Order orrder=new Order();   
            int ret = 0;
            double gdTotal = 0;
            if(result !=0)
            {
                using (var connnection=_context.CreateConnection())
                {
                    foreach(var od in odlist)
                    { 
                        od.oId = result;
                        var query = "select price from tblProductt where pId=@pId";
                        var resultprice = await connnection.QuerySingleAsync<double>(query, new { pId = od.pId });
                        od.totalAmmount = resultprice * od.qty;
                        var query1 = "insert into tblorderDetailss (oId,pId,qty,totalAmmount) values(@oId,@pId,@qty,@totalAmmount)";
                        var result1 = await connnection.ExecuteAsync(query1, od);

                        ret=ret + result1;
                        gdTotal = gdTotal + od.totalAmmount;
                        orrder.totalorderAmmount = gdTotal;

                        //must declare the scaler Variable total orderammount

                        //var p = new DynamicParameters();
                        //p.Add("totalorderAmmount", orrder.totalorderAmmount);
                        //p.Add("oId", od.oId);
                        //var qry = "update tblOrderr set totalorderAmmount=@totalorderAmmount where oId=@oId";
                        //var retss = await connnection.ExecuteAsync(qry, p);
                    }


                   // orrder.totalorderAmmount=gdTotal;   
                  // var qry = "update tblOrderr set totalorderAmmount=@totalorderAmmount where oId=@oId";
                 // var retss = await connnection.ExecuteAsync(qry, new {oId=odlist.oId});
                }
            }
            return gdTotal;
        }

        public async Task<Order> GetAllOrderById(int id)
        {
            Order od=new Order();
            var query = "select * from tblOrderr where oId=@oId";
            using(var connection=_context.CreateConnection())
            {
                var order = await connection.QueryAsync<Order>(query, new { oId = id });
                od = order.FirstOrDefault();
                if(od !=null)
                {
                    var resullt=await connection.QueryAsync<OrderDetails>(@"select * from tblorderDetailss where oId=@oId", new { oId = id });

                    od.orderdetails = resullt.ToList();
                    
                }
                return od;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            List<Order> olist = new List<Order>();
            var query = "select * from tblOrderr";
            using(var connecction=_context.CreateConnection())
            {
                var order = await connecction.QueryAsync<Order>(query);
                olist= order.ToList();

                foreach (var oli in olist)
                {
                    var result = await connecction.QueryAsync<OrderDetails>(@"select * from tblorderDetailss where oId=@oId",new {oId=oli.oId});

                    oli.orderdetails = result.ToList();


                }
                return olist;
            }
        }

        /*public async Task<int> UpdateOrder(Order order)
        {
            double rest = 0;
            var query = @"update tblOrderr set custName=@custName,shippingAddress=@shippingAddress,
                totalorderAmmount=@totalorderAmmount;SELECT CAST(SCOPE_IDENTITY() as int)";
            List<OrderDetails> orderdetailslist = new List<OrderDetails>();
            orderdetailslist = order.orderdetails.ToList();
            using (var connection = _context.CreateConnection())
            { 
                
                // int result = await connection.QueryAsync<int>(query, order);
                int result = await connection.QuerySingleAsync<int>(query, order);
                var qry = "delete from tblorderDetailss where oId=@oId";
                var resultd = await connection.ExecuteAsync(qry, new { oId = result });

                if (result != null)
                {
                    rest = await insertupDate(orderdetailslist, result);
                    order.totalorderAmmount = rest;

                }
                return Convert.ToInt32(rest);

            }
            /* double rest = 0;
             var query = "update tblOrderr set custName=@custName,shippingAddress=@shippingAddress,orderdetails=@orderdetails,totalorderAmmount=@totalorderAmmount;SELECT CAST(SCOPE_IDENTITY() as int)";
            // List<OrderDetails> odlist = new List<OrderDetails>();
            // odlist = order.orderdetails.ToList();
             using (var connection = _context.CreateConnection())
             {

                 var orderr = await connection.ExecuteAsync(query, order);
                 if (orderr != null)
                 {
                     rest = await UpdateOrderDetails(order.orderdetails, orderr);
                 }

                 return Convert.ToInt32(rest);
             }

        }*/
       /* public async Task<double> UpdateOrderDetails(List<OrderDetails> orderlist,int result)
        {
            Order order=new Order();
            int ret = 0;
            double gdTotal = 0;
            if(result !=null)
            {
                using(var connection = _context.CreateConnection())
                {
                    foreach(var od in orderlist)
                    {
                        od.oId=result;
                        var query = "select * from tblProductt where pId=@pId";
                        var resprice=await connection.QuerySingleAsync<int>(query,new { pId = od.pId });
                        od.totalAmmount = resprice * od.qty;
                        var qry = "update tblorderDetailss set oId=@oId,pId=@pId,qty=@qty,totalAmmount=@totalAmmount where odId=@odId";
                        var result1 = await connection.ExecuteAsync(qry, od);
                        ret=ret+result1;    
                        gdTotal=gdTotal+od.totalAmmount;

                        order.totalorderAmmount = gdTotal;

                        //must declare the scaler Variable total orderammount

                        var p = new DynamicParameters();
                        p.Add("totalorderAmmount", order.totalorderAmmount);
                        p.Add("oId", od.oId);
                        var qry1 = "update tblOrderr set totalorderAmmount=@totalorderAmmount where oId=@oId";
                        var retss = await connection.ExecuteAsync(qry1, p);

                    }
                   
                }
                
            }
            return gdTotal;
        }*/

        public async Task<int> DeleteOrder(int id)
        {
            var query = "delete tblOrderr where oId=@oId";
            using(var connection = _context.CreateConnection())
            {
                var resultd =await DeleteOrderDetails(id);   

                var result = await connection.ExecuteAsync(query, new { oId = id });
                return result;

            }
        }
        public async Task<int> DeleteOrderDetails(int id)
        {
            var query = "delete from tblorderDetailss where oId=@oId";
            using(var connection=_context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new {oId=id});
                return result;
            }
        }

        public async Task<double> UpdateOrder(Order order)
        {
            double ret = 0;
            var query = "update tblOrderr set custName=@custName,billintAddress=@billintAddress,shippingAddress=@shippingAddress,totalorderAmmount=@totalorderAmmount where oId=@oId";
           
            //List<OrderDetails> orderdetailslist = new List<OrderDetails>();
            //orderdetailslist = order.orderdetails.ToList();

            using (var connection = _context.CreateConnection())
            {
                int result = await connection.ExecuteAsync(query, order);
                if (result != null)
                {
                    var qery = "delete from tblorderDetailss  where oId=@oId";
                    var resultd = await connection.ExecuteAsync(qery, new { oId = order.oId });


                    ret = await insertupDate(order.orderdetails,order.oId);

                    var qry = "update tblOrderr set totalorderAmmount=@totalorderAmmount where oId=@oId";
                    var retss = await connection.ExecuteAsync(qry, new { totalorderAmmount = ret, oId = order.oId });

                    //must declare the scaler Variable total orderammount
                    // List<OrderDetails> orderdetailslist = new List<OrderDetails>();
                    //  var p = new DynamicParameters();
                    // p.Add("totalorderAmmount", order.totalorderAmmount);
                    //   p.Add("oId", result);
                    //var qry = "update tblOrderr set totalorderAmmount=@totalorderAmmount where oId=@oId";
                    //var retss = await connection.ExecuteAsync(qry, order.);
                }
                return ret; 
            }
        }

    }
}
