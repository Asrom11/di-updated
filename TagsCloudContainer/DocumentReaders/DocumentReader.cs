using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.DocumentReaders;

public class DocumentReader: IDocumentReader
{
    private readonly Dictionary<string, IDocumentReader> _readers;
    public string[] SupportedDocumentExtensions => _readers.Keys.ToArray();
    public DocumentReader(IEnumerable<IDocumentReader> readers)
    {
        _readers = readers
            .SelectMany(reader => reader.SupportedDocumentExtensions.Select(ext => new { ext, reader }))
            .ToDictionary(x => x.ext, x => x.reader);
    }
    public string[] ReadDocument(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLower();
        if (!_readers.TryGetValue(extension, out var reader))
            throw new NotSupportedException($"Format {extension} is not supported");

        return reader.ReadDocument(filePath);
    }
}