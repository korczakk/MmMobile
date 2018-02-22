using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FireBaseClient;
using MmMaker.Model;

namespace MmMobile.ViewModel
{
    public class ProductListViewModel
    {
        string _appKey = "AIzaSyBaQegiosq-yCEp1CdNsZ6dGiAhQgN8fgw";

        public ObservableCollection<ExcelContent> mmContent { get; private set; }

        /// <summary>
        /// Pobiera dane o MM z Firebase
        /// </summary>
        /// <param name="idToken"></param>
        public void GetData(string idToken)
        {
            FirebaseDatabase firebase = new FirebaseDatabase(idToken);

            IEnumerable<ExcelContent> Content = firebase.GetData<ExcelContent>("");

            mmContent = new ObservableCollection<ExcelContent>(mmContent); 
        }

        /// <summary>
        /// Loguje się do Firebase i zwraca idToken
        /// </summary>
        /// <returns>Token</returns>
        public string FirebaseSignIn()
        {
            FirebaseAuthentication auth = new FirebaseAuthentication(_appKey);

            SignInResponse signinResult = auth.SignInWithEmail("kamil.korczak@gmail.com", "HPdj690");

            return signinResult.idToken;
        }
    }
}
