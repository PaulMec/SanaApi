namespace SanaStoreAPI.Domain.Models
{
    /// <summary>
    /// Represents a product in the store.
    /// </summary>
    public class ProductsDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL of the product image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }
    }
}
