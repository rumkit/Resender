using System;
using System.Threading.Tasks;
using Resender.Models;
using Resender.Services;
using Xamarin.Forms;
using Plugin.AppShortcuts;
using Plugin.AppShortcuts.Icons;

namespace Resender.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public Command SendMessageCommand { get; private set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Name;
            Item = item;
            SendMessageCommand = new Command(ExecuteSendMessage);
        }

        private async void ExecuteSendMessage()
        {
            var messageSender = DependencyService.Get<IMessageSender>();
            var toastNotification = DependencyService.Get<IToastNotification>();
            var result = await messageSender.TrySendMessageAsync(Item.Phone, Item.Text);
            if(result)
            {
                toastNotification.SendLongTime("Message sent");
                AddMessageShortcut(Item);
            }                
            else
                toastNotification.SendLongTime("Couldn't send message");
        }

        private async void AddMessageShortcut(Item item)
        {
            var shortcut = new Shortcut()
            {
                Label = item.Name,
                Description = "Send message: " + item.Text,
                Icon = new FavoriteIcon(),
                Uri = $"rsnd://Resender/SendMessage/{item.Id}"
            };
            await CrossAppShortcuts.Current.AddShortcut(shortcut);
        }

        public async void DeleteCurrentItem()
        {
            await DataStore.DeleteItemAsync(Item.Id);
        }
    }
}
