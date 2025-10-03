using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo;

class Program
{
    static string ProcessData(string dataName)
    {
        Console.WriteLine($"[SYNC] Начало обработки '{dataName}'...");
        Thread.Sleep(3000);

        string result = $"Обработка '{dataName}' завершена за 3 секунды";
        Console.WriteLine($"[SYNC] {result}");
        return result;
    }

    static async Task<string> ProcessDataAsync(string dataName)
    {
        Console.WriteLine($"[ASYNC] Начало обратоки '{dataName}'...");
        await Task.Delay(3000);

        string result = $"Обработка '{dataName}' завершена за 3 секунды";
        Console.WriteLine($"[ASYNC] {result}");
        return result;
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Демонстрация синхронной и асинхронный обработки ===");

        Console.WriteLine("-- 1. Синхронная оюработка --");
        Stopwatch syncWatch = Stopwatch.StartNew();

        ProcessData("FILE1");
        ProcessData("FILE2");
        ProcessData("FILE3");

        syncWatch.Stop();
        Console.WriteLine($"\nОбщее время синхронной обработки: {syncWatch.ElapsedMilliseconds / 1000.0} секунд.");

        Console.WriteLine("Нажмите любую клавишу для запуска асинхронной обработки...");
        Console.ReadKey();
        Console.WriteLine();

        Console.WriteLine("-- 2. Acинхронная оюработка --");
        Stopwatch asyncWatch = Stopwatch.StartNew();

        Task task1 = ProcessDataAsync("FILE1");
        Task task2 = ProcessDataAsync("FILE1");
        Task task3 = ProcessDataAsync("FILE1");

        await Task.WhenAll(task1, task2, task3);

        asyncWatch.Stop();

        Console.WriteLine($"\nОбщее время aсинхронной обработки: {asyncWatch.ElapsedMilliseconds / 1000.0} секунд.");
        Console.WriteLine($"Разница в {syncWatch.ElapsedMilliseconds / (double) asyncWatch.ElapsedMilliseconds} раз");

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}