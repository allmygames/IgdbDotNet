using System.Collections.Generic;

namespace IgdbDotNet.V3.Models
{
    /// <summary>
    /// A single news article.
    /// https://api-docs.igdb.com/#pulse
    /// </summary>
    public class Pulse
    {
        /// <summary>
        /// Unique identifier for the IGDB Pulse
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The author of the article according to the publisher
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The url of the main image of the article
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The date this item was initially published by the third party
        /// Unix Time Stamp
        /// </summary>
        public long PublishedAt { get; set; }

        /// <summary>
        /// The ID of the publisher
        /// Reference ID for Pulse Source
        /// https://api-docs.igdb.com/#pulse-source
        /// </summary>
        public int PulseSource { get; set; }

        /// <summary>
        /// A brief extract of the article
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Related entities in the IGDB API
        /// Array of Tag Numbers
        /// https://api-docs.igdb.com/#tag-numbers
        /// </summary>
        public List<int> Tags { get; set; }

        /// <summary>
        /// The title of the article
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Reference ID for Pulse Url
        /// https://api-docs.igdb.com/#pulse-url
        /// </summary>
        public int Website { get; set; }
    }
}
