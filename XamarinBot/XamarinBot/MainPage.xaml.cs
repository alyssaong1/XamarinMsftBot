using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBot.ViewModels;

namespace XamarinBot
{
	public partial class MainPage : ContentPage
	{
        public MainPageViewModel mainPageVM;
		public MainPage()
		{
			InitializeComponent();
            mainPageVM = new MainPageViewModel();
            mainPageVM.messagesListView = MessagesListView;
            BindingContext = mainPageVM;
            MessagesListView.ItemTemplate = new MessageTemplateSelector();
        }
        private void MsgEntry_Send(object sender, EventArgs e)
        {
            mainPageVM.ProcessMessage();
        }
    }
}
