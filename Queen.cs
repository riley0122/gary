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
            ChessBoard board = targetSquare.board;
            IPiece? targetPiece = board.GetPieceAt(targetSquare);

            int rankDelta = targetSquare.rank - this.CurrentPosition.rank;
            int fileDelta = targetSquare.file - this.CurrentPosition.file;

            if (fileDelta == 0 || rankDelta == 0 || Math.Abs(fileDelta) == Math.Abs(rankDelta)) {
                int rankStep = (rankDelta == 0) ? 0 : (rankDelta > 0 ? 1 : -1);
                int fileStep = (fileDelta == 0) ? 0 : (fileDelta > 0 ? 1 : -1);

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

            for (int i = 1; i <= 7; i++) {
                // Top right
                if (this.CurrentPosition.rank + i <= 8 && this.CurrentPosition.file + i <= 'h') {
                    Square targetSquare = new Square((char)(this.CurrentPosition.file + i), this.CurrentPosition.rank + i, board);
                    if (this.IsMoveValid(targetSquare)) {
                        legalMoves.Add(targetSquare);
                    }
                }

                // Top left
                if (this.CurrentPosition.rank + i <= 8 && this.CurrentPosition.file - i >= 'a') {
                    Square targetSquare = new Square((char)(this.CurrentPosition.file - i), this.CurrentPosition.rank + i, board);
                    if (this.IsMoveValid(targetSquare)) {
                        legalMoves.Add(targetSquare);
                    }
                }

                // Bottom right
                if (this.CurrentPosition.rank - i >= 1 && this.CurrentPosition.file + i <= 'h') {
                    Square targetSquare = new Square((char)(this.CurrentPosition.file + i), this.CurrentPosition.rank - i, board);
                    if (this.IsMoveValid(targetSquare)) {
                        legalMoves.Add(targetSquare);
                    }
                }

                // Bottom left
                if (this.CurrentPosition.rank - i >= 1 && this.CurrentPosition.file - i >= 'a') {
                    Square targetSquare = new Square((char)(this.CurrentPosition.file - i), this.CurrentPosition.rank - i, board);
                    if (this.IsMoveValid(targetSquare)) {
                        legalMoves.Add(targetSquare);
                    }
                }
            }

            return legalMoves.ToArray();
        }

        public string GetPieceSymbol() {
            return isWhite ? "Q" : "q";
        }

        public IPiece Clone() {
            return new Queen(CurrentPosition, this.isWhite);
        }
    }
}