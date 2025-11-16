namespace GoVisit.Middleware
{
    using GoVisit.Enums;
    using GoVisit.Exceptions;
    using GoVisit.Models;
    using System.Net;
    using System.Text.Json;

    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");

                var (statusCode, errorCode) = MapException(ex);

                var error = new ErrorResponse
                {
                    ErrorCode = errorCode,
                    Message = ex.Message,
#if DEBUG
                    Details = ex.StackTrace
#endif
                };

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }

        private static (int StatusCode, string ErrorCode) MapException(Exception ex)
        {
            return ex switch
            {
                NotFoundException => ((int)HttpStatusCode.NotFound, ErrorCodes.NotFound),
                ArgumentException => ((int)HttpStatusCode.BadRequest, ErrorCodes.Argument),
                InvalidOperationException => ((int)HttpStatusCode.BadRequest, ErrorCodes.Validation),

                MongoDB.Driver.MongoException
                    => ((int)HttpStatusCode.InternalServerError, ErrorCodes.Database),

                _ => ((int)HttpStatusCode.InternalServerError, ErrorCodes.Unknown),
            };
        }
    }

}
