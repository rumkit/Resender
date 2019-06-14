using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Resender.Droid;
using Resender.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidToastNotification))]
namespace Resender.Droid
{
    class AndroidToastNotification : IToastNotification
    {
        public void SendLongTime(string message)
        {
            Toast.MakeText(MainActivity.Instance, message, ToastLength.Long).Show();
        }

        public void SendShortTime(string message)
        {
            Toast.MakeText(MainActivity.Instance, message, ToastLength.Short).Show();
        }
    }
}