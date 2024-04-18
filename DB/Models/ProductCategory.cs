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
    /// Represents the many-to-many relationship between products and categories.
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Gets or sets the foreign key for the associated product.
        /// </summary>
        /// <value>
        /// The identifier of the product that is part of this relationship.
        /// </value>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated category.
        /// </summary>
        /// <value>
        /// The identifier of the category that is part of this relationship.
        /// </value>
        public int CategoryID { get; set; }

        /// <summary>
        /// Navigation property for the associated product.
        /// </summary>
        /// <value>
        /// The product associated with this category. This allows for navigation from the association
        /// to the product entity.
        /// </value>
        [ForeignKey("ProductID")]
        public virtual Products Product { get; set; }

        /// <summary>
        /// Navigation property for the associated category.
        /// </summary>
        /// <value>
        /// The category associated with this product. This allows for navigation from the association
        /// to the category entity.
        /// </value>
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}
