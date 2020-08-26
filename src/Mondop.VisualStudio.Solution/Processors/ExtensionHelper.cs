using System;
using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution
{
    public class ExtensionHelper : IExtensionHelper
    {
        private readonly Dictionary<Guid, string> _projectFileExtensions = new Dictionary<Guid, string>();
        private readonly Dictionary<Guid, string> _compileItemExtensions = new Dictionary<Guid, string>();
        
        public ExtensionHelper()
        {
            _projectFileExtensions.Add(new Guid(VisualStudioSolutionProjectTypeIds.CsProjectGuid),"csproj");
            _compileItemExtensions.Add(new Guid(VisualStudioSolutionProjectTypeIds.CsProjectGuid), "cs");
        }

        public string GetDefaultCompileItemExtension(Guid projectTypeId)
        {
            return _compileItemExtensions[projectTypeId];
        }

        public string GetDefaultProjectFileExtension(Guid projectTypeId)
        {
            return _projectFileExtensions[projectTypeId];
        }
    }
}
