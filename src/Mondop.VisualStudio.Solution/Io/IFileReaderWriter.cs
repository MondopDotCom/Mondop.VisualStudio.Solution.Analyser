using System.IO;
using System.Text;

namespace Mondop.VisualStudio.Solution.Io
{
    public interface IFileReaderWriter
    {
        bool Exists(string fileName);
        string[] ReadAllLines(string fileName);
        void WriteFile(string fileName, string data,Encoding encoding);
        void WriteFile(string fileName, MemoryStream ms);
    }
}
