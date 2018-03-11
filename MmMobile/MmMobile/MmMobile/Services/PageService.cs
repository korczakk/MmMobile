using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MmMobile.Services
{
    public class PageService : IPageService
    {
        public Task PushAsync(Page page)
        {
            return Application.Current.MainPage.Navigation.PushAsync(page);           
        }
    }
}
