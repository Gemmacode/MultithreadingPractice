using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private static Queue<TaskItem> taskQueue = new Queue<TaskItem>();

    public IActionResult Index()
    {
        return View(taskQueue);
    }

    public async Task<IActionResult> StartTask1()
    {
        var task = new TaskItem("Task 1", 5);
        taskQueue.Enqueue(task);
        await RunTaskAsync(task);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> StartTask2()
    {
        var task = new TaskItem("Task 2", 3);
        taskQueue.Enqueue(task);
        await RunTaskAsync(task);
        return RedirectToAction("Index");
    }

    private async Task RunTaskAsync(TaskItem task)
    {
        task.Status = TaskStatus.Running;
        await Task.Run(() => SimulateTask(task));
        task.Status = TaskStatus.Completed;
    }

    private void SimulateTask(TaskItem task)
    {
        for (int i = 1; i <= task.Iterations; i++)
        {
            Console.WriteLine($"{task.Name}: Iteration {i}");
            System.Threading.Thread.Sleep(1000); // Simulate time-consuming work
        }
    }
}

public class TaskItem
{
    public string Name { get; set; }
    public int Iterations { get; set; }
    public TaskStatus Status { get; set; }
    public int Progress { get; set; } // Add this line

    public TaskItem(string name, int iterations)
    {
        Name = name;
        Iterations = iterations;
        Status = TaskStatus.NotStarted;
        Progress = 0; // Add this line to initialize progress
    }
}

public enum TaskStatus
{
    NotStarted,
    Running,
    Completed
}
