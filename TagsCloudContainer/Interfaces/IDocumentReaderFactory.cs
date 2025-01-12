namespace TagsCloudContainer.Interfaces;

public interface IDocumentReaderFactory
{
    IDocumentReader GetReader(string filePath);
}