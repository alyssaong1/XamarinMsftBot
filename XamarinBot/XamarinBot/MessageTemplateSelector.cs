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

        public DataTemplate FromImgTemplate;

        public DataTemplate ToTemplate;

        public DataTemplate ToImgTemplate;

        public MessageTemplateSelector()
        {
            // Retain instances!

            this.FromTemplate = new DataTemplate(typeof(FromViewCell));
            this.FromImgTemplate = new DataTemplate(typeof(FromImgViewCell));
            this.ToTemplate = new DataTemplate(typeof(ToViewCell));
            this.ToImgTemplate = new DataTemplate(typeof(ToImgViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as MessageViewModel;
            if (messageVm == null)
                return null;
            //return messageVm.IsIncoming ? ToTemplate : FromTemplate;
            if (messageVm.IsIncoming)
            {
                if (messageVm.HasAttachement)
                {
                    return ToImgTemplate;
                } else
                {
                    return ToTemplate;
                }
            } else
            {
                if (messageVm.HasAttachement)
                {
                    return FromImgTemplate;
                } else
                {
                    return FromTemplate;
                }
            }
        }


    }
}
