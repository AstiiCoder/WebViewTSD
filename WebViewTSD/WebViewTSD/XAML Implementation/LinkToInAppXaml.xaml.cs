using Android.OS;
using System;
using System.IO;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebViewSample
{
	public partial class LinkToInAppXaml : ContentPage
	{		
		public interface IStatusBar
			{
			/// <summary>
			/// Hide
			/// </summary>
			void HideStatusBar();

			/// <summary>
			/// Show
			/// </summary>
			void ShowStatusBar();
			}

		public LinkToInAppXaml ()
		{
			//DependencyService.Get<IStatusBar>().HideStatusBar();
			InitializeComponent();
            BackgroundColor = Color.FromHex("#2196F3");

            if (Navigation != null && Navigation.NavigationStack.Count() > 0) 
				{ 
				var existingPages = Navigation.NavigationStack.ToList(); 
				foreach (var page in existingPages) 
					{
					Navigation.RemovePage(page); 
					} 
				}

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
			if (Params.Page == "")
				{
				Params.Page = Preferences.Get("Page", "");
				}
				
			//Определяемся: что открываем изначально
			string IndexHtml = Path.Combine(Params.CurrentServer, Params.Page);
			//Для тестов
			//if ((Params.FolderPath == String.Empty) && (Params.Page == String.Empty))
				//IndexHtml = "http://10.0.2.2/FoxWebApp2/ListNakls.htm";
            //Непосредствеено, переход к странице
			await Navigation.PushAsync(new InAppBrowserXaml(IndexHtml));
			//Подсказка, что нужно делать дальше
			await this.DisplayToastAsync("Выберите документ", 1000);
			}

        async private void navOptionsClicked(object sender, EventArgs e)
            {
			await Navigation.PushAsync(new EvaluateJavaScriptPage());
			}

        async private void retButtonClicked(object sender, EventArgs e)
            {
			if (Params.CurrentServer == "")
				{
				Params.CurrentServer = Preferences.Get("CurrentServer", "http://ts-tsd/tsd");
				}
			if (Params.Page == "")
				{
				Params.Page = Preferences.Get("Page", "");
				}
			//Определяемся: что открываем изначально
			string IndexHtml = Path.Combine(Params.CurrentServer, Params.Page);
			//Возрат имеет параметр ret
			IndexHtml += "?ret=1";

			//Непосредствеено, переход к странице
			await Navigation.PushAsync(new InAppBrowserXaml(IndexHtml));
			//Подсказка, что нужно делать дальше
			await this.DisplayToastAsync("Выберите документ", 1000);
			}

        async private void editButtonClicked(object sender, EventArgs e)
            {
			if (Params.CurrentServer == "")
				{
				Params.CurrentServer = Preferences.Get("CurrentServer", "http://ts-tsd/tsd");
				}
			if (Params.Page == "")
				{
				Params.Page = Preferences.Get("Page", "");
				}
			//Определяемся: что открываем изначально
			string IndexHtml = Path.Combine(Params.CurrentServer, Params.Page);
			//Возрат имеет параметр ret
			IndexHtml += "?edit=1";

			//Непосредствеено, переход к странице
			await Navigation.PushAsync(new InAppBrowserXaml(IndexHtml));
			}

        private void exitButtonClicked(object sender, EventArgs e)
            {
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
			}
        }
}

