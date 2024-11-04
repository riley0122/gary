namespace Gary
{
    public interface IPiece
    {
        Square CurrentPosition { get; set; }
        bool isWhite { get; }
        bool IsMoveValid(Square targetSquare);
        Square[] GetValidMoves();
        void Move(Square targetSquare);
        string GetPieceSymbol();
    }
}