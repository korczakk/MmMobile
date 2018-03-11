using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using FirebaseClient;
using MmMaker.Model;
using MmMobile.Services;
using Xamarin.Forms;
using ZXing.Mobile;

namespace MmMobile.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ExcelContent> _mmContent;
        private readonly IPageService _PageService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ExcelContent> MmContent
        {
            get
            {
                return _mmContent;
            }
            private set
            {
                _mmContent = value;

                OnPropertyChanged("MmContent");
            }
        }

        public MainPageViewModel(IPageService _pageService)
        {
            _PageService = _pageService;
        }

        /// <summary>
        /// Pobiera dane o MM z Firebase
        /// </summary>
        /// <param name="idToken"></param>
        public void GetData()
        {
            string idToken = new FirebaseToken().GetToken().idToken;

            FirebaseDatabase firebase = new FirebaseDatabase(idToken);

            IEnumerable<ExcelContent> Content =
                firebase.GetData<ExcelContent>("https://shoppinglist-dba72.firebaseio.com/MM/MMContent.json");

            MmContent = new ObservableCollection<ExcelContent>(Content);
        }

        /// <summary>
        /// Skanuje kod kreskowy
        /// </summary>
        public async void Scan()
        {
            MobileBarcodeScanner scaner = new MobileBarcodeScanner();

            ZXing.Result scanResult = await scaner.Scan();

            if (scanResult.Text != null)
            {
                await _PageService.PushAsync(new DetailsPage());
            }
        }


        /// <summary>
        /// Rozgłasza zdarzenie OnPropertyChanged (wykonuje metody które się do zdarzenia zapisały)
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
