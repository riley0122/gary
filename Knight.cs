namespace Gary
{
    public class Knight : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }
        public int pointValue { get; }

        public Knight(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
            this.pointValue = 3;
        }

        public bool IsMoveValid(Square targetSquare) {
            int rankDelta = Math.Abs(targetSquare.rank - this.CurrentPosition.rank);
            int fileDelta = Math.Abs(targetSquare.file - this.CurrentPosition.file);

            if (this.CurrentPosition.board.GetPieceAt(targetSquare) is not null && this.CurrentPosition.board.GetPieceAt(targetSquare).isWhite == this.isWhite) {
                return false;
            }

            return (rankDelta == 2 && fileDelta == 1) || (rankDelta == 1 && fileDelta == 2);
        }

        public Square[] GetValidMoves() {
            List<Square> legalMoves = new List<Square>();
            ChessBoard board = this.CurrentPosition.board;

            (int relativeRank, int relativeFile)[] relativePositions = new (int, int)[] {
                (2, 1), (2, -1),
                (-2, 1), (-2, -1),
                (1, 2), (-1, 2),
                (1, -2), (-1, -2)
            };

            foreach ((int relativeRank, int relativeFile) in relativePositions)
            {
                int targetRank = CurrentPosition.rank + relativeRank;
                char targetFile = (char)(CurrentPosition.file + relativeFile);
                if (targetFile < 'a' || targetFile > 'h' || targetRank < 1 || targetRank > 8) continue;

                Square target = new Square(targetFile, targetRank, board);
                if (IsMoveValid(target)) {
                    legalMoves.Add(target);
                }
            }

            return legalMoves.ToArray();
        }

        public string GetPieceSymbol() {
            return isWhite ? "N" : "n";
        }

        public IPiece Clone(ChessBoard toBoard) {
            return new Knight(new Square(CurrentPosition.file, CurrentPosition.rank, toBoard), this.isWhite);
        }
    }
}