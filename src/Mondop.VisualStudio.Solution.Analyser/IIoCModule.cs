using Mondop.Abstractions.IoC;
using System;
using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution.Analyser
{
    public class IoCModule : IIoCModule
    {
        public List<Type> DependsOn => new List<Type> { typeof(Mondop.VisualStudio.Solution.IoCModule) };

        public void Register(IIoCContainer container)
        {
            container.Register<IAnalyser>(new Type[] { typeof(FrameworkAnalyser)});
            container.Register<IStringToFrameworkConverter, StringToFrameworkConverter>();
            container.Register<IVisualStudioProjectAnalyser, VisualStudioProjectAnalyser>();
        }
    }
}
