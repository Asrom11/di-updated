namespace TagsCloudVisualization.Interfaces;

public interface IDocumentReaderFactory
{
    IDocumentReader GetReader(string filePath);
}