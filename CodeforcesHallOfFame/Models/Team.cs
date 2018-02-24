using System.Collections.Generic;
using System.Linq;

namespace CodeforcesHallOfFame.Models
{
    public class Team
    {
        public void Order()
        {
            Members = Members.OrderBy(h => h.Handle).ToList();
        }
        public List<User> Members { get; set; }
        public string TeamName { get; set; }

        public string HandlesToString()
        {
            return Members.Count == 1
                ? $"{Members[0]}"
                : $"{Members[0]} & {Members[1]}";
        }
        public override string ToString()
        {
            return $"{TeamName}({HandlesToString()})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Team ot)
                return Equals(ot);
            return false;
        }

        protected bool Equals(Team other)
        {
            var firstTeam = Members;
            var secondTeam = other.Members;

            if (firstTeam.Count() != secondTeam.Count()) return false;
            if (firstTeam.Count() == 1)
                return firstTeam[0].Handle == secondTeam[0].Handle;
            return firstTeam[0].Handle == secondTeam[0].Handle
                   && firstTeam[1].Handle == secondTeam[1].Handle;
        }

        public override int GetHashCode()
        {
            if (Members == null) return 0;
            if (Members.Count == 1) return Members[0].GetHashCode();
            return Members[0].GetHashCode() + Members[1].GetHashCode();
        }
    }
}