namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioSolutionReader: VisualStudioSolutionReaderIoc
    {
        public VisualStudioSolutionReader(): base(new FileReaderWriter())
        {

        }
    }
}
