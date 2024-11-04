namespace Gary
{
    public interface IPiece
    {
        Square CurrentPosition { get; set; }
        bool isWhite { get; }
        bool IsMoveValid(Square targetSquare);
        Square[] GetValidMoves();
        public void Move(Square targetSquare) {
            if (IsMoveValid(targetSquare)) {
                ChessBoard board = CurrentPosition.board;
                board.RemovePiece(CurrentPosition);
                CurrentPosition = targetSquare;
                board.PlacePiece(this, targetSquare);
                board.whiteToMove = !board.whiteToMove;
            }
        }
        string GetPieceSymbol();
    }
}