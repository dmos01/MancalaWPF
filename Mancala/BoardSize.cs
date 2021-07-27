using System.Windows;

namespace Mancala
{
    public class BoardSize
    {
        public const double MinStoneSize = 14;
        public const double MaxStoneSize = 19;
        public const double DefaultAbsoluteHeight = 450;
        public readonly double AbsoluteHeight;
        public readonly double AbsoluteWidth;
        public readonly Thickness CenterFrameMargin;
        public readonly double CupSizeAndMancalaWidth;
        public readonly double PlayerSideWidth;
        public readonly Thickness RightMargin;
        public readonly double StoneSize;

        const double numStonesPerRow = 3;
        const double numCupsPerRow = 6;
        const double normalDistanceToOuterEdge = 10;
        const double defaultStoneSize = 15;
        const double AbsoluteWidthPadding = 3;
        const double AbsoluteHeightPadding = 8; //Just seems to work.
        const double slightlyMoreThanHalf = (double) 12 / (double) 7;


        //Note: AbsoluteWidth not enough when stoneSize = 19 (or stoneSize == MaxStoneSize?).

        public BoardSize(double stoneSize)
        {
            StoneSize = stoneSize;
            double gapBetweenStones = stoneSize / slightlyMoreThanHalf;

            CupSizeAndMancalaWidth = stoneSize * numStonesPerRow + gapBetweenStones * (numStonesPerRow + 1);

            double sumOfGapsBetweenPlayerCups = CupSizeAndMancalaWidth;
            PlayerSideWidth = CupSizeAndMancalaWidth * numCupsPerRow + sumOfGapsBetweenPlayerCups;

            //Uniform Grid seems to set the distance from edge to first/last item to half the gap between the other items. Therefore, / 2.
            double distanceFromEdgeOfGridToCup = sumOfGapsBetweenPlayerCups / numCupsPerRow / 2;

            CenterFrameMargin = new Thickness(distanceFromEdgeOfGridToCup, normalDistanceToOuterEdge,
                distanceFromEdgeOfGridToCup, normalDistanceToOuterEdge);
            RightMargin = new Thickness(0, 0, gapBetweenStones, 0);

            //From-edge + Mancala + gap + player cups.
            double gapFromMancalaToPlayerCups = gapBetweenStones + distanceFromEdgeOfGridToCup;
            AbsoluteWidth = normalDistanceToOuterEdge + CupSizeAndMancalaWidth + gapFromMancalaToPlayerCups +
                            PlayerSideWidth;

            //Gap + Mancala + to-edge + padding.
            AbsoluteWidth += gapFromMancalaToPlayerCups + CupSizeAndMancalaWidth + normalDistanceToOuterEdge +
                             AbsoluteWidthPadding;

            double differenceFromDefaultStoneSize = stoneSize - defaultStoneSize;
            AbsoluteHeight = DefaultAbsoluteHeight + differenceFromDefaultStoneSize * AbsoluteHeightPadding;
        }
    }
}