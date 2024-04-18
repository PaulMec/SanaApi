using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    /// <summary>
    /// Represents a single line item in an order, linking a product and its quantity to an order.
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// Gets or sets the primary key for the OrderDetail.
        /// </summary>
        /// <value>
        /// The unique identifier for the order detail.
        /// </value>
        [Key]
        public int OrderDetailID { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated Order.
        /// </summary>
        /// <value>
        /// The identifier for the order this detail belongs to.
        /// </value>
        public int OrderID { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated Product.
        /// </summary>
        /// <value>
        /// The identifier for the product being ordered.
        /// </value>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product ordered.
        /// </summary>
        /// <value>
        /// The quantity of the product.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price at which the product was ordered.
        /// </summary>
        /// <value>
        /// The price of the product per unit at the time of the order.
        /// </value>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Navigation property for the associated Order.
        /// </summary>
        /// <value>
        /// The order that this order detail is part of.
        /// </value>
        [ForeignKey("OrderID")]
        public virtual Orders Order { get; set; }

        /// <summary>
        /// Navigation property for the associated Product.
        /// </summary>
        /// <value>
        /// The product that is being ordered.
        /// </value>
        [ForeignKey("ProductID")]
        public virtual Products Product { get; set; }
    }
}
