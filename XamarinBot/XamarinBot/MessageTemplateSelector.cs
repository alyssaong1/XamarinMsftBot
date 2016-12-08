using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBot.CustomCell;
using XamarinBot.ViewModels;

namespace XamarinBot
{
    public class MessageTemplateSelector : DataTemplateSelector
    {

        public DataTemplate FromTemplate;

        public DataTemplate ToTemplate;

        public MessageTemplateSelector()
        {
            // Retain instances!

            this.FromTemplate = new DataTemplate(typeof(FromViewCell));

            this.ToTemplate = new DataTemplate(typeof(ToViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((MessageViewModel)item).IsIncoming ? ToTemplate : FromTemplate;
        }


    }
}
