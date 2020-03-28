namespace IgdbDotNet.V3.Models
{
    /// <summary>
    /// A news article source such as IGN.
    /// https://api-docs.igdb.com/#pulse-source
    /// </summary>
    public class PulseSource
    {
        /// <summary>
        /// Unique identifier for this IGDB Pulse Source
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// If the source only contains news for a specific game, that game ID will be here
        /// Reference ID for Game
        /// https://api-docs.igdb.com/#game
        /// </summary>
        public int Game { get; set; }

        /// <summary>
        /// Name of the article’s publisher
        /// </summary>
        public string Name { get; set; }
    }
}
