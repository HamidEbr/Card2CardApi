using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card2CardApi.Service.Utility
{
    public static class HttpContextExtensions
    {
        public static string GetServiceName(this HttpContext httpContext)
        {
            try
            {
                string serviceName = $"{httpContext.Request.RouteValues["controller"]}/{ httpContext.Request.RouteValues["action"]}";
                if (serviceName.Length <= 1)
                    serviceName = httpContext.Request?.Path.Value;

                return serviceName;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static async Task<object> GetInputParamsAsync(this HttpContext httpContext)
        {
            try
            {
                IEnumerable<KeyValuePair<string, object>> routeValue = httpContext.Request.RouteValues
                    .Where(w => w.Key != "controller" && w.Key != "action")
                    .ToArray();

                httpContext.Request.EnableBuffering();

                var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
                string body = await reader.ReadToEndAsync();

                httpContext.Request.Body.Position = 0;
                return new
                {
                    routeValues = routeValue,
                    queryString = httpContext.Request.QueryString.Value,
                    body
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
