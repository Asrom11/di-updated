using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization;

public class CloudImageRenderer : ITagCloudRenderer
{
    private readonly Random random = new();
    private const float MinFontSize = 12f;
    private const float MaxFontSize = 72f;
    private const float FrequencyMultiplier = 2f;

    public void Render(IEnumerable<Tag> tags, string outputFilePath, RenderingOptions options)
    {
        using var bitmap = new Bitmap(options.ImageSize.Width, options.ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        

        graphics.Clear(options.BackgroundColor);
        

        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        foreach (var tag in tags)
        {
            var fontSize = Math.Max(MinFontSize, Math.Min(MaxFontSize, tag.Frequency * FrequencyMultiplier));

            using var font = new Font(options.Font, fontSize, FontStyle.Bold);
            
            var color = options.WordColors[random.Next(options.WordColors.Length)];
            using var brush = new SolidBrush(color);
            
            graphics.DrawString(
                tag.Word,
                font,
                brush,
                tag.Rectangle.Location
            );
        }

        bitmap.Save(outputFilePath, ImageFormat.Png);
    }
}