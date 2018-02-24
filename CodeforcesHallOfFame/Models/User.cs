namespace CodeforcesHallOfFame.Models
{
    public class User
    {
        public string Handle { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is User u)
                return Handle.Equals(u.Handle);
            return false;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public override string ToString()
        {
            return Formatter.CodeforcesUserLink(Handle);
        }
    }
}