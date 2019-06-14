using System;
using System.Threading.Tasks;
using Resender.Models;
using Resender.Services;
using Xamarin.Forms;

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
                toastNotification.SendLongTime("Message sent");
            else
                toastNotification.SendLongTime("Couldn't send message");
        }

        public async void DeleteCurrentItem()
        {
            await DataStore.DeleteItemAsync(Item.Id);
        }
    }
}
