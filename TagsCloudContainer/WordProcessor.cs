using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer;

public class WordProcessor(IEnumerable<string> boringWords) : IWordProcessor
{
    private readonly HashSet<string> _boringWords = [..boringWords];

    public IEnumerable<string> ProcessWords(IEnumerable<string> words)
    {
        ArgumentNullException.ThrowIfNull(words);

        return words
            .Select(word => word.ToLower().Trim())
            .Where(word => !string.IsNullOrWhiteSpace(word))
            .Where(word => !_boringWords.Contains(word));
    }
}
