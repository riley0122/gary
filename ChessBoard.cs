namespace Gary
{
    public class ChessBoard {
        private Dictionary<Square, IPiece> board;

        public ChessBoard() {
            board = new Dictionary<Square, IPiece>();
        }

        public void PlacePiece(IPiece piece, Square square) {
            if (!board.ContainsKey(square)) {
                board[square] = piece;
            } else {
                throw new InvalidOperationException("[ChessBoard] Square already occupied!");
            }
        }

        public IPiece? GetPieceAt(Square square) {
            return board.ContainsKey(square) ? board[square] : null;
        }

        public void RemovePiece(Square square) {
            if (board.ContainsKey(square)) {
                board.Remove(square);
            } else {
                throw new InvalidOperationException("[ChessBoard] No piece to remove at this square!");
            }
        }

        public override string ToString()
        {
            string boardString = "";
            for (int rank = 8; rank >= 1; rank--) {
                for (char file = 'a'; file <= 'h'; file++) {
                    Square square = new Square(file, rank);
                    IPiece? piece = GetPieceAt(square);
                    boardString += (piece != null ? piece.GetPieceSymbol() : ".") + " ";
                }
                boardString += "\n";
            }
            return boardString;
        }
    }
}