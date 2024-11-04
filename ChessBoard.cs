namespace Gary
{
    public class ChessBoard {
        public bool whiteToMove = true;
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

        // Get a score for the current board.
        // White is higher
        // Black is lower
        public int getScore() {
            int score = 0;
            foreach (IPiece piece in board.Values)
            {
                if (piece.isWhite != this.whiteToMove) {
                    score -= piece.pointValue;
                } else {
                    score += piece.pointValue;
                }
            }
            return score;
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

        public void makeMove(string move) {
            Square fromSquare = new Square(move[0], move[1] - '0', this);
            Square toSquare = new Square(move[2], move[3] - '0', this);
            IPiece frompiece = GetPieceAt(fromSquare);
            bool shouldBeWhite = frompiece.isWhite;
            if (move.Length == 5) {
                RemovePiece(fromSquare);
                if (shouldBeWhite) {
                    PlacePiece(CreatePieceFromSymbol(char.ToUpper(move[4]), fromSquare), fromSquare);
                } else {
                    PlacePiece(CreatePieceFromSymbol(char.ToLower(move[4]), fromSquare), fromSquare);
                }
            }
            frompiece = GetPieceAt(fromSquare);
            frompiece.Move(toSquare);
        }

        public (IPiece, Square)[] GetAllLegalMoves() {
            List<(IPiece, Square)> LegalMoves = new List<(IPiece, Square)>();
            foreach (IPiece piece in board.Values)
            {
                if (piece.isWhite != this.whiteToMove) continue;
                Square[] piece_legalMoves = piece.GetValidMoves();
                foreach (Square legalMove in piece_legalMoves)
                {
                    LegalMoves.Add((piece, legalMove));
                }
            }

            return LegalMoves.ToArray();
        }

        public override string ToString()
        {
            string boardString = "";
            for (int rank = 8; rank >= 1; rank--) {
                for (char file = 'a'; file <= 'h'; file++) {
                    Square square = new Square(file, rank, this);
                    IPiece? piece = GetPieceAt(square);
                    boardString += (piece != null ? piece.GetPieceSymbol() : ".") + " ";
                }
                boardString += "\n";
            }
            return boardString;
        }

        public void LoadFromFEN(string fen) {
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
                        Square square = new Square(file, rank, this);

                        IPiece piece = CreatePieceFromSymbol(symbol, square);
                        if (piece != null) {
                            PlacePiece(piece, square);
                        }
                        fileIndex++;
                    }
                }
            }

            this.whiteToMove = pieces[1] != 'w';
        }

        public IPiece CreatePieceFromSymbol(char symbol, Square square) {
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

        public ChessBoard(ChessBoard original) {
            this.whiteToMove = original.whiteToMove;
            this.board = new Dictionary<Square, IPiece>();

            foreach (var item in original.board)
            {
                Square square = item.Key;
                IPiece piece = item.Value;

                IPiece clonedPiece = piece.Clone(this);
                this.board[square] = clonedPiece;   
            }
        }

        public ChessBoard ImageineMove(string move) {
            ChessBoard hypotheticalBoard = new ChessBoard(this);

            Square fromSquare = new Square(move[0], move[1] - '0', hypotheticalBoard);
            Square toSquare = new Square(move[2], move[3] - '0', hypotheticalBoard);

            IPiece? movingPiece = hypotheticalBoard.GetPieceAt(fromSquare);
            if (movingPiece is null)
            {
                throw new InvalidOperationException("[ChessBoard] No piece to move at the specified from-square.");
            }

            hypotheticalBoard.RemovePiece(fromSquare);
            if (hypotheticalBoard.GetPieceAt(toSquare) is not null) {
                hypotheticalBoard.RemovePiece(toSquare);
            }
            hypotheticalBoard.PlacePiece(movingPiece, toSquare);
            movingPiece.Move(toSquare);

            hypotheticalBoard.whiteToMove = !this.whiteToMove;
            return hypotheticalBoard;
        }
    }
}