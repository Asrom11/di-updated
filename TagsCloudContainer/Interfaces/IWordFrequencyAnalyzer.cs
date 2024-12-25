namespace TagsCloudVisualization.Interfaces;

public interface IWordFrequencyAnalyzer
{
    List<Tag> Analyze(IEnumerable<string> words);
}