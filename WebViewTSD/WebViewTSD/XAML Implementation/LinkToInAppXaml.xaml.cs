using System;
using System.IO;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebViewSample
{
	public partial class LinkToInAppXaml : ContentPage
	{
		public LinkToInAppXaml ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Demonstrates how to load a view for web browsing within an app.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void navButtonClicked(object sender, EventArgs e)
		{
			//await Navigation.PushAsync (new InAppBrowserXaml ("https://dotnet.microsoft.com/apps/xamarin"));
			//await Navigation.PushAsync(new InAppBrowserXaml("http://10.0.2.2/FoxWebApp2/ListNakls.htm"));
			if (Params.CurrentServer == "")
                {
				Params.CurrentServer = Preferences.Get("CurrentServer", "http://ts-tsd/tsd");
                }
			//Определяемся: что открываем изначально
			string IndexHtml = Path.Combine(Params.CurrentServer, Params.Page);
			//Для тестов
			if ((Params.FolderPath == String.Empty) && (Params.Page == String.Empty))
				IndexHtml = "http://10.0.2.2/FoxWebApp2/ListNakls.htm";
            //Непосредствеено, переход к странице
			await Navigation.PushAsync(new InAppBrowserXaml(IndexHtml));
           }
	}
}

