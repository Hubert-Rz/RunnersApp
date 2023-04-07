using System.Xml.Linq;

namespace RunnersApp.tests
{
    public class Tests
    {
        [Test]
        public void WhenAddRunResultsToRunner_ThenGetCorrectSumDistance()
        {
            //arange
            string fileName = "K_Tester_Tester.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var runner = new RunnerInFile("Tester", "Tester", "K");
            runner.AddData("3", "00:18:00");
            runner.AddData("2", "00:12:00");
            runner.AddData("5", "00:30:00");
            //act
            var statistics = runner.GetStatistics();
            //assert
            Assert.AreEqual(Math.Round(10.00, 2), Math.Round(statistics.SumDistance, 2));
        }

        [Test]
        public void WhenAddRunResultsToRunner_ThenGetCorrectSumTime()
        {
            //arange
            string fileName = "K_Tester_Tester.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var runner = new RunnerInFile("Tester", "Tester", "K");
            runner.AddData("3", "00:18:00");
            runner.AddData("2", "00:12:00");
            runner.AddData("4", "00:24:12");
            //act
            var statistics = runner.GetStatistics();
            //assert
            TimeSpan TS = new TimeSpan(0, 0, 54, 12);
            Assert.AreEqual(TS, statistics.SumTime);
        }

        [Test]
        public void WhenAddRunResultsToRunner_ThenGetCorrectPace()
        {
            //arange
            string fileName = "K_Tester_Tester.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var runner = new RunnerInFile("Tester", "Tester", "K");
            runner.AddData("3", "00:18:00");
            runner.AddData("2", "00:12:00");
            runner.AddData("4", "00:24:17");
            //act
            var statistics = runner.GetStatistics();
            //assert
            TimeSpan TS = new TimeSpan(0, 0, 6, 2);
            Assert.AreEqual(TS, statistics.AveragePace);
        }
    }
}