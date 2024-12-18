using Common.Extensions;
using Common.Utils;

namespace BoardGames.TicTacToe;

internal sealed class Game : BaseBoardGame
{
    private const int GridSize = 3;
    private const int MaxMoves = GridSize * GridSize;
    private const int GridSizeIndex = GridSize - 1;
    private const int MaxMoveIndex = MaxMoves - 1;

    private static readonly int[] WinningCombinations =
    [
        0x_7,
        0x_38,
        0x_1C0,
        0x_49,
        0x_92,
        0x_124,
        0x_111,
        0x_54
    ];

    private int _playerBoard;
    private int _computerBoard;
    private int _moveCounter;

    private int TakenMoves => _playerBoard | _computerBoard;

    protected override void PlayTurn()
    {
        var moveModulus = (_moveCounter + 1) % 2;
        var team = (Team)moveModulus;

        var isPlayerTurn = team == Team.Crosses;

        if (isPlayerTurn)
        {
            ConsoleUtils.HighlightConsoleLine("Board:", ConsoleColor.Yellow);

            ShowBoard();

            ConsoleUtils.HighlightConsoleLine($"[TURN]: Pick an available square: (0 - {MaxMoveIndex})", ConsoleColor.Magenta);

            while (true)
            {
                var isValidInput = Int32.TryParse(Console.ReadLine(), out var moveIndex);
                var isInRange = moveIndex is >= 0 and <= MaxMoveIndex;

                var bitmask = 1 << moveIndex;
                var isPositionFree = (TakenMoves & bitmask) == 0;

                if (isValidInput && isInRange && isPositionFree)
                {
                    _playerBoard |= bitmask;

                    break;
                }

                ConsoleUtils.HighlightConsoleLine("[ERROR]: Invalid input. Please try again.", ConsoleColor.Red);
            }
        }
        else
        {
            ConsoleUtils.HighlightConsoleLine("[TURN]: Computer", ConsoleColor.Magenta);

            List<int> bitmasks = [];

            for (var i = 0; i < MaxMoves; i++)
            {
                var bitmask = 1 << i;
                var isPositionFree = (TakenMoves & bitmask) == 0;

                if (isPositionFree)
                {
                    bitmasks.Add(bitmask);
                }
            }

            _computerBoard |= bitmasks.GetRandomElement();
        }

        _moveCounter++;

        var isPossibleToWin = _moveCounter >= GridSize;

        if (isPossibleToWin)
        {
            foreach (var combination in WinningCombinations)
            {
                var hasPlayerWon = (_playerBoard & combination) == combination;

                if (hasPlayerWon)
                {
                    ShowBoard();
                    EndRound(RoundOutcome.Win);

                    return;
                }

                var hasComputerWon = (_computerBoard & combination) == combination;

                if (hasComputerWon)
                {
                    ShowBoard();
                    EndRound(RoundOutcome.Lose);

                    return;
                }
            }
        }

        if (_moveCounter == MaxMoves)
        {
            EndRound(RoundOutcome.Tie);
        }

        ShowBoard();
    }

    protected override void PrepareNextRound()
    {
        _moveCounter = 0;
        _playerBoard = 0x_0;
        _computerBoard = 0x_0;
    }

    private void ShowBoard()
    {
        Console.WriteLine();

        for (var row = 0; row < GridSize; row++)
        {
            for (var column = 0; column < GridSize; column++)
            {
                var position = (row * GridSize) + column;
                var bitmask = 1 << position;

                var symbol = (_playerBoard & bitmask) != 0
                    ? 'X'
                    : (_computerBoard & bitmask) != 0
                        ? 'O'
                        : ' ';

                Console.Write($" {symbol} ");

                if (column < GridSizeIndex)
                {
                    Console.Write("|");
                }
            }

            Console.WriteLine();

            if (row < GridSizeIndex)
            {
                Console.WriteLine("---+---+---");
            }
        }

        Console.WriteLine();
    }
}
