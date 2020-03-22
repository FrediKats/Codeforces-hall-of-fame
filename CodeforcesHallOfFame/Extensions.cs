using CodeforcesApiWrapper.Types;

namespace CodeforcesHallOfFame
{
    public static class Extensions
    {
        public static string HandlesToString(this Party party)
        {
            return party.Members.Length == 1
                ? $"{party.Members[0]}"
                : $"{party.Members[0]} & {party.Members[1]}";
        }
    }
}