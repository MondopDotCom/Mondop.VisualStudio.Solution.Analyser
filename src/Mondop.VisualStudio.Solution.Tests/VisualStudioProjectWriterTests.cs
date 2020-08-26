using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondop.VisualStudio.Solution.Io;
using NSubstitute;
using System;
using System.IO;

namespace Mondop.VisualStudio.Solution.Tests
{
    [TestClass]
    public class VisualStudioProjectWriterTests
    {
        private VisualStudioProjectWriterIoc visualStudioProjectWriter;
        private VisualStudioProjectGeneratorIoc visualStudioProjectGenerator;

        private readonly IFileReaderWriter mockFileReaderWriter = Substitute.For<IFileReaderWriter>();

        [TestInitialize]
        public void TestInitialize()
        {
            visualStudioProjectWriter = new VisualStudioProjectWriterIoc(mockFileReaderWriter);
            visualStudioProjectGenerator = new VisualStudioProjectGeneratorIoc();
        }

        [TestMethod]
        public void CallConstructor_WithInvalidArguments_Expect_ArgumentNullException()
        {
            Action action = () => new VisualStudioProjectWriterIoc(null);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void Call_WithNormalProject()
        {
            var solution = new VisualStudioSolution();
            
            var solutionProject = new VisualStudioSolutionProject();
            solutionProject.Solution = solution;
            var project = visualStudioProjectGenerator.CreateNewProject(solutionProject, "", "", "");

            visualStudioProjectWriter.WriteProject("file", solutionProject);

            mockFileReaderWriter.Received(1).WriteFile(Arg.Any<string>(), Arg.Any<MemoryStream>());
        }

        [TestMethod]
        public void Call_With_SolutionFolder()
        {
            var solution = new VisualStudioSolution();

            var solutionProject = new VisualStudioSolutionProject { TypeGuid = new Guid(VisualStudioSolutionProjectTypeIds.SolutionFolderGuid) };
            solutionProject.Solution = solution;
            visualStudioProjectWriter.WriteProject("file", solutionProject);

            mockFileReaderWriter.Received(0).WriteFile(Arg.Any<string>(), Arg.Any<MemoryStream>());
        }

    }
}
