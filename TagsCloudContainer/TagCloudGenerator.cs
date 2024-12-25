﻿using System.Drawing;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer;

public class TagCloudGenerator(IWordProcessor wordProcessor, 
    ITagCloudLayouter layouter, 
    ITagCloudRenderer renderer,
    ITextReader textReader,
    IWordFrequencyAnalyzer analyzer,
    ITextSizeCalculator sizeCalculator
    )
    : ITagCloudGenerator
{
    public void GenerateCloud(string inputFilePath, string outputFilePath, RenderingOptions options)
    {
        var words = textReader.ReadAllText(inputFilePath);
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