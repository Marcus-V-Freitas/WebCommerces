using BLL.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CulturaController : Controller
    {
        /// <summary>
        /// Muda a Cultura utilizada nos retornos da WebApi
        /// </summary>
        /// <param name="cultura"> Nova Cultura (sigla) </param>
        /// <returns> Status Operação </returns>
        [HttpPost]
        [Route("")]
        public IActionResult MudarCultura([FromBody] string cultura)
        {
            if (cultura != null)
            {
                HttpContext.Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue(
                            new RequestCulture(cultura)),
                        new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddYears(1),
                            SameSite = SameSiteMode.None
                        });
            }
            return Ok(true);
        }

        /// <summary>
        /// Pega a cultura atual utilizada pela WebApi
        /// </summary>
        /// <returns> Cultura Utilizada para formatos e UI </returns>
        [HttpGet]
        [Route("")]
        public IActionResult CulturaAtual()
        {
            return Ok(new CulturaDTO()
            {
                CultureFormat = Thread.CurrentThread.CurrentCulture.Name,
                CultureUI = Thread.CurrentThread.CurrentUICulture.Name
            });
        }
    }
}
