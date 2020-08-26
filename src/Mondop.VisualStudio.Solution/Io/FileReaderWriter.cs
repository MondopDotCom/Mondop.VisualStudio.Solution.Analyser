using Mondop.Mockables;

namespace Mondop.VisualStudio.Solution.Io
{
    public class FileReaderWriter: FileReaderWriterIoc
    {
        public FileReaderWriter(): base(new FileImplementation(),new DirectoryImplementation())
        {

        }
    }
}
