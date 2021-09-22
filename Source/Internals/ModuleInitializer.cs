namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class ModuleInitializerAttribute : Attribute { }
}

namespace RAGENativeUI.Internals
{
    using Rage;
    using System.Runtime.CompilerServices;

    internal static class ModuleInitializer
    {
        [ModuleInitializer]
        internal static void Run()
        {
            Game.LogTrivialDebug("[RAGENativeUI] Initializing...");
#if DEBUG
            var sw = System.Diagnostics.Stopwatch.StartNew();
#endif
#if DEBUG
            sw.Stop();
            Game.LogTrivialDebug($"[RAGENativeUI] >> Took {sw.ElapsedMilliseconds}ms");
#endif

            Game.LogTrivialDebug($"[RAGENativeUI] > {nameof(Memory)}");
#if DEBUG
            sw.Restart();
#endif
            RuntimeHelpers.RunClassConstructor(typeof(Memory).TypeHandle);
#if DEBUG
            sw.Stop();
            Game.LogTrivialDebug($"[RAGENativeUI] >> Took {sw.ElapsedMilliseconds}ms");
#endif

            Game.LogTrivialDebug("[RAGENativeUI] > Applying hooks");
#if DEBUG
            sw.Restart();
#endif
#if DEBUG
            sw.Stop();
            Game.LogTrivialDebug($"[RAGENativeUI] >> Took {sw.ElapsedMilliseconds}ms");
#endif

#if DEBUG
            Game.LogTrivialDebug("[RAGENativeUI] > Registering debug commands");
            Game.AddConsoleCommands(new[] { typeof(DebugCommands) });
#endif
        }
    }
}
