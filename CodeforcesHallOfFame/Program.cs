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
            Console.WriteLine("\n\nTeams with two wins\n");
            analizer.DoubleWinnerCouple();
            Console.WriteLine("\n\nUsers with two wins in different team\n");
            analizer.DoubleWinnersWithDifferentTeam();
            Console.WriteLine("\n\n");
            analizer.OneWinList();
        }
    }
}
