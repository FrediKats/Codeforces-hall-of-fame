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
            Analizer analizer = new Analizer();
            analizer.DoubleWinnerCouple();
            //var topTeam = analizer.AllYearPartition
            //    .GroupBy(p => p.Party)
            //    .OrderBy(p => p.Key.TeamName)
            //    .Where(g => g.Count() >= 2);

            //var allTeam = analizer.AllYearPartition
            //    .GroupBy(p => p.Party)
            //    .OrderBy(p => p.Key.TeamName);

            //foreach (var team in topTeam)
            //{
            //    Console.WriteLine(team.Key);
            //}

            //var users15 = CreateListUsers(listVk15, 2015);
            //var users16 = CreateListUsers(listVk16, 2016);
            //var users17 = CreateListUsers(listVk17, 2017);
            //var users = users15
            //    .Union(users16)
            //    .Union(users17);
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
