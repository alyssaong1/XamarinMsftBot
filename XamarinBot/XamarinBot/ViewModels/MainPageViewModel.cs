using Microsoft.Bot.Connector.DirectLine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinBot.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<MessageViewModel> messagesList;

        public ListView messagesListView;

        public Entry messagesEntry;

        private DirectLineManager directLineManager;

        public ObservableCollection<MessageViewModel> Messages

        {

            get { return messagesList; }

            set { messagesList = value; RaisePropertyChanged(); }

        }



        private string outgoingText;



        public string OutGoingText

        {

            get { return outgoingText; }

            set { outgoingText = value; RaisePropertyChanged(); }

        }



        public ICommand SendCommand { get; set; }





        public MainPageViewModel()

        {

            // Initialize with default values
            Messages = new ObservableCollection<MessageViewModel>
            {
                new MessageViewModel { MessageStr = "Say Hi to start talking to me!", IsIncoming = true, DateTime = DateTime.Now }
            };

            OutGoingText = null;

            SendCommand = new Command(() =>

            {
                ProcessMessage();
            });

            directLineManager = new DirectLineManager();
        }

        public void ProcessPhotoMessage(string photourl)
        {
            Messages.Add(new MessageViewModel { IsIncoming = false, DateTime = DateTime.Now, AttachementUrl = photourl });
            ShowLatestMessage();
        }

        public async void ProcessMessage()
        {
            Messages.Add(new MessageViewModel { MessageStr = OutGoingText, IsIncoming = false, DateTime = DateTime.Now });
            ShowLatestMessage();
            // Handle the incoming message
            var responseArr = await HandleUserMessage(OutGoingText);
            // Reset incoming message
            OutGoingText = null;
            for (var i = 0; i < responseArr.Count; i++)
            {
                Messages.Add(responseArr[i]);
            }
            // Scroll as messages come
            ShowLatestMessage();
            // Reenable the textbox once response is received from the bot
            messagesEntry.IsEnabled = true;
            messagesEntry.Focus();
        }

        private async Task<List<MessageViewModel>> HandleUserMessage(string msg)
        {
            List<MessageViewModel> responseArr = new List<MessageViewModel>();
            //Handle the user's message here, make calls to your bot here
            if (msg.Equals("hello") || msg.Equals("hi"))
            {
                responseArr.Add(new MessageViewModel { MessageStr = "Hey there! How can I help you?", IsIncoming = true, DateTime = DateTime.Now });
                // Reenable the textbox once response is received from the bot
                messagesEntry.IsEnabled = true;
                messagesEntry.Focus();
            }
            else
            {
                //return "Sorry I didn't understand what you said.";
                responseArr = await directLineManager.GetBotResponse(msg);
            }
            return responseArr;
        }

        // public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();

        private void ShowLatestMessage()
        {
            var lastMessage = messagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
            if (lastMessage != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    messagesListView.ScrollTo(lastMessage, ScrollToPosition.MakeVisible, true);
                });
            }
        }


    }
}
