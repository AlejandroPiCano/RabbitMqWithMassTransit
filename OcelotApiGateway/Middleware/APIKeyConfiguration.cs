namespace OcelotApiGateway.Middleware
{
    /// <summary>
    /// The API Key Configuration
    /// </summary>
    internal class APIKeyConfiguration
    {
        /// <summary>
        /// The Client Id
        /// </summary>
        public string? ClientId { get; set; }

        //The Value
        public string? Value { get; set; }
    }
}