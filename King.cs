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
            ChessBoard board = targetSquare.board;
            IPiece? targetPiece = board.GetPieceAt(targetSquare);

            int rankDelta = targetSquare.rank - this.CurrentPosition.rank;
            int fileDelta = targetSquare.file - this.CurrentPosition.file;

            if (rankDelta > 1 || fileDelta > 1) {
                return false;
            }

            if (CurrentPosition.rank + rankDelta < 1 || CurrentPosition.rank + rankDelta > 8 || CurrentPosition.file + fileDelta < 'a' || CurrentPosition.file + fileDelta > 'h') {
                return false;
            }

            return true;
        }

        public Square[] GetValidMoves() {
            // TODO: imlpement getting moves
            return [];
        }

        public string GetPieceSymbol() {
            return isWhite ? "K" : "k";
        }

        public IPiece Clone(ChessBoard toBoard) {
            return new King(new Square(CurrentPosition.file, CurrentPosition.rank, toBoard), this.isWhite);
        }
    }
}