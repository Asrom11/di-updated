using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class CircularCloudLayouter(IPointGenerator pointGenerator): ITagCloudLayouter
{
    private readonly List<Rectangle> _rectangles = new();
    private readonly Grid _grid = new();
    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Размеры прямоугольника должны быть положительными.");

        Rectangle newRectangle;
        do
        {
            var point = pointGenerator.GetNextPoint();
            var location = new Point(
                point.X - rectangleSize.Width / 2,
                point.Y - rectangleSize.Height / 2);
            newRectangle = new Rectangle(location, rectangleSize);
        } while (_grid.IsIntersecting(newRectangle));
        
        _grid.AddRectangle(newRectangle);
        _rectangles.Add(newRectangle);
        return newRectangle;
    }

    public IEnumerable<Rectangle> GetRectangles() => _rectangles;
}