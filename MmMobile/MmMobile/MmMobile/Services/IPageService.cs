using System.Threading.Tasks;
using Xamarin.Forms;

namespace MmMobile.Services
{
    public interface IPageService
    {
        Task PushAsync(Page page);
    }
}