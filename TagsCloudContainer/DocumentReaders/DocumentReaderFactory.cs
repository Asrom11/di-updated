using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.DocumentReaders;

public class DocumentReaderFactory: IDocumentReaderFactory
{
    private readonly Dictionary<string, IDocumentReader> readers;

    public DocumentReaderFactory()
    {
        readers = new Dictionary<string, IDocumentReader>
        {
            { ".txt", new TxtReader() },
            { ".doc", new DocReader() },
            { ".docx", new DocxReader() }
        };
    }

    public IDocumentReader GetReader(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLower();
        if (!readers.ContainsKey(extension))
            throw new NotSupportedException($"Format {extension} is not supported");
        
        return readers[extension];
    }
}