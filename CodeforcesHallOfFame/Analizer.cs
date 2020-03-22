using System;
using System.Collections.Generic;
using System.Linq;
using CodeforcesApiWrapper;
using CodeforcesApiWrapper.Types;
using CodeforcesHallOfFame.Models;

namespace CodeforcesHallOfFame
{
    public class Analizer
    {
        public List<Partition> AllYearPartition { get; set; }
        public List<Partition> WinnersInSameTeam { get; set; }

        public Analizer()
        {
            ReadData();
            foreach (Partition partition in AllYearPartition)
            {
                partition.Party.Members = partition.Party.Members.OrderBy(u => u.Handle).ToArray();
            }
        }

        private void ReadData()
        {
            var cf = new Codeforces();
            AllYearPartition = new List<Partition>();
            var cups = new[] { (15, 562), (16, 695), (17, 823) };

            foreach ((Int32 year, Int32 contestId) in cups)
            {
                List<RanklistRow> rankList = cf
                    .Contest
                    .Standings(contestId).Result
                    .Result.Rows;

                AllYearPartition.AddRange(rankList.Select(r => new Partition(r, year)));
            }
        }

        public List<PartitionSummary> DoubleWinnerCouple()
        {
            var doubleWinner = AllYearPartition
                .GroupBy(p => p.Party.HandlesToString())
                .OrderBy(g => g.Sum(p => p.Rank))
                .Where(g => g.Count() >= 2);

            WinnersInSameTeam = new List<Partition>();
            List<PartitionSummary> partitionSummaries = new List<PartitionSummary>();
            foreach (IGrouping<String, Partition> teamPartitions in doubleWinner)
            {
                partitionSummaries.Add(new PartitionSummary(teamPartitions.Key, teamPartitions.ToList()));
                foreach (Partition partition in teamPartitions)
                {
                    WinnersInSameTeam.Add(partition);
                }
            }

            return partitionSummaries;
        }

        public void DoubleWinnersWithDifferentTeam()
        {
            var doubleWinner = CreateListUsers()
                .GroupBy(p => p.User)
                .OrderBy(g => g.Sum(p => p.Place))
                .Where(g => g.Count() >= 2);

            foreach (var group in doubleWinner)
            {
                string res = $"- {Formatter.CodeforcesUserLink(group.Key)}: ";

                foreach (var partition in group)
                {
                    string partitionInfo = $" {partition.TeamName}( **{partition.Place}** place, {partition.Year}) ";
                    res += partitionInfo;
                }
                Console.WriteLine(res);
            }
        }


        public void OneWinList()
        {
            var doubleWinner = CreateListUsers()
                .GroupBy(p => p.User)
                .OrderBy(g => g.Sum(p => p.Place))
                .Where(g => g.Count() == 1);

            foreach (var group in doubleWinner)
            {
                string res = $"- {Formatter.CodeforcesUserLink(group.Key)}: ";

                foreach (var partition in group)
                {
                    string partitionInfo = $" {partition.TeamName}( **{partition.Place}** place, {partition.Year}) ";
                    res += partitionInfo;
                }
                Console.WriteLine(res);
            }
        }
        private List<UserPartition> CreateListUsers()
        {
            var users = new List<UserPartition>();
            foreach (var partition in AllYearPartition.Except(WinnersInSameTeam))
            {
                users.Add(new UserPartition()
                {
                    Place = partition.Rank,
                    TeamName = partition.Party.TeamName,
                    User = partition.Party.Members[0].Handle,
                    Year = partition.Year
                });

                if (partition.Party.Members.Length == 2)
                {
                    users.Add(new UserPartition()
                    {
                        Place = partition.Rank,
                        TeamName = partition.Party.TeamName,
                        User = partition.Party.Members[1].Handle,
                        Year = partition.Year
                    });
                }
            }
            return users;
        }
    }
}