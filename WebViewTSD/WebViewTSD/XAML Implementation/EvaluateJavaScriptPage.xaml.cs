using System;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebViewSample
{   
    public partial class EvaluateJavaScriptPage : ContentPage
    {
        public EvaluateJavaScriptPage()
        {
            InitializeComponent();

            if (Params.CurrentServer == "")
                {
                    Params.CurrentServer = Preferences.Get("CurrentServer", "http://ts-tsd/tsd/");
                    _numberEntry.Text = Params.CurrentServer;
                }
            
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

        void OnCallJavaScriptButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_numberEntry.Text))
            {
                return;
            }

            string ServerPath = _numberEntry.Text;
            Preferences.Set("CurrentServer", ServerPath);
            Params.CurrentServer = ServerPath;

            //string result = await _webView.EvaluateJavaScriptAsync($"factorial({number})");
            //_resultLabel.Text = $"Factorial of {number} is {result}.";
            }
    }
}
