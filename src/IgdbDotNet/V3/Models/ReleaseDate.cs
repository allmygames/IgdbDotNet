namespace IgdbDotNet.V3.Models
{
    /// <summary>
    /// Options for the format category of the release date
    /// https://api-docs.igdb.com/#release-date
    /// </summary>
    public enum ReleaseDateCategory
    {
        YearMonthDay,   // YYYYMMMMDD
        YearMonth,      // YYYYMMMM
        Year,           // YYYY
        YearQ1,         // YYYYQ1
        YearQ2,         // YYYYQ2
        YearQ3,         // YYYYQ3
        YearQ4,         // YYYYQ4
        TBD,            // TBD
    }

    /// <summary>
    /// A handy endpoint that extends game release dates. Used to dig deeper into release dates, platforms and versions.
    /// https://api-docs.igdb.com/#release-date
    /// </summary>
    public class ReleaseDate
    {
        /// <summary>
        /// The format category of the release date
        /// </summary>
        public ReleaseDateCategory Category { get; set; }

        /// <summary>
        /// The date of the release (Unix Time Stamp)
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// The platform of the release
        /// Reference ID for Platform
        /// https://api-docs.igdb.com/#platform
        /// </summary>
        public int Platform { get; set; }
    }
}
