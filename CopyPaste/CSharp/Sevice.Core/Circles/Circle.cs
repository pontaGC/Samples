using System.Drawing;
using System.Numerics;
using Sevices.Core.Data;

namespace Sevices.Core.Circles
{
    /// <summary>
    /// The circle entity.
    /// </summary>
    public class Circle : Entity<Guid>
    {
        public Circle(CircleData source)
            : this(
                  Guid.Parse(source.Id),
                  source.Name ?? string.Empty,
                  new Vector<double>(new[] { source.Position.X, source.Position.Y }),
                  ColorTranslator.FromHtml(source.FillColor),
                  ColorTranslator.FromHtml(source.StrokeColor)
                  )
        {
        }

        public Circle(
            string name,
            Vector<double> position,
            Color fillColor,
            Color strokeColor)
            : this(Guid.NewGuid(), name, position, fillColor, strokeColor)
        {
        }

        public Circle(
            Guid id,
            string name,
            Vector<double> position,
            Color fillColor,
            Color strokeColor)
        {
            ArgumentNullException.ThrowIfNull(name);

            this.Id = id;
            this.Name = name;
            this.Position = position;
            this.FillColor = fillColor;
            this.StrokeColor = strokeColor;
        }

        /// <inheritdoc />
        public override Guid Id { get; protected set; }

        public string Name { get; } = string.Empty;

        public Vector<double> Position { get; } = new Vector<double>();

        public Color FillColor { get; }

        public Color StrokeColor { get; }
    }
}
