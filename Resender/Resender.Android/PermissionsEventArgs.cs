using System;
using System.Collections.Generic;
using Android.Runtime;
using Android.Content.PM;

namespace Resender.Droid
{
    class PermissionsEventArgs : EventArgs
    {
        public Dictionary<string, Permission> RequestResults;
        public PermissionsEventArgs(string[] permissions, Permission[] grantResults)
        {
            RequestResults = new Dictionary<string,Permission>();
            for (int i = 0; i < permissions.Length; i++)
            {
                RequestResults[permissions[i]] = grantResults[i];
            }        
        }       

    }
}