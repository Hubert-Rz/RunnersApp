﻿using RunnersApp;

namespace RunnerApp
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            WritelineColor(ConsoleColor.Magenta, "Hello to the [RunnersApp] console app.");
            bool CloseApp = false;
            while (!CloseApp)
            {
                RunnerManager runners = new RunnerManager();
                runners.RunnerAdded += NewRunnerAdded;
                int count = 0;

                void NewRunnerAdded(object sender, EventArgs args)
                {
                    Console.WriteLine("New runner added!");
                }

                Console.WriteLine();
                WritelineColor(ConsoleColor.Cyan,
                    "A - Add new runner to the base runners\n" +
                    "number runner - Add runner's results to the base and show statistics\n" +
                    "X - Close app\n");
                try
                {
                    runners.ShowRunners();
                    count = runners.filesList.Count;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception catched: {e.Message}");
                }

                WritelineColor(ConsoleColor.Yellow, "What you want to do? \nPress key A, number runner or X: ");
                var userInput = Console.ReadLine().ToUpper();
                var numerical = int.TryParse(userInput, out int result);
                if (numerical == false)
                {
                    switch (userInput)
                    {
                        case ("A"):
                            Console.Write("Name runner's:");
                            string name = Console.ReadLine();
                            Console.Write("Surname runner's:");
                            string surname = Console.ReadLine();
                            Console.Write("Sex runner's (F or M):");
                            string sex = Console.ReadLine().ToUpper();
                            while (sex != null && sex != "F" && sex != "M")
                            {
                                Console.Write("Sex runner's (F or M):");
                                sex = Console.ReadLine().ToUpper();
                            }
                            
                            try
                            {
                                runners.AddRunner(name, surname, sex);

                                runners.ShowRunners();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Exception catched: {e.Message}");
                            }
                            break;
                        case ("X"):
                            CloseApp = true;
                            break;
                        default:
                            WritelineColor(ConsoleColor.Red, "Invalid operation.\n");
                            continue;
                    }
                }
                else
                {
                    int index = 1;
                    count = runners.filesList.Count;
                    if (result > 0 && result <= count)
                    {
                        foreach (string file in runners.filesList)
                        {

                            if (index == result)
                            {
                                string sex = file.Split("_")[0];
                                string name = file.Split("_")[1];
                                string surname = file.Split("_")[2];
                                WritelineColor(ConsoleColor.Blue, $"Runner:{name} {surname}.\n Enter run data and/or press 'q' to view stats");
                                AddData(name, surname, sex);
                                Statistics(name, surname, sex);
                            }
                            index++;
                        }
                    }
                    else
                    {
                        WritelineColor(ConsoleColor.Red, "Invalid operation.\n");
                    }
                }
            }
            WritelineColor(ConsoleColor.DarkYellow, "\n\nBye Bye! Press any key to leave.");
            Console.ReadKey();
        }

        private static void AddData(string name, string surname, string sex)
        {
            var runner = new RunnerInFile(name, surname, sex);
            runner.TimeAndDistanceAdded += RunnerTimeOrDistanceAdded;

            void RunnerTimeOrDistanceAdded(object sender, EventArgs args)
            {
                Console.WriteLine("Run data added");
            }

            var inputDistance = "";
            var inputTime = "";

            while (true)
            {
                Console.Write("Enter the distance traveled in km (00,00):");
                inputDistance = Console.ReadLine();
                if (inputDistance == "q")
                {
                    break;
                }
                Console.Write("Enter the time for running the above distance (hh:mm:ss):");
                inputTime = Console.ReadLine();

                if (inputDistance == "q" || inputTime == "q")
                {
                    break;
                }
                else if (inputDistance == "" || inputTime == "")
                {
                    Console.WriteLine("You have not entered any data or incomplete data!");
                }

                try
                {
                    runner.AddData(inputDistance, inputTime);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception catched: {e.Message}");
                }
            }
        }

        private static void Statistics(string name, string surname, string sex)
        {
            try
            {
                var runner = new RunnerInFile(name, surname, sex);
                var statistic = runner.GetStatistics();
                var tempo = statistic.AveragePace;
                WritelineColor(ConsoleColor.Red, $"Runner stats: {name} {surname}:");
                WritelineColor(ConsoleColor.Red, $"Number of gears: {statistic.Count}");
                WritelineColor(ConsoleColor.Red, $"Sum Distance: {statistic.SumDistance}");
                WritelineColor(ConsoleColor.Red, $"Sum Time: {statistic.SumTime}");
                WritelineColor(ConsoleColor.Red, $"Average pace: {tempo.Minutes}:{tempo.Seconds}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception catched: {e.Message}");
            }
        }

        private static void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

    }
}
