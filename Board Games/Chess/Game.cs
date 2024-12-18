using Common.Utils;

namespace BoardGames.Chess;

internal sealed class Game
{
    private const byte MinRowIndex = 0;
    private const byte MaxRowIndex = 7;
    private const byte MaxColumnIndex = 63;
    private const byte MinColumnIndex = MaxColumnIndex - MaxRowIndex;
    private const byte Up = 1;
    private const byte Right = 8;

    private static readonly PromotionType[] PromotionTypes = Enum.GetValues<PromotionType>();
    private static readonly Random Random = new();

    private static bool TryGetValidBoardPosition(string prompt, out byte columnIndex, out byte rowIndex)
    {
        columnIndex = 0;
        rowIndex = 0;

        ConsoleUtils.HighlightConsoleLine(prompt, ConsoleColor.Magenta);

        var input = Console.ReadLine()?.Trim();
        if (input?.Length is not 2)
        {
            ConsoleUtils.HighlightConsoleLine("[ERROR]: Invalid format. Please try again.", ConsoleColor.Red);
            Pause();
            return false;
        }

        var column = input[0];
        var row = input[1];
        if (column is < 'A' or > 'H' || row is < '1' or > '8')
        {
            ConsoleUtils.HighlightConsoleLine("[ERROR]: Invalid position. Please try again.", ConsoleColor.Red);
            Pause();
            return false;
        }

        columnIndex = (byte)(column - 'A');
        rowIndex = (byte)(row - '1');
        return true;
    }

    private static sbyte Flatten2DIndex(byte column, byte row) => (sbyte)((column * 8) + row);

    private static (byte, byte) To2DArrayIndex(byte index)
    {
        var column = (byte)(index / 8);
        var row = (byte)(index % 8);
        return (column, row);
    }

    private static void Pause() => Thread.Sleep(500);

    private readonly PositionType[] _board = new PositionType[64];
    private bool _isWhiteTurn = true;
    private bool _isCheck;

    private bool _hasWhiteKingMoved;
    private bool _hasLeftWhiteRookMoved;
    private bool _hasRightWhiteRookMoved;

    private bool _hasBlackKingMoved;
    private bool _hasLeftBlackRookMoved;
    private bool _hasRightBlackRookMoved;

    private byte _currentPieceRowIndex;
    private byte _currentPieceColumnIndex;

    private byte CurrentPieceBoardIndex => (byte)((_currentPieceColumnIndex * 8) + _currentPieceRowIndex);

    [Flags]
    private enum PositionType : byte
    {
        Empty = 0,
        White = 1 << 0,
        Black = 1 << 1,
        Pawn = 1 << 2,
        Knight = 1 << 3,
        Bishop = 1 << 4,
        Rook = 1 << 5,
        Queen = 1 << 6,
        King = 1 << 7
    }

    private enum PromotionType : byte
    {
        Knight,
        Bishop,
        Rook,
        Queen
    }

    public void Init()
    {
        for (var i = Up; i <= Up + (Right * 7); i += Right)
        {
            _board[i] = PositionType.White | PositionType.Pawn;
            _board[i + 5] = PositionType.Black | PositionType.Pawn;
        }

        _board[MinRowIndex + (Right * 0)] = _board[MinColumnIndex - (Right * 0)] = PositionType.White | PositionType.Rook;
        _board[MinRowIndex + (Right * 1)] = _board[MinColumnIndex - (Right * 1)] = PositionType.White | PositionType.Knight;
        _board[MinRowIndex + (Right * 2)] = _board[MinColumnIndex - (Right * 2)] = PositionType.White | PositionType.Bishop;
        _board[MinRowIndex + (Right * 3)] = PositionType.White | PositionType.Queen;
        _board[MinRowIndex + (Right * 4)] = PositionType.White | PositionType.King;

        _board[MaxRowIndex + (Right * 0)] = _board[MaxColumnIndex - (Right * 0)] = PositionType.Black | PositionType.Rook;
        _board[MaxRowIndex + (Right * 1)] = _board[MaxColumnIndex - (Right * 1)] = PositionType.Black | PositionType.Knight;
        _board[MaxRowIndex + (Right * 2)] = _board[MaxColumnIndex - (Right * 2)] = PositionType.Black | PositionType.Bishop;
        _board[MaxRowIndex + (Right * 3)] = PositionType.Black | PositionType.Queen;
        _board[MaxRowIndex + (Right * 4)] = PositionType.Black | PositionType.King;

        DisplayBoard();
        PlayGame();
    }

    private void DisplayBoard()
    {
        Console.WriteLine("  A B C D E F G H");

        for (var row = MaxRowIndex; row <= MaxRowIndex; row--)
        {
            Console.Write(row + 1 + " ");

            for (byte column = 0; column < 8; column++)
            {
                PositionType piece = _board[Flatten2DIndex(column, row)];
                if ((piece & PositionType.White) != 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if ((piece & PositionType.Black) != 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.Write(
                    piece switch
                    {
                        PositionType.Empty => '.',
                        PositionType.Pawn | PositionType.White => '♙',
                        PositionType.Pawn | PositionType.Black => '♟',
                        PositionType.Knight | PositionType.White => '♘',
                        PositionType.Knight | PositionType.Black => '♞',
                        PositionType.Bishop | PositionType.White => '♗',
                        PositionType.Bishop | PositionType.Black => '♝',
                        PositionType.Rook | PositionType.White => '♖',
                        PositionType.Rook | PositionType.Black => '♜',
                        PositionType.Queen | PositionType.White => '♕',
                        PositionType.Queen | PositionType.Black => '♛',
                        PositionType.King | PositionType.White => '♔',
                        PositionType.King | PositionType.Black => '♚',
                        _ => '?'
                    }
                  + " "
                );
                Console.ResetColor();
            }

            Console.WriteLine(row + 1);
        }

        Console.WriteLine("  A B C D E F G H");
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

    private void PlayBlackTurn()
    {
        ConsoleUtils.HighlightConsoleLine("[TURN]: Black's turn (Computer)", ConsoleColor.Cyan);
        Pause();

        List<byte> blackPieces = GetAvailablePieces(false);

        if (blackPieces.Count == 0)
        {
            ConsoleUtils.HighlightConsoleLine("[ERROR]: No valid moves for Black!", ConsoleColor.Red);
            Pause();
            return;
        }

        var moveMade = false;

        while (!moveMade)
        {
            var pieceIndex = blackPieces[Random.Next(blackPieces.Count)];
            var (column, row) = To2DArrayIndex(pieceIndex);

            _currentPieceColumnIndex = column;
            _currentPieceRowIndex = row;

            for (byte targetColumn = 0; targetColumn < 8; targetColumn++)
            {
                for (byte targetRow = 0; targetRow < 8; targetRow++)
                {
                    if (TrySetNewMovePieceIndex(targetColumn, targetRow, out PositionType? piece))
                    {
                        moveMade = true;
                        ConsoleUtils.HighlightConsoleLine(
                            $"[MOVE]: Black moved {piece} from {(char)('A' + column)}{row + 1} to {(char)('A' + targetColumn)}{targetRow + 1}",
                            ConsoleColor.Green
                        );

                        Pause();
                        break;
                    }
                }

                if (moveMade)
                {
                    break;
                }
            }

            if (!moveMade)
            {
                blackPieces.Remove(pieceIndex);
            }

            if (blackPieces.Count == 0)
            {
                break;
            }
        }
    }

    private List<byte> GetAvailablePieces(bool isWhite)
    {
        var availablePieces = new List<byte>();

        for (byte i = 0; i < _board.Length; i++)
        {
            PositionType piece = _board[i];
            PositionType color = isWhite ? PositionType.White : PositionType.Black;

            if ((piece & color) == color)
            {
                availablePieces.Add(i);
            }
        }

        return availablePieces;
    }

    private bool CheckGameEnd()
    {
        List<byte> currentPlayerPieces = GetAvailablePieces(_isWhiteTurn);

        if (currentPlayerPieces.Count == 0)
        {
            if (_isCheck)
            {
                ConsoleUtils.HighlightConsoleLine(_isWhiteTurn ? "Checkmate! Black wins!" : "Checkmate! White wins!", ConsoleColor.Red);
            }
            else
            {
                ConsoleUtils.HighlightConsoleLine("Stalemate! The game is a draw.", ConsoleColor.Yellow);
            }

            Pause();

            return true;
        }

        return false;
    }

    private void PlayWhiteTurn()
    {
        while (true)
        {
            if (TryGetValidBoardPosition("[TURN]: Enter piece to move: (A1 - H8)", out var columnIndex, out var rowIndex))
            {
                if (TrySetActivePiece(columnIndex, rowIndex))
                {
                    break;
                }

                ConsoleUtils.HighlightConsoleLine("[ERROR]: You must select a piece that belongs to you. Please try again.", ConsoleColor.Red);
                Pause();
            }
        }

        while (true)
        {
            if (TryGetValidBoardPosition("[TURN]: Enter destination for piece (A1 - H8)", out var columnIndex, out var rowIndex))
            {
                if (TrySetNewMovePieceIndex(columnIndex, rowIndex, out _))
                {
                    break;
                }

                ConsoleUtils.HighlightConsoleLine("[ERROR]: Illegal move. Please try again.", ConsoleColor.Red);
                Pause();
            }
        }
    }

    private bool IsPathBlocked(byte startColumn, byte startRow, byte targetColumn, byte targetRow)
    {
        var startIndex = Flatten2DIndex(startColumn, startRow);
        PositionType currentPiece = _board[startIndex];

        if ((currentPiece & PositionType.Knight) == PositionType.Knight)
        {
            return false;
        }

        var columnStep = (sbyte)Math.Sign(targetColumn - startColumn);
        var rowStep = (sbyte)Math.Sign(targetRow - startRow);

        var currentColumn = (sbyte)(startColumn + columnStep);
        var currentRow = (sbyte)(startRow + rowStep);

        while (currentColumn != targetColumn || currentRow != targetRow)
        {
            var currentIndex = Flatten2DIndex((byte)currentColumn, (byte)currentRow);

            if (currentIndex is 0 || currentIndex >= _board.Length)
            {
                return false;
            }

            if (_board[currentIndex] != PositionType.Empty)
            {
                return true;
            }

            currentColumn += columnStep;
            currentRow += rowStep;

            if (currentColumn < 0 || currentRow < 0)
            {
                return false;
            }
        }

        return false;
    }

    private bool IsInCheck(bool forWhite)
    {
        var kingIndex = (byte)Array.IndexOf(_board, forWhite ? PositionType.White | PositionType.King : PositionType.Black | PositionType.King);

        var (kingColumn, kingRow) = To2DArrayIndex(kingIndex);

        for (byte i = 0; i < _board.Length; i++)
        {
            PositionType piece = _board[i];

            if ((piece & (forWhite ? PositionType.Black : PositionType.White)) == 0)
            {
                continue;
            }

            var (pieceColumn, pieceRow) = To2DArrayIndex(i);

            if (CanPieceAttackKing(piece, pieceColumn, pieceRow, kingColumn, kingRow))
            {
                return true;
            }
        }

        return false;
    }

    private bool CanPieceAttackKing(PositionType piece, byte pieceColumn, byte pieceRow, byte kingColumn, byte kingRow)
    {
        (sbyte Y, sbyte X) delta = ((sbyte)(kingRow - pieceRow), (sbyte)(kingColumn - pieceColumn));

        if ((piece & PositionType.Black) == PositionType.Black)
        {
            delta.Y *= -1;
        }

        if ((piece & PositionType.Pawn) == PositionType.Pawn)
        {
            return delta is (1, 1 or -1);
        }

        if ((piece & PositionType.Knight) == PositionType.Knight)
        {
            return delta is (2 or -2, 1 or -1) or (1 or -1, 2 or -2);
        }

        if ((piece & (PositionType.Bishop | PositionType.Queen)) != 0)
        {
            if (Math.Abs(delta.Y) == Math.Abs(delta.X) && !IsPathBlocked(pieceColumn, pieceRow, kingColumn, kingRow))
            {
                return true;
            }
        }

        if ((piece & (PositionType.Rook | PositionType.Queen)) != 0)
        {
            if ((delta.X == 0 || delta.Y == 0) && !IsPathBlocked(pieceColumn, pieceRow, kingColumn, kingRow))
            {
                return true;
            }
        }

        return false;
    }

    private bool TrySetActivePiece(byte columnIndex, byte rowIndex)
    {
        var targetIndex = Flatten2DIndex(columnIndex, rowIndex);

        PositionType positionType = _isWhiteTurn ? PositionType.White : PositionType.Black;

        if ((_board[targetIndex] & positionType) == positionType)
        {
            _currentPieceColumnIndex = columnIndex;
            _currentPieceRowIndex = rowIndex;
            return true;
        }

        return false;
    }

    private bool TrySetNewMovePieceIndex(byte targetColumnIndex, byte targetRowIndex, out PositionType? piece)
    {
        piece = null;

        var targetIndex = Flatten2DIndex(targetColumnIndex, targetRowIndex);

        if (targetIndex == CurrentPieceBoardIndex)
        {
            return false;
        }

        if (IsPathBlocked(_currentPieceColumnIndex, _currentPieceRowIndex, targetColumnIndex, targetRowIndex))
        {
            return false;
        }

        PositionType targetPiece = _board[targetIndex];

        if ((targetPiece & PositionType.King) == PositionType.King)
        {
            return false;
        }

        (sbyte Y, sbyte X) delta = ((sbyte)(targetRowIndex - _currentPieceRowIndex), (sbyte)(targetColumnIndex - _currentPieceColumnIndex));

        bool isCapture;
        if (_isWhiteTurn)
        {
            if ((targetPiece & PositionType.White) == PositionType.White)
            {
                return false;
            }

            isCapture = (targetPiece & PositionType.Black) == PositionType.Black;
        }
        else
        {
            if ((targetPiece & PositionType.Black) == PositionType.Black)
            {
                return false;
            }

            isCapture = (targetPiece & PositionType.White) == PositionType.White;
            delta.Y *= -1;
        }

        PositionType currentPiece = _board[CurrentPieceBoardIndex];
        piece = currentPiece & ~ (PositionType.Black | PositionType.White);

        if ((currentPiece & PositionType.Knight) == PositionType.Knight)
        {
            return delta is (2 or -2, 1 or -1) or (1 or -1, 2 or -2) && WouldMoveResolveCheck(targetColumnIndex, targetRowIndex) && MovePiece(targetColumnIndex, targetRowIndex);
        }

        if ((currentPiece & PositionType.Pawn) == PositionType.Pawn)
        {
            var checkPromotion = false;

            var isMovingDiagonally = delta is (1, 1) or (1, -1);

            if (isCapture)
            {
                if (!isMovingDiagonally || !WouldMoveResolveCheck(targetColumnIndex, targetRowIndex) || !MovePiece(targetColumnIndex, targetRowIndex))
                {
                    return false;
                }

                checkPromotion = true;
            }

            var isFirstMove = _isWhiteTurn ? _currentPieceRowIndex == 1 : _currentPieceRowIndex == 6;
            var isMovingUp = delta.X is 0 && (isFirstMove ? delta.Y is 1 or 2 : delta.Y is 1);

            if (isMovingUp)
            {
                if (!WouldMoveResolveCheck(targetColumnIndex, targetRowIndex) || !MovePiece(targetColumnIndex, targetRowIndex))
                {
                    return false;
                }

                checkPromotion = true;
            }

            if (checkPromotion)
            {
                if (_isWhiteTurn ? _currentPieceRowIndex == MaxRowIndex : _currentPieceRowIndex == MinRowIndex)
                {
                    PositionType promotionPositionType = ConsoleUtils.GetEnumChoice(PromotionTypes, "Promotion") switch
                    {
                        PromotionType.Knight => PositionType.Knight,
                        PromotionType.Bishop => PositionType.Bishop,
                        PromotionType.Rook => PositionType.Rook,
                        PromotionType.Queen => PositionType.Queen,
                        _ => PositionType.Empty
                    };

                    promotionPositionType |= _isWhiteTurn ? PositionType.White : PositionType.Black;

                    _board[CurrentPieceBoardIndex] = promotionPositionType;
                }

                return true;
            }

            if (!isMovingDiagonally)
            {
                return false;
            }

            PositionType targetPawn;
            int upIndex;

            if (_isWhiteTurn)
            {
                targetPawn = PositionType.Black | PositionType.Pawn;
                upIndex = CurrentPieceBoardIndex + Up;
            }
            else
            {
                targetPawn = PositionType.White | PositionType.Pawn;
                upIndex = CurrentPieceBoardIndex - Up;
            }

            if (upIndex is < 0 or > MaxColumnIndex || _board[upIndex] != targetPiece)
            {
                return false;
            }

            var rightIndex = CurrentPieceBoardIndex + Right;
            var leftIndex = CurrentPieceBoardIndex - Right;

            if (rightIndex is > 0 and <= MaxColumnIndex
             && _board[rightIndex] == targetPawn
             && WouldMoveResolveCheck(targetColumnIndex, targetRowIndex)
             && MovePiece(targetColumnIndex, targetRowIndex))
            {
                _board[upIndex] = _board[rightIndex] = PositionType.Empty;
                return true;
            }

            if (leftIndex is > 0 and <= MaxColumnIndex
             && _board[leftIndex] == targetPawn
             && WouldMoveResolveCheck(targetColumnIndex, targetRowIndex)
             && MovePiece(targetColumnIndex, targetRowIndex))
            {
                _board[upIndex] = _board[leftIndex] = PositionType.Empty;
                return true;
            }

            return false;
        }

        if ((currentPiece & PositionType.Bishop) == PositionType.Bishop)
        {
            return Math.Abs(delta.Y) == Math.Abs(delta.X) && WouldMoveResolveCheck(targetColumnIndex, targetRowIndex) && MovePiece(targetColumnIndex, targetRowIndex);
        }

        if ((currentPiece & PositionType.Rook) == PositionType.Rook)
        {
            return (delta.X == 0 || delta.Y == 0) && WouldMoveResolveCheck(targetColumnIndex, targetRowIndex) && MovePiece(targetColumnIndex, targetRowIndex);
        }

        if ((currentPiece & PositionType.Queen) == PositionType.Queen)
        {
            return (Math.Abs(delta.Y) == Math.Abs(delta.X) || delta.X == 0 || delta.Y == 0)
                && WouldMoveResolveCheck(targetColumnIndex, targetRowIndex)
                && MovePiece(targetColumnIndex, targetRowIndex);
        }

        if ((currentPiece & PositionType.King) == PositionType.King)
        {
            var isValidMove = Math.Abs(delta.X) <= 1 && Math.Abs(delta.Y) <= 1;
            var isValidCastle = false;

            if (delta.X == 0 && Math.Abs(delta.Y) == 2)
            {
                isValidCastle = TryCastle(targetColumnIndex, targetRowIndex);
            }

            return (isValidMove || isValidCastle) && WouldMoveResolveCheck(targetColumnIndex, targetRowIndex) && MovePiece(targetColumnIndex, targetRowIndex);
        }

        return false;
    }

    private bool TryCastle(byte targetColumnIndex, byte targetRowIndex)
    {
        var isKingSide = targetColumnIndex > _currentPieceColumnIndex;
        var isWhiteCastling = _isWhiteTurn;

        if (isWhiteCastling)
        {
            if (_hasWhiteKingMoved || (isKingSide && _hasRightWhiteRookMoved) || (!isKingSide && _hasLeftWhiteRookMoved))
            {
                return false;
            }
        }
        else
        {
            if (_hasBlackKingMoved || (isKingSide && _hasRightBlackRookMoved) || (!isKingSide && _hasLeftBlackRookMoved))
            {
                return false;
            }
        }

        var rookColumnIndex = isKingSide ? MaxRowIndex : MinRowIndex;
        var rookIndex = Flatten2DIndex(rookColumnIndex, _currentPieceRowIndex);

        if (_board[rookIndex] == PositionType.Empty)
        {
            return false;
        }

        var startCheck = Math.Min(_currentPieceColumnIndex, targetColumnIndex);
        var endCheck = Math.Max(_currentPieceColumnIndex, targetColumnIndex);

        for (var col = (byte)(startCheck + 1); col < endCheck; col++)
        {
            if (_board[Flatten2DIndex(col, _currentPieceRowIndex)] != PositionType.Empty)
            {
                return false;
            }
        }

        if (IsInCheck(_isWhiteTurn)
         || WouldBeInCheckAfterMove(_currentPieceColumnIndex, _currentPieceRowIndex, targetColumnIndex, targetRowIndex)
         || WouldBeInCheckAfterMove(
                _currentPieceColumnIndex,
                _currentPieceRowIndex,
                isKingSide ? (byte)(_currentPieceColumnIndex + 1) : (byte)(_currentPieceColumnIndex - 1),
                _currentPieceRowIndex
            ))
        {
            return false;
        }

        var newRookColumnIndex = isKingSide ? (byte)(targetColumnIndex - 1) : (byte)(targetColumnIndex + 1);
        _board[Flatten2DIndex(newRookColumnIndex, _currentPieceRowIndex)] = _board[rookIndex];
        _board[rookIndex] = PositionType.Empty;

        return true;
    }

    private bool WouldBeInCheckAfterMove(byte currentColumn, byte currentRow, byte targetColumn, byte targetRow)
    {
        var currentIndex = Flatten2DIndex(currentColumn, currentRow);
        var targetIndex = Flatten2DIndex(targetColumn, targetRow);
        PositionType movingPiece = _board[currentIndex];
        PositionType targetPiece = _board[targetIndex];

        _board[currentIndex] = PositionType.Empty;
        _board[targetIndex] = movingPiece;

        var wouldBeInCheck = IsInCheck(_isWhiteTurn);

        _board[currentIndex] = movingPiece;
        _board[targetIndex] = targetPiece;

        return wouldBeInCheck;
    }

    private bool WouldMoveResolveCheck(byte targetColumnIndex, byte targetRowIndex)
        => !_isCheck || !WouldBeInCheckAfterMove(_currentPieceColumnIndex, _currentPieceRowIndex, targetColumnIndex, targetRowIndex);

    private bool MovePiece(byte targetColumnIndex, byte targetRowIndex)
    {
        var targetIndex = Flatten2DIndex(targetColumnIndex, targetRowIndex);
        var sourceIndex = CurrentPieceBoardIndex;

        PositionType currentPiece = _board[sourceIndex];
        if ((currentPiece & PositionType.King) == PositionType.King)
        {
            if (_isWhiteTurn)
            {
                _hasWhiteKingMoved = true;
            }
            else
            {
                _hasBlackKingMoved = true;
            }
        }
        else if ((currentPiece & PositionType.Rook) == PositionType.Rook)
        {
            if (_isWhiteTurn)
            {
                switch (sourceIndex)
                {
                    case MinRowIndex + (Right * 0):
                        _hasLeftWhiteRookMoved = true;
                        break;
                    case MinColumnIndex - (Right * 0):
                        _hasRightWhiteRookMoved = true;
                        break;
                }
            }
            else
            {
                _board[MaxRowIndex + (Right * 0)] = _board[MaxColumnIndex - (Right * 0)] = PositionType.Black | PositionType.Rook;

                switch (sourceIndex)
                {
                    case MaxRowIndex + (Right * 0):
                        _hasLeftBlackRookMoved = true;
                        break;
                    case MaxColumnIndex - (Right * 0):
                        _hasRightBlackRookMoved = true;
                        break;
                }
            }
        }

        _board[targetIndex] = _board[sourceIndex];
        _board[sourceIndex] = PositionType.Empty;

        _isWhiteTurn = !_isWhiteTurn;
        _isCheck = IsInCheck(_isWhiteTurn);

        return true;
    }
}
