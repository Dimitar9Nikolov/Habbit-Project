using System;

namespace habbitMain
{
    public class Habit
    {
        public string Name { get; set; }
        public List<DateTime> Dates { get; set; }

        public Habit()
        {
            Dates = new List<DateTime>();
        }
    }
}
// This is the habit class that will be used to store the name of the habit and the dates that the habit has been completed on.