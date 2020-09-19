﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsList : ContentPage
    {
        readonly NewsListViewModel _newsListViewModel;
        public string CategoryId { get; set; }
        public NewsList(string categoryID)
        {
            InitializeComponent();
            CategoryId = categoryID;
            BindingContext = _newsListViewModel = new NewsListViewModel(categoryID);
        }
        protected override void OnAppearing()
        {
            //load data by cat CategoryId
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
            if (_newsListViewModel.obsCollectionNews.Count == 0)
                _newsListViewModel.LoadNewsCommand.Execute(null);
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            
            var item = args.SelectedItem as NEWS;
            if (item == null)
                return;
            // We can set the SelectedGroup both in binding or using the static method
            // SharedTransitionShell.SetTransitionSelectedGroup(this, item.Id.ToString());

            _newsListViewModel.SelectedNews = item;
            //await Navigation.PushAsync(new ListviewToPage(new ListviewToPageViewModel(item)));//for shared

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}