using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudVisualization.Interfaces;

public interface ITagCloudGenerator
{
    void GenerateCloud(string inputFilePath, string outputFilePath, RenderingOptions options);
}