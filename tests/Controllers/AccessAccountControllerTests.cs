using System;
using Xunit;
using tyf.data.service;
using tyf.data.service.Controllers;
using tyf.data.service.Repositories;
using Moq;
using tyf.data.service.Requests;
using tyf.data.service.Models; // Assuming this is where your AccessAccountController is located

namespace tyf.data.service.tests
{
    public class AccessAccountControllerTests
    {
        private readonly AccessAccountController _controller;
        private readonly Mock<IUserRepository> _userRepository;

        public AccessAccountControllerTests()
        {
            // Initialize your controller here. This might involve setting up a mock service.
            _userRepository = new Mock<IUserRepository>();// Replace this with your actual implementation of IUserRepository
            _controller = new AccessAccountController(_userRepository.Object);
        }

        [Fact]
        public void Post_ReturnsUserModel()
        {
            // Arrange
            var request = new CreateUserRequest();
            _userRepository.Setup(x=>x.CreateUser(request)).Returns(new UserModel());
            // Act
            var result = _controller.Post(request);
            // Assert
            Assert.IsType<UserModel>(result);
        }

        [Fact]
        public void Post_ThrowsException()
        {
            // Arrange
            var request = new CreateUserRequest();
            _userRepository.Setup(x=>x.CreateUser(request)).Throws(new Exception());
   
            // Act and Assert
            Assert.Throws<Exception>(()=>_controller.Post(request));
        }

        [Fact]
        public void Get_ReturnsUserModel()
        {
            // Arrange
            var request = Guid.NewGuid();
            _userRepository.Setup(x=>x.GetUser(request)).Returns(new UserModel());
            // Act
            var result = _controller.Get(request);
            // Assert
            Assert.IsType<UserModel>(result);
        }
        [Fact]
        public void Get_ThrowsException()
        {
            // Arrange
            var request = Guid.NewGuid();
            _userRepository.Setup(x=>x.GetUser(request)).Throws(new Exception());
             
            // Act and Assert
            Assert.Throws<Exception>(()=>_controller.Get(request));
        }
    }
}
    

