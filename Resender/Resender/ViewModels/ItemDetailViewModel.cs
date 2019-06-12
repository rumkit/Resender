using System;
using System.Threading.Tasks;
using Plugin.Messaging;
using Resender.Models;
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
            await Task.Run(() =>
                {
                    var smsMessenger = CrossMessaging.Current.SmsMessenger;
                    if (smsMessenger.CanSendSmsInBackground)
                        smsMessenger.SendSmsInBackground(Item.Phone, Item.Text);
                }
            );
        }

        public async void DeleteCurrentItem()
        {
            await DataStore.DeleteItemAsync(Item.Id);
        }
    }
}
