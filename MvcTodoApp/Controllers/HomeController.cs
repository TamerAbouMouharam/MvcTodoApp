using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;

namespace MvcTodoApp.Controllers;

public class HomeController : Controller
{
    static List<TaskItem> tasks = new()
    {
        new() { Id = 1, Title = "MVC Training", IsComplete = false },
        new() { Id = 2, Title = "N-Tier Training", IsComplete = false },
        new() { Id = 3, Title = "Git Training", IsComplete = false }
    };
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(tasks);
    }

    [HttpPost]
    public IActionResult AddTask(string title)
    {
        if (!string.IsNullOrEmpty(title))
        {
            TaskItem task = new()
            {
                Id = tasks.Max(t => t.Id) + 1,
                Title = title,
                IsComplete = false
            };

            tasks.Add(task);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult CompleteTask(int id)
    {
        TaskItem? task = tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
        {
            task.IsComplete = true;
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EditTask(int id, string title)
    {
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
