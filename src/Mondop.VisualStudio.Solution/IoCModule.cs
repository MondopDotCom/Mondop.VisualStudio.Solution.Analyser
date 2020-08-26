using Mondop.Abstractions.IoC;
using Mondop.VisualStudio.Solution.Io;
using System;
using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution
{
    public class IoCModule : IIoCModule
    {
        public List<Type> DependsOn => new List<Type>{};

        public void Register(IIoCContainer container)
        {
            container.Register<IFileReaderWriter, FileReaderWriterIoc>();
            container.Register<IProjectNodeSelector, ProjectNodeSelector>();
            container.Register<IVisualStudioProjectGenerator, VisualStudioProjectGeneratorIoc>();
            container.Register<IVisualStudioSolutionReader, VisualStudioSolutionReaderIoc>();
            container.Register<IVisualStudioSolutionWriter, VisualStudioSolutionWriterIoc>();
            container.Register<IPackageConfigReader, PackageConfigReader>();
            container.Register<IVisualStudioProjectReader, VisualStudioProjectReaderIoc>();
            container.Register<IVisualStudioSolutionManager, VisualStudioSolutionManagerIoc>();
            container.Register<IVisualStudioProjectWriter, VisualStudioProjectWriterIoc>();
            container.Register<IExtensionHelper, ExtensionHelper>();
            container.Register<IProjectFileNameComposer, ProjectFileNameComposer>();
            container.Register<ICompileItemFileNameComposer, CompileItemFileNameComposer>();
        }
    }
}
