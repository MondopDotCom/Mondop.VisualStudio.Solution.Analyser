using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondop.VisualStudio.Solution.Io;
using NSubstitute;

namespace Mondop.VisualStudio.Solution.Tests
{
    [TestClass]
    public class VisualStudioProjectReaderTests
    {
        private VisualStudioProjectReaderIoc visualStudioProjectReader;
        private IPackageConfigReader mockPackageConfigReader = Substitute.For<IPackageConfigReader>();

        [TestInitialize]
        public void TestInitialize()
        {
            visualStudioProjectReader = new VisualStudioProjectReaderIoc(mockPackageConfigReader);
        }
    }
}
