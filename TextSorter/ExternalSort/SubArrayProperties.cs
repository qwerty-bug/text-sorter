using Common;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;

namespace TextSorter.ExternalSort
{
    public class SubArrayProperties : IDisposable
    {
        private bool _disposedValue;
        private SafeHandle? _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private readonly StreamReader reader;

        public int ReaderId { get; private set; }
        public string? CurrentValue { get; private set; }

        public SubArrayProperties(string fileName, int readerId)
        {
            ReaderId = readerId;

            var fileStream = File.OpenRead(fileName);
            reader = new StreamReader(fileStream, Encoding.UTF8, bufferSize: Common.FileOptions.BufferSize8MB);

            CurrentValue = ReadNextLine();
        }

        public string? ReadNextLine()
        {
            CurrentValue = reader.ReadLine();
            if (CurrentValue == "")
                CurrentValue = reader.ReadLine();

            return CurrentValue;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    reader.Dispose();
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }
    }
}
