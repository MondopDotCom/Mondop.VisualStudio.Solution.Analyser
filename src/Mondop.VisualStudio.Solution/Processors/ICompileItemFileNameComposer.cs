using System;

namespace Mondop.VisualStudio.Solution
{
    public interface ICompileItemFileNameComposer
    {
        string ComposeFileName(string name,Guid projectTypeId);
    }
}
