using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using EngineETL.Core.Domain.Common.Exceptions;

namespace EngineETL.API
{
    public class CustomResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomResponseMiddleware(RequestDelegate next, ILogger<CustomResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.
            if (context.Request.Path.HasValue && context.Request.Path.ToUriComponent().StartsWith("/swagger", StringComparison.CurrentCultureIgnoreCase))
                await _next.Invoke(context);
            else
                await InvokeCustom(context);

            // Clean up.
        }

        public async Task InvokeCustom(HttpContext context)
        {

            var currentBody = context.Response.Body;
            CommonApiResponse result = null;
            using (var memoryStream = new MemoryStream())
            {

                //set the current response to the memorystream.                
                try
                {
                    context.Response.Body = memoryStream;

                    await _next(context);

                    //reset the body 
                    context.Response.Body = currentBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    object objResult;

                    var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                    if (IsValidJson(readToEnd))
                        objResult = JsonConvert.DeserializeObject(readToEnd);
                    else
                        objResult = readToEnd;
                    result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, null);
                }
                catch (HandledException ex)
                {
                    context.Response.Body = memoryStream;
                    context.Response.Body = currentBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                    var objResult = readToEnd;
                    result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, null, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"INTERNAL_ERROR - {ex.Message}");
                    var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                    var objResult = readToEnd;
                    result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, null, "INTERNAL_ERROR");
                }


                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }

        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }

    public class CommonApiResponse
    {
        public static CommonApiResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new CommonApiResponse(statusCode, result, errorMessage);
        }


        public int StatusCode { get; set; }

        public string RequestId { get; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        protected CommonApiResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
