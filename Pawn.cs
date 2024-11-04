namespace Gary
{
    public class Pawn : IPiece
    {
        public Square CurrentPosition { get; set; }
        public bool isWhite { get; }

        public Pawn(Square initialPosition, bool isWhite) {
            this.CurrentPosition = initialPosition;
            this.isWhite = isWhite;
        }

        public bool IsMoveValid(Square targetSquare) {
            ChessBoard board = targetSquare.board;

            IPiece? targetPiece = board.GetPieceAt(targetSquare);

            // If there is a piece of the same colour on the target square
            if (targetPiece is not null && targetPiece.isWhite == this.isWhite) {
                return false;
            }

            // If there is a piece already on the target piece and the piece is within the same file (vertical)
            if (targetPiece is not null && targetPiece.CurrentPosition.file == this.CurrentPosition.file) {
                return false;
            }

            // If the target square is not in a rank (horizontal) next to the current one
            if (Math.Max(targetSquare.rank, this.CurrentPosition.rank) - Math.Min(targetSquare.rank, (int)this.CurrentPosition.rank) != 1) {
                return false;
            }

            // If the file is not the same and there is not a piece that is the same colour
            if (targetSquare.file != this.CurrentPosition.file && targetPiece is not null && targetPiece.isWhite == this.isWhite) {
                return false;
            }

            // If the target piece exists and it's file is the same or its rank is not one higher or lower
            if (targetPiece is not null && (targetSquare.file == this.CurrentPosition.file || Math.Max(targetSquare.rank, this.CurrentPosition.rank) - Math.Min(targetSquare.rank, this.CurrentPosition.rank) != 1)) {
                return false;
            }

            // rank diffrences
            if (this.isWhite) {
                // Is starting position
                if (this.CurrentPosition.rank == 2) {
                    if (targetSquare.rank - this.CurrentPosition.rank != 1 && targetSquare.rank - this.CurrentPosition.rank != 2) {
                        return false;
                    }
                } else {
                    if (targetSquare.rank - this.CurrentPosition.rank != 1) {
                        return false;
                    }
                }
            } else {
                // Is starting position
                if (this.CurrentPosition.rank == 7) {
                    if (targetSquare.rank - this.CurrentPosition.rank != 1 && targetSquare.rank - this.CurrentPosition.rank != 2) {
                        return false;
                    }
                } else {
                    if (targetSquare.rank - this.CurrentPosition.rank != 1) {
                        return false;
                    }
                }
            }

            return true;
        }

        public Square[] GetValidMoves() {
            // TODO: imlpement getting moves
            return [];
        }

        public void Move(Square targetSquare) {
            if (IsMoveValid(targetSquare)) {
                CurrentPosition = targetSquare;
            }
        }

        public string GetPieceSymbol() {
            return isWhite ? "P" : "p";
        }
    }
}