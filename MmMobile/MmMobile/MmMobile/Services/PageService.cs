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

        public async  Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
