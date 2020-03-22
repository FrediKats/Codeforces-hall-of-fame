using CodeforcesApiWrapper.Types;

namespace CodeforcesHallOfFame.Models
{
    public class Partition
    {
        public Party Party { get; set; }
        public int Rank { get; set; }
        public double Points { get; set; }

        public Partition(RanklistRow rank, int year)
        {
            Year = year;
            Party = rank.Party;
            Rank = rank.Rank;
            Points = rank.Points;
        }

        public int Year { get; }

        public override string ToString()
        {
            return $"{Party} - {Rank}({Points})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Partition p)
                return Party.Equals(p.Party);
            return false;
        }

        protected bool Equals(Partition other)
        {
            return Party.Equals(other.Party);
        }

        public override int GetHashCode()
        {
            return (Party != null ? Party.GetHashCode() : 0);
        }
    }
}