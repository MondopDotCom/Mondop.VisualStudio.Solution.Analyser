using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution
{
    public interface IProjectNodeSelector
    {
        VisualStudioProjectNode First(VisualStudioProject project, string name);
        VisualStudioProjectNode First(VisualStudioProjectNode node, string name);
        List<VisualStudioProjectNode> GetPlatformConfigurations(VisualStudioProject project);
    }
}
