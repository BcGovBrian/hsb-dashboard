using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HSB.API.Config
{

    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "HSB API", // TODO: From configuration
                Version = description.ApiVersion.ToString(),
                Description = "Hosting Storage Dashboard", // TODO: From configuration
                Contact = new OpenApiContact() { Name = "Support", Email = "support@gov.bc.ca" }, // TODO: From configuration
                License = new OpenApiLicense() { Name = "APACHE", Url = new Uri("https://github.com/bcgov/hsb-dashboard/blob/main/LICENSE") } // TODO: From configuration
            };

            if (description.IsDeprecated)
            {
                info.Description += " (This API version has been deprecated).";
            }

            return info;
        }
    }
}
