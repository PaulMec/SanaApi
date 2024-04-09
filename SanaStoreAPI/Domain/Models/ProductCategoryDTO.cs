namespace SanaStoreAPI.Domain.Models
{
    /// <summary>
    /// Represents the association between products and categories in the store.
    /// </summary>
    public class ProductCategoryDTO
    {
        /// <summary>
        /// Gets or sets the identifier of the product associated with the category.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the category associated with the product.
        /// </summary>
        public int CategoryID { get; set; }
    }
}
