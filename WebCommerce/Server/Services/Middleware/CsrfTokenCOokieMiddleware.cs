using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Server.Services.Middleware
{
    public class CsrfTokenCOokieMiddleware
    {
        private readonly IAntiforgery _antiforgery;
        private readonly RequestDelegate _next;

        public CsrfTokenCOokieMiddleware(IAntiforgery antiforgery, RequestDelegate next)
        {
            _antiforgery = antiforgery;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies["CSRF-TOKEN"] == null)
            {
                var token = _antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("CSRF-TOKEN", token.RequestToken, new Microsoft.AspNetCore.Http.CookieOptions { HttpOnly = false });
            }
            await _next(context);
        }
    }
}

