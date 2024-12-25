using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.DocumentReaders;

public class TxtReader : IDocumentReader
{
    public string[] ReadDocument(string filePath)
    {
        return File.ReadAllLines(filePath);
    }
}