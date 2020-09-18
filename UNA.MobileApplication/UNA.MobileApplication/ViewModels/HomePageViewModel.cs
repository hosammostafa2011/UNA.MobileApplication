using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UNA.MobileApplication.Models;

namespace UNA.MobileApplication.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<Place> Items { get; set; }
        public ObservableCollection<Tab> TabItems { get; set; }
        public Tab Item { get; set; }
        public string SelectedItemId { get; set; }
        public ListLayoutOptions ListLayout { get; set; }
        public string Section { get; set; }
        public DelegateCommand<Place> GoToDetailCommand { get; set; }
        public DelegateCommand<object> ChangeLayoutCommand { get; set; }
        public Xamarin.Forms.INavigation Navigation { get; internal set; }

        private async void GoToDetail(Place place)
        {
            SelectedItemId = place.IdAnimation;
            //var navParam = new NavigationParameters { { nameof(place), place } };
            //await NavigationService.NavigateAsync($"{nameof(PlaceDetailPage)}", navParam);
        }

        public HomePageViewModel()
        {
            GoToDetailCommand = new DelegateCommand<Place>(GoToDetail);
            Items = new ObservableCollection<Place>();
            Item = new Tab();
            ListLayout = ListLayoutOptions.Big;
            Xamarin.Forms.INavigation xNavigation = Navigation;
            LoadData();
            //ChangeLayoutCommand = new DelegateCommand<object>(ChangeLayout);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() != NavigationMode.Back)
            {
                LoadData();
            }
            else
            {
                SelectedItemId = null;
            }
        }

        private void LoadData()
        {
            var all = new List<Place>();
            var trending = new List<Place>();
            var featured = new List<Place>();
            var popular = new List<Place>();
            for (int i = 0; i < Data.places.Count; i++)
            {
                var element = Data.places[i];
                all.Add(new Place
                {
                    Id = element.Id,
                    Description = element.Description,
                    Title = element.Title,
                    SubTitle = element.SubTitle,
                    Images = element.Images,
                    Image = element.Image,
                    IdAnimation = $"All{Guid.NewGuid()}"
                });
                if (element.IsTrending)
                    trending.Add(new Place
                    {
                        Id = element.Id,
                        Description = element.Description,
                        Title = element.Title,
                        SubTitle = element.SubTitle,
                        Images = element.Images,
                        Image = element.Image,
                        IdAnimation = $"Trending{Guid.NewGuid()}"
                    });

                if (element.IsFeatured)
                    featured.Add(new Place
                    {
                        Id = element.Id,
                        Description = element.Description,
                        Title = element.Title,
                        SubTitle = element.SubTitle,
                        Images = element.Images,
                        Image = element.Image,
                        IdAnimation = $"Featured${Guid.NewGuid()}"
                    });

                if (element.IsPopular)
                    popular.Add(new Place
                    {
                        Id = element.Id,
                        Description = element.Description,
                        Title = element.Title,
                        SubTitle = element.SubTitle,
                        Images = element.Images,
                        Image = element.Image,
                        IdAnimation = $"Popular{Guid.NewGuid()}"
                    });
            }

            TabItems = new ObservableCollection<Tab>
            {
                new Tab
                {
                    Title="All",
                    Selected = true,
                    Id = "A",
                    Items = all
                },
                 new Tab
                {
                    Title="Featured",
                    Id = "F",
                    Items = featured
                },
                new Tab
                {
                    Title="Popular",
                    Id = "P",
                    Items = popular
                },
                new Tab
                {
                    Title="Trending",
                    Id = "T",
                    Items = trending
                }
            };
        }
    }
}