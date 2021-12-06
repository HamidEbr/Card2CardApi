using Card2CardApi.Service.Utility;
using Domain.Base.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Card2CardApi.Service.Middleware
{
    internal class ApiExceptionHandlerMiddleware
    {
        internal sealed class ErrorMessage
        {
            public string Message { get; set; }

            public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

            public ErrorMessage(string message)
            {
                Message = message;
            }
        }

        private readonly ILogger<ApiExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly RequestDelegate _next;

        public ApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var stopWatch = new Stopwatch();
            object inputs = null;
            try
            {
                stopWatch.Start();
                inputs = await httpContext.GetInputParamsAsync();

                await _next(httpContext);
            }
            catch (Exception exception)
            {
                stopWatch.Stop();
                var errorMessage = await HandleExceptionAsync(httpContext, exception);

                _logger.LogCritical(new LogStruct
                {
                    Message = exception.Message,
                    ServiceName = httpContext.GetServiceName(),
                    InputParams = inputs,
                    ResponseTimeStopWatcher = stopWatch,
                    Results = errorMessage,
                    Exception = exception,
                    Tags = LogMessageTag.Input,
                    Method = httpContext.Request?.Method
                });
            }
        }

        private Task<ErrorMessage> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorMessage = new ErrorMessage(CreateErrorMessage(exception));

            switch (exception)
            {
                case NotFoundException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case ConflictException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;

                case InvalidDomainOperationException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;

                case InvalidPanNumberException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    break;

                case UnauthorizedAccessException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case ValidationException validationException:

                    foreach (var error in validationException.Errors)
                        errorMessage.Errors.Add(error.PropertyName, error.ErrorMessage);

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case BusinessException businessException:
                    errorMessage.Errors.Add("Description", businessException.Description);
                    errorMessage.Errors.Add("DueDate", businessException.DueDate.ToString());
                    errorMessage.Errors.Add("Amount", businessException.Amount.ToString());
                    errorMessage.Errors.Add("TrackingCode", businessException.TrackingCode);
                    errorMessage.Errors.Add("DigitalId", businessException.DigitalId);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case ArgumentException argumentException:

                    var argumentMessages = argumentException.FromHierarchy(ex => ex.InnerException)
                        .Select(ex => ex.Message);

                    var argumentMessageCounter = 0;
                    foreach (var message in argumentMessages)
                    {
                        argumentMessageCounter++;
                        errorMessage.Errors.Add(argumentMessageCounter.ToString(), message);
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case SourcePanEqualsDestinationPanException:
                case BaseException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.WriteAsync(JsonConvert.SerializeObject(errorMessage));

            return Task.FromResult(errorMessage);
        }

        private string CreateErrorMessage(Exception exception)
        {
            var exceptionType = exception.GetType();
            var stringBuilder = new StringBuilder(exceptionType.Name);

            IEnumerable<PropertyInfo> exceptionProperties = exceptionType.GetProperties();

            foreach (var exceptionProperty in exceptionProperties)
            {
                stringBuilder.Replace($"{{{exceptionProperty.Name}}}", exceptionProperty.GetValue(exception, null)?.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
