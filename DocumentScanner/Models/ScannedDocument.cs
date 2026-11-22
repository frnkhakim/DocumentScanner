using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentScanner.Models;

public class ScannedDocument
{
    private readonly List<ScannedPage> _pages = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = "Untitled";
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    // Read-only to the outside. Callers can look, not reorder.
    public IReadOnlyList<ScannedPage> Pages => _pages;

    public bool CanExport => _pages.Count > 0;

    public ScannedPage AddPage(string fileName)
    {
        var page = new ScannedPage(fileName)
        {
            Order = _pages.Count + 1   // 1-based: first page is 1, not 0
        };

        _pages.Add(page);
        return page;
    }

    public void Rename(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("A document needs a title.", nameof(title));

        Title = title.Trim();
    }
}