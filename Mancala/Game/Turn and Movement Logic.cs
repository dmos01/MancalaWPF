using System;
using System.Windows;
using System.Windows.Threading;


namespace Mancala.Game
{
    public partial class PageGameBoard
    {
        bool requireMultipleLaps;
        bool requireExtraTurn;
        byte stonesToMove;
        int sowInto;
        Cup chosenCup;
        int chosenCupIndex;
        int ChosenCup
        {
            set
            {
                chosenCupIndex = value;
                chosenCup = value == -1 ? null : cups[value];
            }
        }

        public bool CurrentPlayerIsHuman => currentPlayer == Enums.Player.Player1 || player2Type == Enums.PlayerType.Human;

        void StartNewTurnAndUnhighlightPreviousChosenCupIfApplicable(bool withNewPlayer)
        {
            requireExtraTurn = false;

            if (withNewPlayer)
            {
                currentPlayer = currentPlayer == Enums.Player.Player1
                    ? Enums.Player.Player2 : Enums.Player.Player1;

                //There was a chosen cup from previous turn. Does not cover the computer getting another turn — handled below.
                if (chosenCup != null)
                    chosenCup.IsHighlighted = false;
            }

            if (currentPlayer == Enums.Player.Player1)
            {
                ChosenCup = -1;
                UpdatePositionsOfAllStones();
                EnableAndHighlightPlayer1Cups();
                centrePage.ChangePlayerTurnText(Enums.Player.Player1);
                centrePage.EnableControls();
            }
            else
            {
                if (player2Type == Enums.PlayerType.Human)
                {
                    ChosenCup = -1;
                    UpdatePositionsOfAllStones();
                    EnableAndHighlightPlayer2Cups();
                    centrePage.ChangePlayerTurnText(Enums.Player.Player2);
                    centrePage.EnableControls();
                }
                else
                {
                    if (Animation.ComputerAnimationIsOn)
                    {
                        UnhighlightPlayer1Cups(false);

                        //The computer does not start its turn by having all cups enabled, so disable the chosen cup.
                        if (withNewPlayer == false && chosenCup != null)
                            chosenCup.IsHighlighted = false;

                        centrePage.ChangePlayerTurnText(Enums.Player.Player2);
                        UpdatePositionsOfAllStones();
                    }

                    ChooseComputerCup();
                }
            }
        }

        /// <summary>
        /// Also calls MoveNextStone(), starting movement.
        /// </summary>
        void ShowChosenCupAndPrepareToSow()
        {
            stonesToMove = chosenCup.NumberOfStones;
            requireExtraTurn = false;
            requireMultipleLaps = false;
            sowInto = chosenCupIndex + 1;

            if (currentPlayer == Enums.Player.Player1)
            {
                if (Animation.HumanAnimationIsOn)
                {
                    centrePage.DisableControls();
                    UnhighlightPlayer1Cups(true);
                }
                else if (player2Type == Enums.PlayerType.Human)
                {
                    //No animation, but next player is human.
                    centrePage.DisableControls();
                    UnhighlightPlayer1Cups(false);
                }
                else if (Animation.ComputerAnimationIsOn)
                {
                    centrePage.DisableControls();
                    UnhighlightPlayer1Cups(false);
                }
                //No animation will occur before Player 1's next turn — there is no
                //human animation, player 2 is not human and there is no computer animation.
                MoveNextStone();
            }
            else
            {
                if (player2Type == Enums.PlayerType.Human)
                {
                    centrePage.DisableControls();
                    UnhighlightPlayer2Cups(exceptChosenCup: Animation.HumanAnimationIsOn);
                    MoveNextStone();
                }
                else if (Animation.ComputerAnimationIsOn)
                {
                    chosenCup.IsHighlighted = true;
                    Animation.StartTimerForComputerChosenCupOrNewLapCup(Tick);

                    void Tick(object sender, EventArgs e)
                    {
                        ((DispatcherTimer)sender)?.Stop();
                        MoveNextStone();
                    }
                }
                else
                    MoveNextStone();
            }
        }

        void MoveNextStone()
        {
            stonesToMove--;
            chosenCup.NumberOfStones--;

            if (cups[sowInto].IsMancala)
            {
                if (cups[sowInto].OwnedBy == Enums.Player.Player1) //sowInto = 13. Next is 0.
                {
                    if (currentPlayer == Enums.Player.Player1)
                    {
                        if (stonesToMove == 0)
                            requireExtraTurn = true;
                        MoveStone(0);

                    }
                    else //Player 2 skips Player 1's Mancala.
                    {
                        sowInto = 0;
                        MoveStone(1);
                    }
                }
                else //sowInto = 6
                {
                    if (currentPlayer == Enums.Player.Player2)
                    {
                        if (stonesToMove == 0)
                            requireExtraTurn = true;
                        MoveStone(7);

                    }
                    else //Player 1 skips 2's Mancala.
                    {
                        sowInto = 7;
                        MoveStone(8);
                    }
                }
            }
            else
            {
                MoveStone(sowInto + 1);
            }
        }

        void MoveStone(int nextSowInto)
        {
            cups[sowInto].NumberOfStones++;

            ////Animate
            if (CurrentPlayerIsHuman)
            {
                if (Animation.HumanAnimationIsOn)
                {
                    //Don't need to update all - just the chosen one and one being sown into.
                    chosenCup.UpdateVisibleStonesToMatchNumberOfStones();
                    cups[sowInto].UpdateVisibleStonesToMatchNumberOfStones();
                    Animation.StartHumanTimer(Tick);
                }
                else
                    Tick(null, null);
            }
            else
            {
                if (Animation.ComputerAnimationIsOn)
                {
                    //Don't need to update all - just the chosen one and one being sown into.
                    chosenCup.UpdateVisibleStonesToMatchNumberOfStones();
                    cups[sowInto].UpdateVisibleStonesToMatchNumberOfStones();
                    Animation.StartComputerTimer(Tick);
                }
                else
                    Tick(null, null);
            }

            void Tick(object sender, EventArgs e)
            {
                ((DispatcherTimer)sender)?.Stop();
                sowInto = nextSowInto;

                if (stonesToMove == 0)
                    EndOfLap();
                else
                    MoveNextStone();
            }
        }

        /// <summary>
        /// Includes testing if Multiple Laps required.
        /// </summary>
        void EndOfLap()
        {
            if (Player1sSideIsEmpty())
                EndGame(Enums.Player.Player1);
            else if (Player2sSideIsEmpty())
                EndGame(Enums.Player.Player2);
            else if (requireExtraTurn)
                StartNewTurnAndUnhighlightPreviousChosenCupIfApplicable(false);

            else if (multipleLaps && cups[sowInto - 1].NumberOfStones > 1)
            {
                AnimateAndStartMultipleLaps();
            }
            else if (capturing && cups[12 - (sowInto - 1)].NumberOfStones > 0 &&
                     cups[sowInto - 1].NumberOfStones == 1 &&
                     cups[sowInto - 1].OwnedBy == currentPlayer)
            {
                Capture();
            }
            else
                StartNewTurnAndUnhighlightPreviousChosenCupIfApplicable(true);
        }

        void AnimateAndStartMultipleLaps()
        {
            chosenCup.IsHighlighted = false;
            ChosenCup = sowInto - 1;

            ////Animate
            if (CurrentPlayerIsHuman)
            {
                if (Animation.HumanAnimationIsOn)
                {
                    //Don't need to update all - just the chosen one and one being sown into.
                    chosenCup.UpdateVisibleStonesToMatchNumberOfStones();
                    cups[sowInto].UpdateVisibleStonesToMatchNumberOfStones();

                    //Pause to show cup here because ShowChosenCupAndPrepareToSow()
                    //does not pause to show human chosen cups.
                    chosenCup.IsHighlighted = true;
                    Animation.StartHumanTimer(Tick);
                }
                else
                    ShowChosenCupAndPrepareToSow();
            }
            else
            {
                if (Animation.ComputerAnimationIsOn)
                {
                    //Don't need to update all - just the chosen one and one being sown into.
                    chosenCup.UpdateVisibleStonesToMatchNumberOfStones();
                    cups[sowInto].UpdateVisibleStonesToMatchNumberOfStones();
                }

                //Pause to show new computer chosen cup will happen in ShowChosenCupAndPrepareToSow().
                ShowChosenCupAndPrepareToSow();
            }

            void Tick(object sender, EventArgs e)
            {
                ((DispatcherTimer)sender)?.Stop();
                ShowChosenCupAndPrepareToSow();
            }
        }

        void Capture()
        {
            byte capturedStones = cups[12 - (sowInto - 1)].NumberOfStones;
            cups[12 - (sowInto - 1)].NumberOfStones = 0;
            capturedStones++;
            cups[sowInto - 1].NumberOfStones = 0;
            if (currentPlayer == Enums.Player.Player1)
                IncreasePlayer1Score(capturedStones);
            else
                IncreasePlayer2Score(capturedStones);

            if (Player1sSideIsEmpty())
                EndGame(Enums.Player.Player1);
            else if (Player2sSideIsEmpty())
                EndGame(Enums.Player.Player2);
            else
                StartNewTurnAndUnhighlightPreviousChosenCupIfApplicable(true);
        }

        bool Player1sSideIsEmpty()
        {
            for (byte i = 7; i <= 12; i++)
            {
                if (cups[i].NumberOfStones != 0)
                    return false;
            }

            return true;
        }

        bool Player2sSideIsEmpty()
        {
            for (byte i = 0; i <= 5; i++)
            {
                if (cups[i].NumberOfStones != 0)
                    return false;
            }

            return true;
        }

        void EndGame(Enums.Player playerWithEmptySide)
        {
            CollectRemainingStonesAndGiveToPlayer(playerWithEmptySide);
            ShowWinnerMessageBox();
            parentWindow.EndGame();
        }

        void CollectRemainingStonesAndGiveToPlayer(Enums.Player player)
        {
            byte stonesCollected = 0;
            for (byte i = 0; i < cups.Length; i++)
            {
                if (cups[i].IsMancala)
                    continue;

                stonesCollected += cups[i].NumberOfStones;
                cups[i].NumberOfStones = 0;
            }

            switch (emptyPlayerGetsRemainingStones)
            {
                case true when player == Enums.Player.Player1:
                case false when player == Enums.Player.Player2:
                    IncreasePlayer1Score(stonesCollected);
                    break;
                default:
                    IncreasePlayer2Score(stonesCollected);
                    break;
            }

            UpdatePositionsOfAllStones();
        }

        void ShowWinnerMessageBox()
        {
            if (GetPlayer1Score > GetPlayer2Score)
                MessageBox.Show(player1Name + " wins the game by " + (GetPlayer1Score - GetPlayer2Score) + " stones.",
                    player1Name + " Wins");
            else if (GetPlayer2Score > GetPlayer1Score)
                MessageBox.Show(player2Name + " wins the game by " + (GetPlayer2Score - GetPlayer1Score) + " stones.",
                    player2Name + " Wins");
            else if (GetPlayer1Score == GetPlayer2Score)
                MessageBox.Show("The game has ended in a tie.", "Game Over");
        }
    }
}