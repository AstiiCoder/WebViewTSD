using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WebViewSample
	{	
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
		{
		public LoginPage()
			{
			InitializeComponent();
			}

		async void OnLoginButtonClicked(object sender, EventArgs e)
			{			
			var user = new User
				{
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
				};

			bool isValid = false;
			HttpClient client = new HttpClient();
			string LoginAPI = "api/login?login=" + user.Username + "&password=" + user.Password;
			if (Params.CurrentServer == "")
				{
				Params.CurrentServer = Preferences.Get("CurrentServer", "http://ts-tsd/tsd");
				}
			//Определяемся: что открываем изначально
			string IndexHtml = Path.Combine(Params.CurrentServer, LoginAPI);
			//Для тестов
			//if ((Params.FolderPath == String.Empty) && (Params.Page == String.Empty))
			//IndexHtml = "http://10.0.2.2/FoxWebApp2/api_login.htm";
			HttpResponseMessage response;
			try
                {
				response = await client.GetAsync(IndexHtml);
                }
            catch (Exception)
                {
				messageLabel.Text = "Сервер недоступен!";
				return;
                }
			
			if (response.StatusCode == HttpStatusCode.OK)
				{
                HttpContent responseContent = response.Content;
                try
                    {
                    var json = await responseContent.ReadAsStringAsync();
                    if (json.ToString() == "\"1\"")
                        {
                        Params.IsUserLoggedIn = true;
                        isValid = true;
                        }
                    else if (json.ToString() == "\"0\"")
                        {
                        messageLabel.Text = "Нет доступа";
                        passwordEntry.Text = string.Empty;
                        await Task.Delay(2000);
                        }
                    else
                        messageLabel.Text = "Неопределённый ответ " + json.ToString();
                    }
                catch (Exception)
                    {
						messageLabel.Text = "Ресурс недоступен!";
					    return;
					}


                }

            //var isValid = AreCredentialsCorrect(user);
            if (isValid)
				{
				messageLabel.Text = "";
				var tabs = new TabbedPage();
				var navPage = new NavigationPage { Title = "Сканир." };
				tabs.Children.Add(navPage);

				bool useXaml = true; //Не будем использовать вариант без Xaml

				//Странички навигации
				if (useXaml)
					{
					await navPage.PushAsync(new LinkToInAppXaml());
					tabs.Children.Add(new LoadingLabelXaml());
					tabs.Children.Add(new EvaluateJavaScriptPage());
					}
				else
					{
					await navPage.PushAsync(new LinkToInAppCode());
					tabs.Children.Add(new LoadingLabelCode());
					}

				//Application.MainPage = tabs;
				Params.IsUserLoggedIn = true;
				Navigation.InsertPageBefore(tabs, this);
				await Navigation.PopAsync();				
				}
			else
				{
				messageLabel.Text = "Неправильный логин/пароль";
				passwordEntry.Text = string.Empty;
				//Чтобы проинформировать ещё и динамическим сообщением
				this.DisplayToastAsync("Неправильный логин/пароль", 2000);
				}
			}

		bool AreCredentialsCorrect(User user)
			{
			//return user.Username == "sa" && user.Password == "1";
			TestUserLoginPassword(user);

			return Params.IsUserLoggedIn;
			}

		async void TestUserLoginPassword(User user)
			{
			HttpClient client = new HttpClient();
			string LoginAPI = "api/login?login=" + user.Username + "&password=" + user.Password;
			if (Params.CurrentServer == "")
				{
				Params.CurrentServer = Preferences.Get("CurrentServer", "http://ts-tsd/tsd");
				}
			//Определяемся: что открываем изначально
			string IndexHtml = Path.Combine(Params.CurrentServer, LoginAPI);
			//Для тестов
			//IndexHtml = "http://10.0.2.2/FoxWebApp2/api_login.htm";
			HttpResponseMessage response = await client.GetAsync(IndexHtml);
			if (response.StatusCode == HttpStatusCode.OK)
				{
				HttpContent responseContent = response.Content;
				var json = await responseContent.ReadAsStringAsync();
				if (json.ToString() == "\"0\"")
					Params.IsUserLoggedIn = true;
				else if (json.ToString() == "\"1\"")
					{
					messageLabel.Text = "Не правильный логин/пароль";
					passwordEntry.Text = string.Empty;
					}
				//messageLabel.Text = json.ToString();
				}
			}

        private async void OnImgClick(object sender, EventArgs e)
            {
			var tabs = new TabbedPage();
			var navPage = new NavigationPage { Title = "Сканир." };
			tabs.Children.Add(navPage);

			bool useXaml = true; 

			//Странички навигации
			if (useXaml)
				{
				await navPage.PushAsync(new LinkToInAppXaml());
				tabs.Children.Add(new LoadingLabelXaml());
				tabs.Children.Add(new EvaluateJavaScriptPage());
				}
			else
				{
				await navPage.PushAsync(new LinkToInAppCode());
				tabs.Children.Add(new LoadingLabelCode());
				}

			Params.IsUserLoggedIn = true;
			Navigation.InsertPageBefore(tabs, this);
			await Navigation.PopAsync();
			}
        }
    }