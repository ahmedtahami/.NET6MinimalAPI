namespace MinimalAPI.Endpoints
{
    public static class DefaultAPI
    {
        public static void ConfigureDefaultAPI(this WebApplication app)
        {
            var dotNetVer = Environment.Version.ToString()[0];
            app.MapGet("/", () => $"Hello World! This is .NET {dotNetVer} Minimal API").ExcludeFromDescription();
        }
    }
}
