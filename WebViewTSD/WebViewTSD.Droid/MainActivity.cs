using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace WebViewSample.Droid
{
    [Activity(Label = "WebViewTSD", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //Это там где часы, WiFi и пр. - самый верх
            //На старом Андроид 4.4 не работает
            //Window.SetStatusBarColor(Android.Graphics.Color.DodgerBlue);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);     
            LoadApplication(new App());

            }
    }
}

