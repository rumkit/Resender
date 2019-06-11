using System;

using Resender.Models;
using Xamarin.Forms;

namespace Resender.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Name;
            Item = item;
        }

        public async void DeleteCurrentItem()
        {
            await DataStore.DeleteItemAsync(Item.Id);
        }
    }
}
