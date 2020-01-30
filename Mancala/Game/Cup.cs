using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mancala.Game
{
    /// <summary>
    /// Handles controls and properties for cups and Mancalas. Is not itself a custom control.
    /// </summary>
    class Cup
    {
        public static SolidColorBrush HighlightedColor { get; } = new SolidColorBrush(Colors.Lime);

        public static SolidColorBrush UnhighlightedColor { get; } = new SolidColorBrush(Colors.Black);

        public static SolidColorBrush BackgroundColor { get; } = new SolidColorBrush(Colors.SaddleBrown);


        public Enums.Player OwnedBy { get; }

        public bool IsMancala { get; }

        public Label MainLabel { get; }

        public Label StoneTextLabel { get; }

        public Label[] StoneLabels { get; }

        byte numberOfStones;

        public byte NumberOfStones
        {
            get => numberOfStones;
            set
            {
                /*Byte has over/under-run and looped back round. 241 in case the value
                  is 12 fewer than 256 by the time this triggers (NumberOfStones++).
                  Also, 240 is the maximum number of stones given 12 cups at up to
                  20 starting stones each.
                  Or, this could be triggered with NumberOfStones--.*/
                if (value > 241)
                {
                    MessageBox.Show("The program tried to move a stone from a cup with no stones. Press OK to quit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new OverflowException("The program tried to move a stone from a cup with no stones.");
                }
                numberOfStones = value;
            }
        }

        public void EnableAndHighlight()
        {
            if (numberOfStones == 0)
                return;

            IsEnabled = true;
            IsHighlighted = true;
        }

        public bool IsEnabled
        {
            get => MainLabel.IsEnabled;
            set
            {
                if (value && numberOfStones == 0)
                {
                    //Enabling a cup with no stones must not happen.
                    return;
                }

                MainLabel.IsEnabled = value;
            }
        }

        // ReSharper disable once InconsistentNaming
        bool _isHighlighted;
        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                if (_isHighlighted == value)
                    return;

                if (value)
                {
                    if (NumberOfStones == 0)
                    {
                        //Highlighting a cup with no stones should not happen.
                        return;
                    }

                    _isHighlighted = true;
                    StoneTextLabel.Foreground = HighlightedColor;
                    Array.ForEach(StoneLabels, stone => stone.Background = HighlightedColor);
                }
                else
                {
                    _isHighlighted = false;
                    StoneTextLabel.Foreground = UnhighlightedColor;
                    Array.ForEach(StoneLabels, x => x.Background = UnhighlightedColor);
                }
            }
        }


        public Cup(Enums.Player ownedBy, bool isMancala, Label mainLabel, Label stoneTextLabel,
            params Label[] stoneLabels) : this(0, ownedBy, isMancala, mainLabel, stoneTextLabel, stoneLabels) { }

        public Cup(byte numberOfStones, Enums.Player ownedBy, bool isMancala, Label mainLabel,
            Label stoneTextLabel, params Label[] stoneLabels)
        {
            OwnedBy = ownedBy;
            IsMancala = isMancala;
            MainLabel = mainLabel;
            StoneTextLabel = stoneTextLabel;
            StoneLabels = stoneLabels;
            NumberOfStones = numberOfStones;

            UpdateVisibleStonesToMatchNumberOfStones();

            MainLabel.IsEnabled = false;
            StoneTextLabel.IsEnabled = false;

            StoneTextLabel.Foreground = UnhighlightedColor;
            foreach (Label label in StoneLabels)
            {
                label.IsEnabled = false;
                label.Background = UnhighlightedColor;
            }
        }

        public override string ToString()
        {
            string output = OwnedBy.ToString();
            output += IsMancala ? " Mancala. " : " cup. ";
            output += NumberOfStones;
            output += NumberOfStones == 1 ? " stone." : " stones.";

            if (IsEnabled && IsHighlighted)
                return output + " Enabled and highlighted.";
            else if (IsEnabled)
                return output + " Enabled.";
            else if (IsHighlighted)
                return output + " Highlighted.";
            else
                return output;
        }

        public void UpdateVisibleStonesToMatchNumberOfStones()
        {
            if (NumberOfStones <= StoneLabels.Length)
            {
                for (byte stone = 0; stone < NumberOfStones; stone++)
                    StoneLabels[stone].Visibility = Visibility.Visible;

                for (byte stone = NumberOfStones; stone < StoneLabels.Length; stone++)
                    StoneLabels[stone].Visibility = Visibility.Hidden;
                StoneTextLabel.Content = "";
            }
            else
            {
                Array.ForEach(StoneLabels, stone => stone.Visibility = Visibility.Collapsed);
                StoneTextLabel.Content = NumberOfStones;
            }
        }
    }
}
