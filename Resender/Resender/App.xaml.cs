using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Resender.Services;
using Resender.Views;
using System.Linq;
using Resender.Models;

namespace Resender
{
    public partial class App : Application
    {
        public IRepository<Item> DataStore => DependencyService.Get<IDataService>().GetRepository<Item>() ?? new MockDataStore();

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override async void OnAppLinkRequestReceived(Uri uri)
        {                    
            var itemId = uri.ToString().Split('/').Last();
            var item = await DataStore.GetItemAsync(int.Parse(itemId));
            if(item == null)
                return;
            var messageSender = DependencyService.Get<IMessageSender>();
            var toastNotification = DependencyService.Get<IToastNotification>();
            var result = await messageSender.TrySendMessageAsync(item.Phone, item.Text);
            if (result)
            {
                toastNotification.SendLongTime("Message sent");                
            }
            else
                toastNotification.SendLongTime("Couldn't send message");  
            MainPage.SendBackButtonPressed();
        }
    }
}
