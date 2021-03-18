using System;

using Xamarin.Forms;

namespace WebViewSample
{
	public class LinkToInAppCode : ContentPage
	{
		public LinkToInAppCode ()
		{
			this.Title = "СкаМа";
            var layout = new StackLayout { Margin = new Thickness(20) };

			var label = new Label () {
				Text = "Это приложение предназначено для сканирования марок"
			};

			var button = new Button (){ Text = "Открыть список документов" };
			button.Clicked += navButtonClicked;

			layout.Children.Add (label);
			layout.Children.Add (button);
			Content = layout;
		}

		/// <summary>
		/// launches the browser window
		/// </summary>
		void navButtonClicked (object sender, EventArgs e)
		{
			this.Navigation.PushAsync (new InAppBrowserCode ("https://dotnet.microsoft.com/apps/xamarin"));
		}
	}
}


