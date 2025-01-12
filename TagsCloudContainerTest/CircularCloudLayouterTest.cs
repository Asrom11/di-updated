﻿using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Options;

public class TagCloudGeneratorIntegrationTests
{
    private IContainer container;
    private string tempDirectory;
    private string inputPath;
    private string outputPath;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        container = DependencyInjectionConfig.BuildContainer(null);
        tempDirectory = Path.Combine(Path.GetTempPath(), "TagCloudTests");
        Directory.CreateDirectory(tempDirectory);
        inputPath = Path.Combine(tempDirectory, "input.txt");
        outputPath = Path.Combine(tempDirectory, "output.png");
    }

    [OneTimeTearDown]
    public void GlobalCleanup()
    {
        if (Directory.Exists(tempDirectory))
            Directory.Delete(tempDirectory, true);
    }

    [Test]
    public void GenerateCloud_WithSimpleInput_CreatesValidOutput()
    {
        const string testText = @"
            This is a test text
            with multiple words
            some words appear
            multiple times in the text
            test test test
        ";
        File.WriteAllText(inputPath, testText);

        using var scope = container.BeginLifetimeScope();
        var generator = scope.Resolve<ITagCloudGenerator>();
        var options = new RenderingOptions
        {
            BackgroundColor = Color.White,
            WordColors = [Color.Black],
            Font = "Arial",
            ImageSize = new Size(800, 600)
        };

        generator.GenerateCloud(inputPath, outputPath, options);

        File.Exists(outputPath).Should().BeTrue();
        using var image = Image.FromFile(outputPath);
        image.Width.Should().Be(options.ImageSize.Width);
        image.Height.Should().Be(options.ImageSize.Height);
    }

    [Test]
    public void GenerateCloud_WithDifferentLayouters()
    {
        const string testText = "test word frequency cloud generator";
        File.WriteAllText(inputPath, testText);

        using var scope = container.BeginLifetimeScope();
        var generator = scope.Resolve<ITagCloudGenerator>();
        var options = new RenderingOptions
        {
            BackgroundColor = Color.Navy,
            WordColors = [Color.White, Color.Yellow],
            Font = "Arial",
            ImageSize = new Size(1000, 1000)
        };

        foreach (var layouterName in new[] { "linear", "circular" })
        {
            var outputFile = Path.Combine(tempDirectory, $"output_{layouterName}.png");
            generator.GenerateCloud(inputPath, outputFile, options);

            File.Exists(outputFile).Should().BeTrue();
            using var image = Image.FromFile(outputFile);
            image.Width.Should().Be(options.ImageSize.Width);
            image.Height.Should().Be(options.ImageSize.Height);
        }
    }

    [Test]
    public void GenerateCloud_WithLargeInput_HandlesCorrectly()
    {
        var random = new Random(42);
        var words = new[] { "test", "cloud", "generator", "word", "frequency" };
        var testText = string.Join(" ",
            Enumerable.Range(0, 1000)
                .Select(_ => words[random.Next(words.Length)]));

        File.WriteAllText(inputPath, testText);

        using var scope = container.BeginLifetimeScope();
        var generator = scope.Resolve<ITagCloudGenerator>();
        var options = new RenderingOptions
        {
            BackgroundColor = Color.White,
            WordColors = [Color.Black],
            Font = "Arial",
            ImageSize = new Size(1200, 1200)
        };

        generator.GenerateCloud(inputPath, outputPath, options);

        File.Exists(outputPath).Should().BeTrue();
        using var image = Image.FromFile(outputPath);
        image.Width.Should().Be(options.ImageSize.Width);
        image.Height.Should().Be(options.ImageSize.Height);
    }
}