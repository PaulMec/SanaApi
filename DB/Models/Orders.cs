using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB.Models
{
    /// <summary>
    /// Represents an order placed by a customer.
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// Gets or sets the primary key for the Order.
        /// </summary>
        /// <value>
        /// The unique identifier for the order.
        /// </value>
        [Key]
        public int OrderID { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated Customer.
        /// </summary>
        /// <value>
        /// The identifier for the customer who placed the order.
        /// </value>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was placed.
        /// </summary>
        /// <value>
        /// The date and time of the order.
        /// </value>
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Navigation property for the associated Customer.
        /// </summary>
        /// <value>
        /// The customer who placed the order. This property is ignored during JSON serialization
        /// to prevent cyclical references.
        /// </value>
        [JsonIgnore]
        [ForeignKey("CustomerID")]
        public virtual Customers Customer { get; set; }

        /// <summary>
        /// Navigation property for accessing the list of order details associated with this order.
        /// </summary>
        /// <value>
        /// A collection of order details that specify the individual products ordered.
        /// This property is ignored during JSON serialization to prevent cyclical references.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
