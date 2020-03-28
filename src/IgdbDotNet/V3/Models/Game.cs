using System.Collections.Generic;

namespace IgdbDotNet.V3.Models
{
    /// <summary>
    /// https://api-docs.igdb.com/#game
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The cover of this game
        /// Reference ID for Cover
        /// https://api-docs.igdb.com/#cover
        /// </summary>
        public int Cover { get; set; }

        /// <summary>
        /// Genres of the game
        /// Array of Genre IDs
        /// https://api-docs.igdb.com/#genre
        /// </summary>
        public List<int> Genres { get; set; }

        /// <summary>
        /// Unique Identifier for this IGDB Game entry
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the game
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Platforms this game was released on
        /// Array of Platform IDs
        /// https://api-docs.igdb.com/#platform
        /// </summary>
        public List<int> Platforms { get; set; }

        /// <summary>
        /// Release dates of this game
        /// Array of Release Date IDs
        /// https://api-docs.igdb.com/#release-date
        /// </summary>
        public List<int> ReleaseDates { get; set; }

        /// <summary>
        /// A url-safe, unique, lower-case version of the name
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// A description of the game
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// How long the game takes to be completed
        /// Reference ID for Time To Beat
        /// https://api-docs.igdb.com/#time-to-beat
        /// </summary>
        public int TimeToBeat { get; set; }
    }
}
