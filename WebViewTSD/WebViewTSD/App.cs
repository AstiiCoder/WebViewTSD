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
			var tabs = new TabbedPage ();
			var navPage = new NavigationPage { Title="Сканирование" };
			tabs.Children.Add (navPage);

			bool useXaml = true; //Не будем использовать вариант без Xaml
            
            //Странички навигации
			if (useXaml) 
            {				
				navPage.PushAsync (new LinkToInAppXaml ());
				tabs.Children.Add (new LoadingLabelXaml ());
                tabs.Children.Add (new EvaluateJavaScriptPage ());
			} 
            else 
            {
				navPage.PushAsync (new LinkToInAppCode ());
				tabs.Children.Add (new LoadingLabelCode ());
			}

			MainPage = tabs;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            App.Current.MainPage.DisplayAlert("Добро пожаловать!", "Вы запустили приложение для сканирование марок.", "ОK");
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
