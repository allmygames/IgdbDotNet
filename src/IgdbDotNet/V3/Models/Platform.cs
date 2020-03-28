namespace IgdbDotNet.V3.Models
{
    /// <summary>
    /// The hardware used to run the game or game delivery network
    /// https://api-docs.igdb.com/#platform
    /// </summary>
    public class Platform
    {
        /// <summary>
        /// Unique identifier for this IGDB platform
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of this platform
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A url-safe, unique, lower-case version of the name
        /// </summary>
        public string Slug { get; set; }
    }
}
