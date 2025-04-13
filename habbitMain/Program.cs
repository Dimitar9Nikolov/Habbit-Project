using System.Text.Json;
using System.Text.Json.Serialization;


namespace habbitMain;
class Program
{
    static string filePath = "habits.json";

    static List<Habit> LoadHabits()
    {
        if (!File.Exists(filePath)) return new List<Habit>();
    
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Habit>>(json) ?? new List<Habit>();
    }

    static void SaveHabits(List<Habit> habits)
    {
        string json = JsonSerializer.Serialize(habits, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public static List<Habit> habits = LoadHabits();

    public static Dictionary<string, List<DateTime>> DoneHabits = new Dictionary<string, List<DateTime>>();
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- Habit Tracker Menu ---");
            Console.WriteLine("1. Add a habit");
            Console.WriteLine("2. Mark habit as done");
            Console.WriteLine("3. View all habits");
            Console.WriteLine("4. View progress");
            Console.WriteLine("5. Delete a habit");
            Console.WriteLine("6. Exit");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddHabit();
                    break;
                case "2":
                    MarkHabitAsDone();
                    break;
                case "3":
                    ViewAllHabits();
                    break;
                case "4":
                    ViewProgress();
                    break;
                case "5":
                    DeleteHabit();
                    break;
                case "6":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void AddHabit()
    {
        Console.WriteLine("Enter the name of the habit:");
        string habitName = Console.ReadLine().Trim();

        if (string.IsNullOrWhiteSpace(habitName))
        {
        Console.WriteLine("Habit name cannot be empty.");
        return;
    }

    var existingHabit = habits.FirstOrDefault(h => h.Name == habitName);
    if (existingHabit != null)
    {
        Console.WriteLine("Habit already exists. Do you want to add a date to it? (y/n)");
        string choice = Console.ReadLine();

        if (choice.ToLower() == "y")
        {
            Console.WriteLine("Enter date (yyyy-mm-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                existingHabit.Dates.Add(date);
                SaveHabits(habits);
                Console.WriteLine("Date added!");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
        }

        return;
    }

    var newHabit = new Habit { Name = habitName };
    habits.Add(newHabit);
    SaveHabits(habits);
    Console.WriteLine($"Habit '{habitName}' added.");
    
}
    static void MarkHabitAsDone()
{
    if (habits.Count == 0)
    {
        Console.WriteLine("No habits available.");
        return;
    }

    Console.WriteLine("Select a habit to mark as done today:");
    for (int i = 0; i < habits.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {habits[i].Name}");
    }

    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= habits.Count)
    {
        habits[choice - 1].Dates.Add(DateTime.Today);
        SaveHabits(habits);
        Console.WriteLine("Habit marked as done for today!");
    }
    else
    {
        Console.WriteLine("Invalid choice.");
    }
}


    static void ViewProgress()
    {
        if (habits.Count == 0)
        {
            Console.WriteLine("No habits found.");
            return;
        }

        Console.WriteLine("Select a habit to view its progress:");
        for (int i = 0; i < habits.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {habits[i].Name}");
        }

        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= habits.Count)
        {
            var habit = habits[choice - 1];
            Console.WriteLine($"Progress for '{habit.Name}':");
            if (habit.Dates.Count == 0)
            {
                Console.WriteLine("No progress recorded yet.");
            }
            else
            {
                foreach (var date in habit.Dates)
                {
                    Console.WriteLine(date.ToString("yyyy-MM-dd"));
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    static void DeleteHabit()
    {
        if (habits.Count == 0)
        {
            Console.WriteLine("No habits to delete.");
            return;
        }

        Console.WriteLine("Select a habit to delete:");
        for (int i = 0; i < habits.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {habits[i].Name}");
        }

        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= habits.Count)
        {
            string removedName = habits[choice - 1].Name;
            habits.RemoveAt(choice - 1);
            SaveHabits(habits);
            Console.WriteLine($"Habit '{removedName}' deleted.");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
}

   static void ViewAllHabits()
    {
        if (habits.Count == 0)
        {
            Console.WriteLine("No habits to display.");
            return;
        }

        Console.WriteLine("Your Habits:");
        foreach (var habit in habits)
        {
            Console.WriteLine($"- {habit.Name} | Days Done: {habit.Dates.Count}");
        }
    }
}