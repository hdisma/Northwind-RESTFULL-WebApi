using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Northwind.Core.Entities.Northwind
{
    public class OrderDetail
    {
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
