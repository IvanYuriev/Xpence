using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Scenario.AuthSrv
{
    public static class ConfigurationOptions
    {
        public static void ApplyDefaultVersioning(ApiVersioningOptions options)
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.UseApiBehavior = false;

            options.Conventions.Add(new VersionByNamespaceConvention());
        }

        public static void ApplyUrlBasedVersioning(ApiExplorerOptions options)
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        }
    }
}