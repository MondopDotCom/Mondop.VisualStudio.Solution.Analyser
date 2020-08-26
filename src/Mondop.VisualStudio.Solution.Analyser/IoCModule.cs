using Mondop.Abstractions.IoC;
using Mondop.VisualStudio.Solution.Analyser.Analysers;
using System;
using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution.Analyser
{
    public class IoCModule : IIoCModule
    {
        public List<Type> DependsOn => new List<Type> { typeof(Solution.IoCModule) };

        public void Register(IIoCContainer container)
        {
            container.RegisterCollection<IAnalyser>(new Type[] {
                typeof(FrameworkAnalyser),
                typeof(CodeAnalysisAnalyser),
                typeof(WarningLevelAnalyser),
                typeof(LanguageSpecificationAnalyser)
            });
            container.Register<IStringToFrameworkConverter, StringToFrameworkConverter>();
            container.Register<IVisualStudioProjectAnalyser, VisualStudioProjectAnalyser>();
            container.Register<IConfigurationPlatformParser, ConfigurationPlatformParser>();
        }
    }
}
