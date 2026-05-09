using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Models.Order_Aggreation
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentRecievrd")]
        PaymentRecievrd,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }
}
