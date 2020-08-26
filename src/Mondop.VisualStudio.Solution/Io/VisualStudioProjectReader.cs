namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioProjectReader: VisualStudioProjectReaderIoc
    {
        public VisualStudioProjectReader(): base(new PackageConfigReader())
        {

        }
    }
}
