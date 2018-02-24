using System;

namespace CodeforcesHallOfFame.Models
{
    public class Partition
    {
        public string ContestName { get; set; }
        public Team Party { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }

        public int Year
        {
            get
            {
                if (ContestName == null) return 0;
                return int.Parse(ContestName.Split(' ')[2]);
            }
        }

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