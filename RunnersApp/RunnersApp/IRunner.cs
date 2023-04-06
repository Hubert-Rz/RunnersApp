using static RunnersApp.RunnerBase;

namespace RunnersApp
{
    public interface IRunner
    {
        string Name { get; }
        string Surname { get; }
        string Sex { get; }
        void AddData(string distance, string time);
        void AddData(float distance, TimeSpan time);

        event TimeAndDistanceAddedDelegate TimeAndDistanceAdded;
        Statistics GetStatistics();
    }
}
