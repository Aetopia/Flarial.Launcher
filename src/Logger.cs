using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Flarial.Launcher;

sealed class Logger
{
    static readonly object _ = new();

    static readonly SemaphoreSlim Semaphore = new(1, 1);

    static Logger Object;

    readonly StreamWriter Writer = new("Launcher.log") { AutoFlush = true };

    internal static Logger Current { get { lock (_) return Object ??= new(); } }

    static void Write(string value)
    {
        Semaphore.Wait();
        try { Current.Writer.WriteLine($"{value}"); }
        finally { Semaphore.Release(); }
    }

    static async Task WriteAsync(string value)
    {
        await Semaphore.WaitAsync();
        try { Current.Writer.WriteLine($"{value}"); }
        finally { Semaphore.Release(); }
    }

    internal static void Information(string value) => Write($"[Information][{DateTime.UtcNow}] {value}");

    internal static void Warning(string value) => Write($"[Warning][{DateTime.UtcNow}] {value}");

    internal static void Error(string value) => Write($"[Error][{DateTime.UtcNow}] {value}");


    internal static async Task InformationAsync(string value) => await WriteAsync($"[Information][{DateTime.UtcNow}] {value}");

    internal static async Task WarningAsync(string value) => await WriteAsync($"[Warning][{DateTime.UtcNow}] {value}");

    internal static async Task ErrorAsync(string value) => await WriteAsync($"[Error][{DateTime.UtcNow}] {value}");
}