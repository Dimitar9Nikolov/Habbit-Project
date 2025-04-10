using System.Net;

namespace habbitMain;

class Program
{
    public static Dictionary<string, List<DateTime>> habits = new Dictionary<string, List<DateTime>>();
    public static Dictionary<string, List<DateTime>> DoneHabits = new Dictionary<string, List<DateTime>>();
    static void Main(string[] args)
    {
        System.Console.WriteLine($"Welcome to the Habit Tracker!");
        bool exit = false;
        while(!exit)
        {
            Console.WriteLine(
                            $"Please select an option: 1, 2, 3, 4, 5, 6\n" +
                            $"1: Add a new habit \n" +
                            $"2: Mark habit as done \n" +
                            $"3: View progress for a habit \n" +
                            $"4: View all habits with streak \n" +
                            $"5: Delete a habit \n" +
                            $"6: Exit");
            string inputUser = Console.ReadLine();
            int input;
            input = int.TryParse(inputUser, out input) ? input : 0;
            
            if (input == 6)
            {
                exit = true;
                Console.WriteLine("Exiting the program...");
                continue;
            }
            else if(input == 1)
            {
                AddHabit();
            }
            else if (input == 2)
            {
                MarkHabitAsDone();
            }
            else if (input == 3)
            {
                ViewProgressForHabit();
            }
            else if (input == 4)
            {
                ViewAllHabitsWithStreak();
            }
            else if (input == 5)
            {
                DeleteHabit();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter: 1, 2, 3, 4, 5, 6");
                continue;
            }
        }
    }
    static void AddHabit()
    {
        Console.WriteLine("Enter the name of the habit:");
        string habitName = Console.ReadLine().Trim();
        if (string.IsNullOrWhiteSpace(habitName)) {
            Console.WriteLine("Habit name cannot be empty.");
            return;
        }

        if (habits.ContainsKey(habitName))
        {
            Console.WriteLine("Habit already exists.");
            return;
        }
        else
        {
            habits[habitName] = new List<DateTime>();
            Console.WriteLine($"Habit '{habitName}' added.");
            System.Console.WriteLine("Do you want to add a date for this habit? (y/n)");
            string addDate = Console.ReadLine();
            
            if (addDate.ToLower() == "y")
            {
                System.Console.WriteLine("Enter the date (yyyy-mm-dd):");
                string input = Console.ReadLine();  
                DateTime date;
                if (DateTime .TryParse(input, out date))
                {
                    habits[habitName].Add(date);
                    System.Console.WriteLine($"Date '{date.ToString("yyyy-MM-dd")}' successfully added to habit '{habitName}'.");
                }
                else
                {
                    System.Console.WriteLine("Invalid date format. Please try again. ");
                }
            }
        }
    }
    static void MarkHabitAsDone()
    {
        System.Console.WriteLine("Enter the name of the habit you want to mark as done:");
        foreach (var item in habits)
        {
            Console.WriteLine(item.Key);
        }
        string habitName = Console.ReadLine().Trim();
        if (string.IsNullOrWhiteSpace(habitName)) {
            Console.WriteLine("Habit name cannot be empty.");
            return;
        }

        if (habits.ContainsKey(habitName))
        {
            System.Console.WriteLine("Do you want to mark as done for today or a specific date? (t/s)");
            string input = Console.ReadLine();
            if(input.ToLower() == "t")
            {
                if (!DoneHabits.ContainsKey(habitName))
                    DoneHabits[habitName] = new List<DateTime>();
                DoneHabits[habitName].Add(DateTime.Now);

                System.Console.WriteLine("Do you want to keep your habit? (y/n)");
                string keepHabit = Console.ReadLine();
                if (keepHabit.ToLower() == "y")
                {
                    System.Console.WriteLine($"Habit {habitName} marked as done for today.");
                }
                else if (keepHabit == "n")
                {
                    DoneHabits.Remove(habitName);
                    System.Console.WriteLine($"Habit '{habitName}' removed.");
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter: y or n");
                }
            }
            else if (input.ToLower() == "s")
            {
                System.Console.WriteLine($"Available dates for '{habitName}':");
                foreach (var item in habits[habitName])
                {
                    Console.WriteLine(item.ToString("yyyy-MM-dd"));
                }
                System.Console.WriteLine("Enter the date (yyyy-mm-dd):");
                string dateInput = Console.ReadLine();
                if(DateTime.TryParse(dateInput, out DateTime date))
                {
                    if (habits[habitName].Contains(date))
                    {
                        if (!DoneHabits.ContainsKey(habitName))
                            DoneHabits[habitName] = new List<DateTime>();
                        DoneHabits[habitName].Add(date);

                        System.Console.WriteLine("Do you want to keep your habit? (y/n)");
                        string keepHabit = Console.ReadLine();
                        if(keepHabit.ToLower() == "y")
                        {
                            System.Console.WriteLine($"Habit '{habitName}' marked as done for {date.ToString("yyyy-MM-dd")}");
                        }
                        else if (keepHabit == "n")
                        {
                            DoneHabits.Remove(habitName);
                            System.Console.WriteLine($"Habit '{habitName}' marked as done for {date.ToString("yyyy-MM-dd")} And it is removed.");
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid input. Please enter: y or n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine($"Date '{date.ToString("yyyy-MM-dd")}' not found for habit '{habitName}'.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid date format. Please try again.");
                }
            }
            else
            {
                System.Console.WriteLine("Invalid input. Please enter: t or s");
                return;
            }
        }
        else
        {
            System.Console.WriteLine("Habit not found.");
            return;
        }
    }
    static void ViewProgressForHabit()
    {

    }
    static void ViewAllHabitsWithStreak()
    {

    }
    static void DeleteHabit()
    {

    }
}
