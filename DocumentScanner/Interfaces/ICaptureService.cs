using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentScanner.Interfaces;

public interface ICaptureService
{
    bool IsSupported { get; }

    // Returns null if the user cancelled or capture isn't available.
    Task<Stream?> CapturePageAsync();
}