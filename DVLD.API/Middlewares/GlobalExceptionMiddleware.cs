using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DVLD.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // حاول تمرير الطلب للطبقات الداخلية (الكنترولر -> البزنس)
                await _next(context);
            }
            catch (Exception ex)
            {
                // إذا حدث أي خطأ (Exception) في أي طبقة، سيتم اصطياده هنا
                _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // تحديد نوع الخطأ: إذا كان خطأ من البزنس نرجعه كـ 400، وإلا 500
            if (exception is InvalidOperationException || exception is ArgumentException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is KeyNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            // الرد الذي سيظهر للفرونت إند
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message, // سيأخذ الرسالة التي كتبناها في الـ throw
                Detailed = context.Response.StatusCode == 500 ? "Internal Server Error" : null
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}