using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

//Handles Cup objects, particularly when more than one must be interacted with at once.

namespace Mancala.Game
{
    public partial class PageGameBoard
    {
        Cup[] cups;

        //Cup Player1Mancala => cups[13];
        //Cup Player2Mancala => cups[6];

        byte GetPlayer1Score => cups[13].NumberOfStones;

        byte GetPlayer2Score => cups[6].NumberOfStones;

        void IncreasePlayer1Score(byte increase) => cups[13].NumberOfStones += increase;

        void IncreasePlayer2Score(byte increase) => cups[6].NumberOfStones += increase;

        void InitialiseCupsAndMancalas(byte startingStones)
        {
            cups = new Cup[14];
            cups[13] = new Cup(0, Enums.Player.Player1, true, btnPlayer1Mancala,
                lblPlayer1MancalaStoneText, lblPlayer1MancalaStone0,
                lblPlayer1MancalaStone1, lblPlayer1MancalaStone2, lblPlayer1MancalaStone3, lblPlayer1MancalaStone4,
                lblPlayer1MancalaStone5, lblPlayer1MancalaStone6, lblPlayer1MancalaStone7, lblPlayer1MancalaStone8,
                lblPlayer1MancalaStone9, lblPlayer1MancalaStone10, lblPlayer1MancalaStone11, lblPlayer1MancalaStone12,
                lblPlayer1MancalaStone13, lblPlayer1MancalaStone14, lblPlayer1MancalaStone15, lblPlayer1MancalaStone16,
                lblPlayer1MancalaStone17, lblPlayer1MancalaStone18, lblPlayer1MancalaStone19, lblPlayer1MancalaStone20,
                lblPlayer1MancalaStone21, lblPlayer1MancalaStone22, lblPlayer1MancalaStone23, lblPlayer1MancalaStone24,
                lblPlayer1MancalaStone25, lblPlayer1MancalaStone26, lblPlayer1MancalaStone27, lblPlayer1MancalaStone28,
                lblPlayer1MancalaStone29, lblPlayer1MancalaStone30, lblPlayer1MancalaStone31, lblPlayer1MancalaStone32,
                lblPlayer1MancalaStone33, lblPlayer1MancalaStone34, lblPlayer1MancalaStone35, lblPlayer1MancalaStone36,
                lblPlayer1MancalaStone37, lblPlayer1MancalaStone38, lblPlayer1MancalaStone39, lblPlayer1MancalaStone40,
                lblPlayer1MancalaStone41, lblPlayer1MancalaStone42, lblPlayer1MancalaStone43, lblPlayer1MancalaStone44);

            cups[12] = new Cup(startingStones, Enums.Player.Player1, false, btnPlayer1Cup0, lblPlayer1Cup0StoneText,
                lblPlayer1Cup0Stone0, lblPlayer1Cup0Stone1, lblPlayer1Cup0Stone2, lblPlayer1Cup0Stone3,
                lblPlayer1Cup0Stone4, lblPlayer1Cup0Stone5, lblPlayer1Cup0Stone6, lblPlayer1Cup0Stone7,
                lblPlayer1Cup0Stone8);

            cups[11] = new Cup(startingStones, Enums.Player.Player1, false, btnPlayer1Cup1, lblPlayer1Cup1StoneText,
                lblPlayer1Cup1Stone0, lblPlayer1Cup1Stone1, lblPlayer1Cup1Stone2, lblPlayer1Cup1Stone3,
                lblPlayer1Cup1Stone4, lblPlayer1Cup1Stone5, lblPlayer1Cup1Stone6, lblPlayer1Cup1Stone7,
                lblPlayer1Cup1Stone8);

            cups[10] = new Cup(startingStones, Enums.Player.Player1, false, btnPlayer1Cup2, lblPlayer1Cup2StoneText,
                lblPlayer1Cup2Stone0, lblPlayer1Cup2Stone1, lblPlayer1Cup2Stone2, lblPlayer1Cup2Stone3,
                lblPlayer1Cup2Stone4, lblPlayer1Cup2Stone5, lblPlayer1Cup2Stone6, lblPlayer1Cup2Stone7,
                lblPlayer1Cup2Stone8);

            cups[9] = new Cup(startingStones, Enums.Player.Player1, false, btnPlayer1Cup3, lblPlayer1Cup3StoneText,
                lblPlayer1Cup3Stone0, lblPlayer1Cup3Stone1, lblPlayer1Cup3Stone2, lblPlayer1Cup3Stone3,
                lblPlayer1Cup3Stone4, lblPlayer1Cup3Stone5, lblPlayer1Cup3Stone6, lblPlayer1Cup3Stone7,
                lblPlayer1Cup3Stone8);

            cups[8] = new Cup(startingStones, Enums.Player.Player1, false, btnPlayer1Cup4, lblPlayer1Cup4StoneText,
                lblPlayer1Cup4Stone0, lblPlayer1Cup4Stone1, lblPlayer1Cup4Stone2, lblPlayer1Cup4Stone3,
                lblPlayer1Cup4Stone4, lblPlayer1Cup4Stone5, lblPlayer1Cup4Stone6, lblPlayer1Cup4Stone7,
                lblPlayer1Cup4Stone8);

            cups[7] = new Cup(startingStones, Enums.Player.Player1, false, btnPlayer1Cup5, lblPlayer1Cup5StoneText,
                lblPlayer1Cup5Stone0, lblPlayer1Cup5Stone1, lblPlayer1Cup5Stone2, lblPlayer1Cup5Stone3,
                lblPlayer1Cup5Stone4, lblPlayer1Cup5Stone5, lblPlayer1Cup5Stone6, lblPlayer1Cup5Stone7,
                lblPlayer1Cup5Stone8);


            cups[6] = new Cup(0, Enums.Player.Player2, true, btnPlayer2Mancala,
                lblPlayer2MancalaStoneText, lblPlayer2MancalaStone0,
                lblPlayer2MancalaStone1, lblPlayer2MancalaStone2, lblPlayer2MancalaStone3, lblPlayer2MancalaStone4,
                lblPlayer2MancalaStone5, lblPlayer2MancalaStone6, lblPlayer2MancalaStone7, lblPlayer2MancalaStone8,
                lblPlayer2MancalaStone9, lblPlayer2MancalaStone10, lblPlayer2MancalaStone11, lblPlayer2MancalaStone12,
                lblPlayer2MancalaStone13, lblPlayer2MancalaStone14, lblPlayer2MancalaStone15, lblPlayer2MancalaStone16,
                lblPlayer2MancalaStone17, lblPlayer2MancalaStone18, lblPlayer2MancalaStone19, lblPlayer2MancalaStone20,
                lblPlayer2MancalaStone21, lblPlayer2MancalaStone22, lblPlayer2MancalaStone23, lblPlayer2MancalaStone24,
                lblPlayer2MancalaStone25, lblPlayer2MancalaStone26, lblPlayer2MancalaStone27, lblPlayer2MancalaStone28,
                lblPlayer2MancalaStone29, lblPlayer2MancalaStone30, lblPlayer2MancalaStone31, lblPlayer2MancalaStone32,
                lblPlayer2MancalaStone33, lblPlayer2MancalaStone34, lblPlayer2MancalaStone35, lblPlayer2MancalaStone36,
                lblPlayer2MancalaStone37, lblPlayer2MancalaStone38, lblPlayer2MancalaStone39, lblPlayer2MancalaStone40,
                lblPlayer2MancalaStone41, lblPlayer2MancalaStone42, lblPlayer2MancalaStone43, lblPlayer2MancalaStone44);

            cups[5] = new Cup(startingStones, Enums.Player.Player2, false, btnPlayer2Cup0, lblPlayer2Cup0StoneText,
                lblPlayer2Cup0Stone0, lblPlayer2Cup0Stone1, lblPlayer2Cup0Stone2, lblPlayer2Cup0Stone3,
                lblPlayer2Cup0Stone4, lblPlayer2Cup0Stone5, lblPlayer2Cup0Stone6, lblPlayer2Cup0Stone7,
                lblPlayer2Cup0Stone8);

            cups[4] = new Cup(startingStones, Enums.Player.Player2, false, btnPlayer2Cup1, lblPlayer2Cup1StoneText,
                lblPlayer2Cup1Stone0, lblPlayer2Cup1Stone1, lblPlayer2Cup1Stone2, lblPlayer2Cup1Stone3,
                lblPlayer2Cup1Stone4, lblPlayer2Cup1Stone5, lblPlayer2Cup1Stone6, lblPlayer2Cup1Stone7,
                lblPlayer2Cup1Stone8);

            cups[3] = new Cup(startingStones, Enums.Player.Player2, false, btnPlayer2Cup2, lblPlayer2Cup2StoneText,
                lblPlayer2Cup2Stone0, lblPlayer2Cup2Stone1, lblPlayer2Cup2Stone2, lblPlayer2Cup2Stone3,
                lblPlayer2Cup2Stone4, lblPlayer2Cup2Stone5, lblPlayer2Cup2Stone6, lblPlayer2Cup2Stone7,
                lblPlayer2Cup2Stone8);

            cups[2] = new Cup(startingStones, Enums.Player.Player2, false, btnPlayer2Cup3, lblPlayer2Cup3StoneText,
                lblPlayer2Cup3Stone0, lblPlayer2Cup3Stone1, lblPlayer2Cup3Stone2, lblPlayer2Cup3Stone3,
                lblPlayer2Cup3Stone4, lblPlayer2Cup3Stone5, lblPlayer2Cup3Stone6, lblPlayer2Cup3Stone7,
                lblPlayer2Cup3Stone8);

            cups[1] = new Cup(startingStones, Enums.Player.Player2, false, btnPlayer2Cup4, lblPlayer2Cup4StoneText,
                lblPlayer2Cup4Stone0, lblPlayer2Cup4Stone1, lblPlayer2Cup4Stone2, lblPlayer2Cup4Stone3,
                lblPlayer2Cup4Stone4, lblPlayer2Cup4Stone5, lblPlayer2Cup4Stone6, lblPlayer2Cup4Stone7,
                lblPlayer2Cup4Stone8);

            cups[0] = new Cup(startingStones, Enums.Player.Player2, false, btnPlayer2Cup5, lblPlayer2Cup5StoneText,
                lblPlayer2Cup5Stone0, lblPlayer2Cup5Stone1, lblPlayer2Cup5Stone2, lblPlayer2Cup5Stone3,
                lblPlayer2Cup5Stone4, lblPlayer2Cup5Stone5, lblPlayer2Cup5Stone6, lblPlayer2Cup5Stone7,
                lblPlayer2Cup5Stone8);
        }

        void DisableAll() => Array.ForEach(cups, cup => cup.IsEnabled = false);

        void EnableAndHighlightPlayer1Cups()
        {
            for (byte i = 7; i < 13; i++)
                cups[i].EnableAndHighlight();
        }

        void UnhighlightPlayer1Cups(bool exceptChosenCup)
        {
            for (byte i = 7; i < 13; i++)
            {
                if (exceptChosenCup && chosenCupIndex == i)
                    continue;

                cups[i].IsHighlighted = false;
            }
        }

        void EnableAndHighlightPlayer2Cups()
        {
            for (byte i = 0; i < 6; i++)
                cups[i].EnableAndHighlight();
        }

        void UnhighlightPlayer2Cups(bool exceptChosenCup = true)
        {
            for (byte i = 0; i < 6; i++)
            {
                if (exceptChosenCup && chosenCupIndex == i)
                    continue;

                cups[i].IsHighlighted = false;
            }
        }

        void UpdatePositionsOfAllStones() => Array.ForEach(cups, cup => cup.UpdateVisibleStonesToMatchNumberOfStones());

        void CupClick(object sender, MouseButtonEventArgs e)
        {
            DisableAll();
            gameCentrePageGame.DisableControls();

            if (chosenCup is null)
                FindClickedCup(sender);

            ShowChosenCupAndPrepareToSow();
        }

        void FindClickedCup(object sender)
        {
            if (!(sender is Label clicked))
            {
                MessageBox.Show(MessageResources.ClickedCupInvalidMessage, MessageResources.ErrorTitle);
                return;
            }

            for (byte cupNumber = 0; cupNumber < cups.Length; cupNumber++)
            {
                if (clicked == cups[cupNumber].CupControl)
                {
                    ChosenCup = cupNumber;
                    return;
                }
            }

            throw new Exception(MessageResources.ClickedCupInvalidMessage);
        }

        public void ChangeSize(BoardSize boardSize)
        {
            foreach (Cup c in cups)
            {
                c.CupControl.Width = boardSize.CupSizeAndMancalaWidth;
                if (c.IsMancala == false)
                    c.CupControl.Height = boardSize.CupSizeAndMancalaWidth;

                foreach (Label stone in c.StoneControls)
                {
                    stone.Width = boardSize.StoneSize;
                    stone.Height = boardSize.StoneSize;
                }
            }

            dockPanelExclMancalas.Width = boardSize.PlayerSideWidth;
            frameCentre.Margin = boardSize.CentreFrameMargin;
            btnPlayer1Mancala.Margin = boardSize.RightMargin;
            dockPanelExclMancalas.Margin = boardSize.RightMargin;
        }
    }
}