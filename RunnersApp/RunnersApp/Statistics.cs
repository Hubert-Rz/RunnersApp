
namespace RunnersApp
{
    public class Statistics
    {
        public TimeSpan SumTime { get; private set; }
        public float SumDistance { get; private set; }
        public int Count { get; private set; }
        public TimeSpan AveragePace
        {
            get
            {
                TimeSpan time = this.SumTime / this.SumDistance;

                var sec = (int)Math.Round(time.TotalSeconds) - time.Days * 24 * 60 * 60 - time.Hours * 60 * 60 - time.Minutes * 60;
                TimeSpan t1 = new TimeSpan(time.Days, time.Hours, time.Minutes, sec, 0);
                return t1;
            }
        }

        public float AverageDistance
        {
            get
            {
                return this.SumDistance / this.Count;
            }
        }

        public Statistics()
        {
            this.Count = 0;
            this.SumDistance = 0;
            this.SumTime = TimeSpan.Zero;
        }

        public void AddTime(TimeSpan time)
        {
            this.Count++;
            this.SumTime += time;


        }

        public void AddDistance(float distance)
        {
            this.SumDistance += distance;
        }

    }
}
