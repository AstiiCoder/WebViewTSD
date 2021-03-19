using System;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using System.Net;

namespace WebViewSample
{   
    public partial class EvaluateJavaScriptPage : ContentPage
    {
        public EvaluateJavaScriptPage()
        {
            InitializeComponent();

            //Загружаем параметры - настройки приложения
            if (Params.CurrentServer == "")
                {
                    Params.CurrentServer = Preferences.Get("CurrentServer", "http://ts-tsd/tsd/");
                    _ServerPathEntry.Text = Params.CurrentServer;
                }
            Params.ScannerLogin = Preferences.Get("ScannerLogin", "");
            _Login.Text = Params.ScannerLogin;

            _webView.Source = LoadHTMLFileFromResource();
        }

        HtmlWebViewSource LoadHTMLFileFromResource()
        {
            var source = new HtmlWebViewSource();

            // Load the HTML file embedded as a resource in the .NET Standard library
            var assembly = typeof(EvaluateJavaScriptPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WebViewSample.index.html");
            
            if (stream!=null)
                {
                using (var reader = new StreamReader(stream))
                    {
                    source.Html = reader.ReadToEnd();
                    }
                }

            return source;
        }

        void OnSaveParamsClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_ServerPathEntry.Text))
            {
                return;
            }

            string ServerPath = _ServerPathEntry.Text;
            string Login = _Login.Text;
            Preferences.Set("CurrentServer", ServerPath);
            Params.CurrentServer = ServerPath;
            Preferences.Set("ScannerLogin", Login);
            Params.ScannerLogin = Login;

            //Чтобы проинформировать пользователя о том, что всё нажатие на кнопку программа обрабатывает 
            this.DisplayToastAsync("Сохранение...", 2000);
            //string result = await _webView.EvaluateJavaScriptAsync($"factorial({number})");
            //_resultLabel.Text = $"Factorial of {number} is {result}.";
        }

        private static bool CheckURL(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 15000;
            request.Method = "HEAD";
            try
                {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                    return response.StatusCode == HttpStatusCode.OK;
                    }
                }
            catch (WebException)
                {
                return false;
                }
        }

        private async void TestConnectivity(object sender, EventArgs e)
        {
            //Проверим подключения
            var current = Connectivity.NetworkAccess;
            string Mes = "Сеть: " + current.ToString();
            await this.DisplayToastAsync(Mes, 1500);

            if (current == NetworkAccess.Internet)
                {
                var profiles = Connectivity.ConnectionProfiles;
                Mes = "";
                foreach (var profile in profiles)
                    {
                    Mes += $"{profile.ToString()} \n";
                    }
                }
            else
                {
                Mes = "Подключение отсутствует";
                }

            Mes = "Тип подключения:\n" + Mes;
            await this.DisplayToastAsync(Mes, 2000);

            if (CheckURL(Params.CurrentServer))
                await this.DisplayToastAsync("Сервер доступен", 2000);
            else
                await this.DisplayToastAsync("Нет доступа к серверу", 2000);
            }

     }
}
