using System;
using System.Collections.Generic;
using System.Linq;
using CodeforcesHallOfFame.Models;

namespace CodeforcesHallOfFame
{
    public class Analizer
    {
        public List<Partition> AllYearPartition { get; set; }
        public List<Partition> WinnersInSameTeam { get; set; }
        public List<Partition> WinnersIsDiffTeam { get; set; }
        public Analizer()
        {
            ReadData();
            foreach (var partition in AllYearPartition)
            {
                partition.Party.Order();
            }
        }

        private void ReadData()
        {
            DataReader dr = new DataReader();
            var listVk15 = dr.CodeforcesApiRequest(562);
            var listVk16 = dr.CodeforcesApiRequest(695);
            var listVk17 = dr.CodeforcesApiRequest(823);
            AllYearPartition = new List<Partition>();
            AllYearPartition.AddRange(listVk15);
            AllYearPartition.AddRange(listVk16);
            AllYearPartition.AddRange(listVk17);
        }

        public void DoubleWinnerCouple()
        {
            
            var doubleWinner = AllYearPartition
                .GroupBy(p => p.Party)
                .OrderBy(g => g.Sum(p => p.Rank))
                .Where(g => g.Count() >= 2);

            WinnersInSameTeam = new List<Partition>();
            foreach (var team in doubleWinner)
            {
                string res = $"- {team.Key.HandlesToString()}: ";
                foreach (var partition in team)
                {
                    WinnersInSameTeam.Add(partition);

                    string partitionInfo = $" {partition.Party.TeamName}( **{partition.Rank}** place, {partition.Year}) ";
                    res += partitionInfo;
                }
                Console.WriteLine(res);
            }
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
                if (partition.Party.Members.Count == 2)
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