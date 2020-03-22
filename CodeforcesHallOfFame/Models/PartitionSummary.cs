using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeforcesHallOfFame.Models
{
    public class PartitionSummary
    {
        public PartitionSummary(String handles, List<Partition> partitions)
        {
            Handles = handles;
            Partitions = partitions;
        }

        public String Handles { get; set; }
        public List<Partition> Partitions { get; set; }

        public string ToFormatString()
        {
            string res = $"- {Handles}: ";

            return Partitions
                .Select(partition => $" {partition.Party.TeamName}( **{partition.Rank}** place, {partition.Year}) ")
                .Aggregate(res, (current, partitionInfo) => current + partitionInfo);
        }
    }
}