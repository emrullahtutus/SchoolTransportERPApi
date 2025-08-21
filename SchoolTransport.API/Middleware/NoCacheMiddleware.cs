namespace SchoolTransport.API.Middleware
{
    public class NoCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public NoCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Sadece API endpoint'leri için
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate, private");
                context.Response.Headers.Add("Pragma", "no-cache");
                context.Response.Headers.Add("Expires", "0");
            }

            await _next(context);
        }
    }
}
