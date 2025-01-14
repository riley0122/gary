using Xunit;
using Gary;
namespace Gary.Tests;

public class PiecesTests
{
    [Theory]
    [InlineData('b', 8, false)]
    [InlineData('h', 1, true)]
    [InlineData('f', 1, true)]
    public void CheckColours(char file, int rank, bool expected)
    {
        string starting_fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        ChessBoard board = new();
        board.LoadFromFEN(starting_fen);

        Square location = new(file, rank, board);
        bool actual = board.GetPieceAt(location).isWhite;

        Assert.Equal(actual, expected);
    }
}