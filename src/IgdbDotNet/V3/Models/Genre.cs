namespace IgdbDotNet.V3.Models
{
    public class Genre
    {
        /// <summary>
        /// Unique identifier for the IGDB Genre
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the genre
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A url-safe, unique, lower-case version of the name
        /// </summary>
        public string Slug { get; set; }
    }
}
