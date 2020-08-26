using System;

namespace Mondop.VisualStudio.Solution
{
    public interface IExtensionHelper
    {
        string GetDefaultProjectFileExtension(Guid projectTypeId);
        string GetDefaultCompileItemExtension(Guid projectTypeId);
    }
}
