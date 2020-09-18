using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNA.MobileApplication.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Component
{
    public partial class NewsComponent : StackLayout
    {
        public static readonly BindableProperty GoToDetailCommandProperty =
           BindableProperty.Create(nameof(GoToDetailCommand), typeof(DelegateCommand<Place>), typeof(NewsComponent));

        public DelegateCommand<Place> GoToDetailCommand
        {
            get { return (DelegateCommand<Place>)GetValue(GoToDetailCommandProperty); }
            set { SetValue(GoToDetailCommandProperty, value); }
        }

        public static readonly BindableProperty ListLayoutProperty =
           BindableProperty.Create(nameof(ListLayout), typeof(ListLayoutOptions), typeof(NewsComponent), null, BindingMode.OneWay, propertyChanged: ListLayoutChange);

        public ListLayoutOptions ListLayout
        {
            get { return (ListLayoutOptions)GetValue(ListLayoutProperty); }
            set { SetValue(ListLayoutProperty, value); }
        }

        private static void ListLayoutChange(object bindable, object oldValue, object newValue)
        {
            var elemnt = (NewsComponent)bindable;
            if ((ListLayoutOptions)newValue == ListLayoutOptions.Big)
            {
                var newBounds = new Rectangle(elemnt.container.Bounds.X, elemnt.container.Bounds.Y, elemnt.container.Bounds.Width, 200);
                elemnt.imgContainer.LayoutTo(newBounds, 200, Easing.SinIn);
                elemnt.textContainer.LayoutTo(new Rectangle(0, 200, elemnt.container.Bounds.Width, 80), 200, Easing.SinIn);
                var parentAnimation = new Animation
                {
                    { 0, 1, new Animation(v => elemnt.HeightRequest = v, elemnt.HeightRequest, 300, Easing.SinIn) }
                };
                parentAnimation.Commit(elemnt, "parentAnimationUp", 60, 300, null, (e, z) =>
                {
                    elemnt.container.LayoutTo(newBounds, 200, Easing.SinIn);
                });
            }
            else
            {
                var parentAnimation = new Animation
                {
                    { 0, 1, new Animation(v => elemnt.HeightRequest = v, elemnt.HeightRequest, 100, Easing.SinIn) }
                };
                var newBoundImage = new Rectangle(elemnt.container.Bounds.X, elemnt.container.Bounds.Y, 100, 100);
                elemnt.imgContainer.LayoutTo(newBoundImage, 200, Easing.SinIn);
                parentAnimation.Commit(elemnt, "parentAnimationDown", 16, 100, null, (e, z) =>
                {
                    var newBounds = new Rectangle(elemnt.container.Bounds.X, elemnt.container.Bounds.Y, elemnt.container.Bounds.Width, 100);
                    elemnt.textContainer.LayoutTo(new Rectangle(110, 0, elemnt.container.Bounds.Width - 120, 80), 200, Easing.SinIn);
                    elemnt.container.LayoutTo(newBounds, 100, Easing.SinIn);
                });
            }
        }

        public NewsComponent()
        {
            InitializeComponent();
            if (ListLayout == ListLayoutOptions.Big)
            {
                HeightRequest = 300;
                AbsoluteLayout.SetLayoutBounds(container, new Rectangle(0, 0, 1, 200));
                AbsoluteLayout.SetLayoutFlags(container, AbsoluteLayoutFlags.WidthProportional);

                AbsoluteLayout.SetLayoutBounds(imgContainer, new Rectangle(0, 0, 1, 200));
                AbsoluteLayout.SetLayoutFlags(imgContainer, AbsoluteLayoutFlags.WidthProportional);

                AbsoluteLayout.SetLayoutBounds(textContainer, new Rectangle(0, 200, 1, 80));
                AbsoluteLayout.SetLayoutFlags(textContainer, AbsoluteLayoutFlags.WidthProportional);
            }
            else
            {
                HeightRequest = 100;
                AbsoluteLayout.SetLayoutBounds(container, new Rectangle(0, 0, 1, 100));
                AbsoluteLayout.SetLayoutFlags(container, AbsoluteLayoutFlags.WidthProportional);

                AbsoluteLayout.SetLayoutBounds(imgContainer, new Rectangle(0, 0, 100, 100));
                AbsoluteLayout.SetLayoutFlags(imgContainer, AbsoluteLayoutFlags.None);

                AbsoluteLayout.SetLayoutBounds(textContainer, new Rectangle(110, 0, .7, 80));
                AbsoluteLayout.SetLayoutFlags(textContainer, AbsoluteLayoutFlags.WidthProportional);
            }
        }
    }
}