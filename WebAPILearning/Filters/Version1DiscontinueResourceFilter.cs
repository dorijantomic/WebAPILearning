using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPILearning.Filters
{
    public class Version1DiscontinueResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2"))
            {
                context.Result = new BadRequestObjectResult(
                        new
                        {
                            Versioning = new[] { "This version of the API has expired, please use the latest version" }
                        }
                       );
            }
        }
    }
}