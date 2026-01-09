using System.Text.Json;
using System.IO;

namespace TaskTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 0) return;

            List<Task> tasks = new List<Task>();

            string taskFilePath = Path.Combine(AppContext.BaseDirectory, "tasks.json");

            if (File.Exists(taskFilePath))
            {
                string existingJson = File.ReadAllText(taskFilePath);
                tasks = JsonSerializer.Deserialize<List<Task>>(existingJson);
            }

            string action = args[0];
            if (action == "add")
            {
                string task = args[1];
                int id = (tasks.Count + 1);

                tasks.Add(new Task(id, task, "to-do"));

                Console.WriteLine($"Task added successfully (ID: {id})");
            }
            else if (action == "delete")
            {
                int id = int.Parse(args[1]);
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Id == id)
                    {
                        if (i < tasks.Count)
                        {
                            for (int j = i; j < tasks.Count - 1; j++)
                            {
                                tasks[j] = tasks[j + 1];
                                tasks[j].Id = j + 1;
                            }

                        }

                        tasks.RemoveAt(tasks.Count - 1);

                        Console.WriteLine($"Task with ID {id} deleted successfully.");

                        break;
                    }
                }
            }
            else if (action == "update")
            {
                int id = int.Parse(args[1]);
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Id == id)
                    {
                        string newTask = args[2];
                        tasks[i].Description = newTask;
                        Console.WriteLine($"Task with ID {id} updated successfully.");
                        break;
                    }
                }
            }
            else if (action == "mark-in-progress")
            {
                int id = int.Parse(args[1]);
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Id == id)
                    {
                        tasks[i].Status = "in-progress";
                        Console.WriteLine($"Task with ID {id} updated successfully.");
                        break;
                    }
                }
            }
            else if (action == "mark-done")
            {
                int id = int.Parse(args[1]);
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Id == id)
                    {
                        tasks[i].Status = "done";
                        Console.WriteLine($"Task with ID {id} updated successfully.");
                        break;
                    }
                }
            }
            else if (action == "list")
            {
                string statusFilter = (args.Length >= 2) ? args[1] : "";

                foreach (Task task in tasks)
                {
                    if (statusFilter != "" && task.Status != statusFilter)
                    {
                        continue;
                    }

                    Console.WriteLine($"{task.Id}: {task.Description.PadRight(50)} {task.Status}");
                }
            }

            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(taskFilePath, json);
        }
    }
}
