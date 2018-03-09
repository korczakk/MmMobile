
using FirebaseClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MmMobile.Services
{
    public class FirebaseToken
    {
        string _appKey = "AIzaSyBaQegiosq-yCEp1CdNsZ6dGiAhQgN8fgw";


        /// <summary>
        /// Pobiera token z cacha
        /// </summary>
        /// <returns></returns>
        public SignInResponse GetToken()
        {
            SignInResponse token = new SignInResponse();

            //jeśli token w ogóle nie istnieje to loguje się i cachuje token
            if (Application.Current.Properties.ContainsKey("token") == false)
            {
                //loguję do firebase
                token = FirebaseSignIn();

                token.SetExpirationDate();

                CacheToken(token);

                return token;
            }

            //jeśli token istnieje w cachu
            string json = Application.Current.Properties["token"].ToString();

            token = JsonConvert.DeserializeObject<SignInResponse>(json);

            //czy token jest jeszcze ważny
            if (IsTokenValid(token) == false)
            {
                token = RefreshToken(token.refreshToken);

                token.SetExpirationDate();

                CacheToken(token);
            }

            return token;
        }

        /// <summary>
        /// Dodaje do obiektu Application obiekt SignInResponse w postaci JSON
        /// </summary>
        /// <param name="token"></param>
        private async void CacheToken(SignInResponse token)
        {
            string json = JsonConvert.SerializeObject(token);

            Application.Current.Properties["token"] = json;

            //zapisuje natychmiast - normalnie zapisuje po wyjścu z app
            await Application.Current.SavePropertiesAsync();
        }

        /// <summary>
        /// Odświerza token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns>Zwraca odświerzony token</returns>
        private SignInResponse RefreshToken(string refreshToken)
        {
            FirebaseAuthentication auth = new FirebaseAuthentication(_appKey);
            RefreshResponse refreshedToken = auth.RefreshToken(refreshToken);

            //Przy refreshu jest zwracany typ RefreshResponse więc muszę przepisać na SignInResponse
            return new SignInResponse()
            {
                idToken = refreshedToken.id_token,
                refreshToken = refreshedToken.refresh_token,
                expiresIn = refreshedToken.expires_in
            };
        }

        /// <summary>
        /// Sprawdza czy token jest jeszcze ważny przez 5 min
        /// </summary>
        /// <param name="token">Token do sprawdzenia</param>
        /// <returns>True jeśli token jest ważny</returns>
        private bool IsTokenValid(SignInResponse token)
        {
            if (token.ExpirationDate == null)
            {
                return false;
            }

            TimeSpan diff = token.ExpirationDate.Value.Subtract(DateTime.Now);

            return diff.Minutes > 5;
        }

        /// <summary>
        /// Loguje się do Firebase i zwraca idToken
        /// </summary>
        /// <returns>Token</returns>
        private SignInResponse FirebaseSignIn()
        {
            FirebaseAuthentication auth = new FirebaseAuthentication(_appKey);

            SignInResponse signinResult = auth.SignInWithEmail("kamil.korczak@gmail.com", "HPdj690P");

            return signinResult;
        }

    }
}
