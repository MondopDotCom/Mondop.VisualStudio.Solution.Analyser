using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Mondop.VisualStudio.Solution.Tests
{
    [TestClass]
    public class CompileItemFileNameComposerTests
    {
        private CompileItemFileNameComposer _compileItemFileNameComposer;

        [TestInitialize]
        public void TestInitialize()
        {
            _compileItemFileNameComposer = new CompileItemFileNameComposer(new ExtensionHelper());
        }

        [TestMethod]
        public void CallConstructor_WithInvalidArguments_Expect_ArgumentNullException()
        {
            Action action = () => new CompileItemFileNameComposer(null);

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void CallCompose_WithValidArguments_Expect_CorrectFileName()
        {
            var fileName = _compileItemFileNameComposer.ComposeFileName("Test", new Guid(VisualStudioSolutionProjectTypeIds.CsProjectGuid));

            fileName.Should().Be("Test.Mondop.cs");
        }
    }
}
