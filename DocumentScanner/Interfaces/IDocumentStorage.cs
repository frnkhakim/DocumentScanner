using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentScanner.Interfaces;

public interface IDocumentStorage
{
    // Copies a captured image into the document's folder.
    // Returns the stored file name.
    Task<string> SavePageAsync(Guid documentId, Stream imageStream);

    Task<Stream> OpenPageAsync(Guid documentId, string fileName);

    Task DeletePageAsync(Guid documentId, string fileName);
}
