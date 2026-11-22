using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentScanner.Models;

public class ScannedPage
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    // The file name only — not a full path.
    // Paths change between app updates and across devices.
    public string FileName { get; private set; }

    // 1-based position in the document.
    public int Order { get; internal set; }

    public DateTime CapturedAt { get; private set; } = DateTime.UtcNow;

    public ScannedPage(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("A page must have a file name.", nameof(fileName));

        FileName = fileName;
    }
}