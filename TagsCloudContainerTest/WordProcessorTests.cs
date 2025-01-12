using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTest;

public class WordProcessorTests
{
    private WordProcessor processor;
    private string boringWordsFilePath;

    [SetUp]
    public void Setup()
    {
        boringWordsFilePath = Path.GetTempFileName();
        File.WriteAllLines(boringWordsFilePath, new[] { "the", "a", "an" });

        processor = new WordProcessor(boringWordsFilePath);
    }

    [TearDown]
    public void Cleanup()
    {
        // Удаляем временный файл после тестов
        if (File.Exists(boringWordsFilePath))
        {
            File.Delete(boringWordsFilePath);
        }
    }

    [Test]
    public void ProcessWords_WithNullInput_ThrowsArgumentNullException()
    {
        Action act = () => processor.ProcessWords(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void ProcessWords_WithEmptyInput_ReturnsEmptyCollection()
    {
        var result = processor.ProcessWords(Array.Empty<string>());

        result.Should().BeEmpty();
    }

    [Test]
    public void ProcessWords_RemovesBoringWords()
    {
        var words = new[] { "Hello", "the", "World", "a" };
        var expected = new List<string>(){"hello", "world"};

        var actual  = processor.ProcessWords(words).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ProcessWords_ConvertsToLowerCase()
    {
        var words = new[] { "Hello", "WORLD" };
        var expected = new List<string>(){"hello", "world"};

        var actual = processor.ProcessWords(words).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ProcessWords_RemovesWhitespace()
    {
        var words = new[] { "  hello  ", "world  " };
        var expected = new List<string>(){"hello", "world"};

        var actual = processor.ProcessWords(words).ToList();

        actual.Should().BeEquivalentTo(expected);
    }
}