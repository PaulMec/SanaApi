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
    /// Represents a product available for sale in the database.
    /// </summary>
    public class Products
    {
        /// <summary>
        /// Gets or sets the primary key for the product.
        /// </summary>
        /// <value>
        /// The unique identifier for the product.
        /// </value>
        [Key]
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product. This field is required and has a maximum length of 50 characters.
        /// </value>
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        /// <value>
        /// A brief description of the product, up to 500 characters.
        /// </value>
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL or path of the product's image.
        /// </summary>
        /// <value>
        /// The image URL or path, up to 500 characters.
        /// </value>
        [StringLength(500)]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        /// <value>
        /// The price of the product, stored as a decimal value with precision 18 and scale 2.
        /// </value>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        /// <value>
        /// The number of units in stock for the product.
        /// </value>
        public int Stock { get; set; }

        /// <summary>
        /// Navigation property for the product categories associated with the product.
        /// </summary>
        /// <value>
        /// A collection of product categories that link this product to its categories.
        /// </value>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Navigation property for the order details that include this product.
        /// </summary>
        /// <value>
        /// A collection of order details where this product has been included in orders.
        /// </value>
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
