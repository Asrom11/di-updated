using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.DocumentReaders;

public class TextReader(IDocumentReaderFactory documentReaderFactory): ITextReader
{
    public string[] ReadAllText(string inputFilePath)
    {
        var reader =  documentReaderFactory.GetReader(inputFilePath);
        return reader.ReadDocument(inputFilePath);
    }
}