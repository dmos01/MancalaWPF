using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mancala.Game
{
    /// <summary>
    ///     Handles controls and properties for cups and Mancalas. Is not itself a custom control.
    /// </summary>
    class Cup
    {
        // ReSharper disable once InconsistentNaming
        bool _isHighlighted;

        byte numberOfStones;

        public Enums.Player OwnedBy { get; }

        public bool IsMancala { get; }

        /// <summary>
        /// The label that represents the visible cup.
        /// </summary>
        public Label CupControl { get; }

        /// <summary>
        /// The label that represents the number of stones when that number is too large to display individual controls.
        /// </summary>
        public Label StoneTextLabel { get; }

        /// <summary>
        /// The labels that represent the visible stones.
        /// </summary>
        public Label[] StoneControls { get; }

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
                    MessageBox.Show(MessageResources.CupHasNoStonesMessage, MessageResources.ErrorTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    throw new OverflowException(MessageResources.CupHasNoStonesMessage);
                }

                numberOfStones = value;
            }
        }

        public bool IsEnabled
        {
            get => CupControl.IsEnabled;
            set
            {
                if (value && numberOfStones == 0)
                {
                    //Enabling a cup with no stones must not happen.
                    return;
                }

                CupControl.IsEnabled = value;
            }
        }

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
                    SolidColorBrush color = ColorScheme.CurrentColorScheme.EnabledStoneColor;
                    SolidColorBrush borderColor = ColorScheme.CurrentColorScheme.EnabledStoneBorderColor;
                    StoneTextLabel.Foreground = color;
                    foreach (Label stone in StoneControls)
                    {
                        stone.Background = color;
                        stone.BorderBrush = borderColor;
                    }
                }
                else
                {
                    _isHighlighted = false;
                    SolidColorBrush color = ColorScheme.CurrentColorScheme.DisabledStoneColor;
                    SolidColorBrush borderColor = ColorScheme.CurrentColorScheme.DisabledStoneBorderColor;
                    StoneTextLabel.Foreground = color;
                    foreach (Label stone in StoneControls)
                    {
                        stone.Background = color;
                        stone.BorderBrush = borderColor;
                    }
                }
            }
        }


        public Cup(Enums.Player ownedBy, bool isMancala, Label mainLabel, Label stoneTextLabel,
            params Label[] stoneLabels) : this(0, ownedBy, isMancala, mainLabel, stoneTextLabel, stoneLabels) { }

        public Cup(byte numberOfStones, Enums.Player ownedBy, bool isMancala, Label cupControl,
            Label stoneTextLabel, params Label[] stoneControls)
        {
            OwnedBy = ownedBy;
            IsMancala = isMancala;
            CupControl = cupControl;
            StoneTextLabel = stoneTextLabel;
            StoneControls = stoneControls;
            NumberOfStones = numberOfStones;

            UpdateVisibleStonesToMatchNumberOfStones();

            CupControl.IsEnabled = false;
            StoneTextLabel.IsEnabled = false;

            CupControl.Background = ColorScheme.CurrentColorScheme.CupBackgroundColor;
            CupControl.BorderBrush = ColorScheme.CurrentColorScheme.CupBorderColor;
            CupControl.BorderThickness = ColorScheme.CurrentColorScheme.CupBorderThickness;

            SolidColorBrush color = ColorScheme.CurrentColorScheme.DisabledStoneColor;
            SolidColorBrush borderColor = ColorScheme.CurrentColorScheme.DisabledStoneBorderColor;
            Thickness borderThickness = ColorScheme.CurrentColorScheme.StoneBorderThickness;
            StoneTextLabel.Foreground = color;
            foreach (Label stone in StoneControls)
            {
                stone.IsEnabled = false;
                stone.Background = color;
                stone.BorderBrush = borderColor;
                stone.BorderThickness = borderThickness;
            }
        }

        public void EnableAndHighlight()
        {
            if (numberOfStones == 0)
                return;

            IsEnabled = true;
            IsHighlighted = true;
        }

        public override string ToString()
        {
            StringBuilder cupInfo = new();
            cupInfo.Append(OwnedBy.ToString());
            cupInfo.Append(IsMancala ? InternalCupInfoResources.Mancala : InternalCupInfoResources.Cup);
            cupInfo.Append(NumberOfStones);
            cupInfo.Append(NumberOfStones == 1 ? MessageResources.StoneSingular : MessageResources.StonesPlural);

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (IsEnabled && IsHighlighted)
                cupInfo.Append(InternalCupInfoResources.EnabledAndHighlighted);
            else if (IsEnabled)
                cupInfo.Append(InternalCupInfoResources.Enabled);
            else
                cupInfo.Append(InternalCupInfoResources.Highlighted);

            return cupInfo.ToString();
        }

        public void UpdateVisibleStonesToMatchNumberOfStones()
        {
            if (NumberOfStones <= StoneControls.Length)
            {
                for (byte stone = 0; stone < NumberOfStones; stone++)
                    StoneControls[stone].Visibility = Visibility.Visible;

                for (byte stone = NumberOfStones; stone < StoneControls.Length; stone++)
                    StoneControls[stone].Visibility = Visibility.Hidden;
                StoneTextLabel.Content = "";
            }
            else
            {
                Array.ForEach(StoneControls, stone => stone.Visibility = Visibility.Collapsed);
                StoneTextLabel.Content = NumberOfStones;
            }
        }
    }
}