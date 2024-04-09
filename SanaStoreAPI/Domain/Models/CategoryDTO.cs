namespace SanaStoreAPI.Domain.Models
{
    /// <summary>
    /// Represents a category of products in the store.
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the category.
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
