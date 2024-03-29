﻿using System;
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

namespace Resender.Droid
{
    public class AndroidDataRepository<T> : IRepository<T> where T : IDataBaseEntity, new()
    {
        private readonly string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "items.db3");

        public Task<bool> AddItemAsync(T item)
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
            var db = new SQLiteConnection(_dbPath);
            return Task.FromResult(db.Table<T>().Delete(item => item.Id == id) > 0);
        }

        public Task<T> GetItemAsync(int id)
        {
            var db = new SQLiteConnection(_dbPath);
            return Task.FromResult<T>(db.Table<T>().FirstOrDefault(t => t.Id == id));
        }

        public Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            var db = new SQLiteConnection(_dbPath);
            // Orderby isn't supported in entities beacuse of c->IDataBaseItem conversion
            return Task.FromResult<IEnumerable<T>>(db.Table<T>().ToList().OrderBy(c => c.Id));
        }

        public Task<bool> UpdateItemAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}