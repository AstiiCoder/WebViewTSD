using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebViewTSD.Droid;
using Xamarin.Forms;
using static WebViewSample.LinkToInAppXaml;

[assembly: Dependency(typeof(StatusBarHelper))]
namespace WebViewTSD.Droid
    {
     public class StatusBarHelper : IStatusBar
        {
        private WindowManagerFlags _originalFlags;
        private bool IsHide { get; set; }

        #region IStatusBar implementation

        /// <summary>
        /// Hide
        /// </summary>
        public void HideStatusBar()
            {
            if (IsHide) return;

            IsHide = true;

            var activity = (AppCompatActivity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
            }

        /// <summary>
        /// Show
        /// </summary>
        public void ShowStatusBar()
            {
            if (!IsHide) return;

            IsHide = false;

            var activity = (AppCompatActivity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
            }

        #endregion
        }
    }