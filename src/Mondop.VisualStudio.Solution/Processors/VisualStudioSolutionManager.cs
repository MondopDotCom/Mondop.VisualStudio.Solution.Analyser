using Mondop.VisualStudio.Solution.Io;

namespace Mondop.VisualStudio.Solution.Processors
{
    public class VisualStudioSolutionManager: VisualStudioSolutionManagerIoc
    {
        public VisualStudioSolutionManager(): base(
            new VisualStudioSolutionWriter(),
            new VisualStudioSolutionReader(),
            new VisualStudioProjectReader(),
            new VisualStudioProjectWriter())
        {

        }
    }
}
