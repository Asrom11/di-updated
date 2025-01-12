using System.Drawing;
using Autofac;
using CommandLine;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;

public static class Program
{
    public static void Main(string[] args)
    {
        var options = Parser.Default.ParseArguments<Options>(args)
            .Value;

        var container = DependencyInjectionConfig.BuildContainer(options);

        using var scope = container.BeginLifetimeScope();
        var tagCloudGenerator = scope.Resolve<ITagCloudGenerator>();

        var renderingOptions = ParseRenderingOptions(options);

        tagCloudGenerator.GenerateCloud(
            options.InputFilePath,
            options.OutputFilePath,
            renderingOptions);
    }

    private static RenderingOptions ParseRenderingOptions(Options options)
    {
        return new RenderingOptions
        {
            BackgroundColor = ParseColor(options.BackgroundColor),
            WordColors = options.WordColors
                .Split(',')
                .Select(ParseColor)
                .ToArray(),
            Font = options.Font,
            ImageSize = new Size(options.Width, options.Height)
        };
    }

    static Color ParseColor(string colorString)
    {
        try
        {
            if (Enum.TryParse(typeof(KnownColor), colorString, true, out var knownColor))
            {
                return Color.FromKnownColor((KnownColor)knownColor);
            }

            return ColorTranslator.FromHtml(colorString.StartsWith("#") || colorString.Length == 6
                ? $"#{colorString.TrimStart('#')}"
                : colorString);
        }
        catch
        {
            throw new FormatException($"Invalid color format: {colorString}. Use a valid name (e.g., White) or HEX (e.g., #FFFFFF).");
        }
    }
}