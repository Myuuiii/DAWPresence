using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter search string: ");
        string search = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(search))
        {
            Console.WriteLine("No search string entered.");
            return;
        }

        var processes = Process.GetProcesses();
        var matches = processes
            .Where(p => p.ProcessName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        if (matches.Count == 0)
        {
            Console.WriteLine("No matching processes found.");
        }
        else
        {
            Console.WriteLine("Matching processes:");
            foreach (var proc in matches)
            {
                Console.WriteLine($"{proc.ProcessName} (ID: {proc.Id})");
            }
        }
    }
}