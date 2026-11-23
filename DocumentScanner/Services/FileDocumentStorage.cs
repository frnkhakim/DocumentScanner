using DocumentScanner.Interfaces;

namespace DocumentScanner.Services;

public class FileDocumentStorage : IDocumentStorage
{
    // One folder per document, under the app's private, backed-up data directory.
    // "N" formats the Guid with no dashes — a safe folder name everywhere.
    private static string FolderFor(Guid documentId) =>
        Path.Combine(FileSystem.AppDataDirectory, "documents", documentId.ToString("N"));

    private static string PathFor(Guid documentId, string fileName) =>
        Path.Combine(FolderFor(documentId), fileName);

    public async Task<string> SavePageAsync(Guid documentId, Stream imageStream)
    {
        var folder = FolderFor(documentId);
        Directory.CreateDirectory(folder);          // no-op if it already exists

        var fileName = $"{Guid.NewGuid():N}.jpg";

        // 'await using' disposes the file handle even if the copy throws.
        await using var file = File.Create(PathFor(documentId, fileName));
        await imageStream.CopyToAsync(file);

        return fileName;                            // the name only — never the path
    }

    public Task<Stream> OpenPageAsync(Guid documentId, string fileName)
    {
        var path = PathFor(documentId, fileName);

        if (!File.Exists(path))
            throw new FileNotFoundException("Page image is missing.", path);

        return Task.FromResult<Stream>(File.OpenRead(path));
    }

    public Task DeletePageAsync(Guid documentId, string fileName)
    {
        var path = PathFor(documentId, fileName);

        if (File.Exists(path))                      // deleting twice is not an error
            File.Delete(path);

        return Task.CompletedTask;
    }
}