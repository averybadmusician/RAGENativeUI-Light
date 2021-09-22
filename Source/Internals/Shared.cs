namespace RAGENativeUI.Internals
{
    using Rage;
    using System.IO.MemoryMappedFiles;

    internal static unsafe class Shared
    {
        private const string MappedFileName = "rnui_shared_state{74904B8B-6080-42D9-B17F-506FEF596943}";

        private static readonly StaticFinalizer Finalizer = new StaticFinalizer(Shutdown);
        private static MemoryMappedFile mappedFile;
        private static MemoryMappedViewAccessor mappedFileAccessor;
        private static SharedData* data;

        public static long* MemoryAddresses => data->MemoryAddresses;
        public static int* MemoryInts => data->MemoryInts;

        static Shared()
        {
            Game.LogTrivialDebug($"[RAGENativeUI::Shared] Init from '{System.AppDomain.CurrentDomain.FriendlyName}'");
            Game.LogTrivialDebug($"[RAGENativeUI::Shared] > sizeof(SharedData) = {sizeof(SharedData)}");

            mappedFile = MemoryMappedFile.CreateOrOpen(MappedFileName, sizeof(SharedData));
            mappedFileAccessor = mappedFile.CreateViewAccessor(0, sizeof(SharedData));

            byte* ptr = null;
            mappedFileAccessor.SafeMemoryMappedViewHandle.AcquirePointer(ref ptr);
            data = (SharedData*)ptr;
        }

        private static void Shutdown()
        {
            Game.LogTrivialDebug($"[RAGENativeUI::Shared] Shutdown from '{System.AppDomain.CurrentDomain.FriendlyName}'");

            // dispose mapped file
            if (mappedFileAccessor != null)
            {
                data = null;
                mappedFileAccessor.SafeMemoryMappedViewHandle.ReleasePointer();
                mappedFileAccessor.Dispose();
                mappedFileAccessor = null;
            }

            if (mappedFile != null)
            {
                mappedFile.Dispose();
                mappedFile = null;
            }
        }

        private struct SharedData
        {
            public fixed long MemoryAddresses[Memory.MaxMemoryAddresses];
            public fixed int MemoryInts[Memory.MaxInts];
        }
    }
}
