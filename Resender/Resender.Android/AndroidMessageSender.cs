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

            return await SendMessageWithPermissionRequestAsync(phoneNumber, message);
        }

        async Task<bool> SendMessageWithPermissionRequestAsync(string phoneNumber, string message)
        {
            var context = MainActivity.Instance;
            // Check permission and try to send message
            if (await PermissionsHelper.RequestPermissionAsync(Manifest.Permission.SendSms))
            {
                await SendMessageAsync(phoneNumber, message);
                return true;
            }
            return false;
        }      

        async Task SendMessageAsync(string phoneNumber, string message)
        {
            await Task.Run(() => SmsManager.Default.SendTextMessage(phoneNumber, null, message, null, null));
        }
    }
}