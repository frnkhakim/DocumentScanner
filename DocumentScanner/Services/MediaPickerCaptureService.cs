using System;
using System.Collections.Generic;
using System.Text;

using DocumentScanner.Interfaces;

namespace DocumentScanner.Services;

public class MediaPickerCaptureService : ICaptureService
{
    public bool IsSupported => MediaPicker.Default.IsCaptureSupported;

    public async Task<Stream?> CapturePageAsync()
    {
        if (!IsSupported)
            return null;

        var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
        {
            Title = "Capture page",
            MaximumWidth = 2000,      // long edge, in pixels
            CompressionQuality = 85
        });

        if (photo is null)            // user backed out
            return null;

        return await photo.OpenReadAsync();
    }
}