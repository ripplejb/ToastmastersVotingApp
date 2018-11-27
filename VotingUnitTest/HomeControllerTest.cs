using Microsoft.AspNetCore.Mvc;
using Voting.Controllers;
using Xunit;

namespace VotingUnitTest
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexTest1()
        {
            // Arrange
            var controller = new HomeController();
            
            // Act
            var result = controller.Index();
            
            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}