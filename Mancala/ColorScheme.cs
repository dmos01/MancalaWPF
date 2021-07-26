using System.Windows;
using System.Windows.Media;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
// ReSharper disable MemberCanBePrivate.Global

namespace Mancala
{
    public class ColorScheme
    {
        public static readonly ColorScheme CurrentColorScheme = new(Colors.Lime, Colors.Black, Colors.SaddleBrown);

        public SolidColorBrush EnabledStoneColor { get; set; }
        public Border EnabledStoneBorder { get; set; }
        public SolidColorBrush DisabledStoneColor { get; set; }
        public Border DisabledStoneBorder { get; set; }
        public SolidColorBrush CupBackgroundColor { get; set; }
        public Border CupBorder { get; set; }

        public ColorScheme(Color enabledStoneColor, Color disabledStoneColor, Color cupColor)
        {
            EnabledStoneColor = new(enabledStoneColor);
            DisabledStoneColor = new(disabledStoneColor);
            CupBackgroundColor = new(cupColor);

            EnabledStoneBorder = new(enabledStoneColor, 0);
            DisabledStoneBorder = new(disabledStoneColor, 0);
            CupBorder = new(cupColor, 0);
        }
    }

    public class Border
    {
        public SolidColorBrush Color { get; }
        public Thickness Thickness { get; }

        public Border(Color color, byte thickness)
        {
            Color = new(color);
            Thickness = new Thickness(thickness);
        }
    }
}