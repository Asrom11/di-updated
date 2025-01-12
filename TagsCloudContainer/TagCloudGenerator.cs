using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Options;

namespace TagsCloudContainer;

public class TagCloudGenerator(
    IWordProcessor wordProcessor,
    ITagCloudLayouter layouter,
    ITagCloudRenderer renderer,
    IDocumentReader textReader,
    IWordFrequencyAnalyzer analyzer,
    ITextSizeCalculator sizeCalculator
)
    : ITagCloudGenerator
{
    public void GenerateCloud(string inputFilePath, string outputFilePath, RenderingOptions options)
    {
        var words = textReader.ReadDocument(inputFilePath);
        var processedWords = wordProcessor.ProcessWords(words);
        var tags = analyzer.Analyze(processedWords);

        foreach (var tag in tags)
        {
            var size = sizeCalculator.GetWordSize(tag.Word, tag.Frequency, options);
            var rectangle = layouter.PutNextRectangle(size);
            tag.Rectangle = rectangle;
        }

        renderer.Render(tags, outputFilePath, options);
    }
}