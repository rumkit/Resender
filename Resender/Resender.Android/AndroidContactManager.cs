using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Resender.Droid;
using Resender.Services;
using Xamarin.Forms;
using Android.Provider;

[assembly: Dependency(typeof(AndroidContactManager))]
namespace Resender.Droid
{
    public class AndroidContactManager : IContactManager
    {
        public Task<string> ChoosePhoneNumber()
        {
            //request code to identify intent
            var requestCode = 0xCA;
            var activityListener = new ActivityResultListener(MainActivity.Instance, requestCode);
            var contactPickerIntent = new Intent(Intent.ActionPick);
            contactPickerIntent.SetType(ContactsContract.CommonDataKinds.Phone.ContentType);
            MainActivity.Instance.StartActivityForResult(contactPickerIntent, requestCode);
            return activityListener.Task;
        }


        private class ActivityResultListener
        {
            private readonly TaskCompletionSource<string> Complete = new TaskCompletionSource<string>();
            private readonly int _requestCode;
            public Task<string> Task { get { return this.Complete.Task; } }

            public ActivityResultListener(MainActivity activity, int requestCode)
            {
                // subscribe to activity results
                activity.ActivityResult += OnActivityResult;
                _requestCode = requestCode;
            }

            private void OnActivityResult(int requestCode, Result resultCode, Intent intent)
            {
                // check request code
                if (requestCode != _requestCode)
                    return;
                // unsubscribe from activity results
                var activity = MainActivity.Instance;
                activity.ActivityResult -= OnActivityResult;

                string[] projection = { ContactsContract.CommonDataKinds.Phone.Number };
                var cursor = activity.ContentResolver.Query(intent.Data, projection, null, null, null);

                var contactNumber = string.Empty;

                // parse phone number from result query
                if (cursor.MoveToFirst())
                {
                    contactNumber = cursor.GetString(cursor.GetColumnIndex(projection[0]));
                }

                // process result
                if (resultCode != Result.Ok)
                    this.Complete.TrySetResult(string.Empty);
                else
                    this.Complete.TrySetResult(contactNumber);
            }
        }
    }
}