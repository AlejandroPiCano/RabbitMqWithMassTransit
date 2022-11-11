namespace OcelotApiGateway.Middleware
{
    /// <summary>
    /// The API Key Dependency Injection
    /// </summary>
    public static class APIKeyDependencyInjection
    {
        /// <summary>
        /// The Add Api Token method.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiToken(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<APIKeyConfiguration>(configuration.GetSection(key: "ApiKey"));
        }

        /// <summary>
        /// The Use Api Key Middleware.
        /// </summary>
        /// <param name="webApp"></param>
        public static void UseApiKeyMiddleware(this WebApplication webApp)
        {
            //Do no act on swagger.
            webApp.UseWhen(predicate: context => !context.Request.Path.StartsWithSegments(other: "/swagger"), 
                configuration: appbuilder => appbuilder.UseMiddleware<ApiKeyMiddleware>());
        }
    }
}
