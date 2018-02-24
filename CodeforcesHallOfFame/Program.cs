using System;
using System.Collections.Generic;
using System.Linq;
using CodeforcesHallOfFame.Models;

namespace CodeforcesHallOfFame
{
    class Program
    {
        static void Main(string[] args)
        {
            DataReader dr = new DataReader();
            var listVk15 = dr.CodeforcesApiRequest(562);
            var listVk16 = dr.CodeforcesApiRequest(695);
            var listVk17 = dr.CodeforcesApiRequest(823);

            //var users15 = CreateListUsers(listVk15, 2015);
            //var users16 = CreateListUsers(listVk16, 2016);
            //var users17 = CreateListUsers(listVk17, 2017);

            var allYearList = new List<Partition>(listVk15);
            allYearList.AddRange(listVk16);
            allYearList.AddRange(listVk17);

            foreach (var partition in allYearList)
            {
                partition.Party.Order();
            }

            //var users = users15
            //    .Union(users16)
            //    .Union(users17);

            var topTeam = allYearList
                .GroupBy(p => p.Party)
                .OrderBy(p => p.Key.TeamName)
                .Where(g => g.Count() >= 2);

            var allTeam = allYearList
                .GroupBy(p => p.Party)
                .OrderBy(p => p.Key.TeamName);

            foreach (var team in topTeam)
            {
                Console.WriteLine(team.Key);
            }
            Console.WriteLine("+++++++++++++++++++++++++++++++");
            foreach (var team in allTeam)
            {
                Console.WriteLine(team.Key);
            }

        }

        static void PrintContestData(IEnumerable<Partition> data, string contest)
        {
            Console.WriteLine(contest);
            foreach (var partition in data)
            {
                Console.WriteLine(partition);
            }
        }

        static List<UserPartition> CreateListUsers(IEnumerable<Partition> data, int year)
        {
            var users = new List<UserPartition>();
            foreach (var partition in data)
            {
                users.Add(new UserPartition()
                {
                    Place = partition.Rank,
                    TeamName = partition.Party.TeamName,
                    User = partition.Party.Members[0].Handle,
                    Year = year
                });
                if (partition.Party.Members.Count == 2)
                {
                    users.Add(new UserPartition()
                    {
                        Place = partition.Rank,
                        TeamName = partition.Party.TeamName,
                        User = partition.Party.Members[1].Handle,
                        Year = year
                    });
                }
            }
            return users;
        }
    }
}
