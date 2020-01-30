using System.Windows;

namespace Mancala
{
    public class BoardSize
    {
        public const double MinStoneSize = 14;
        public const double MaxStoneSize = 19;

        public readonly double StoneSize;
        public readonly double CupSizeAndMancalaWidth;
        public readonly double PlayerSideWidth;
        public readonly Thickness CentreFrameMargin;
        public readonly Thickness RightMargin;
        public readonly double AbsoluteWidth;
        public const double DefaultAbsoluteHeight = 450;
        public readonly double AbsoluteHeight;

        //Note: AbsoluteWidth not enough when stoneSize = 19 (or stoneSize == MaxStoneSize?).

        public BoardSize(double stoneSize)
        {
            //Constants are doubles to prevent errors when multiplying a byte and non-whole double.

            StoneSize = stoneSize;
            double gapBetweenStones = stoneSize / 12 * 7; //Slightly more than 1/2.

            const double numStonesPerRow = 3;
            CupSizeAndMancalaWidth = stoneSize * numStonesPerRow;
            CupSizeAndMancalaWidth += gapBetweenStones * (numStonesPerRow + 1);

            const double numCupsPerRow = 6;
            double sumOfGapsBetweenPlayerCups = CupSizeAndMancalaWidth;
            PlayerSideWidth = CupSizeAndMancalaWidth * numCupsPerRow + sumOfGapsBetweenPlayerCups;

            //Uniform Grid seems to set the distance from edge to first/last item to half the gap between the other items. Therefore, / 2.
            double distanceFromEdgeOfGridToCup = sumOfGapsBetweenPlayerCups / numCupsPerRow / 2;

            const double normalDistanceToOuterEdge = 10;
            CentreFrameMargin = new Thickness(distanceFromEdgeOfGridToCup, normalDistanceToOuterEdge, distanceFromEdgeOfGridToCup, normalDistanceToOuterEdge);
            RightMargin = new Thickness(0, 0, gapBetweenStones, 0);


            //From-edge + Mancala + gap + player cups.
            double gapFromMancalaToPlayerCups = gapBetweenStones + distanceFromEdgeOfGridToCup;
            AbsoluteWidth = normalDistanceToOuterEdge + CupSizeAndMancalaWidth + gapFromMancalaToPlayerCups + PlayerSideWidth;

            //Gap + Mancala + to-edge + padding.
            double arbitraryPadding = 3;
            AbsoluteWidth += gapFromMancalaToPlayerCups + CupSizeAndMancalaWidth + normalDistanceToOuterEdge + arbitraryPadding;

            const double defaultStoneSize = 15;
            double differenceFromDefaultStoneSize = stoneSize - defaultStoneSize;
            const double justSeemsToWork = 8;
            AbsoluteHeight = DefaultAbsoluteHeight + differenceFromDefaultStoneSize * justSeemsToWork;
        }
    }
}