using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;

namespace MmMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanerPage : ContentPage
    {
        MobileBarcodeScanner scanner;

        public ScanerPage()
        {
            InitializeComponent();

            scanner = new ZXing.Mobile.MobileBarcodeScanner();

            Scan();
        }

        private async void Scan()
        {
            var result = await scanner.Scan();

            if (result != null)
            {
                DisplayAlert("scan", result.ToString(), "anuluj");
            }
        }
    }
}