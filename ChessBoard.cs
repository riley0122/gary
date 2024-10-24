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
                Console.WriteLine($"Placed piece: {piece.GetPieceSymbol()} at {square}");
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

        public void LoadFromFEN(string fen) {
            Console.WriteLine($"Loading FEN: {fen}");
            string[] parts = fen.Split(" ");
            string pieces = parts[0];
            string[] ranks = pieces.Split("/");

            for (int rank = 8; rank >= 1; rank--) {
                int fileIndex = 0;
                for (int i = 0; i < ranks[8 - rank].Length; i++) {
                    char symbol = ranks[8 - rank][i];

                    if (char.IsDigit(symbol)) {
                        // Empty squares
                        fileIndex += symbol - '0';
                    } else {
                        char file = (char)('a' + fileIndex);
                        Square square = new Square(file, rank);

                        IPiece piece = CreatePieceFromSymbol(symbol, square);
                        if (piece != null) {
                            PlacePiece(piece, square);
                        }
                        fileIndex++;
                    }
                }
            }
        }

        private IPiece CreatePieceFromSymbol(char symbol, Square square) {
            Console.WriteLine($"Creating piece from symbol: {symbol} at {square}");
            switch (symbol)
            {
                case 'P': return new Pawn(square, true); // White pawn
                case 'p': return new Pawn(square, false); // Black pawn
                case 'R': return new Rook(square, true); // White rook
                case 'r': return new Rook(square, false); // Black rook
                case 'N': return new Knight(square, true); // White knight
                case 'n': return new Knight(square, false); // Black knight
                case 'B': return new Bishop(square, true); // White bishop
                case 'b': return new Bishop(square, false); // Black bishop
                case 'Q': return new Queen(square, true); // White queen
                case 'q': return new Queen(square, false); // Black queen
                case 'K': return new King(square, true); // White king
                case 'k': return new King(square, false); // Black king
                default: return null;
            }
        }
    }
}