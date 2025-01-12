namespace TagsCloudContainer.Interfaces;

public interface ITagCloudGenerator
{
    void GenerateCloud(string inputFilePath, string outputFilePath, RenderingOptions options);
}