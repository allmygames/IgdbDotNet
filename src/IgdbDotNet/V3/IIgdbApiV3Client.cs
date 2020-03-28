using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgdbDotNet.V3
{
    public interface IIgdbApiV3Client
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<IEnumerable<T>> GetAllAsync<T>(string fields = "*")
            where T : class;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <param name="fields"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<IEnumerable<T>> GetManyByIdAsync<T>(IEnumerable<int> ids, string fields = "*")
            where T : class;
    }
}
