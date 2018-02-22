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
        ProductListViewModel vm = new ProductListViewModel();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //zalogowanie do Firebase
            string idToken = vm.FirebaseSignIn();
            
            //Pobranie danych z Firebase  
            vm.GetData(idToken);

            base.OnAppearing();
        }


    }
}
