namespace Gary
{
    public class Queen : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }
        public int pointValue { get; }

        public Queen(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
            this.pointValue = 9;
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
            return isWhite ? "Q" : "q";
        }
    }
}