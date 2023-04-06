using RunnersApp;
using System;
using System.Collections.Generic;

namespace RunnersApp
{
    public abstract class RunnerBase : IRunner
    {
        public delegate void TimeAndDistanceAddedDelegate(object sender, EventArgs args);
        public abstract event TimeAndDistanceAddedDelegate TimeAndDistanceAdded;

        public RunnerBase(string name, string surname, string sex)
        {
            this.Name = name;
            this.Surname = surname;
            this.Sex = sex;
        }

        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string Sex { get; private set; }
        public abstract Statistics GetStatistics();
        public abstract void AddData(string distance, string time);
        public abstract void AddData(float distance, TimeSpan time);
   
    }
}
