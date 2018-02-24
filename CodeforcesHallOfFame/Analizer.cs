using System;
using System.Collections.Generic;
using System.Linq;
using CodeforcesHallOfFame.Models;

namespace CodeforcesHallOfFame
{
    public class Analizer
    {
        public List<Partition> AllYearPartition { get; set; }
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
                .OrderBy(p => p.Key.TeamName)
                .Where(g => g.Count() >= 2);

            foreach (var team in doubleWinner)
            {
                string res = $"{team.Key.HandlesToString()}: ";
                foreach (var partition in team)
                {
                    string partitionInfo = $" {partition.Party.TeamName}({partition.Year}) ";
                    res += partitionInfo;
                }
                Console.WriteLine(res);
            }
        }
    }
}