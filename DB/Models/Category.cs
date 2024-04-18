using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    /// <summary>
    /// Represents a category of products within the database.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the primary key for this category.
        /// </summary>
        /// <value>
        /// The unique identifier for the category.
        /// </value>
        [Key]
        public int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category, which is required and limited to 50 characters.
        /// </value>
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        /// <summary>
        /// Navigation property for accessing related product categories.
        /// </summary>
        /// <value>
        /// A collection of product categories that are linked to this category.
        /// </value>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
