using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudVisualization.Interfaces;

public interface ITagCloudRenderer
{
    void Render(IEnumerable<Tag> tags, string outputFilePath, RenderingOptions options);
}