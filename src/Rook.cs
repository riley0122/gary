namespace Gary
{
    public class Rook : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }
        public int pointValue { get; }

        public Rook(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
            this.pointValue = 5;
        }

        public bool IsMoveValid(Square targetSquare) {
            ChessBoard board = targetSquare.board;
            IPiece? targetPiece = board.GetPieceAt(targetSquare);

            int rankDelta = targetSquare.rank - this.CurrentPosition.rank;
            int fileDelta = targetSquare.file - this.CurrentPosition.file;

            if (fileDelta == 0 || rankDelta == 0) {
                int rankStep = (rankDelta == 0) ? 0 : (rankDelta > 0 ? 1 : -1);
                int fileStep = (fileDelta == 0) ? 0 : (rankDelta > 0 ? 1 : -1);

                int currentRank = this.CurrentPosition.rank + rankStep;
                char currentFile = (char)(this.CurrentPosition.file + fileStep);
                while (currentRank != targetSquare.rank || currentFile != targetSquare.file) {
                    if (currentRank < 1 || currentRank > 8 || currentFile < 'a' || currentFile > 'h') {
                        return false;
                    }

                    if (board.GetPieceAt(new Square(currentFile, currentRank, board)) is not null) {
                        return false;
                    }
                    currentRank += rankStep;
                    currentFile = (char)(currentFile + fileStep);
                }

                return targetPiece is null || targetPiece.isWhite != this.isWhite;
            }

            return false;
        }

        public Square[] GetValidMoves() {
            List<Square> legalMoves = new List<Square>();
            ChessBoard board = this.CurrentPosition.board;

            for (char file = 'a'; file <= 'h'; file++) {
                if (file != this.CurrentPosition.file) {
                    Square targetSquare = new Square(file, this.CurrentPosition.rank, board);
                    if (this.IsMoveValid(targetSquare)) {
                        legalMoves.Add(targetSquare);
                    }
                }
            }

            for (int rank = 1; rank <= 8; rank++) {
                if (rank != this.CurrentPosition.rank) {
                    Square targetSquare = new Square(this.CurrentPosition.file, rank, board);
                    if (this.IsMoveValid(targetSquare)) {
                        legalMoves.Add(targetSquare);
                    }
                }
            }

            return legalMoves.ToArray();
        }

        public string GetPieceSymbol() {
            return isWhite ? "R" : "r";
        }

        public IPiece Clone(ChessBoard toBoard) {
            return new Rook(new Square(CurrentPosition.file, CurrentPosition.rank, toBoard), this.isWhite);
        }
    }
}