namespace eShopOnContainers.Core.Models.Catalog
{
    /// <summary>
    /// Catalog store data class
    /// </summary>
    public class CatalogStore
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Store name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Store type identifier
        /// </summary>
        public string TypeIdentifier { get; set; }

        /// <summary>
        /// Store type description
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Store latititude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Store longitude
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Store icon
        /// </summary>
        public string Icon { get; set; }
    }
}
