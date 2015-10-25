﻿using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace HearthstoneCards
{
    public sealed partial class MultiSelector : UserControl
    {
        public event SelectionChangedEventHandler SelectionChanged;

        public MultiSelector()
        {
            InitializeComponent();
            XLayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MultiSelector), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(MultiSelector), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register("Options", typeof(IList), typeof(MultiSelector), new PropertyMetadata(new List<string>()));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(MultiSelector), new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(MultiSelector), new PropertyMetadata(default(Style)));

        internal void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
            {
                return;
            }

            // TODO sort?
            var selectedOptions = listView.SelectedItems;

            // change status
            var status = string.Empty;
            if (selectedOptions.Count == 0)
            {
                status = "None";
            }
            else if (selectedOptions.Count == ((IList)GetValue(OptionsProperty)).Count)
            {
                status = "All";
            }
            else
            {
                if (selectedOptions.Count < 4)
                {
                    for (var i = 0; i < selectedOptions.Count - 1; i++)
                    {
                        status += selectedOptions[i] + ", ";
                    }
                    status += selectedOptions[selectedOptions.Count - 1];
                }
                else
                {
                    const int threshold = 3;
                    for (var i = 0; i < threshold - 1; i++)
                    {
                        status += selectedOptions[i] + ", ";
                    }
                    status += selectedOptions[threshold - 1];
                    status += string.Format(" and {0} more.", selectedOptions.Count - threshold);
                }
            }
            SetValue(StatusProperty, status);

            // fire public control event
            if (SelectionChanged != null)
            {
                SelectionChanged(this, e);
            }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            private set { SetValue(StatusProperty, value); }
        }

        public IList Options
        {
            get { return (IList)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        private void MultiSelector_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            //FlyoutBase.ShowAttachedFlyout(sender as Grid);
            ((Frame)Window.Current.Content).Navigate(typeof(MultiSelectorPage), this);
        }
    }
}
