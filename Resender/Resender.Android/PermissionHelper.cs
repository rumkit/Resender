using System.Threading.Tasks;
using Android.Content.PM;


namespace Resender.Droid
{
    static class PermissionsHelper
    {
        private static MainActivity _context => MainActivity.Instance;

        class PermissionHelperInstance
        {
            private string _permission;        

            public TaskCompletionSource<bool> CompletionSource;
            public PermissionHelperInstance(string permission)
            {
                CompletionSource = new TaskCompletionSource<bool>();
                _permission = permission;
                _context.RequestPermissionsCompleted += Context_RequestPermissionsCompleted;
            }

            private void Context_RequestPermissionsCompleted(object sender, PermissionsEventArgs e)
            {
                if(!e.RequestResults.ContainsKey(_permission))
                    return;
                (sender as MainActivity).RequestPermissionsCompleted -= Context_RequestPermissionsCompleted;
                CompletionSource.TrySetResult(e.RequestResults[_permission] == Permission.Granted);
            }
        }

        public static Task<bool> RequestPermissionAsync(string permission)
        {
            if (_context.CheckSelfPermission(permission) == (int)Permission.Granted)
                return Task.FromResult(true);
            var helper = new PermissionHelperInstance(permission);
            _context.RequestPermissions(new[] { permission }, 101);
            return helper.CompletionSource.Task;
        }
    }


}