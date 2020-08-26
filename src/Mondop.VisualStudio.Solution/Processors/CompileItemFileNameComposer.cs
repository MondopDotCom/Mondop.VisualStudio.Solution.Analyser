using Mondop.Guard;
using System;

namespace Mondop.VisualStudio.Solution
{
    public class CompileItemFileNameComposer : ICompileItemFileNameComposer
    {
        private readonly IExtensionHelper _extensionHelper;

        public CompileItemFileNameComposer(IExtensionHelper extensionHelper)
        {
            _extensionHelper = Ensure.IsNotNull( extensionHelper, nameof(extensionHelper));
        }

        public string ComposeFileName(string name,Guid projectTypeId)
        {
            return name + "." + "Mondop." + _extensionHelper.GetDefaultCompileItemExtension(projectTypeId);
        }
    }
}
