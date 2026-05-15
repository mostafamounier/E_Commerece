using E_Commerece.Core.Models.Order_Aggreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Specifications
{
    public class OrderStatusSpecification:BaseSpecification<Order>
    {
        public OrderStatusSpecification(string IntendId):base(o=>o.paymentIntentId==IntendId)
        {
                
        }
    }
}
