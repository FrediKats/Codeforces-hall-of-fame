﻿using System.Collections.Generic;
using System.Net;
using CodeforcesHallOfFame.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeforcesHallOfFame
{
    public class DataReader
    {
        private const string ContestUrl = "http://codeforces.com/api/contest.standings?";

        public List<Partition> CodeforcesApiRequest(int contestId)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString($"{ContestUrl}contestId={contestId}");
                var jObjectData = JObject.Parse(json)["result"];
                var res = jObjectData["rows"].ToObject<List<Partition>>();
                foreach (var partition in res)
                {
                    partition.ContestName = (string) jObjectData["contest"]["name"];
                }

                return res;
            }
        }
    }
}