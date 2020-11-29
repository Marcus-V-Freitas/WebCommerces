using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommerce.Client
{
    public partial class LoginLayout : IDisposable
    {
        protected override void OnInitialized()
        {
            interceptor.RegisterEvent();
        }

        public void Home()
        {
            navigation.NavigateTo("/");
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }
    }
}
