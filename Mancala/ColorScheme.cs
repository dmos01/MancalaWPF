using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mancala
{
    public class ColorScheme
    {
        public SolidColorBrush EnabledStoneColor { get; }
        public SolidColorBrush EnabledStoneBorderColor { get; }
        public SolidColorBrush DisabledStoneColor { get; }
        public SolidColorBrush DisabledStoneBorderColor { get; }
        public Thickness StoneBorderThickness { get; }
        public SolidColorBrush CupBackgroundColor { get; }
        public SolidColorBrush CupBorderColor { get; }
        public Thickness CupBorderThickness { get; }

        public ColorScheme(Color enabledStoneColor, Color disabledStoneColor, Color cupBackgroundColor)
        {
            EnabledStoneColor = new SolidColorBrush(enabledStoneColor);
            DisabledStoneColor = new SolidColorBrush(disabledStoneColor);
            CupBackgroundColor = new SolidColorBrush(cupBackgroundColor);

            EnabledStoneBorderColor = new SolidColorBrush(enabledStoneColor);
            DisabledStoneBorderColor = new SolidColorBrush(disabledStoneColor);
            CupBorderColor = new SolidColorBrush(Colors.Black);
            StoneBorderThickness = new Thickness(0);
            CupBorderThickness = new Thickness(0);
        }

        public ColorScheme(Color enabledStoneColor, Color enabledStoneBorderColor, Color disabledStoneColor, Color disabledStoneBorderColor, byte stoneBorderThickness, Color cupBackgroundColor, Color cupBorderColor, byte cupBorderThickness)
        {
            EnabledStoneColor = new SolidColorBrush(enabledStoneColor);
            EnabledStoneBorderColor = new SolidColorBrush(enabledStoneBorderColor);

            DisabledStoneColor = new SolidColorBrush(disabledStoneColor);
            DisabledStoneBorderColor = new SolidColorBrush(disabledStoneBorderColor);

            CupBackgroundColor = new SolidColorBrush(cupBackgroundColor);
            CupBorderColor = new SolidColorBrush(cupBorderColor);

            StoneBorderThickness = new Thickness(stoneBorderThickness);
            CupBorderThickness = new Thickness(cupBorderThickness);
        }

        public static ColorScheme CurrentColorScheme = new(Colors.Lime, Colors.Black, Colors.SaddleBrown);
    }
}
