using System.Linq;
using CodeforcesApiWrapper.Types;

namespace CodeforcesHallOfFame
{
    public static class Extensions
    {
        public static string HandlesToString(this Party party)
        {
            return string.Join(" & ", party.Members.Select(m => m.Handle).ToList());
        }
    }
}