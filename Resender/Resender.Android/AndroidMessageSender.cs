using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Resender.Droid;
using Resender.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidMessageSender))]
namespace Resender.Droid
{
    public class AndroidMessageSender : IMessageSender
    {
        // Check permission to send SMS and perform user interaction if needed
        public async Task<bool> TrySendMessageAsync(string phoneNumber, string message)
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                await SendMessageAsync(phoneNumber, message);         
                return true;
            }

            return await GetMessagePermissionAsync(phoneNumber, message);            
        }

        async Task<bool> GetMessagePermissionAsync(string phoneNumber, string message)
        {
            //Check to see if any permission in our group is available, if one, then all are
            const string permission = Manifest.Permission.SendSms;
            var context = MainActivity.Instance;
            if (context.CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                await SendMessageAsync(phoneNumber, message);
                return true;
            }

            //Finally request permissions with the list of permissions and Id
            context.RequestPermissions(new[] { Manifest.Permission.SendSms }, 101);
            //todo: add another send attempt if request succeed
            return false;
        }

        async Task SendMessageAsync(string phoneNumber, string message)
        {
            await Task.Run(() => SmsManager.Default.SendTextMessage(phoneNumber, null, message, null, null));
        }
    }
}