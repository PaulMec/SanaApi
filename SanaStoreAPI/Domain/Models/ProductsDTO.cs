namespace SanaStoreAPI.Domain.Models
{
    /// <summary>
    /// Represents a product in the store.
    /// </summary>
    public class ProductsDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
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
        /// Gets or sets the URL to the image representing the product.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity available for the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the list of categories associated with the product.
        /// </summary>
        public List<CategoryDTO> Categories { get; set; }
    }
}
