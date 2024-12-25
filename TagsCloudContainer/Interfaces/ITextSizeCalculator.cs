using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudVisualization.Interfaces;

public interface ITextSizeCalculator
{
    Size GetWordSize(string word, int frequency, RenderingOptions options);
}