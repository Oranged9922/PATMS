using API.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace API.Common.Errors
{
    public class PatmsProblemDetailsFactory : ProblemDetailsFactory
    {

        private readonly ApiBehaviorOptions options;
        private readonly Action<ProblemDetailsContext>? configure;

        public PatmsProblemDetailsFactory(IOptions<ApiBehaviorOptions> options,
                                          IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
        {
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
        }

        public override ProblemDetails CreateProblemDetails(HttpContext httpContext,
                                                            int? statusCode = null,
                                                            string? title = null,
                                                            string? type = null,
                                                            string? detail = null,
                                                            string? instance = null)
        {
            statusCode ??= 500;

            ProblemDetails problemDetails = new()
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }


        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
                                                                                ModelStateDictionary modelStateDictionary,
                                                                                int? statusCode = null,
                                                                                string? title = null,
                                                                                string? type = null,
                                                                                string? detail = null,
                                                                                string? instance = null)
        {
            throw new NotImplementedException();
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status ??= statusCode;

            if (options.ClientErrorMapping.TryGetValue(statusCode, out ClientErrorData? clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            string? traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId is null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            configure?.Invoke(new() { HttpContext = httpContext, ProblemDetails = problemDetails });

            List<Error>? errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;
            if (errors is not null)
            {
                problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
            }
        }

    }
}
