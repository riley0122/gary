namespace Gary
{
    public interface IPiece
    {
        Square CurrentPosition { get; set; }
        bool IsMoveValid(Square targetSquare);
        void Move(Square targetSquare);
        string GetPieceSymbol();
    }
}