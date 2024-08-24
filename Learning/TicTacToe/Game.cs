namespace Learning.TicTacToe;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
    : GameBase(requiredWins, enableDeuce)
{
    private const int GridSize = 3;
    private const int MaxMoves = GridSize * GridSize;
    private const int GridSizeIndex = GridSize - 1;
    private const int MaxMoveIndex = MaxMoves - 1;

    private static readonly int[] WinningCombinations =
    [
        0b000000111,
        0b000111000,
        0b111000000,
        0b001001001,
        0b010010010,
        0b100100100,
        0b100010001,
        0b001010100,
    ];

    private int _playerBoard = 0b0;
    private int _computerBoard = 0b0;
    private int _moveCounter = 0;

    private int TakenMoves => _playerBoard | _computerBoard;

    protected override void StartRound(int roundCounter)
    {
        ResetRoundData();

        ConsoleUtils.HighlightConsoleLine($"--- [ROUND {roundCounter}] ---", ConsoleColor.Cyan);
    }

    protected override void PlayRound()
    {
        int moveModulus = (_moveCounter + 1) % 2;
        var team = (Team)moveModulus;
        bool isPlayerTurn = team == Team.Crosses;

        if (isPlayerTurn)
        {
            ConsoleUtils.HighlightConsoleLine("Board:", ConsoleColor.Yellow);

            ShowBoard();

            ConsoleUtils.HighlightConsoleLine(
                $"[TURN]: Pick an available square: (0 - {MaxMoveIndex})",
                ConsoleColor.Magenta
            );

            while (true)
            {
                var isValidInput = int.TryParse(Console.ReadLine(), out int moveIndex);
                var isInRange = moveIndex >= 0 && moveIndex <= MaxMoveIndex;

                var bitmask = 1 << moveIndex;
                var isPositionFree = (TakenMoves & bitmask) == 0;

                if (isValidInput && isInRange && isPositionFree)
                {
                    _playerBoard |= bitmask;

                    break;
                }

                ConsoleUtils.HighlightConsoleLine(
                    "[ERROR]: Invalid input. Please try again.",
                    ConsoleColor.Red
                );
            }
        }
        else
        {
            ConsoleUtils.HighlightConsoleLine("[TURN]: Computer", ConsoleColor.Magenta);

            for (int i = 0; i < MaxMoves; i++)
            {
                int bitmask = 1 << i;

                var isPositionFree = (TakenMoves & bitmask) != 0;

                if (isPositionFree)
                {
                    _computerBoard |= bitmask;

                    break;
                }
            }
        }

        _moveCounter++;

        var isPossibleToWin = _moveCounter >= GridSize;

        if (isPossibleToWin)
        {
            foreach (int combination in WinningCombinations)
            {
                var hasPlayerWon = (_playerBoard & combination) == combination;

                if (hasPlayerWon)
                {
                    PlayerScore++;

                    ConsoleUtils.HighlightConsoleLine("You win this round!", ConsoleColor.Green);

                    ResetRoundData();

                    return;
                }

                var hasComputerWon = (_computerBoard & combination) == combination;

                if (hasComputerWon)
                {
                    ComputerScore++;

                    ConsoleUtils.HighlightConsoleLine(
                        "Computer wins this round!",
                        ConsoleColor.Red
                    );

                    ResetRoundData();

                    return;
                }
            }
        }

        if (_moveCounter == MaxMoves)
        {
            ResetRoundData();

            ConsoleUtils.HighlightConsoleLine("This round is a tie!", ConsoleColor.Yellow);
        }

        ShowBoard();
    }

    private void ShowBoard()
    {
        for (int row = 0; row < GridSize; row++)
        {
            for (int column = 0; column < GridSize; column++)
            {
                var position = (row * GridSize) + column;
                var bitmask = 1 << position;

                var symbol =
                    (_playerBoard & bitmask) != 0 ? 'X'
                    : (_computerBoard & bitmask) != 0 ? 'O'
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

    private void ResetRoundData()
    {
        _moveCounter = 0;
        _playerBoard = 0b0;
        _computerBoard = 0b0;
    }
}
