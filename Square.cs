namespace Gary
{
    public class Square
    {
        public char file;
        public int rank;

        
        public ChessBoard board;

        public override string ToString() {
            return file.ToString() + rank.ToString();
        }

        public Square(char file, int rank, ChessBoard board) {
            if (file < 'a' || file > 'h') {
                throw new ArgumentOutOfRangeException("[Square] File outside of range!");
            }
            this.file = file;
            if (rank < 1 || rank > 8) {
                throw new ArgumentOutOfRangeException("[Square] Rank outside of range!");
            }
            this.rank = rank;

            this.board = board;
        }

        public override bool Equals(object obj)
        {
            return obj is Square square &&
                   file == square.file &&
                   rank == square.rank;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(file, rank);
        }
    }
}