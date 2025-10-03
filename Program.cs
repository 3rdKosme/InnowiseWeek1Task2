using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo;

class Program
{
    static string ProcessData(string dataName)
    {
        Thread.Sleep(3000);
        
        return $"Обработка '{dataName}' завершена за 3 секунды";
    }

    static async Task<string> ProcessDataAsync(string dataName)
    {
        await Task.Delay(3000);

        return $"Обработка '{dataName}' завершена за 3 секунды";
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Демонстрация синхронной и асинхронный обработки ===");

        Console.WriteLine("-- 1. Синхронная обработка --");
        Stopwatch syncWatch = Stopwatch.StartNew();

        Console.WriteLine(ProcessData("FILE1"));
        Console.WriteLine(ProcessData("FILE2"));
        Console.WriteLine(ProcessData("FILE3"));

        syncWatch.Stop();
        Console.WriteLine($"\nОбщее время синхронной обработки: {syncWatch.ElapsedMilliseconds / 1000.0} секунд.");

        Console.WriteLine("Нажмите любую клавишу для запуска асинхронной обработки...");
        Console.ReadKey();
        Console.WriteLine();

        Console.WriteLine("-- 2. Acинхронная оюработка --");
        Stopwatch asyncWatch = Stopwatch.StartNew();

        Task<string> task1 = ProcessDataAsync("FILE1");
        Task<string> task2 = ProcessDataAsync("FILE2");
        Task<string> task3 = ProcessDataAsync("FILE3");

        await Task.WhenAll(task1, task2, task3);
        Console.WriteLine(task1.Result + "\n" + task2.Result + "\n" + task3.Result);

        asyncWatch.Stop();

        Console.WriteLine($"\nОбщее время aсинхронной обработки: {asyncWatch.ElapsedMilliseconds / 1000.0} секунд.");
        Console.WriteLine($"Разница в {syncWatch.ElapsedMilliseconds / (double) asyncWatch.ElapsedMilliseconds} раз");

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}