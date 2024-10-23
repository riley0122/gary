namespace Gary
{
    public class Square
    {
        private char file;
        private int rank;

        
        public override string ToString() {
            return file.ToString() + rank.ToString();
        }

        public Square(char file, int rank) {
            if (file < 'a' || file > 'h') {
                throw new ArgumentOutOfRangeException("[Square] File outside of range!");
            }
            this.file = file;
            if (rank < 1 || rank > 8) {
                throw new ArgumentOutOfRangeException("[Square] Rank outside of range!");
            }
            this.rank = rank;
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