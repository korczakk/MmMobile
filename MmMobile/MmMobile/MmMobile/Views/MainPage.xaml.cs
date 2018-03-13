using MmMobile.Services;
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

        public MainPage()
        {
            MainPageViewModel vm = new MainPageViewModel(new PageService());


            this.BindingContext = vm;

            //Pobranie danych z Firebase  
            vm.GetData();

            InitializeComponent();
        }

    }
}
