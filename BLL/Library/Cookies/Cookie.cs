using BLL.Library.Criptografia;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace BLL.Library.Cookies
{
    public class Cookie
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public Cookie(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        /*
           CRUD
        */
        public void Cadastrar(string key, string valor)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);
            options.IsEssential = true;

            var valorCrypt = StringCipher.Encrypt(valor, _configuration.GetValue<string>("KeyCrypt"));


            _contextAccessor.HttpContext.Response.Cookies.Append(key, valorCrypt, options);
        }

        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
            {
                Remover(key);
            }
            Cadastrar(key, valor);
        }

        public void Remover(string key)
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        public bool Existe(string key)
        {
            if (_contextAccessor.HttpContext.Request.Cookies[key] == null)
            {
                return false;
            }
            return true;
        }

        public string Consultar(string key, bool Cript = true)
        {
            var valor = _contextAccessor.HttpContext.Request.Cookies[key];

            if (Cript)
            {
                valor = StringCipher.Decrypt(valor, _configuration.GetValue<string>("KeyCrypt"));
            }
            return valor;
        }

        public void RemoverTodos()
        {
            var ListaCookie = _contextAccessor.HttpContext.Request.Cookies.ToList();

            foreach (var cookie in ListaCookie)
            {
                Remover(cookie.Key);
            }
        }
    }
}
