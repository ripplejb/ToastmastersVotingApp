using System.Linq;
using ElectionServiceUnitTests.Arrange;
using Voting.TemplateLoaders.JsonTemplateLoader;
using Xunit;

namespace ElectionServiceUnitTests
{
    public class ElectionJsonTemplateLoaderUnitTest
    {
        [Fact]
        public void GetElectionUnitTest()
        {
            // Arrange
            var jsonTemplateLoader = new ElectionJsonTemplateLoader();
            
            // Act
            var election = jsonTemplateLoader.Load(new TemplateCreateElectionSetup().TEMPLATE);

            // Assert
            Assert.NotNull(election);
            Assert.True((from ballot in election.Ballots select ballot).Count() == 3);
        }
    }
}