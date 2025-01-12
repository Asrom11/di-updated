using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface ITextSizeCalculator
{
    Size GetWordSize(string word, int frequency, RenderingOptions options);
}