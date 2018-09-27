using System;
using System.IO;

namespace NeoSharp.Core.Wallet.Wrappers
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException(nameof(fileName));
            }

            var file = new FileInfo(fileName);

            return file.Exists;
        }

        public string Load(string fileName)
        {
            if (!Exists(fileName))
            {
                throw new ArgumentException("File not found");
            }
            
            var textFromFile = File.ReadAllText(fileName);
            return textFromFile;
        }

        public byte[] LoadBytes(string fileName)
        {
            if (!Exists(fileName))
            {
                throw new ArgumentException("File not found");
            }

            var bytesFromFile = File.ReadAllBytes(fileName);
            return bytesFromFile;
        }

        public void WriteToFile(string content, string fileName)
        {
            var file = new FileInfo(fileName);
            File.WriteAllText(file.FullName, content);
        }
    }
}
