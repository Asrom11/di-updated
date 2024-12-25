using System.Drawing;
using Autofac;
using TagsCloudContainer.DocumentReaders;
using TagsCloudContainer.PointGenerators;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;
using TextReader = TagsCloudContainer.DocumentReaders.TextReader;

namespace TagsCloudContainer;

public static class DependencyInjectionConfig
{
    public static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();
        
        builder.Register(c => new RenderingOptions())
            .AsSelf()
            .SingleInstance();

        
        builder.RegisterType<WordProcessor>()
            .As<IWordProcessor>()
            .SingleInstance()
            .WithParameter("boringWords", new[] 
            { 
                "and", "or", "the", "a", "in", "on", "at", "to", "for", "of", 
                "with", "by", "from", "up", "about", "into", "over", "after",
                "is", "are", "was", "were", "be", "have", "has", "had",
                "this", "that", "these", "those", "my", "your", "his", "her",
                "their", "our", "its", "any", "all", "they", "we", "i", "you"
            });
        
        builder.RegisterType<DocumentReaderFactory>()
            .As<IDocumentReaderFactory>()
            .SingleInstance();
        
        builder.RegisterType<TextReader>()
            .As<ITextReader>()
            .SingleInstance();

        builder.RegisterType<LogarithmicScaling>().
            As<ITextSizeCalculator>().
            SingleInstance();
        
        builder.RegisterType<WordFrequencyAnalyzer>()
            .As<IWordFrequencyAnalyzer>()
            .SingleInstance();
        
        builder.RegisterType<CircularCloudLayouter>()
            .As<ITagCloudLayouter>()
            .SingleInstance();

        builder.RegisterType<SpiralGenerator>()
            .As<IPointGenerator>()
            .SingleInstance()
            .WithParameter("center", new Point(500, 500));

        builder.RegisterType<LinearSpiral>()
            .Named<IPointGenerator>("linear");
        
        builder.RegisterType<CloudImageRenderer>()
            .As<ITagCloudRenderer>()
            .SingleInstance();
        
        builder.RegisterType<TagCloudGenerator>()
            .As<ITagCloudGenerator>()
            .SingleInstance();

        return builder.Build();
    }
}