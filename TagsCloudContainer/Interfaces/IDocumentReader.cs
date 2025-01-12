namespace TagsCloudContainer.Interfaces;

public interface IDocumentReader
{
    string[] SupportedDocumentExtensions { get; }
    string[] ReadDocument(string filePath);
}