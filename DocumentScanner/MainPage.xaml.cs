using DocumentScanner.Interfaces;

namespace DocumentScanner
{
    public partial class MainPage : ContentPage
    {
        // Temporary: a fixed id so relaunching reuses the same folder.
        private static readonly Guid ScratchDocumentId =
            new("11111111-1111-1111-1111-111111111111");

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCaptureClicked(object? sender, EventArgs e)
        {
            var services = IPlatformApplication.Current!.Services;
            var capture = services.GetRequiredService<ICaptureService>();
            var storage = services.GetRequiredService<IDocumentStorage>();

            // 'await using' — we opened this stream, so we close it.
            await using var captured = await capture.CapturePageAsync();

            if (captured is null)
            {
                await DisplayAlert("No page", "Capture was cancelled or isn't available.", "OK");
                return;
            }

            var fileName = await storage.SavePageAsync(ScratchDocumentId, captured);

            // Read it back off disk — this is the proof.
            var fromDisk = await storage.OpenPageAsync(ScratchDocumentId, fileName);
            PreviewImage.Source = ImageSource.FromStream(() => fromDisk);

            await DisplayAlert("Saved", fileName, "OK");
        }
    }
}