using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTest;

public class WordProcessorTests
{
    private WordProcessor processor;
    private readonly HashSet<string> boringWords = new() { "the", "a", "an" };

    [SetUp]
    public void Setup()
    {
        processor = new WordProcessor(boringWords);
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
        
        var result = processor.ProcessWords(words).ToList();
        
        result.Should().HaveCount(2);
        result.Should().Contain("hello");
        result.Should().Contain("world");
    }

    [Test]
    public void ProcessWords_ConvertsToLowerCase()
    {
        var words = new[] { "Hello", "WORLD" };
        
        var result = processor.ProcessWords(words).ToList();
        
        result.Should().HaveCount(2);
        result.Should().Contain("hello");
        result.Should().Contain("world");
    }

    [Test]
    public void ProcessWords_RemovesWhitespace()
    {
        var words = new[] { "  hello  ", "world  " };
        
        var result = processor.ProcessWords(words).ToList();
        
        result.Should().HaveCount(2);
        result.Should().Contain("hello");
        result.Should().Contain("world");
    }
}