using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBot.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private string text;



        public string MessageStr

        {

            get { return text; }

            set { text = value; RaisePropertyChanged(); }

        }



        private DateTime dateTime;



        public DateTime DateTime

        {

            get { return dateTime; }

            set { dateTime = value; RaisePropertyChanged(); }

        }



        private bool isIncoming;



        public bool IsIncoming

        {

            get { return isIncoming; }

            set { isIncoming = value; RaisePropertyChanged(); }

        }



        public bool HasAttachement => !string.IsNullOrEmpty(attachementUrl);



        private string attachementUrl;



        public string AttachementUrl

        {

            get { return attachementUrl; }

            set { attachementUrl = value; RaisePropertyChanged(); }

        }




    }
}
