using System.Runtime.InteropServices;

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

            int rankDelta = targetSquare.rank - this.CurrentPosition.rank;
            int fileDelta = targetSquare.file - this.CurrentPosition.file;

            if (fileDelta == 0) {
                if (this.isWhite && rankDelta == 1 || !this.isWhite && rankDelta == -1) {
                    return targetPiece is null;
                }

                if (this.isWhite && rankDelta == 2 && this.CurrentPosition.rank == 2 || !this.isWhite && rankDelta == -2 && this.CurrentPosition.rank == 7) {
                    Square inbetween = new Square(this.CurrentPosition.file, this.CurrentPosition.rank + rankDelta / 2, board);
                    return targetPiece is null && board.GetPieceAt(inbetween) is null;
                }

                return false;
            }

            if (Math.Abs(fileDelta) == 1 && (this.isWhite && rankDelta == 1 || !this.isWhite && rankDelta == -1)) {
                return targetPiece is not null && targetPiece.isWhite != this.isWhite;
            }

            return false;
        }

        public Square[] GetValidMoves() {
            List<Square> legalMoves = new List<Square>();
            if (this.isWhite) {
                if (this.CurrentPosition.rank == 2) {
                    Square longSquare = new Square(this.CurrentPosition.file, this.CurrentPosition.rank + 2, this.CurrentPosition.board);
                    if (this.IsMoveValid(longSquare)) {
                        legalMoves.Add(longSquare);
                    }
                }

                Square shortSquare = new Square(this.CurrentPosition.file, this.CurrentPosition.rank + 1, this.CurrentPosition.board);
                if (this.IsMoveValid(shortSquare)) {
                    legalMoves.Add(shortSquare);
                }

                if (this.CurrentPosition.file != 'h') {
                    Square leftCaptureSquare = new Square((char)(this.CurrentPosition.file + 1), this.CurrentPosition.rank + 1, this.CurrentPosition.board);
                    if (this.IsMoveValid(leftCaptureSquare)) {
                        legalMoves.Add(leftCaptureSquare);
                    }
                }

                if (this.CurrentPosition.file != 'a') {
                    Square rightCaptureSquare = new Square((char)(this.CurrentPosition.file - 1), this.CurrentPosition.rank + 1, this.CurrentPosition.board);
                    if (this.IsMoveValid(rightCaptureSquare)) {
                        legalMoves.Add(rightCaptureSquare);
                    }
                }
            } else {
                if (this.CurrentPosition.rank == 7) {
                    Square longSquare = new Square(this.CurrentPosition.file, this.CurrentPosition.rank - 2, this.CurrentPosition.board);
                    if (this.IsMoveValid(longSquare)) {
                        legalMoves.Add(longSquare);
                    }
                }

                Square shortSquare = new Square(this.CurrentPosition.file, this.CurrentPosition.rank - 1, this.CurrentPosition.board);
                if (this.IsMoveValid(shortSquare)) {
                    legalMoves.Add(shortSquare);
                }

                if (this.CurrentPosition.file != 'h') {
                    Square leftCaptureSquare = new Square((char)(this.CurrentPosition.file + 1), this.CurrentPosition.rank - 1, this.CurrentPosition.board);
                    if (this.IsMoveValid(leftCaptureSquare)) {
                        legalMoves.Add(leftCaptureSquare);
                    }
                }

                if (this.CurrentPosition.file != 'a') {
                    Square rightCaptureSquare = new Square((char)(this.CurrentPosition.file - 1), this.CurrentPosition.rank - 1, this.CurrentPosition.board);
                    if (this.IsMoveValid(rightCaptureSquare)) {
                        legalMoves.Add(rightCaptureSquare);
                    }
                }
            }

            return legalMoves.ToArray();
        }

        public string GetPieceSymbol() {
            return isWhite ? "P" : "p";
        }
    }
}