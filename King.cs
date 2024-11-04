namespace Gary
{
    public class King : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }

        public King(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
        }

        public bool IsMoveValid(Square targetSquare) {
            // TODO: implement correct movement validation
            return true;
        }

        public Square[] GetValidMoves() {
            // TODO: imlpement getting moves
            return [];
        }

        public string GetPieceSymbol() {
            return isWhite ? "K" : "k";
        }
    }
}