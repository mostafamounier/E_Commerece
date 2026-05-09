using E_Commerece.Core.Models.Order_Aggreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {





        public OrderSpecification(string Email) : base(o => o.BuryerEmail == Email)
        {
            Includes.Add(o=>o.deliveryMethod);
            Includes.Add(o => o.Items);
            this.OrderByDesnc = o => o.OrderDate;
        }

        public OrderSpecification(int Id,string Email) :base(o => o.BuryerEmail == Email&& o.Id == Id)
        {

            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.Items);

        }


    }
}
