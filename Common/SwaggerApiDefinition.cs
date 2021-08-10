namespace Common
{
    public class SwaggerApiDefinition
    {
        public SwaggerApiDefinition(string apiName, string apiVersion, string apiXmlName)
        {
            this.ApiName = apiName;
            this.ApiVersion = apiVersion;
            this.SwaggerUrl = $"/swagger/{apiVersion}/swagger.json";
            this.ApiXmlName = apiXmlName;
        }

        public string ApiName { get; }
        public string ApiVersion { get; }
        public string SwaggerUrl { get; }
        public string ApiXmlName { get; }
    }
}