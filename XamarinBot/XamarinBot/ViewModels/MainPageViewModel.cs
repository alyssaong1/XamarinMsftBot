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
                Messages.Add(new MessageViewModel { MessageStr = responseArr[i], IsIncoming = true, DateTime = DateTime.Now });
            }
            // Scroll as messages come
            ShowLatestMessage();
        }

        private async Task<List<string>> HandleUserMessage(string msg)
        {
            List<string> responseArr = new List<string>();
            //Handle the user's message here, make calls to your bot here
            if (msg.Equals("hello") || msg.Equals("hi"))
            {
                // TODO: make it work for multiline
                responseArr.Add("Hey there! How can I help you?");
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
