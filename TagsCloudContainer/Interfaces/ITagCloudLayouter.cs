using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface ITagCloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
    IEnumerable<Rectangle> GetRectangles();
}