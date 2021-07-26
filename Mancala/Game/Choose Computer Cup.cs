using System;
using System.Collections.Generic;

namespace Mancala.Game
{
    public partial class PageGameBoard
    {
        byte tempChosenCup; //In case multiple capturing opportunities are available.
        byte[] testCups;

        void ChooseComputerCup()
        {
            ChosenCup = -1;

            //Each harder difficulty adds an additional thing to check for (if the checks from easier difficulties fail).

            if (player2Type != Enums.PlayerType.Easy)
            {
                //Therefore Normal, Hard or Very Hard
                SearchForFreeTurn();

                if (chosenCup == null && player2Type != Enums.PlayerType.Normal)
                {
                    //Therefore Hard or Very Hard
                    if (capturing)
                        SearchForCapturing();
                    if (chosenCup == null && multipleLaps)
                        SearchForMultipleLapsThatGiveAnExtraTurn();

                    //By elimination
                    if (chosenCup == null && player2Type == Enums.PlayerType.VeryHard)
                        WeightedRandom();
                }
            }

            //The difficulty is Easy or all other checks have fallen through. Use random.
            if (chosenCup == null)
                Random();

            CupClick(chosenCup.CupControl, null);
        }

        void SearchForFreeTurn()
        {
            byte requiredNumberOfStones = 1;
            for (byte i = 5; i != 255; i--)
            {
                if (cups[i].NumberOfStones == requiredNumberOfStones)
                {
                    ChosenCup = i;
                    break;
                }

                requiredNumberOfStones++;
            }
        }

        void SearchForCapturing()
        {
            testCups = new byte[14];
            tempChosenCup = 0; //In case multiple capturing opportunities are available.
            byte numCapturedStonesWithTempChosenCup = 0; //In case multiple capturing opportunities are available.

            for (byte testChosen = 5; testChosen != 255; testChosen--)
            {
                if (cups[testChosen].NumberOfStones == 0)
                    continue;

                //Recreate number of stones.
                for (byte i = 0; i < cups.Length; i++)
                    testCups[i] = cups[i].NumberOfStones;

                stonesToMove = testCups[testChosen];
                sowInto = testChosen + 1;
                bool landInMancala; //But not necessarily finish in.
                do
                    landInMancala = TestSowingOneStone(testChosen);
                while (stonesToMove > 0);

                if (landInMancala) //Would be free turn, but not capturing.
                    continue;


                byte numCapturedStones = testCups[12 - (sowInto - 1)];

                //Stones will be captured — and more than previous tests.
                if (numCapturedStones > numCapturedStonesWithTempChosenCup && testCups[sowInto - 1] == 1)
                {
                    //The final sowed cup is on player 2's side.
                    if (sowInto - 1 < 6)
                    {
                        tempChosenCup = testChosen;
                        numCapturedStonesWithTempChosenCup = numCapturedStones;
                    }
                }
            }

            if (numCapturedStonesWithTempChosenCup > 0)
                ChosenCup = tempChosenCup;
        }

        void SearchForMultipleLapsThatGiveAnExtraTurn()
        {
            testCups = new byte[14];
            for (byte testChosen = 5; testChosen != 255; testChosen--)
            {
                if (cups[testChosen].NumberOfStones == 0)
                    continue;

                //Recreate number of stones.
                for (byte i = 0; i < cups.Length; i++)
                    testCups[i] = cups[i].NumberOfStones;

                bool landInMancala; //But not necessarily finish in.
                int multipleLapsChosen = testChosen;
                sowInto = testChosen + 1;

                do
                {
                    stonesToMove = testCups[multipleLapsChosen];
                    requireMultipleLaps = false;

                    //Do one lap.
                    do
                        landInMancala = TestSowingOneStone(multipleLapsChosen);
                    while (stonesToMove > 0);

                    //Prepare to repeat loop if another lap required.
                    if (landInMancala == false && testCups[sowInto - 1] > 1)
                    {
                        requireMultipleLaps = true;
                        multipleLapsChosen = sowInto - 1;
                    }
                } while (requireMultipleLaps);

                //If final lap landed in Mancala.
                if (landInMancala)
                    ChosenCup = testChosen;
            }
        }

        void WeightedRandom()
        {
            //Randomly choose a cup, but give more weight to cups with more stones
            //In this case, cups with 1-3 stones are given 1 weight, 4-6 are given 2, etc.
            //(Higher weights better.) This gives more variance.

            List<byte> weightedCupNumbers = new();

            for (byte cup = 5; cup != 255; cup--)
            {
                byte weightCounter = 0;
                int numStones = cups[cup].NumberOfStones;

                while (numStones > 0)
                {
                    weightCounter++;
                    numStones -= 3;
                }

                while (weightCounter > 0)
                {
                    weightedCupNumbers.Add(cup);
                    weightCounter--;
                }
            }

            ChosenCup = weightedCupNumbers[new Random().Next(0, weightedCupNumbers.Count)];
        }

        void Random()
        {
            do
            {
                ChosenCup = new Random().Next(0, 6);
            } while (chosenCup.NumberOfStones == 0);
        }

        /// <summary>
        ///     Returns true if the lap ends in a Mancala.
        /// </summary>
        /// <param name="testChosenCup"></param>
        /// <returns></returns>
        bool TestSowingOneStone(int testChosenCup)
        {
            stonesToMove -= 1;
            testCups[testChosenCup] -= 1;

            switch (sowInto)
            {
                case 13 when currentPlayer == Enums.Player.Player1 && stonesToMove == 0:
                    return true;
                case 13:
                    testCups[0]++;
                    sowInto = 1;
                    break;

                case 6 when currentPlayer == Enums.Player.Player2:
                {
                    switch (stonesToMove)
                    {
                        case 0:
                            return true;
                        case > 0:
                            stonesToMove--;
                            testCups[testChosenCup]--;
                            testCups[7]++;
                            sowInto = 8;
                            break;
                    }

                    break;
                }

                case 6:
                    testCups[6]++;
                    sowInto = 7;
                    break;
                default:
                    testCups[sowInto]++;
                    sowInto++;
                    break;
            }

            return false;
        }
    }
}