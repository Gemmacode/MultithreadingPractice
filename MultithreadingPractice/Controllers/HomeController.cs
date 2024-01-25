using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> StartTask1()
    {
        ViewBag.Task1Status = "Running";
        ViewBag.Task1Progress = 0;

        // Show progress bar
        ViewBag.ShowTask1ProgressBar = true;

        await Task.Run(() => SimulateTask("Task 1", 5, percentage => ViewBag.Task1Progress = percentage));

        // Hide progress bar after completion
        ViewBag.ShowTask1ProgressBar = false;
        ViewBag.Task1Status = "Completed";

        return View("Index");
    }

    public async Task<IActionResult> StartTask2()
    {
        ViewBag.Task2Status = "Running";
        ViewBag.Task2Progress = 0;

        // Show progress bar
        ViewBag.ShowTask2ProgressBar = true;

        await Task.Run(() => SimulateTask("Task 2", 3, percentage => ViewBag.Task2Progress = percentage));

        // Hide progress bar after completion
        ViewBag.ShowTask2ProgressBar = false;
        ViewBag.Task2Status = "Completed";

        return View("Index");
    }

    private void SimulateTask(string taskName, int iterations, Action<int> updateProgress)
    {
        for (int i = 1; i <= iterations; i++)
        {
            Console.WriteLine($"{taskName}: Iteration {i}");
            System.Threading.Thread.Sleep(1000); // Simulate time-consuming work

            // Update progress
            int percentage = (i * 100) / iterations;
            updateProgress(percentage);
        }
    }
}
