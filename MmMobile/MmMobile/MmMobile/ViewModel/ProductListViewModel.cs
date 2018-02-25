using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using FirebaseClient;
using MmMaker.Model;

namespace MmMobile.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        string _appKey = "AIzaSyBaQegiosq-yCEp1CdNsZ6dGiAhQgN8fgw";

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ExcelContent> mmContent { get; private set; }

        /// <summary>
        /// Pobiera dane o MM z Firebase
        /// </summary>
        /// <param name="idToken"></param>
        public void GetData(string idToken)
        {
            FirebaseDatabase firebase = new FirebaseDatabase(idToken);

            IEnumerable<ExcelContent> Content = 
                firebase.GetData<ExcelContent>("https://shoppinglist-dba72.firebaseio.com/MM/MMContent.json");

            mmContent = new ObservableCollection<ExcelContent>(Content);
        }

        /// <summary>
        /// Loguje się do Firebase i zwraca idToken
        /// </summary>
        /// <returns>Token</returns>
        public string FirebaseSignIn()
        {
            FirebaseAuthentication auth = new FirebaseAuthentication(_appKey);

            SignInResponse signinResult = auth.SignInWithEmail("kamil.korczak@gmail.com", "HPdj690P");

            return signinResult.idToken;
        }

        protected  void OnPropertyChanged(string name)
        {

        }
    }
}
