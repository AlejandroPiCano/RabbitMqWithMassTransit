using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace OcelotApiGateway.Middleware
{
    /// <summary>
    /// Api Key Middleware class.
    /// </summary>
    internal class ApiKeyMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Api Key Middleware constructor
        /// </summary>
        /// <param name="next"></param>
        public ApiKeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// The Invoke.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="apiToken"></param>
        /// <returns>The Task</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task Invoke(HttpContext context, IOptions<APIKeyConfiguration> apiToken)
        {
            if (context.Request.Headers.TryGetValue("apiKey", out StringValues apiKey))
            {
                if (apiKey == apiToken.Value.Value)
                {
                    await next(context);
                }
                else
                {
                    ReturnApiKeyError();
                }
            }
            else
            {
                ReturnApiKeyError();
            }

            void ReturnApiKeyError()
            {
                throw new UnauthorizedAccessException("The API Key is missing or not match");
            }
        }
    }
}