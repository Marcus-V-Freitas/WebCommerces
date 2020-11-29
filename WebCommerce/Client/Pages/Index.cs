using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Pages
{
    public partial class Index : IDisposable
    {
        public HomeViewModel homeView { get; set; } = new HomeViewModel();


        protected override async Task OnInitializedAsync()
        {
            interceptor.RegisterEvent();
            homeView = await http.GET<HomeViewModel>(Api.Home);
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

    }
}
