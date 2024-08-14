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
        private readonly Lazy<StreamReader> reader;

        public int ReaderId { get; private set; }

        private string? _currentValue = null;

        public string? CurrentValue {
            get
            {
                if(_currentValue is not null)
                    return _currentValue;

                ReadNextLine();
                return _currentValue;
            }
        }

        public SubArrayProperties(string fileName, int readerId)
        {
            ReaderId = readerId;
            reader = new Lazy<StreamReader>(new StreamReader(File.OpenRead(fileName), Encoding.UTF8, bufferSize: Options.BufferSize64MB));
        }

        public string? ReadNextLine()
        {
            _currentValue = reader.Value.ReadLine();
            if (_currentValue == "")
                _currentValue = reader.Value.ReadLine();

            return _currentValue;
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
                    reader.Value.Dispose();
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }
    }
}
