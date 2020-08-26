using Mondop.Guard;
using System;
using System.IO;

namespace Mondop.VisualStudio.Solution
{
    public class ProjectFileNameComposer : IProjectFileNameComposer
    {
        private readonly IExtensionHelper _extensionHelper;

        public ProjectFileNameComposer(IExtensionHelper extensionHelper)
        {
            _extensionHelper = Ensure.IsNotNull(extensionHelper, nameof(extensionHelper));
        }

        public string Compose(string solutionFolder, string projectFolder, string name, Guid typeId)
        {
            var folder = Path.GetFullPath(Path.Combine(solutionFolder, projectFolder));
            var fileName = Path.Combine(folder,  name + "." + _extensionHelper.GetDefaultProjectFileExtension(typeId));

            return fileName;
        }
    }
}
