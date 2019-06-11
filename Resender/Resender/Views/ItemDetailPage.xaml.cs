using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Resender.Models;
using Resender.ViewModels;

namespace Resender.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Name = "Item name here",
                Text = "Text to send",
                Phone = "+7 000 000-00-00"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            string answer = await DisplayActionSheet("Delete current item?", "Cancel", "Delete");
            if (answer == "Delete")
            {
                viewModel.DeleteCurrentItem();
                MessagingCenter.Send(this, "RefreshItems");
                await Navigation.PopAsync();
            }
        }
    }
}