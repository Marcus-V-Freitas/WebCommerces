﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCommerce.Client.Services.Cookie;

namespace WebCommerce.Client.Services.Cookie
{
    /// <summary>
    /// Service for reading the current cookie on the browser
    /// </summary>
    public class BrowserCookieService : IBrowserCookieService
    {
        private readonly IJsInterop JsInterop;

        public BrowserCookieService(IJsInterop jsInterop)
        {
            JsInterop = jsInterop;
        }

        /// <summary>
        /// returns the cookie value or null if not set
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public async Task<string> Get(Func<string, bool> filterCookie)
        {
            return (await JsInterop
                .GetCookie())
                .Split(';')
                .Select(v => v.TrimStart().Split('='))
                .Where(s => filterCookie(s[0]))
                .Select(s => s[1])
                .FirstOrDefault();
        }
    }
}
