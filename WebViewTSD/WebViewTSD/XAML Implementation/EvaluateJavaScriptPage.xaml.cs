using System;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;

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
    }
}
