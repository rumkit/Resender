using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Resender.Droid;
using Resender.Models;
using Resender.Services;
using Xamarin.Forms;
using System.IO;
using SQLite;

[assembly: Dependency(typeof(AndroidDataStore))]
namespace Resender.Droid
{
    public class AndroidDataStore : IDataStore<Item>
    {
        private string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "items.db3");

        public AndroidDataStore()
        {

        }

        public Task<bool> AddItemAsync(Item item)
        {
            return Task.Run(() =>
            {
                var db = new SQLiteConnection(_dbPath);
                db.CreateTable<Item>();
                var maxId = db.Table<Item>().DefaultIfEmpty().Max(c => c?.Id) ?? -1;

                item.Id = maxId + 1;
                return db.Insert(item) == 1;
            }
            );
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var db = new SQLiteConnection(_dbPath);
            return Task.FromResult<IEnumerable<Item>>(db.Table<Item>().OrderBy(c => c.Id).ToList());
        }

        public Task<bool> UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }
}