namespace Gary
{
    public class Rook : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }

        public Rook(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
        }

        public bool IsMoveValid(Square targetSquare) {
            // TODO: implement correct movement validation
            return true;
        }

        public void Move(Square targetSquare) {
            if (IsMoveValid(targetSquare)) {
                CurrentPosition = targetSquare;
            }
        }

        public string GetPieceSymbol() {
            return isWhite ? "R" : "r";
        }
    }
}