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

        public List<PartitionSummary> DoubleWinnersWithDifferentTeam()
        {
            var doubleWinner = CreateListUsers()
                .GroupBy(p => p.Username)
                .OrderBy(g => g.Sum(p => p.Partition.Rank))
                .Where(g => g.Count() >= 2);

            List<PartitionSummary> partitionSummaries = doubleWinner
                .Select(userPartitions => new PartitionSummary(
                    userPartitions.Key,
                    userPartitions.Select(g => g.Partition).ToList()))
                .ToList();

            return partitionSummaries;
        }


        public List<PartitionSummary> OneWinList()
        {
            var doubleWinner = CreateListUsers()
                .GroupBy(p => p.Username)
                .OrderBy(g => g.Sum(p => p.Partition.Rank))
                .Where(g => g.Count() == 1);

            List<PartitionSummary> partitionSummaries = doubleWinner
                .Select(userPartitions => new PartitionSummary(
                    userPartitions.Key,
                    userPartitions.Select(g => g.Partition).ToList()))
                .ToList();
            return partitionSummaries;
        }

        private List<(string Username, Partition Partition)> CreateListUsers()
        {
            var users = new List<(string, Partition)>();

            foreach (Partition partition in AllYearPartition.Except(WinnersInSameTeam))
            {
                users.Add((partition.Party.Members[0].Handle, partition));
                if (partition.Party.Members.Length == 2)
                    users.Add((partition.Party.Members[1].Handle, partition));
            }

            return users;
        }
    }
}