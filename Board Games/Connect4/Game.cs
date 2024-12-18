using Common.Extensions;
using Common.Utils;

namespace BoardGames.Connect4;

internal sealed class Game
{
    private const byte Rows = 6;
    private const byte Columns = 7;
    private const byte MaxMoves = Rows * Columns;
    private const byte BLTR = Rows + 1;
    private const byte TLBR = Rows - 1;

    private static readonly HashSet<ulong> WinningCombinations =
    [
        #region Vertical (21)

        (1UL << (0 + (Rows * 0))) | (1UL << (1 + (Rows * 0))) | (1UL << (2 + (Rows * 0))) | (1UL << (3 + (Rows * 0))),
        (1UL << (1 + (Rows * 0))) | (1UL << (2 + (Rows * 0))) | (1UL << (3 + (Rows * 0))) | (1UL << (4 + (Rows * 0))),
        (1UL << (2 + (Rows * 0))) | (1UL << (3 + (Rows * 0))) | (1UL << (4 + (Rows * 0))) | (1UL << (5 + (Rows * 0))),
        (1UL << (0 + (Rows * 1))) | (1UL << (1 + (Rows * 1))) | (1UL << (2 + (Rows * 1))) | (1UL << (3 + (Rows * 1))),
        (1UL << (1 + (Rows * 1))) | (1UL << (2 + (Rows * 1))) | (1UL << (3 + (Rows * 1))) | (1UL << (4 + (Rows * 1))),
        (1UL << (2 + (Rows * 1))) | (1UL << (3 + (Rows * 1))) | (1UL << (4 + (Rows * 1))) | (1UL << (5 + (Rows * 1))),
        (1UL << (0 + (Rows * 2))) | (1UL << (1 + (Rows * 2))) | (1UL << (2 + (Rows * 2))) | (1UL << (3 + (Rows * 2))),
        (1UL << (1 + (Rows * 2))) | (1UL << (2 + (Rows * 2))) | (1UL << (3 + (Rows * 2))) | (1UL << (4 + (Rows * 2))),
        (1UL << (2 + (Rows * 2))) | (1UL << (3 + (Rows * 2))) | (1UL << (4 + (Rows * 2))) | (1UL << (5 + (Rows * 2))),
        (1UL << (0 + (Rows * 3))) | (1UL << (1 + (Rows * 3))) | (1UL << (2 + (Rows * 3))) | (1UL << (3 + (Rows * 3))),
        (1UL << (1 + (Rows * 3))) | (1UL << (2 + (Rows * 3))) | (1UL << (3 + (Rows * 3))) | (1UL << (4 + (Rows * 3))),
        (1UL << (2 + (Rows * 3))) | (1UL << (3 + (Rows * 3))) | (1UL << (4 + (Rows * 3))) | (1UL << (5 + (Rows * 3))),
        (1UL << (0 + (Rows * 4))) | (1UL << (1 + (Rows * 4))) | (1UL << (2 + (Rows * 4))) | (1UL << (3 + (Rows * 4))),
        (1UL << (1 + (Rows * 4))) | (1UL << (2 + (Rows * 4))) | (1UL << (3 + (Rows * 4))) | (1UL << (4 + (Rows * 4))),
        (1UL << (2 + (Rows * 4))) | (1UL << (3 + (Rows * 4))) | (1UL << (4 + (Rows * 4))) | (1UL << (5 + (Rows * 4))),
        (1UL << (0 + (Rows * 5))) | (1UL << (1 + (Rows * 5))) | (1UL << (2 + (Rows * 5))) | (1UL << (3 + (Rows * 5))),
        (1UL << (1 + (Rows * 5))) | (1UL << (2 + (Rows * 5))) | (1UL << (3 + (Rows * 5))) | (1UL << (4 + (Rows * 5))),
        (1UL << (2 + (Rows * 5))) | (1UL << (3 + (Rows * 5))) | (1UL << (4 + (Rows * 5))) | (1UL << (5 + (Rows * 5))),
        (1UL << (0 + (Rows * 6))) | (1UL << (1 + (Rows * 6))) | (1UL << (2 + (Rows * 6))) | (1UL << (3 + (Rows * 6))),
        (1UL << (1 + (Rows * 6))) | (1UL << (2 + (Rows * 6))) | (1UL << (3 + (Rows * 6))) | (1UL << (4 + (Rows * 6))),
        (1UL << (2 + (Rows * 6))) | (1UL << (3 + (Rows * 6))) | (1UL << (4 + (Rows * 6))) | (1UL << (5 + (Rows * 6))),

        #endregion

        #region Horizontal (24)

        (1UL << (0 + (Rows * 0))) | (1UL << (0 + (Rows * 1))) | (1UL << (0 + (Rows * 2))) | (1UL << (0 + (Rows * 3))),
        (1UL << (0 + (Rows * 1))) | (1UL << (0 + (Rows * 2))) | (1UL << (0 + (Rows * 3))) | (1UL << (0 + (Rows * 4))),
        (1UL << (0 + (Rows * 2))) | (1UL << (0 + (Rows * 3))) | (1UL << (0 + (Rows * 4))) | (1UL << (0 + (Rows * 5))),
        (1UL << (0 + (Rows * 3))) | (1UL << (0 + (Rows * 4))) | (1UL << (0 + (Rows * 5))) | (1UL << (0 + (Rows * 6))),
        (1UL << (1 + (Rows * 0))) | (1UL << (1 + (Rows * 1))) | (1UL << (1 + (Rows * 2))) | (1UL << (1 + (Rows * 3))),
        (1UL << (1 + (Rows * 1))) | (1UL << (1 + (Rows * 2))) | (1UL << (1 + (Rows * 3))) | (1UL << (1 + (Rows * 4))),
        (1UL << (1 + (Rows * 2))) | (1UL << (1 + (Rows * 3))) | (1UL << (1 + (Rows * 4))) | (1UL << (1 + (Rows * 5))),
        (1UL << (1 + (Rows * 3))) | (1UL << (1 + (Rows * 4))) | (1UL << (1 + (Rows * 5))) | (1UL << (1 + (Rows * 6))),
        (1UL << (2 + (Rows * 0))) | (1UL << (2 + (Rows * 1))) | (1UL << (2 + (Rows * 2))) | (1UL << (2 + (Rows * 3))),
        (1UL << (2 + (Rows * 1))) | (1UL << (2 + (Rows * 2))) | (1UL << (2 + (Rows * 3))) | (1UL << (2 + (Rows * 4))),
        (1UL << (2 + (Rows * 2))) | (1UL << (2 + (Rows * 3))) | (1UL << (2 + (Rows * 4))) | (1UL << (2 + (Rows * 5))),
        (1UL << (2 + (Rows * 3))) | (1UL << (2 + (Rows * 4))) | (1UL << (2 + (Rows * 5))) | (1UL << (2 + (Rows * 6))),
        (1UL << (3 + (Rows * 0))) | (1UL << (3 + (Rows * 1))) | (1UL << (3 + (Rows * 2))) | (1UL << (3 + (Rows * 3))),
        (1UL << (3 + (Rows * 1))) | (1UL << (3 + (Rows * 2))) | (1UL << (3 + (Rows * 3))) | (1UL << (3 + (Rows * 4))),
        (1UL << (3 + (Rows * 2))) | (1UL << (3 + (Rows * 3))) | (1UL << (3 + (Rows * 4))) | (1UL << (3 + (Rows * 5))),
        (1UL << (3 + (Rows * 3))) | (1UL << (3 + (Rows * 4))) | (1UL << (3 + (Rows * 5))) | (1UL << (3 + (Rows * 6))),
        (1UL << (4 + (Rows * 0))) | (1UL << (4 + (Rows * 1))) | (1UL << (4 + (Rows * 2))) | (1UL << (4 + (Rows * 3))),
        (1UL << (4 + (Rows * 1))) | (1UL << (4 + (Rows * 2))) | (1UL << (4 + (Rows * 3))) | (1UL << (4 + (Rows * 4))),
        (1UL << (4 + (Rows * 2))) | (1UL << (4 + (Rows * 3))) | (1UL << (4 + (Rows * 4))) | (1UL << (4 + (Rows * 5))),
        (1UL << (4 + (Rows * 3))) | (1UL << (4 + (Rows * 4))) | (1UL << (4 + (Rows * 5))) | (1UL << (4 + (Rows * 6))),
        (1UL << (5 + (Rows * 0))) | (1UL << (5 + (Rows * 1))) | (1UL << (5 + (Rows * 2))) | (1UL << (5 + (Rows * 3))),
        (1UL << (5 + (Rows * 1))) | (1UL << (5 + (Rows * 2))) | (1UL << (5 + (Rows * 3))) | (1UL << (5 + (Rows * 4))),
        (1UL << (5 + (Rows * 2))) | (1UL << (5 + (Rows * 3))) | (1UL << (5 + (Rows * 4))) | (1UL << (5 + (Rows * 5))),
        (1UL << (5 + (Rows * 3))) | (1UL << (5 + (Rows * 4))) | (1UL << (5 + (Rows * 5))) | (1UL << (5 + (Rows * 6))),

        #endregion

        #region Bottom Left to Top Right (12)

        (1UL << ((Rows * 0) + 0 + (BLTR * 0))) | (1UL << ((Rows * 0) + 0 + (BLTR * 1))) | (1UL << ((Rows * 0) + 0 + (BLTR * 2))) | (1UL << ((Rows * 0) + 0 + (BLTR * 3))),
        (1UL << ((Rows * 0) + 0 + (BLTR * 1))) | (1UL << ((Rows * 0) + 0 + (BLTR * 2))) | (1UL << ((Rows * 0) + 0 + (BLTR * 3))) | (1UL << ((Rows * 0) + 0 + (BLTR * 4))),
        (1UL << ((Rows * 0) + 0 + (BLTR * 2))) | (1UL << ((Rows * 0) + 0 + (BLTR * 3))) | (1UL << ((Rows * 0) + 0 + (BLTR * 4))) | (1UL << ((Rows * 0) + 0 + (BLTR * 5))),
        (1UL << ((Rows * 0) + 1 + (BLTR * 0))) | (1UL << ((Rows * 0) + 1 + (BLTR * 1))) | (1UL << ((Rows * 0) + 1 + (BLTR * 2))) | (1UL << ((Rows * 0) + 1 + (BLTR * 3))),
        (1UL << ((Rows * 0) + 1 + (BLTR * 1))) | (1UL << ((Rows * 0) + 1 + (BLTR * 2))) | (1UL << ((Rows * 0) + 1 + (BLTR * 3))) | (1UL << ((Rows * 0) + 1 + (BLTR * 4))),
        (1UL << ((Rows * 0) + 2 + (BLTR * 0))) | (1UL << ((Rows * 0) + 2 + (BLTR * 1))) | (1UL << ((Rows * 0) + 2 + (BLTR * 2))) | (1UL << ((Rows * 0) + 2 + (BLTR * 3))),
        (1UL << ((Rows * 1) + 0 + (BLTR * 0))) | (1UL << ((Rows * 1) + 0 + (BLTR * 1))) | (1UL << ((Rows * 1) + 0 + (BLTR * 2))) | (1UL << ((Rows * 1) + 0 + (BLTR * 3))),
        (1UL << ((Rows * 1) + 0 + (BLTR * 1))) | (1UL << ((Rows * 1) + 0 + (BLTR * 2))) | (1UL << ((Rows * 1) + 0 + (BLTR * 3))) | (1UL << ((Rows * 1) + 0 + (BLTR * 4))),
        (1UL << ((Rows * 1) + 0 + (BLTR * 2))) | (1UL << ((Rows * 1) + 0 + (BLTR * 3))) | (1UL << ((Rows * 1) + 0 + (BLTR * 4))) | (1UL << ((Rows * 1) + 0 + (BLTR * 5))),
        (1UL << ((Rows * 2) + 0 + (BLTR * 0))) | (1UL << ((Rows * 2) + 0 + (BLTR * 1))) | (1UL << ((Rows * 2) + 0 + (BLTR * 2))) | (1UL << ((Rows * 2) + 0 + (BLTR * 3))),
        (1UL << ((Rows * 2) + 0 + (BLTR * 1))) | (1UL << ((Rows * 2) + 0 + (BLTR * 2))) | (1UL << ((Rows * 2) + 0 + (BLTR * 3))) | (1UL << ((Rows * 2) + 0 + (BLTR * 4))),
        (1UL << ((Rows * 3) + 0 + (BLTR * 0))) | (1UL << ((Rows * 3) + 0 + (BLTR * 1))) | (1UL << ((Rows * 3) + 0 + (BLTR * 2))) | (1UL << ((Rows * 3) + 0 + (BLTR * 3))),

        #endregion

        #region Top Left to Bottom Right (12)

        (1UL << ((Rows * 1) - 1 + (TLBR * 0))) | (1UL << ((Rows * 1) - 1 + (TLBR * 1))) | (1UL << ((Rows * 1) - 1 + (TLBR * 2))) | (1UL << ((Rows * 1) - 1 + (TLBR * 3))),
        (1UL << ((Rows * 1) - 1 + (TLBR * 1))) | (1UL << ((Rows * 1) - 1 + (TLBR * 2))) | (1UL << ((Rows * 1) - 1 + (TLBR * 3))) | (1UL << ((Rows * 1) - 1 + (TLBR * 4))),
        (1UL << ((Rows * 1) - 1 + (TLBR * 2))) | (1UL << ((Rows * 1) - 1 + (TLBR * 3))) | (1UL << ((Rows * 1) - 1 + (TLBR * 4))) | (1UL << ((Rows * 1) - 1 + (TLBR * 5))),
        (1UL << ((Rows * 1) - 2 + (TLBR * 0))) | (1UL << ((Rows * 1) - 2 + (TLBR * 1))) | (1UL << ((Rows * 1) - 2 + (TLBR * 2))) | (1UL << ((Rows * 1) - 2 + (TLBR * 3))),
        (1UL << ((Rows * 1) - 2 + (TLBR * 1))) | (1UL << ((Rows * 1) - 2 + (TLBR * 2))) | (1UL << ((Rows * 1) - 2 + (TLBR * 3))) | (1UL << ((Rows * 1) - 2 + (TLBR * 4))),
        (1UL << ((Rows * 1) - 3 + (TLBR * 0))) | (1UL << ((Rows * 1) - 3 + (TLBR * 1))) | (1UL << ((Rows * 1) - 3 + (TLBR * 2))) | (1UL << ((Rows * 1) - 3 + (TLBR * 3))),
        (1UL << ((Rows * 2) - 1 + (TLBR * 0))) | (1UL << ((Rows * 2) - 1 + (TLBR * 1))) | (1UL << ((Rows * 2) - 1 + (TLBR * 2))) | (1UL << ((Rows * 2) - 1 + (TLBR * 3))),
        (1UL << ((Rows * 2) - 1 + (TLBR * 1))) | (1UL << ((Rows * 2) - 1 + (TLBR * 2))) | (1UL << ((Rows * 2) - 1 + (TLBR * 3))) | (1UL << ((Rows * 2) - 1 + (TLBR * 4))),
        (1UL << ((Rows * 2) - 1 + (TLBR * 2))) | (1UL << ((Rows * 2) - 1 + (TLBR * 3))) | (1UL << ((Rows * 2) - 1 + (TLBR * 4))) | (1UL << ((Rows * 2) - 1 + (TLBR * 5))),
        (1UL << ((Rows * 3) - 1 + (TLBR * 0))) | (1UL << ((Rows * 3) - 1 + (TLBR * 1))) | (1UL << ((Rows * 3) - 1 + (TLBR * 2))) | (1UL << ((Rows * 3) - 1 + (TLBR * 3))),
        (1UL << ((Rows * 3) - 1 + (TLBR * 1))) | (1UL << ((Rows * 3) - 1 + (TLBR * 2))) | (1UL << ((Rows * 3) - 1 + (TLBR * 3))) | (1UL << ((Rows * 3) - 1 + (TLBR * 4))),
        (1UL << ((Rows * 4) - 1 + (TLBR * 0))) | (1UL << ((Rows * 4) - 1 + (TLBR * 1))) | (1UL << ((Rows * 4) - 1 + (TLBR * 2))) | (1UL << ((Rows * 4) - 1 + (TLBR * 3)))

        #endregion
    ];

    private static void Pause() => Thread.Sleep(500);

    private ulong _playerBoard;
    private ulong _computerBoard;
    private byte _moveCounter;

    private ulong Board => _playerBoard | _computerBoard;

    public void Init()
    {
        DisplayBoard();
        PlayGame();
    }

    private void DisplayBoard()
    {
        for (var col = 0; col < Columns; col++)
        {
            Console.Write((char)('A' + col) + " ");
        }

        Console.WriteLine();

        for (var row = Rows - 1; row >= 0; row--)
        {
            for (var col = 0; col < Columns; col++)
            {
                var index = row + (col * Rows);

                var bitmapPosition = 1UL << index;
                Console.Write(
                    (_playerBoard & bitmapPosition) != 0
                        ? "X "
                        : (_computerBoard & bitmapPosition) != 0
                            ? "O "
                            : ". "
                );
            }

            Console.WriteLine();
        }
    }

    private void PlayGame()
    {
        while (true)
        {
            PlayWhiteTurn();
            DisplayBoard();

            if (CheckGameEnd())
            {
                break;
            }

            PlayBlackTurn();
            DisplayBoard();

            if (CheckGameEnd())
            {
                break;
            }
        }
    }

    private bool CheckGameEnd()
    {
        foreach (var combination in WinningCombinations)
        {
            if ((_playerBoard & combination) == combination)
            {
                ConsoleUtils.HighlightConsoleLine("You have won!", ConsoleColor.Green);
                return true;
            }

            if ((_computerBoard & combination) == combination)
            {
                ConsoleUtils.HighlightConsoleLine("Computer has won!", ConsoleColor.Red);
                return true;
            }
        }

        if (_moveCounter == MaxMoves)
        {
            ConsoleUtils.HighlightConsoleLine("It's a draw!", ConsoleColor.Yellow);
            return true;
        }

        return false;
    }

    private void PlayWhiteTurn()
    {
        while (true)
        {
            ConsoleUtils.HighlightConsoleLine("[TURN]: Select a column (A - G)", ConsoleColor.Magenta);

            if (Console.ReadKey().KeyChar is not (>= 'A' and <= 'H' and var column))
            {
                ConsoleUtils.HighlightConsoleLine("[ERROR]: Invalid format. Please try again.", ConsoleColor.Red);
                Pause();
                continue;
            }

            if (IsValidMove((byte)(column - 'A'), out var bitmapPosition))
            {
                _playerBoard |= bitmapPosition;
                _moveCounter++;
                break;
            }

            ConsoleUtils.HighlightConsoleLine("[ERROR]: Invalid move. Please try again.", ConsoleColor.Red);
            Pause();
        }
    }

    private void PlayBlackTurn()
    {
        ConsoleUtils.HighlightConsoleLine("[TURN]: Black's turn (Computer)", ConsoleColor.Cyan);
        Pause();

        List<ulong> moves = [];

        for (byte i = 0; i < Columns; i++)
        {
            if (IsValidMove(i, out var bitmapPosition))
            {
                moves.Add(bitmapPosition);
            }
        }

        _computerBoard |= moves.GetRandomElement();
        _moveCounter++;
    }

    private bool IsValidMove(byte columnIndex, out ulong bitmapPosition)
    {
        bitmapPosition = 1UL << (columnIndex * Rows);
        for (byte i = 0; i < Rows; i++)
        {
            if ((Board & bitmapPosition) == 0)
            {
                return true;
            }

            bitmapPosition <<= 1;
        }

        return false;
    }
}
