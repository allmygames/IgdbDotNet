using IgdbDotNet.V3.Models;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IgdbDotNet.V3
{
    /// <summary>
    /// Client for the IGDB V3 API.
    /// https://api-docs.igdb.com/
    /// </summary>
    public class IgdbApiV3Client : IIgdbApiV3Client
    {
        private const string BaseUrl = "https://api-v3.igdb.com";

        private readonly string apiKey;

        /// <summary>
        /// https://api-docs.igdb.com/#endpoints
        /// </summary>
        private readonly IDictionary<Type, string> endpointDictionary = new Dictionary<Type, string>()
        {
            { typeof(Game), "/games/" },
            { typeof(GameExtended), "/games/" },
            { typeof(Genre), "/genres/" },
            { typeof(Platform), "/platforms/" },
            { typeof(Pulse), "/pulses/" },
            { typeof(PulseExtended), "/pulses/" },
            { typeof(PulseSource), "/pulse_sources/" },
            { typeof(ReleaseDate), "/release_dates/" },
        };

        private readonly ILogger logger;

        /// <summary>
        /// Initializes IgdbApiV3Client object with the given IGDB V3 API user key and optional ILogger.
        /// </summary>
        /// <param name="apiKey">IGDB V3 user key</param>
        /// <param name="logger">Optional ILogger</param>
        public IgdbApiV3Client(string apiKey, ILogger logger = null)
        {
            this.apiKey = apiKey;
            this.logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>(string fields = "*")
            where T : class
        {
            // Igdb API V3 restricts access to results on the Free tier.
            // Limit max is 50 and offset max is 150, effectively limiting the caller to the first 200 results.
            const int MaxLimit = 50;
            const int MaxOffset = 150;

            Type type = typeof(T);
            if (!this.endpointDictionary.ContainsKey(type))
            {
                this.logger?.LogError($"GetAllAsync<T> called with unsupported type: {type}.");
                throw new InvalidOperationException($"Unsupported type: {type}");
            }

            string endpoint = this.endpointDictionary[type];

            var results = new List<T>();

            int offset = 0;
            int totalResults = 0;
            bool moreResultsExist;
            do
            {
                var request = new RestRequest(endpoint).AddQueryParameter("fields", fields)
                    .AddQueryParameter("limit", $"{MaxLimit}").AddQueryParameter("offset", $"{offset}");

                var response = await this.BuildRestClient().ExecuteGetAsync<List<T>>(request);
                if (!response.IsSuccessful)
                {
                    return null;
                }

                results.AddRange(response.Data);

                // If this is the first request, try to get the total result count from the header
                if (offset == 0)
                {
                    string totalResultsString =
                        response.Headers.FirstOrDefault(x => x.Name == "X-Count")?.Value?.ToString();
                    if (totalResultsString != null)
                    {
                        bool parsedSuccessfully = int.TryParse(totalResultsString, out totalResults);
                        if (!parsedSuccessfully)
                        {
                            this.logger.LogError(
                                $"Failed to parse X-Count header from IGDB response for endpoint: {endpoint}");
                        }
                    }
                }

                // Increase the offset parameter
                offset += MaxLimit;

                // Keep issuing requests until we have exceeded the max offset or there are no remaining results
                moreResultsExist = totalResults == 0 || totalResults > offset;
            }
            while (offset <= MaxOffset && moreResultsExist);

            return results;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<T> GetByIdAsync<T>(int id, string fields = "*")
            where T : class
        {
            var response = await this.GetManyByIdAsync<T>(new List<int>() { id }, fields);
            return response.FirstOrDefault();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <param name="fields"></param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<IEnumerable<T>> GetManyByIdAsync<T>(IEnumerable<int> ids, string fields = "*")
            where T : class
        {
            Type type = typeof(T);
            if (!this.endpointDictionary.ContainsKey(type))
            {
                this.logger?.LogError($"GetByIdAsync<T> called with unsupported type: {type}.");
                throw new InvalidOperationException($"Unsupported type: {type}");
            }

            string endpoint = this.endpointDictionary[type];
            string csvIds = string.Join(",", ids);
            IRestRequest request = new RestRequest($"{endpoint}{csvIds}").AddQueryParameter("fields", fields);

            var response = await this.BuildRestClient().ExecuteGetAsync<IEnumerable<T>>(request);
            if (!response.IsSuccessful)
            {
                this.logger?.LogError($"Failed response from endpoint: {endpoint}.");
            }

            return response.Data;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParameters"></param>
        /// <param name="fields"></param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(
            IDictionary<string, string> queryParameters,
            string fields = "*")
        {
            if (queryParameters == null)
            {
                throw new ArgumentNullException(nameof(queryParameters));
            }

            Type type = typeof(T);
            if (!this.endpointDictionary.ContainsKey(type))
            {
                this.logger?.LogError($"SearchAsync<T> called with unsupported type: {type}.");
                throw new InvalidOperationException($"Unsupported type: {type}");
            }

            string endpoint = this.endpointDictionary[type];
            IRestRequest request = new RestRequest(endpoint);
            request.AddQueryParameter("fields", fields);
            foreach (var queryParameter in queryParameters)
            {
                request.AddQueryParameter(queryParameter.Key, queryParameter.Value);
            }

            var response = await this.BuildRestClient().ExecuteGetAsync<IEnumerable<T>>(request);
            if (!response.IsSuccessful)
            {
                this.logger?.LogError($"Failed response from endpoint: {endpoint}.");
            }

            return response.Data;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="fields"></param>
        /// <param name="numResults"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IEnumerable<T>> SearchAsync<T>(string query, string fields = "*", int numResults = 50)
        {
            Type type = typeof(T);
            if (!this.endpointDictionary.ContainsKey(type))
            {
                this.logger?.LogError($"SearchAsync<T> called with unsupported type: {type}.");
                throw new InvalidOperationException($"Unsupported type: {type}");
            }

            string endpoint = this.endpointDictionary[type];
            IRestRequest request = new RestRequest(endpoint).AddQueryParameter("fields", fields)
                .AddParameter("limit", numResults)
                .AddQueryParameter("search", query);

            var response = await this.BuildRestClient().ExecuteGetAsync<IEnumerable<T>>(request);
            if (!response.IsSuccessful)
            {
                this.logger?.LogError($"Failed response from endpoint: {endpoint}.");
            }

            return response.Data;
        }

        /// <summary>
        /// Builds a new IRestClient configured to make requests to the IGDB V3 API.
        /// </summary>
        /// <returns>Configured IRestClient object.</returns>
        private IRestClient BuildRestClient()
        {
            return new RestClient(BaseUrl)
                .AddDefaultHeader("Accept", "application/json")
                .AddDefaultHeader("user-key", this.apiKey);
        }
    }
}
