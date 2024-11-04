namespace Gary
{
    public class King : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }
        public int pointValue { get; }

        public King(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
            this.pointValue = 0;
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

        public IPiece Clone() {
            return new King(CurrentPosition, this.isWhite);
        }
    }
}