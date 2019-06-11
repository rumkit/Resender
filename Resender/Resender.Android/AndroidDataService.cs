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

[assembly: Dependency(typeof(AndroidDataService))]
namespace Resender.Droid
{
    class AndroidDataService : IDataService
    {
        public IRepository<T> GetRepository<T>() where T : IDataBaseEntity, new()
        {
            return new AndroidDataRepository<T>();
        }
    }
}