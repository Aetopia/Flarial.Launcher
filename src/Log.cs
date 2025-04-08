using System;
using System.Diagnostics;
using System.IO;

sealed class Log
{
    static Log Object;

    internal static Log Current { get { lock (_) return Object ??= new(); } }

    Log()
    {
        Directory.CreateDirectory("Logs");
        Trace.Listeners.Add(new TextWriterTraceListener(@$"Logs\{DateTime.UtcNow:yyyyMMddTHHmmss}.log"));
        Trace.Listeners.Add(new ConsoleTraceListener());
        Trace.AutoFlush = true;
    }

    internal void Write(string value) => Trace.WriteLine($"[{DateTime.UtcNow}] {value}");

    static readonly object _ = new();
}