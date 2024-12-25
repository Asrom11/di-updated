using DocumentFormat.OpenXml.Packaging;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.DocumentReaders;

public class DocxReader : IDocumentReader
{
    public string[] ReadDocument(string filePath)
    {
        using var doc = WordprocessingDocument.Open(filePath, false);
        var body = doc.MainDocumentPart?.Document.Body;
        if (body == null) return Array.Empty<string>();

        var text = body.InnerText;
        return text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    }
}