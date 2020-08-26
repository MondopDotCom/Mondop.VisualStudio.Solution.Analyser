namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioSolutionWriter: VisualStudioSolutionWriterIoc
    {
        public VisualStudioSolutionWriter(): base(new FileReaderWriter())
        {

        }
    }
}
