using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using FirebaseClient;
using MmMaker.Model;
using MmMobile.Services;
using Xamarin.Forms;
using ZXing.Mobile;
using System.Linq;

namespace MmMobile.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ExcelContent> _mmContent;
        private readonly IPageService _PageService;

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
        public ICommand ScanButtonClick { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel(IPageService _pageService)
        {
            MmContent = new ObservableCollection<ExcelContent>();

            _PageService = _pageService;

            ScanButtonClick = new Command(Scan);
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
                ExcelContent foundItem = FindItem(scanResult.Text);

                if (foundItem == null)
                {
                    await _PageService.DisplayAlert("Brak danych", "Produkt o kodzie " + scanResult.Text + " nie został odnaleziony", "OK");

                    return;
                }

                await _PageService.PushAsync(new DetailsPage(foundItem));

            }
        }

        /// <summary>
        /// Przeszukuje kolekcje MmContent i wybiera jeden obiekt o podanym kodzie kreskowym
        /// </summary>
        /// <param name="text">Kod kreskowy w postaci ciągu znaków</param>
        /// <returns>Znaleziony obiekt lub null</returns>
        private ExcelContent FindItem(string text)
        {
            double barcode;
            bool tryParse = double.TryParse(text, out barcode);

            if (!tryParse)
                return null;

            return MmContent.FirstOrDefault(x => x.BarCode == barcode);

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


        //metoda do wyszukania obiektu w kolekcji - DONE

        //metoda do zapisania nowego obiektu poprzez jego podmiane(?) - współdzilemy VM ale nie instancje obiektu
        //przy otwarciu strony z detalami trzeba przekazać wyszukany obiekt. Następnie przy powrocie do MainPage
        //trzeba przekazać zmieniony obiekt i VM musi go podmienić na podstawie GUID (wyszukać, usunąć, dodać).

        //metoda do zapisania odanychw Firebase



        //ExcelContent found = MmContent[0];

        //found.ProductName = "Dupa Dupa";

        //    MmContent.RemoveAt(0);

        //    MmContent.Insert(0, found);
    }
}
