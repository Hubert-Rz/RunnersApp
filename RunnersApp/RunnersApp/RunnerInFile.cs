
namespace RunnersApp
{
    public class RunnerInFile : RunnerBase
    {
        public override event TimeAndDistanceAddedDelegate TimeAndDistanceAdded;
        //public override event RunnerAddedDelegate RunnerAdded;
        private string fileName;

        public RunnerInFile(string name, string surname, string sex)
            : base(name, surname, sex)

        {
            fileName = sex + "_" + name + "_" + surname + ".txt";
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();

            if (File.Exists(fileName))
            {

                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        throw new Exception("file is empty");
                    }
                    while (line != null)
                    {
                        var parts = line.Split('\t');
                        if (float.TryParse(parts[0], out float resultDistance) && TimeSpan.TryParse(parts[1], out TimeSpan resultTime))
                        {
                            statistics.AddDistance(resultDistance);
                            statistics.AddTime(resultTime);
                        }
                        else
                        {
                            throw new Exception("invalid value in file");
                        }
                        line = reader.ReadLine();
                    }
                }
            }
            return statistics;
        }

        public override void AddData(string distance, string time)
        {
            if (float.TryParse(distance, out float resultDistance) && TimeSpan.TryParse(time, out TimeSpan resultTime))
            {
                this.AddData(resultDistance, resultTime);
            }
            else
            {
                throw new Exception("String is not float");
            }
        }


        public override void AddData(float distance, TimeSpan time)
        {
            TimeSpan TS = new TimeSpan(0, 5, 0, 0);

            if (distance >= 0 && distance <= 100 && time > TimeSpan.Zero && time < TS)
            {
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine("{0}\t{1}", distance, time);
                }
                if (TimeAndDistanceAdded != null)
                {
                    TimeAndDistanceAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("invalid value");
            }
        }

    }
}
