using System;
using System.Text;

namespace CodeforcesHallOfFame
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var analizer = new Analizer();

            Console.WriteLine("\tTeams with two wins\n");
            analizer.DoubleWinnerCouple().ForEach(s => Console.WriteLine(s.ToFormatString()));
            
            Console.WriteLine("\n\tUsers with two wins in different team\n");
            analizer.DoubleWinnersWithDifferentTeam().ForEach(s => Console.WriteLine(s.ToFormatString()));
            
            Console.WriteLine("\n\tUsers with one win\n");
            analizer.OneWinList().ForEach(s => Console.WriteLine(s.ToFormatString()));
        }
    }
}
