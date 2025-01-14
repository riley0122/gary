using Xunit;
using Gary;
namespace Gary.Tests;

public class ChessBoardTests
{
    [Fact]
    public void CheckFen()
    {
        string testing_fen = "k6K/8/8/8/8/8/8/P6p b - - 0 1";

        ChessBoard board = new ChessBoard();
        board.LoadFromFEN(testing_fen);

        Assert.IsType<Pawn>(board.GetPieceAt(new Square('a', 1, board)));
        Assert.IsType<Pawn>(board.GetPieceAt(new Square('h', 1, board)));
        Assert.IsType<King>(board.GetPieceAt(new Square('a', 8, board)));
        Assert.IsType<King>(board.GetPieceAt(new Square('h', 8, board)));
        Assert.Null(board.GetPieceAt(new Square('d', 4, board)));
        Assert.Null(board.GetPieceAt(new Square('e', 5, board)));
    }
}