using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace WebViewSample
{
    public class App : Application
    {
        public App()
        {
            if (!Params.IsUserLoggedIn)
                {
                MainPage = new NavigationPage(new LoginPage());
                return;
                }
            else
                {
                //MainPage = new NavigationPage(new LinkToInAppXaml());
                }

            }

        protected override void OnStart()
        {
            // Handle when your app starts
            //App.Current.MainPage.DisplayAlert("Добро пожаловать!", "Вы запустили приложение для сканирование марок.", "ОK");
            }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
