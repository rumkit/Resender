using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Resender.Models;
using Resender.Services;

namespace Resender.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Text to send",
                Phone = string.Empty
            };

            BindingContext = Item;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void GetContact_Clicked(object sender, EventArgs e)
        {
            var contactManager = DependencyService.Get<IContactManager>();
            Item = new Item
            {
                Text = Item.Text,
                Phone = await contactManager.ChoosePhoneNumber()
            };
            // Dirty hack to update bindings
            // todo: move to viewModel
            BindingContext = Item;
        }
    }
}