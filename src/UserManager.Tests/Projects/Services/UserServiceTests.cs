using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using EscNet.Cryptography.Interfaces;
using FluentAssertions;
using Moq;
using UserManager.Domain.Entities;
using UserManager.Infra.Interfaces;
using UserManager.Services.DTO;
using UserManager.Services.Interfaces;
using UserManager.Services.Services;
using UserManager.Tests.Projects.Configuration;
using UserManager.Tests.Projects.Fixtures;

namespace UserManager.Tests.Projects.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IRijndaelCryptography> _rijndaelCryptography;


        public UserServiceTests()
        {
            _mapper = AutoMapperConfig.GetConfigurations();
            _userRepository = new Mock<IUserRepository>();
            _rijndaelCryptography = new Mock<IRijndaelCryptography>();
            _userService = new UserService(_mapper, _userRepository.Object, _rijndaelCryptography.Object);
        }

        [Fact(DisplayName = "Create Valid User")]
        [Trait("Category", "User Service Tests")]
        // NOMEMETODO_RESULTADOESPERADO
        public async Task Create_WhenUserIsValid_ReturnUserDTO()
        {
            // Arrange
            var userToCreate = UserFixture.CreateValidUserDTO();
            var encryptedPassword = new Faker().Internet.Password();
            var userCreated = _mapper.Map<User>(userToCreate);
            userCreated.ChangePassword(encryptedPassword);
            _userRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            _rijndaelCryptography.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encryptedPassword);
            _userRepository.Setup(x => x.Create(It.IsAny<User>())).ReturnsAsync(userCreated);

            // Act
            var result = await _userService.Create(userToCreate);

            // Assert
            result.Should().BeEquivalentTo(_mapper.Map<UserDTO>(userCreated));

        }
    }
}