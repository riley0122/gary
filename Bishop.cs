namespace Gary
{
    public class Bishop : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }
        public int pointValue { get; }

        public Bishop(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
            this.pointValue = 3;
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
            return isWhite ? "B" : "b";
        }

        public IPiece Clone() {
            return new Bishop(CurrentPosition, this.isWhite);
        }
    }
}