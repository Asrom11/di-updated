namespace TagsCloudContainer.Interfaces;

public interface ITagCloudRenderer
{
    void Render(IEnumerable<Tag> tags, string outputFilePath, RenderingOptions options);
}