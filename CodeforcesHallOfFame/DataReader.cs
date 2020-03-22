using System.Collections.Generic;
using CodeforcesApiWrapper;
using CodeforcesApiWrapper.NonStandardTypes;
using CodeforcesApiWrapper.Types;

namespace CodeforcesHallOfFame
{
    public class DataReader
    {
        public List<RanklistRow> CodeforcesApiRequest(int contestId)
        {
            var cf = new Codeforces();

            ResponseContainer<Standing> result = cf.Contest.Standings(contestId).Result;
            return result.Result.Rows;
        }
    }
}