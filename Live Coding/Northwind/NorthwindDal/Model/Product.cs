using System;
using System.Collections.Generic;

namespace NorthwindDal.Model
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public short ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public short? SupplierId { get; set; }
        public short? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        public float? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public int Discontinued { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
