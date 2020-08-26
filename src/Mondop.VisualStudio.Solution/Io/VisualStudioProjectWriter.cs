using Mondop.Core;

namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioProjectWriter: VisualStudioProjectWriterIoc
    {
        public VisualStudioProjectWriter(): base(new FileReaderWriter())
        {

        }
    }
}
