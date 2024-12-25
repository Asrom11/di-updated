using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer;

public class LogarithmicScaling: ITextSizeCalculator
{
    public Size GetWordSize(string word, int frequency, RenderingOptions options)
    {
        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);
        
        const float minFontSize = 12f;
        const float maxFontSize = 72f;
        
        const float power = 0.7f;
        
        var normalizedFreq = (float)Math.Pow(frequency, power);
        var fontSize = minFontSize + (maxFontSize - minFontSize) * normalizedFreq / 
            (float)Math.Pow(100, power);

        fontSize = Math.Max(minFontSize, Math.Min(maxFontSize, fontSize));
        
        using var font = new Font(options.Font, fontSize, FontStyle.Bold);
        
        var size = graphics.MeasureString(word, font);

        var padding = fontSize * 0.2f;
        
        return new Size(
            (int)Math.Ceiling(size.Width + padding),
            (int)Math.Ceiling(size.Height + padding)
        );
    }
}