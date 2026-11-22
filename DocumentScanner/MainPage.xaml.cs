using DocumentScanner.Interfaces;

namespace DocumentScanner
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCaptureClicked(object? sender, EventArgs e)
        {
            var capture = IPlatformApplication.Current!
                .Services.GetRequiredService<ICaptureService>();

            var stream = await capture.CapturePageAsync();

            if (stream is null)
            {
                await DisplayAlert("No page", "Capture was cancelled or isn't available.", "OK");
                return;
            }

            // FromStream takes a factory because MAUI may re-read the stream.
            PreviewImage.Source = ImageSource.FromStream(() => stream);
        }
    }
}