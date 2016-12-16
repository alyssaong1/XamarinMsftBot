using Plugin.Media;
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
            mainPageVM.messagesEntry = MsgEntry;
            BindingContext = mainPageVM;
            MessagesListView.ItemTemplate = new MessageTemplateSelector();
            //Creating TapGestureRecognizers   
            var tapImage = new TapGestureRecognizer();
            //Binding events   
            tapImage.Tapped += tapImage_Tapped;
            //Associating tap events to the image buttons   
            imgCamera.GestureRecognizers.Add(tapImage);
        }
        private void MsgEntry_Send(object sender, EventArgs e)
        {
            mainPageVM.ProcessMessage();
            MsgEntry.Text = "";
            MsgEntry.IsEnabled = false;
        }
        private void OnSendButtonClicked(object sender, EventArgs e)
        {
            MsgEntry.Text = "";
            MsgEntry.IsEnabled = false;
        }
        private async void tapImage_Tapped(object sender, EventArgs e)
        {
            
            var action = await DisplayActionSheet("", "Cancel", null, "Photo library", "Camera");
            if (action == "Camera")
            {
                TakePhoto();
            } else
            {
                GetPhotoFromLibrary();
            }
        }

        private async void TakePhoto()
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                AllowCropping = false,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            // Handle the photo taken here
            mainPageVM.ProcessPhotoMessage(file.Path);

        }

        private async void GetPhotoFromLibrary()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photo picker not supported", "Photo picker not supported.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");
            mainPageVM.ProcessPhotoMessage(file.Path);
            
        }

    }
}
