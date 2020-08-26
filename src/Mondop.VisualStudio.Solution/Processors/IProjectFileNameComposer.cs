using System;

namespace Mondop.VisualStudio.Solution
{
    public interface IProjectFileNameComposer
    {
        string Compose(string solutionFolder, string projectFolder, string name, Guid typeId);
    }
}
