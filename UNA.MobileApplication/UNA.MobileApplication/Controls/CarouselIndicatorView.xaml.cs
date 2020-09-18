﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNA.MobileApplication.Controls
{
    public partial class CarouselIndicatorView : Grid
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(IEnumerable), typeof(CarouselIndicatorView), null);
        public static readonly BindableProperty CurrentItemProperty = BindableProperty.Create(nameof(CurrentItem), typeof(object), typeof(CarouselIndicatorView), null, BindingMode.TwoWay, propertyChanged: CurrentItemChange);

        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public object CurrentItem
        {
            get { return (object)GetValue(CurrentItemProperty); }
            set { SetValue(CurrentItemProperty, value); }
        }

        public CarouselIndicatorView()
        {
            InitializeComponent();
        }

        private static void CurrentItemChange(object bindable, object oldValue, object newValue)
        {
            var x = (CarouselIndicatorView)bindable;
            var labelContainer = x.FindByName("myList") as FlexLayout;
            foreach (Label label in labelContainer.Children)
            {
                var tabGesture = label.GestureRecognizers[0] as TapGestureRecognizer;
                if (newValue == tabGesture.CommandParameter)
                {
                    x.MoveActiveIndicator(label);
                    return;
                }
            }
        }

        private void MoveActiveIndicator(Label target)
        {
            var width = target.Width - activeIndicator.Width;
            activeIndicator.TranslateTo(target.X + (width / 2), 0, 100, Easing.Linear);
        }

        private void ChangeTab(System.Object sender, System.EventArgs e)
        {
            foreach (var item in Items)
            {
                if (item == ((TappedEventArgs)e).Parameter)
                {
                    CurrentItem = item;
                    return;
                }
            }
        }
    }
}