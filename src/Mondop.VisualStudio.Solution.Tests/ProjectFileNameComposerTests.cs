using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Mondop.VisualStudio.Solution.Tests
{
    [TestClass]
    public class ProjectFileNameComposerTests
    {
        private ProjectFileNameComposer _composer;

        [TestInitialize]
        public void TestInitialize()
        {
            _composer = new ProjectFileNameComposer(new ExtensionHelper());
        }

        [TestMethod]
        public void CallConstructor_WithInvalidArguments_Expect_ArgumentNullException()
        {
            Action action = () => new ProjectFileNameComposer(null);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void CallCompose_EmptyNamespace_Expect_CorrectFileName()
        {
            var solutionFolder = "c:\\solution";
            var projectFolder = "project";

            var fileName = _composer.Compose(solutionFolder,projectFolder, "Test", new Guid(VisualStudioSolutionProjectTypeIds.CsProjectGuid));

            fileName.Should().Be("c:\\solution\\project\\Test.csproj");
        }

        [TestMethod]
        public void CallCompose_Relatieve_Expect_CorrectFileName()
        {
            var solutionFolder = "c:\\solution";
            var projectFolder = ".\\project";
            
            var fileName = _composer.Compose(solutionFolder,projectFolder, "Test", new Guid(VisualStudioSolutionProjectTypeIds.CsProjectGuid));

            fileName.Should().Be("c:\\solution\\project\\Test.csproj");
        }

        [TestMethod]
        public void CallCompose_ProjectFullPath_Expect_CorrectFileName()
        {
            var solutionFolder = "c:\\solution";
            var projectFolder = "d:\\project";

            var fileName = _composer.Compose(solutionFolder, projectFolder, "Test", new Guid(VisualStudioSolutionProjectTypeIds.CsProjectGuid));

            fileName.Should().Be("d:\\project\\Test.csproj");
        }
    }
}
