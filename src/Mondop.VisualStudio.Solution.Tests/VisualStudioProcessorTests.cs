using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondop.VisualStudio.Solution.Io;
using NSubstitute;
using System.Linq;

namespace Mondop.VisualStudio.Solution.Tests
{
    [TestClass]
    public class VisualStudioProcessorTests
    {
        private IVisualStudioSolutionReader _visualStudioReader;
        private readonly IFileReaderWriter _mockFileReader = Substitute.For<IFileReaderWriter>();

        private string[] GetFileSolution()
        {
            return new[]{
                "",
                "Microsoft Visual Studio Solution File, Format Version 12.00",
                "# Visual Studio 15",
                "VisualStudioVersion = 15.0.27130.2010",
                "MinimumVisualStudioVersion = 10.0.40219.1",
                "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"mondop.codegeneration.visualstudio\", \"mondop.codegeneration.visualstudio\\mondop.codegeneration.visualstudio.csproj\", \"{52E8A9C9-F5C6-4429-A11A-94059DACC6E3}\"",
                "EndProject",
                "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"mondop.codegeneration.visualstudio.tests\", \"mondop.codegeneration.visualstudio.tests\\mondop.codegeneration.visualstudio.tests.csproj\", \"{45709FEA-D568-41C5-83DD-DAE807C8C460}\"",
                "EndProject",
                "Global",
                "   GlobalSection(SolutionConfigurationPlatforms) = preSolution",
                "        Debug|Any CPU = Debug|Any CPU",
                "        Release|Any CPU = Release|Any CPU",
                "   EndGlobalSection",
                "   GlobalSection(ProjectConfigurationPlatforms) = postSolution",
                "        {52E8A9C9-F5C6-4429-A11A-94059DACC6E3}.Debug|Any CPU.ActiveCfg = Debug|Any CPU",
                "        {52E8A9C9-F5C6-4429-A11A-94059DACC6E3}.Debug|Any CPU.Build.0 = Debug|Any CPU",
                "        {52E8A9C9-F5C6-4429-A11A-94059DACC6E3}.Release|Any CPU.ActiveCfg = Release|Any CPU",
                "        {52E8A9C9-F5C6-4429-A11A-94059DACC6E3}.Release|Any CPU.Build.0 = Release|Any CPU",
                "        {45709FEA-D568-41C5-83DD-DAE807C8C460}.Debug|Any CPU.ActiveCfg = Debug|Any CPU",
                "        {45709FEA-D568-41C5-83DD-DAE807C8C460}.Debug|Any CPU.Build.0 = Debug|Any CPU",
                "        {45709FEA-D568-41C5-83DD-DAE807C8C460}.Release|Any CPU.ActiveCfg = Release|Any CPU",
                "        {45709FEA-D568-41C5-83DD-DAE807C8C460}.Release|Any CPU.Build.0 = Release|Any CPU",
                "    EndGlobalSection",
                "    GlobalSection(SolutionProperties) = preSolution",
                "        HideSolutionNode = FALSE",
                "    EndGlobalSection",
                "    GlobalSection(ExtensibilityGlobals) = postSolution",
                "        SolutionGuid = {D2B24701-CF05-4B28-82E8-5F9A5233FB0E}",
                "    EndGlobalSection",
                "EndGlobal"
            };
        }

        [TestInitialize]
        public void SetUp()
        {
            _mockFileReader.Exists(Arg.Any<string>()).Returns(true);
            _mockFileReader.ReadAllLines(Arg.Any<string>()).Returns(GetFileSolution());

            _visualStudioReader = new VisualStudioSolutionReaderIoc(_mockFileReader);
        }

        [TestMethod]
        public void CallWithValidArguments_Expect_CorrectSolution()
        {
            var solution = _visualStudioReader.Load("Test.sln");

            solution.FormatVersion.Should().Be("12.00");
            solution.VisualStudio.Should().Be("15");
            solution.VisualStudioVersion.Should().Be("15.0.27130.2010");
            solution.MinimumVisualStudioVersion.Should().Be("10.0.40219.1");
            solution.Projects.Count.Should().Be(2);
            solution.Global.Should().NotBeNull();
            solution.Global.Sections.Single(x => x.Name.Equals("SolutionConfigurationPlatforms")).Should().NotBeNull();
            solution.Global.Sections.Single(x => x.Name.Equals("SolutionConfigurationPlatforms")).Items.Count.Should().Be(2);

            solution.Projects[0].Name.Should().Be("mondop.codegeneration.visualstudio");
        }
    }
}
