using System.Linq;
using imperugo.wpc.netflix.apis.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace imperugo.wpc.netflix.apis.Swagger
{
    public class BadRequestResponseOperationFilter: IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var authAttributes = context.ApiDescription
                .ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<ValidateModelAttribute>();

            if (authAttributes.Any() && !operation.Responses.Any(x => x.Key == "400"))
            {
                operation.Responses.Add("400", new Response { Description = "Bad Request" });
            }
        }
    }
}