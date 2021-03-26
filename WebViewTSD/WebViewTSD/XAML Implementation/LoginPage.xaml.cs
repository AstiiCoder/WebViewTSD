﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

		async void OnSignUpButtonClicked(object sender, EventArgs e)
			{
			//await Navigation.PushAsync(new SignUpPage());
			//navPage.PushAsync(new LinkToInAppXaml());
			}

		async void OnLoginButtonClicked(object sender, EventArgs e)
			{			
			var user = new User
				{
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
				};

			var isValid = AreCredentialsCorrect(user);
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
				messageLabel.Text = "Идёт проверка...";
				//messageLabel.Text = "Не правильный логин/пароль";
				//passwordEntry.Text = string.Empty;
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
			//if ((Params.FolderPath == String.Empty) && (Params.Page == String.Empty))
				IndexHtml = "http://10.0.2.2/FoxWebApp2/api_login.htm";
			HttpResponseMessage response = await client.GetAsync(IndexHtml);
			if (response.StatusCode == HttpStatusCode.OK)
				{
				HttpContent responseContent = response.Content;
				var json = await responseContent.ReadAsStringAsync();
				LoginButton.IsEnabled = false;
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
		}
    }