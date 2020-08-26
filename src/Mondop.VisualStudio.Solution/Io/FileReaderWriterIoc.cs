using Mondop.Guard;
using Mondop.Mockables;
using System.IO;
using System.Text;

namespace Mondop.VisualStudio.Solution.Io
{
    public class FileReaderWriterIoc : IFileReaderWriter
    {
        private readonly IFile _file;
        private readonly IDirectory _directory;

        public FileReaderWriterIoc(IFile file,IDirectory directory)
        {
            _file = Ensure.IsNotNull(file, nameof(file));
            _directory = Ensure.IsNotNull(directory, nameof(directory));
        }

        public bool Exists(string fileName)
        {
            return _file.Exists(fileName);
        }

        public string[] ReadAllLines(string fileName)
        {
            return _file.ReadAllLines(fileName);
        }

        public void WriteFile(string fileName, string data,Encoding encoding)
        {
            if (!_directory.Exists(Path.GetDirectoryName(fileName)))
                _directory.CreateDirectory(Path.GetDirectoryName(fileName));

            _file.WriteAllText(fileName, data,encoding);
        }

        public void WriteFile(string fileName,MemoryStream ms)
        {
            if (!_directory.Exists(Path.GetDirectoryName(fileName)))
                _directory.CreateDirectory(Path.GetDirectoryName(fileName));

            _file.WriteAllBytes(fileName,ms.ToArray());
        }
    }
}
