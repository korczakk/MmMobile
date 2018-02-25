using MmMobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MmMobile
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm = new MainPageViewModel();

        public MainPage()
        {
            this.BindingContext = new MainPageViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //zalogowanie do Firebase
            string idToken = vm.FirebaseSignIn();
                        
            //Pobranie danych z Firebase  
            vm.GetData(idToken);

            lista.ItemsSource = vm.mmContent;

            base.OnAppearing();
        }


    }
}
